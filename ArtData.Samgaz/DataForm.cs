using System;
using System.Collections.Specialized;
using System.Data;
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
        private const string IndexKlasoru = "I:\\ANKARA\\ANKARA\\ASKI\\";
        //private const string IndexKlasoru = "D:\\Scanned\\";
        private const string TempKlasoru = "ArtData\\";
        private NameValueCollection _tarananlar;
        private NameValueCollection _tarananVeritabaniBilgileri;
        private string _ciltId = "";
        private string _cilt = "";
        private string _selectedItem = "";

        private int _mouseSelection;
        private Point _mouseSelectionStartPoint;
        private Point _mouseSelectionEndPoint;

        public DataForm()
        {
            InitializeComponent();
        }

        private string Tarananlar()
        {
            return _tarananlar.AllKeys.Aggregate("", (current, t) => current + (t + "~" + _tarananlar[t] + Environment.NewLine));
        }

        private void DataFormLoad(object sender, EventArgs e)
        {
            CiltGetir();
        }

        private void CiltGetir()
        {
            _taranan.Image = null;

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

            LoadInfo.Instance.AddInfo("Cilt Bulundu. " + _cilt);

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
                    if (line.Contains("~"))
                        fileTaranan.Add(line.Split('~')[0], line.Split('~')[1]);
            }

            var dsFile = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = :ciltId AND DOSYA_DURUM = 'B' ORDER BY DOSYA_ID", paramCilt);
            if (dsFile == null)
            {
                LoadInfo.Instance.AddInfo("CILT_DURUM: BEK --> DOSYA SAYISI: 0 --> HATA GIDERILIYOR.");
                dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'KNT' WHERE CILT_ID = :ciltId", paramCilt);
                Invoke(new Action(() => _f.Close()));
                CiltGetir();
                return;
            }
            for (var i = 0; i < dsFile.Tables[0].Rows.Count; i++)
            {
                //_tarananVeritabaniBilgileri.Add(folder + dsFile.Tables[0].Rows[i]["DOSYA_PATH"], DsFileToString(dsFile.Tables[0].Rows[i]));
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
                _taranan.Image = image;

                _mouseSelection = 1;
                _mouseSelectionStartPoint = new Point(0, 0);
                _mouseSelectionEndPoint = new Point(image.Width, image.Height);

                _ciltBilgisi.Text = _cilt + @" " + (SelectedIndex() + 1) + @"/" + _tarananlar.AllKeys.Length + ((_tarananlar[_selectedItem] ?? "").StartsWith("HATA") ? " HATALI" : "");
                if (string.IsNullOrEmpty(_tarananlar[_selectedItem]) || _tarananlar[_selectedItem].StartsWith("HATA"))
                {
                    _adiSoyadi.Clear();
                    _konusu.Clear();
                    _evrakKayitTarihi.Clear();
                    _evrakKayitNo.Clear();
                    _aboneNo.Clear();
                    _ilgiliBirimNo.Clear();
                    _gelenEvrakTarihi.Clear();
                    _gelenEvrakNo.Clear();
                    _standartDosyaKodu.Clear();
                    _havaleKisileri.Clear();
                    _adiSoyadi.Focus();
                }
                else
                {
                    _adiSoyadi.Text = _tarananlar[_selectedItem].Split('|')[0].Replace("|", "");
                    _konusu.Text = _tarananlar[_selectedItem].Split('|')[1].Replace("|", "");
                    _evrakKayitTarihi.Text = _tarananlar[_selectedItem].Split('|')[2].Replace("|", "");
                    _evrakKayitNo.Text = _tarananlar[_selectedItem].Split('|')[3].Replace("|", "");
                    _aboneNo.Text = _tarananlar[_selectedItem].Split('|')[4].Replace("|", "");
                    _ilgiliBirimNo.Text = _tarananlar[_selectedItem].Split('|')[5].Replace("|", "");
                    _gelenEvrakTarihi.Text = _tarananlar[_selectedItem].Split('|')[6].Replace("|", "");
                    _gelenEvrakNo.Text = _tarananlar[_selectedItem].Split('|')[7].Replace("|", "");
                    _standartDosyaKodu.Text = _tarananlar[_selectedItem].Split('|')[8].Replace("|", "");
                    _havaleKisileri.Text = _tarananlar[_selectedItem].Split('|')[9].Replace("|", "");
                }
            }
            catch { }
            // ReSharper restore EmptyGeneralCatchClause

            var firstChild = splitContainer1.Panel1.Controls[0];
            foreach (Control control in splitContainer1.Panel1.Controls)
            {
                if (control.TabIndex < firstChild.TabIndex)
                    firstChild = control;
            }
            firstChild.Focus();
        }

        private void DataFormSizeChanged(object sender, EventArgs e)
        {
            TarananYukle();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
                case Keys.E:
                    if (e.Control)
                        Sil();
                    else
                        Hatali();
                    break;
                case Keys.X:
                    Goster(e);
                    break;
                case Keys.S:
                    if (e.Control)
                        KaydetClick(sender, e);
                    break;
                case Keys.Z:
                    if (e.Control)
                        OncekiGetirClick(sender, e);
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

        private void TarihLeave(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (string.IsNullOrEmpty(((TextBoxBase)sender).Text))
                return;

            DateTime dt;
            if (DateTime.TryParse(((TextBoxBase)sender).Text, out dt))
                return;

            ((TextBoxBase)sender).Clear();
        }

        private void KaydetClick(object sender, EventArgs e)
        {
            var evrakKayitTarihi = _evrakKayitTarihi.Text;
            if (_evrakKayitTarihi.Text == "  .  .")
                evrakKayitTarihi = "";
            var gelenEvrakTarihi = _gelenEvrakTarihi.Text;
            if (_gelenEvrakTarihi.Text == "  .  .")
                gelenEvrakTarihi = "";

            _tarananlar[_selectedItem] = _adiSoyadi.Text.Replace("|", "") + "|" + _konusu.Text.Replace("|", "") + "|" + evrakKayitTarihi.Replace("|", "") + "|" +
                                         _evrakKayitNo.Text.Replace("|", "") + "|" + _aboneNo.Text.Replace("|", "") + "|" + _ilgiliBirimNo.Text.Replace("|", "") + "|" +
                                         gelenEvrakTarihi.Replace("|", "") + "|" + _gelenEvrakNo.Text.Replace("|", "") + "|" + _standartDosyaKodu.Text.Replace("|", "") + "|" +
                                         _havaleKisileri.Text.Replace("|", "").Replace("\n", "½");

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
                else if (val == "SİL")
                {
                    var dosyaParam = new Parameter[2];
                    dosyaParam[0] = new Parameter("ciltId", Convert.ToInt32(_ciltId));
                    dosyaParam[1] = new Parameter("dosyaPath", Path.GetFileName(file));
                    dbo.Execute("DELETE FROM SCAN_DOSYA WHERE CILT_ID = :ciltId AND DOSYA_PATH = :dosyaPath ", dosyaParam);
                }
                else
                {
                    var dosyaParam = new Parameter[13];
                    dosyaParam[0] = new Parameter("indeks", Util.GetName());
                    dosyaParam[1] = new Parameter("dataAdisoyadi", val.Split('|')[0].Replace("|", ""));
                    dosyaParam[2] = new Parameter("dataKonusu", val.Split('|')[1].Replace("|", ""));
                    dosyaParam[3] = new Parameter("dataEvrakkayittarihi", val.Split('|')[2].Replace("|", ""));
                    dosyaParam[4] = new Parameter("dataEvrakkayitno", val.Split('|')[3].Replace("|", ""));
                    dosyaParam[5] = new Parameter("dataAboneno", val.Split('|')[4].Replace("|", ""));
                    dosyaParam[6] = new Parameter("dataIlgilibirimno", val.Split('|')[5].Replace("|", ""));
                    dosyaParam[7] = new Parameter("dataGelenevraktarihi", val.Split('|')[6].Replace("|", ""));
                    dosyaParam[8] = new Parameter("dataGelenevrakno", val.Split('|')[7].Replace("|", ""));
                    dosyaParam[9] = new Parameter("dataStandartdosyakodu", val.Split('|')[8].Replace("|", ""));
                    dosyaParam[10] = new Parameter("dataHavalekisileri", val.Split('|')[9].Replace("|", "").Replace("½", "\n"));
                    dosyaParam[11] = new Parameter("ciltId", Convert.ToInt32(_ciltId));
                    dosyaParam[12] = new Parameter("dosyaPath", Path.GetFileName(file));
                    dbo.Execute(@"
UPDATE SCAN_DOSYA 
  SET DOSYA_DURUM = 'K', 
      INDEKS = :indeks, 
      DATA_ADISOYADI = :dataAdiSoyadi, 
      DATA_KONUSU = :dataKonusu, 
      DATA_EVRAKKAYITTARIHI = :dataEvrakkayittarihi,
      DATA_EVRAKKAYITNO = :dataEvrakkayitno,
      DATA_ABONENO = :dataAboneno,
      DATA_ILGILIBIRIMNO = :dataIlgilibirimno,
      DATA_GELENEVRAKTARIHI = :dataGelenevraktarihi,
      DATA_GELENEVRAKNO = :dataGelenevrakno,
      DATA_STANDARTDOSYAKODU = :dataStandartdosyakodu,
      DATA_HAVALEKISILERI = :dataHavalekisileri
WHERE CILT_ID = :ciltId 
  AND DOSYA_PATH = :dosyaPath", dosyaParam);
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

        private int Onceki()
        {
            var index = 0;
            var val = "SİL";
            while ((val == "" || val == "SİL") && SelectedIndex() > index)
            {
                val = _tarananlar[_tarananlar.AllKeys[SelectedIndex() - index]];
                index++;
            }
            return index - 1;
        }

        private void OncekiGetirClick(object sender, EventArgs e)
        {
            if (SelectedIndex() == 0)
                return;

            var val = _tarananlar[_tarananlar.AllKeys[SelectedIndex() - Onceki()]];
            if (string.IsNullOrEmpty(val))
                return;

            var dataAdiSoyadi = val.Split('|')[0].Replace("|", "");
            var dataKonusu = val.Split('|')[1].Replace("|", "");
            var dataEvrakkayittarihi = val.Split('|')[2].Replace("|", "");
            var dataEvrakkayitno = val.Split('|')[3].Replace("|", "");
            var dataAboneno = val.Split('|')[4].Replace("|", "");
            var dataIlgilibirimno = val.Split('|')[5].Replace("|", "");
            var dataGelenevraktarihi = val.Split('|')[6].Replace("|", "");
            var dataGelenevrakno = val.Split('|')[7].Replace("|", "");
            var dataStandartdosyakodu = val.Split('|')[8].Replace("|", "");
            var dataHavalekisileri = val.Split('|')[9].Replace("|", "").Replace("½", "\n");

            _adiSoyadi.Text = dataAdiSoyadi;
            _konusu.Text = dataKonusu;
            _evrakKayitTarihi.Text = dataEvrakkayittarihi;
            _evrakKayitNo.Text = dataEvrakkayitno;
            _aboneNo.Text = dataAboneno;
            _ilgiliBirimNo.Text = dataIlgilibirimno;
            _gelenEvrakTarihi.Text = dataGelenevraktarihi;
            _gelenEvrakNo.Text = dataGelenevrakno;
            _standartDosyaKodu.Text = dataStandartdosyakodu;
            _havaleKisileri.Text = dataHavalekisileri;
        }

        private void VeritabanindanGetirClick(object sender, EventArgs e)
        {
            VeritabanindanGetir(Path.GetFileName(_selectedItem));
        }

        private void VeritabanindanGetir(string dosyaAdi)
        {
            var parameters = new Parameter[2];
            parameters[0] = new Parameter("ciltId", _ciltId);
            parameters[1] = new Parameter("dosyaPath", dosyaAdi);

            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = :ciltId AND DOSYA_PATH = :dosyaPath", parameters);
            dbo.Dispose();

            _adiSoyadi.Text = ds.Tables[0].Rows[0]["DATA_ADISOYADI"].ToString();
            _konusu.Text = ds.Tables[0].Rows[0]["DATA_KONUSU"].ToString();
            _evrakKayitTarihi.Text = ds.Tables[0].Rows[0]["DATA_EVRAKKAYITTARIHI"].ToString();
            _evrakKayitNo.Text = ds.Tables[0].Rows[0]["DATA_EVRAKKAYITNO"].ToString();
            _aboneNo.Text = ds.Tables[0].Rows[0]["DATA_ABONENO"].ToString();
            _ilgiliBirimNo.Text = ds.Tables[0].Rows[0]["DATA_ILGILIBIRIMNO"].ToString();
            _gelenEvrakTarihi.Text = ds.Tables[0].Rows[0]["DATA_GELENEVRAKTARIHI"].ToString();
            _gelenEvrakNo.Text = ds.Tables[0].Rows[0]["DATA_GELENEVRAKNO"].ToString();
            _standartDosyaKodu.Text = ds.Tables[0].Rows[0]["DATA_STANDARTDOSYAKODU"].ToString();
            _havaleKisileri.Text = ds.Tables[0].Rows[0]["DATA_HAVALEKISILERI"].ToString();
        }

        private void TarananMouseDown(object sender, MouseEventArgs e)
        {
            if (_mouseSelection == 1)
            {
                _mouseSelectionStartPoint = TranslateZoomMousePosition(e.Location);
                _mouseSelection = 2;
            }
            else
            {
                _mouseSelectionEndPoint = TranslateZoomMousePosition(e.Location);
                _mouseSelection = 1;

                var kirp = new Rectangle(_mouseSelectionStartPoint, new Size(_mouseSelectionEndPoint.X - _mouseSelectionStartPoint.X, _mouseSelectionEndPoint.Y - _mouseSelectionStartPoint.Y));
                if (kirp.Width < 0 || kirp.Height < 0)
                    return;

                var orjResim = _taranan.Image;
                var kirpilmisResim = new Bitmap(orjResim).Clone(kirp, orjResim.PixelFormat);
                _taranan.Image = kirpilmisResim;
                if (MessageBox.Show("Kırpma işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    _taranan.Image = orjResim;
                else
                {
                    kirpilmisResim.Save(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length));
                }
            }
        }

        protected Point TranslateZoomMousePosition(Point coordinates)
        {
            // test to make sure our image is not null
            if (_taranan.Image == null) return coordinates;
            // This is the one that gets a little tricky. Essentially, need to check 
            // the aspect ratio of the image to the aspect ratio of the control
            // to determine how it is being rendered
            float imageAspect = (float)_taranan.Image.Width / _taranan.Image.Height;
            float controlAspect = (float)_taranan.Width / _taranan.Height;
            float newX = coordinates.X;
            float newY = coordinates.Y;
            if (imageAspect > controlAspect)
            {
                // This means that we are limited by width, 
                // meaning the image fills up the entire control from left to right
                float ratioWidth = (float)_taranan.Image.Width / _taranan.Width;
                newX *= ratioWidth;
                float scale = (float)_taranan.Width / _taranan.Image.Width;
                float displayHeight = scale * _taranan.Image.Height;
                float diffHeight = _taranan.Height - displayHeight;
                diffHeight /= 2;
                newY -= diffHeight;
                newY /= scale;
            }
            else
            {
                // This means that we are limited by height, 
                // meaning the image fills up the entire control from top to bottom
                float ratioHeight = (float)_taranan.Image.Height / _taranan.Height;
                newY *= ratioHeight;
                float scale = (float)_taranan.Height / _taranan.Image.Height;
                float displayWidth = scale * _taranan.Image.Width;
                float diffWidth = _taranan.Width - displayWidth;
                diffWidth /= 2;
                newX -= diffWidth;
                newX /= scale;
            }
            return new Point((int)newX, (int)newY);
        }

        private void DataFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _mouseSelection = 1;
                _mouseSelectionStartPoint = new Point(0, 0);
                _mouseSelectionEndPoint = new Point(_taranan.Image.Width, _taranan.Image.Height);
            }
        }

        private void SilClick(object sender, EventArgs e)
        {
            Sil();
        }









    }
}