using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using ArtData.Properties;
using Library.Scan;

namespace ArtData
{
    public partial class DataForm : BaseForm
    {
        private const string IndexKlasoru = "I:\\";
        //private const string IndexKlasoru = "D:\\Scanned\\";
        private const string TempKlasoru = "ArtData\\";
        private NameValueCollection _tarananlar;
        private NameValueCollection _tarananVeritabaniBilgileri;
        private string _ciltId = "";
        private string _cilt = "";
        private string _selectedItem = "";

        public DataForm()
        {
            InitializeComponent();
        }

        private string Tarananlar()
        {
            return _tarananlar.AllKeys.Aggregate("", (current, t) => current + (t + ";" + _tarananlar[t] + Environment.NewLine));
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
            parameters[0] = new Parameter("indeks", Util.GetName());

            var ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE INDEKS = :indeks AND CILT_DURUM = 'BEK' ORDER BY CILT_ID", parameters);
            if (ds != null)
            {
                ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();
            }
            else
            {
                ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE INDEKS IS NULL AND CILT_DURUM = 'BEK'");
                if (ds != null)
                {
                    ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();
                    parameters[1] = new Parameter("ciltId", ciltId);
                    dbo.Execute("UPDATE SCAN_CILT SET INDEKS = :indeks WHERE CILT_ID = :ciltId", parameters);
                }
            }

            _ciltId = ciltId;
            var paramCilt = new Parameter[1];
            paramCilt[0] = new Parameter("ciltId", ciltId);
            var dsCilt = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_ID = :ciltId", paramCilt);
            if (dsCilt == null)
            {
                _timer.Enabled = true;
                _kaydet.Enabled = false;
                return;
            }

            var t = new System.Threading.Thread(CreateForm);
            t.Start();

            LoadInfo.Instance.AddInfo("Cilt Aranıyor.");
            _timer.Enabled = false;
            _kaydet.Enabled = true;
            _cilt = string.IsNullOrEmpty(dsCilt.Tables[0].Rows[0]["ZIP_NAME"].ToString()) ? dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().Substring(dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().LastIndexOf("\\") + 1) : dsCilt.Tables[0].Rows[0]["ZIP_NAME"].ToString();

            var paramTapu = new Parameter[1];
            paramTapu[0] = new Parameter("tapuId", dsCilt.Tables[0].Rows[0]["TAPU_ID"].ToString());
            var dsTapu = dbo.DataSet("SELECT * FROM SCAN_TAPU WHERE TAPU_ID = :tapuId", paramTapu);
            _mudurluk.Text = dsTapu.Tables[0].Rows[0]["ILCE"] + @"/" + dsTapu.Tables[0].Rows[0]["IL"] + @"/" + dsTapu.Tables[0].Rows[0]["BOLGE"];

            LoadInfo.Instance.AddInfo("Cilt Bulundu. " + _cilt + " " + _mudurluk.Text);

            /*
            var folder = Path.GetTempPath() + TempKlasoru;
            if (Directory.Exists(folder))
                Directory.Delete(folder, true);
            Directory.CreateDirectory(folder);            
            */
            var folder = Path.GetTempPath() + TempKlasoru + _cilt + "\\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (_tarananlar == null)
                _tarananlar = new NameValueCollection();
            _tarananlar.Clear();

            if (_tarananVeritabaniBilgileri == null)
                _tarananVeritabaniBilgileri = new NameValueCollection();
            _tarananVeritabaniBilgileri.Clear();

            var fileTaranan = new NameValueCollection();
            if (File.Exists(folder + "data.txt"))
            {
                var lines = File.ReadAllLines(folder + "data.txt");
                foreach (var line in lines)
                    fileTaranan.Add(line.Split(';')[0], line.Split(';')[1]);
            }

            var dsFile = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = :ciltId AND DOSYA_DURUM = 'B' ORDER BY DOSYA_ID", paramCilt);
            if (dsFile == null)
            {
                LoadInfo.Instance.AddInfo("CILT_DURUM: BEK --> DOSYA SAYISI: 0 --> HATA GIDERILIYOR.");
                dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'KNT' WHERE CILT_ID = :ciltId", paramCilt);
                Invoke(new Action(() => _f.Close()));
                CiltGetir();                
            }
            for (var i = 0; i < dsFile.Tables[0].Rows.Count; i++)
            {
                _tarananVeritabaniBilgileri.Add(folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"], dsFile.Tables[0].Rows[i]["YTARIH"] + "-" + dsFile.Tables[0].Rows[i]["YNO"] + "-" + dsFile.Tables[0].Rows[i]["ACIKLAMA"]);
                LoadInfo.Instance.AddInfo("Dosyalar Yükleniyor." + (i + 1) + "/" + dsFile.Tables[0].Rows.Count);
                _tarananlar.Add(folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"], fileTaranan[folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"]]);
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
            File.WriteAllText(Path.GetTempPath() + TempKlasoru + _cilt + "\\data.txt", Tarananlar());

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
            // ReSharper disable EmptyGeneralCatchClause
            try
            {
                var image = Image.FromFile(_selectedItem);
                _tarananTam.Image = image;

                var rect = new Rectangle(image.Width - _tarananZoomed.Width, 0, _tarananZoomed.Size.Width, _tarananZoomed.Size.Height);
                var bitmap = new Bitmap(image);
                var cropped = bitmap.Clone(rect, bitmap.PixelFormat);
                _tarananZoomed.Image = cropped;

                _ciltBilgisi.Text = _cilt + @" " + (SelectedIndex() + 1) + @"/" + _tarananlar.AllKeys.Length + (_tarananlar[_selectedItem].StartsWith("HATA") ? " HATALI" : "");
                _dosyaAdi.Text = Path.GetFileName(_selectedItem);
                _veritabaniBilgileri.Text = _tarananVeritabaniBilgileri[_selectedItem];
                if (string.IsNullOrEmpty(_tarananlar[_selectedItem]) || _tarananlar[_selectedItem].StartsWith("HATA"))
                {
                    _yevmiyeTarihi.Clear();
                    _yevmiyeNo.Clear();
                    _yil.Clear();
                    _yevmiyeTarihi.Focus();
                }
                else
                {
                    _yevmiyeTarihi.Text = _tarananlar[_selectedItem].Split('|')[0].Replace("|", "");
                    _yevmiyeNo.Text = _tarananlar[_selectedItem].Split('|')[1].Replace("|", "");
                    _yil.Text = _tarananlar[_selectedItem].Split('|')[2].Replace("|", "");
                }
            }
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }

        private void DataFormSizeChanged(object sender, EventArgs e)
        {
            TarananYukle();
        }

        private void YevmiyeTarihiKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _yevmiyeNo.Focus();
                    break;
                case Keys.Left:
                    Left(e);
                    break;
                case Keys.Right:
                    Right(e);
                    break;
                case Keys.Delete:
                    if (e.Control)
                        Sil();
                    else
                        Hatali();
                    break;
                case Keys.X:
                    Goster(e);
                    break;
            }
        }

        private void YevmiyeNoKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _yil.Focus();
                    break;
                case Keys.Left:
                    Left(e);
                    break;
                case Keys.Right:
                    Right(e);
                    break;
                case Keys.Delete:
                    if (e.Control)
                        Sil();
                    else
                        Hatali();
                    break;
                case Keys.X:
                    Goster(e);
                    break;
            }
        }

        private void YilKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    KaydetClick(sender, e);
                    break;
                case Keys.Left:
                    Left(e);
                    break;
                case Keys.Right:
                    Right(e);
                    break;
                case Keys.Delete:
                    if (e.Control)
                        Sil();
                    else
                        Hatali();
                    break;
                case Keys.X:
                    Goster(e);
                    break;
            }
        }

        private void Goster(KeyEventArgs e)
        {
            if (!e.Control) return;
            var frm = new InfoForm();

            foreach (var t in _tarananlar.AllKeys)
                frm._info.Items.Add(t + "-" + _tarananlar[t] + "\n");
            frm.Show();
        }

        private void Hatali()
        {
            if (ShowTextDialog() == DialogResult.Yes)
            {
                _tarananlar[_selectedItem] = "HATA|" + DText.Replace("|", ";");
                TarananGetir();
            }
        }

        private void Sil()
        {
            if (MessageBox.Show("Tarama silinecektir. Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _tarananlar[_selectedItem] = "SİL";
                TarananGetir();
            }
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

        private new void Left(KeyEventArgs e)
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

        private new void Right(KeyEventArgs e)
        {
            if (!e.Control) return;
            var selectedIndex = SelectedIndex();
            if (_tarananlar.AllKeys.Length > selectedIndex + 1)
                selectedIndex = selectedIndex + 1;

            _selectedItem = _tarananlar.AllKeys[selectedIndex];
            TarananYukle();
        }

        private void YevmiyeTarihiLeave(object sender, EventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParse(_yevmiyeTarihi.Text, out dt))
                _yil.Text = dt.Year.ToString();
            else
            {
                _yevmiyeTarihi.Clear();
                _yevmiyeTarihi.Focus();
            }
        }

        private void KaydetClick(object sender, EventArgs e)
        {
            DateTime dt;
            if (!DateTime.TryParse(_yevmiyeTarihi.Text, out dt))
            {
                _yevmiyeTarihi.Clear();
                _yevmiyeTarihi.Focus();
                return;
            }
            int yNo;
            if (!int.TryParse(_yevmiyeNo.Text, out yNo) || yNo < 0)
            {
                _yevmiyeNo.Clear();
                _yevmiyeNo.Focus();
                return;
            }
            int yil;
            if (!int.TryParse(_yil.Text, out yil) || yil < 1900 || yil > 2012)
            {
                _yil.Clear();
                _yil.Focus();
                return;
            }
            _tarananlar[_selectedItem] = dt.ToShortDateString() + "|" + yNo + "|" + yil;
            _oncekiYevmiyeTarihi.Text = dt.ToShortDateString();
            _oncekiYevmiyeNo.Text = yNo.ToString();
            TarananGetir();
        }

        private void CiltKaydet()
        {
            _kaydet.Enabled = false;
            var dbo = new DbObject();
            var param = new Parameter[1];
            param[0] = new Parameter("ciltId", _ciltId);

            bool kontrol = true;
            foreach (var file in _tarananlar.AllKeys)
            {
                var val = _tarananlar[file];
                if (val.StartsWith("HATA"))
                {
                    kontrol = false;
                    var dosyaParam = new Parameter[4];
                    dosyaParam[0] = new Parameter("indeks", Util.GetName());
                    dosyaParam[1] = new Parameter("aciklama", val.Split('|')[1]);
                    dosyaParam[2] = new Parameter("ciltId", Convert.ToInt32(_ciltId));
                    dosyaParam[3] = new Parameter("dosyaPath", Path.GetFileName(file));

                    dbo.Execute("UPDATE SCAN_DOSYA SET DOSYA_DURUM = 'S', INDEKS = :indeks, ACIKLAMA = :aciklama WHERE CILT_ID = :ciltId AND DOSYA_PATH = :dosyaPath", dosyaParam);
                }
                else if (val.StartsWith("SİL"))
                {
                    var dosyaParam = new Parameter[2];
                    dosyaParam[0] = new Parameter("ciltId", Convert.ToInt32(_ciltId));
                    dosyaParam[1] = new Parameter("dosyaPath", Path.GetFileName(file));
                    dbo.Execute("DELETE FROM SCAN_DOSYA WHERE CILT_ID = :ciltId AND DOSYA_PATH = :dosyaPath ", dosyaParam);
                }
                else
                {
                    var dosyaParam = new Parameter[6];
                    dosyaParam[0] = new Parameter("indeks", Util.GetName());
                    dosyaParam[1] = new Parameter("yTarih", val.Split('|')[0].Replace("|", ""));
                    dosyaParam[2] = new Parameter("yNo", val.Split('|')[1].Replace("|", ""));
                    dosyaParam[3] = new Parameter("yYil", val.Split('|')[2].Replace("|", ""));
                    dosyaParam[4] = new Parameter("ciltId", Convert.ToInt32(_ciltId));
                    dosyaParam[5] = new Parameter("dosyaPath", Path.GetFileName(file));
                    dbo.Execute("UPDATE SCAN_DOSYA SET DOSYA_DURUM = 'K', INDEKS = :indeks, YTARIH = :yTarih, YNO = :yNo, YYIL = :yYil WHERE CILT_ID = :ciltId AND DOSYA_PATH = :dosyaPath", dosyaParam);
                }
            }

            dbo.Execute("UPDATE SCAN_CILT SET TARIH = '" + DateTime.Now.ToShortDateString() + "', CILT_DURUM = '" + (kontrol ? "KNT" : "SCN") + "' WHERE CILT_ID = :ciltId", param);
            dbo.Dispose();

            try
            {
                Directory.Delete(Path.GetTempPath() + TempKlasoru + _cilt, true);
            }
            catch { }
            _ciltBilgisi.Text = @"Cilt Bekleniyor";
            _timer.Enabled = true;
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

        private void DataFormFormClosed(object sender, FormClosedEventArgs e)
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
                var ds = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_DURUM = 'BEK' AND INDEKS = '" + Util.GetName() + "' ORDER BY CILT_ID");
                if (ds != null && ds.Tables[0].Rows.Count < 2)
                {
                    var dsCilt = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE INDEKS IS NULL AND CILT_DURUM = 'BEK'");
                    if (dsCilt == null)
                        return;

                    var parameters = new Parameter[2];
                    parameters[0] = new Parameter("indeks", Util.GetName());
                    parameters[1] = new Parameter("ciltId", dsCilt.Tables[0].Rows[0]["CILT_ID"].ToString());

                    var folder = Path.GetTempPath() + TempKlasoru + dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().Substring(dsCilt.Tables[0].Rows[0]["CILT_PATH"].ToString().LastIndexOf("\\") + 1) + "\\";
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    dbo.Execute("UPDATE SCAN_CILT SET INDEKS = :indeks WHERE CILT_ID = :ciltId", parameters);

                    var dsFile = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = " + dsCilt.Tables[0].Rows[0]["CILT_ID"] + " AND DOSYA_DURUM = 'B' ORDER BY DOSYA_ID");
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