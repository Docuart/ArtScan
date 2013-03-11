using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using ArtData.Properties;
using Library.Scan;
using Image = System.Drawing.Image;
using Rectangle = System.Drawing.Rectangle;

namespace ArtData
{
    public partial class KontrolForm : BaseForm
    {
        private const string IndexKlasoru = "D:\\Scanned\\SAMGAZ\\";
        //private const string IndexKlasoru = "I:\\";
        private const string TempKlasoru = "ArtData\\";
        private NameValueCollection _tarananlar;
        private string _ciltId = "";
        private string _cilt = "";
        private string _selectedItem = "";

        public KontrolForm()
        {
            InitializeComponent();
            KeyDown += OnKeyDown;
            splitContainer1.KeyDown += OnKeyDown;
            _tarananTam.KeyDown += OnKeyDown;
            _tarananZoomed.KeyDown += OnKeyDown;
            _dogru.KeyDown += OnKeyDown;
            _yanlis.KeyDown += OnKeyDown;
        }

        private int SelectedIndex()
        {
            for (var i = 0; i < _tarananlar.AllKeys.Length; i++)
            {
                if (_tarananlar.AllKeys[i] == _selectedItem)
                    return i;
            }
            return 0;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {            
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    DogruClick(sender, e);
                    break;
                case Keys.Delete:
                    if (!e.Control)
                        YanlisClick(sender, e);
                    else
                        Hatali();
                    break;                    
                case Keys.Left:
                    Left(e);
                    break;
                case Keys.Right:
                    Right(e);
                    break;                    
            }
        }

        private void Left(KeyEventArgs e)
        {
            if (!e.Control) return;
            var selectedIndex = SelectedIndex();
            if (Math.Sign(selectedIndex) == 1)
                selectedIndex = selectedIndex - 1;
            else
                selectedIndex = 0;

            _selectedItem = _tarananlar.AllKeys[selectedIndex];
            TarananYukle();
        }

        private void Right(KeyEventArgs e)
        {
            if (!e.Control) return;
            var selectedIndex = SelectedIndex();
            if (_tarananlar.AllKeys.Length > selectedIndex + 1)
                selectedIndex = selectedIndex + 1;

            _selectedItem = _tarananlar.AllKeys[selectedIndex];
            TarananYukle();
        }

        private void Hatali()
        {
            if (ShowTextDialog() == DialogResult.Yes)
            {
                _tarananlar[_selectedItem] = "HATA|" + DText.Replace("|", ";");
                TarananGetir();
            }
        }

        private void DataFormLoad(object sender, EventArgs e)
        {
            CiltGetir();
        }

        private void CiltGetir()
        {
            _tarananTam.Image = null;
            _tarananZoomed.Image = null;

            var ciltId = "";
            var dbo = new DbObject();
            var parameters = new Parameter[2];
            parameters[0] = new Parameter("kontrol", Util.GetName());

            var ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE KONTROL = :kontrol AND CILT_DURUM = 'KNT' ORDER BY CILT_ID", parameters);
            if (ds != null)
            {
                ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();
            }
            else
            {
                ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE KONTROL IS NULL AND CILT_DURUM = 'KNT'");
                if (ds != null)
                {
                    ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();
                    parameters[1] = new Parameter("ciltId", ciltId);
                    dbo.Execute("UPDATE SCAN_CILT SET KONTROL = :kontrol WHERE CILT_ID = :ciltId", parameters);
                }
            }

            _ciltId = ciltId;
            var paramCilt = new Parameter[1];
            paramCilt[0] = new Parameter("ciltId", ciltId);
            var dsCilt = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_ID = :ciltId", paramCilt);
            if (dsCilt == null)
            {
                _timer.Enabled = true;
                _dogru.Enabled = false;
                _yanlis.Enabled = false;
                return;
            }

            var t = new System.Threading.Thread(CreateForm);
            t.Start();

            LoadInfo.Instance.AddInfo("Cilt Aranıyor.");
            _timer.Enabled = false;
            _dogru.Enabled = true;
            _yanlis.Enabled = true;
            _cilt = string.IsNullOrEmpty(dsCilt.Tables[0].Rows[0]["ZIP_NAME"].ToString()) ? dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().Substring(dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().LastIndexOf("\\") + 1) : dsCilt.Tables[0].Rows[0]["ZIP_NAME"].ToString();

            
            var folder = Path.GetTempPath() + TempKlasoru + _cilt + "\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (_tarananlar == null)
                _tarananlar = new NameValueCollection();
            _tarananlar.Clear();

            var dsFile = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = :ciltId AND DOSYA_DURUM = 'K' ORDER BY DOSYA_ID", paramCilt);
            for (var i = 0; i < dsFile.Tables[0].Rows.Count; i++)
            {
                LoadInfo.Instance.AddInfo("Dosyalar Yükleniyor." + (i + 1) + "/" + dsFile.Tables[0].Rows.Count);
                _tarananlar.Add(folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"], "");

                if (File.Exists(folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"]))
                    continue;
                File.Copy(IndexKlasoru + dsCilt.Tables[0].Rows[0]["CILT_PATH"] + "\\" + dsFile.Tables[0].Rows[i]["DOSYA_PATH"], folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"]);
            }

            dbo.Dispose();

            TarananGetir();
            new SoundPlayer(Resources.notify).Play();
            Invoke(new Action(() => _f.Close()));
        }

        private void TarananGetir()
        {
            var devam = false;
            for (var i = 0; i < _tarananlar.AllKeys.Length; i++)
            {
                if (!string.IsNullOrEmpty(_tarananlar[i]))
                    continue;

                devam = true;
                _selectedItem = _tarananlar.AllKeys[i];                

                TarananYukle();
                break;
            }

            if (!devam)
                CiltKaydet();
        }

        private void TarananYukle()
        {
            var param = new Parameter[2];
            param[0] = new Parameter("ciltId", _ciltId);
            param[1] = new Parameter("dosyaPath", Path.GetFileName(_selectedItem));

            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = :ciltId AND DOSYA_PATH = :dosyaPath", param);
            dbo.Dispose();

            _yevmiyeTarihi.Text = ds.Tables[0].Rows[0]["YTARIH"].ToString();
            _yevmiyeNo.Text = ds.Tables[0].Rows[0]["YNO"].ToString();
            //_yil.Text = ds.Tables[0].Rows[0]["YYIL"].ToString();

            var tarananDurum = _tarananlar[SelectedIndex()];
            switch (tarananDurum)
            {
                case "E":
                    tarananDurum = "DOĞRU";
                    break;
                case "H":
                    tarananDurum = "YANLIŞ";
                    break;
            }

            _ciltBilgisi.Text = _cilt + @" - " + (SelectedIndex() + 1) + @"/" + _tarananlar.AllKeys.Length + " - " + tarananDurum;

            var image = Image.FromFile(_selectedItem);
            _tarananTam.Image = image;

            var rect = new Rectangle(image.Width - _tarananZoomed.Width, 0, _tarananZoomed.Size.Width, _tarananZoomed.Size.Height);
            var bitmap = new Bitmap(image);
            var cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            _tarananZoomed.Image = cropped;
        }

        private void DataFormSizeChanged(object sender, EventArgs e)
        {
            try
            {
                TarananYukle();
            }
            catch { }
        }

        private void DogruClick(object sender, EventArgs e)
        {
            _tarananlar[_selectedItem] = "E";
            TarananGetir();
        }

        private void YanlisClick(object sender, EventArgs e)
        {
            if (ShowTextDialog() == DialogResult.Yes)
            {
                _tarananlar[_selectedItem] = "H|" + DText.Replace("|", ";");
                TarananGetir();
            }
        }

        private void CiltKaydet()
        {
            _dogru.Enabled = false;
            _yanlis.Enabled = false;
            var dbo = new DbObject();

            var ciltDurum = "OK";
            foreach (var file in _tarananlar.AllKeys)
            {
                var val = _tarananlar[file];
                var sonuc = "O";
                if (val.StartsWith("HATA"))
                {
                    ciltDurum = "SCN";
                    sonuc = "S";                        
                }
                else if (val.StartsWith("H"))
                {
                    ciltDurum = "BEK";
                    sonuc = "B";
                    HataKaydet(file);                        
                }
                
                var dosyaParam = new Parameter[5];
                dosyaParam[0] = new Parameter("dosyaDurum", sonuc);
                dosyaParam[1] = new Parameter("kontrol", Util.GetName());
                dosyaParam[2] = new Parameter("aciklama", val.StartsWith("H") ? val.Split('|')[1] : "");
                dosyaParam[3] = new Parameter("ciltId", Convert.ToInt32(_ciltId));
                dosyaParam[4] = new Parameter("dosyaPath", Path.GetFileName(file));                

                dbo.Execute("UPDATE SCAN_DOSYA SET DOSYA_DURUM = :dosyaDurum, KONTROL = :kontrol, ACIKLAMA = :aciklama WHERE CILT_ID = :ciltId AND DOSYA_PATH = :dosyaPath", dosyaParam);
            }

            var param = new Parameter[2];
            param[0] = new Parameter("ciltDurum", ciltDurum);
            param[1] = new Parameter("ciltId", Convert.ToInt32(_ciltId));

            dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = :ciltDurum WHERE CILT_ID = :ciltId", param);
            dbo.Dispose();

            _ciltBilgisi.Text = @"Cilt Bekleniyor";
            _timer.Enabled = true;
        }

        private static void HataKaydet(string file)
        {
            file = Path.GetFileName(file);

            var dbo = new DbObject();
            dbo.Execute("INSERT INTO SCAN_HATA (INDEKS, DOSYA, TARIH) VALUES ((SELECT INDEKS FROM SCAN_DOSYA WHERE DOSYA_PATH = '" + file + "'), '" + file + "', '" + DateTime.Now.ToShortDateString() + "')", null);
            dbo.Dispose();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (_ciltBilgisi.Text.EndsWith("..."))
                _ciltBilgisi.Text = _ciltBilgisi.Text.Substring(0, _ciltBilgisi.Text.Length - 3);
            else
                _ciltBilgisi.Text += '.';
            CiltGetir();
        }

        private LoadingForm _f;
        private void CreateForm()
        {
            _f = new LoadingForm();
            _f.ShowDialog();
        }

        private void KontrolFormFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private bool _inProgress;
        private void YeniCiltGetir()
        {
            if (_inProgress) return;
            try
            {
                _inProgress = true;
                var dbo = new DbObject();
                var ds = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_DURUM = 'KNT' AND KONTROL = '" + Util.GetName() + "' ORDER BY CILT_ID");
                if (ds != null && ds.Tables[0].Rows.Count < 2)
                {
                    var dsCilt = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE KONTROL IS NULL AND CILT_DURUM = 'KNT' ORDER BY CILT_ID");
                    if (dsCilt == null)
                        return;

                    var parameters = new Parameter[2];
                    parameters[0] = new Parameter("kontrol", Util.GetName());
                    parameters[1] = new Parameter("ciltId", dsCilt.Tables[0].Rows[0]["CILT_ID"].ToString());

                    var folder = Path.GetTempPath() + TempKlasoru + dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().Substring(dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().LastIndexOf("\\") + 1) + "\\";
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    dbo.Execute("UPDATE SCAN_CILT SET KONTROL = :kontrol WHERE CILT_ID = :ciltId", parameters);

                    var dsFile = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = " + dsCilt.Tables[0].Rows[0]["CILT_ID"] + " AND DOSYA_DURUM = 'K' ORDER BY DOSYA_ID");
                    for (var i = 0; i < dsFile.Tables[0].Rows.Count; i++)
                    {
                        if (File.Exists(folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"]))
                            continue;
                        File.Copy(IndexKlasoru + dsCilt.Tables[0].Rows[0]["CILT_PATH"] + "\\" + dsFile.Tables[0].Rows[i]["DOSYA_PATH"], folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"]);
                    }
                }
                dbo.Dispose();
            }
            finally
            {
                _inProgress = false;
            }
        }

        private void BackgroundProcessTick(object sender, EventArgs e)
        {
            var t = new System.Threading.Thread(YeniCiltGetir);
            t.Start();
        }
    }
}