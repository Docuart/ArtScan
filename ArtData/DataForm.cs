using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
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
        //private const string IndexKlasoru = "D:\\Scanned\\SAMGAZ\\";
        private const string TempKlasoru = "ArtData\\";
        private NameValueCollection _tarananlar;
        private NameValueCollection _tarananVeritabaniBilgileri;
        private string _ciltId = "";
        private string _cilt = "";
        private string _selectedItem = "";

        private int _mouseSelection;
        private Point _mouseSelectionStartPoint;
        private Point _mouseSelectionEndPoint;
        private ArrayList _oncekiHaller = new ArrayList();

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

            _evrakTuru.Items.Clear();
            _evrakTuru.Items.Add("");

            if (!File.Exists(Application.StartupPath + "\\EvrakTurleri.txt"))
            {
                using (var f = File.CreateText(Application.StartupPath + "\\EvrakTurleri.txt")) { }
            }

            var evrakTurleri = File.ReadAllLines(Application.StartupPath + "\\EvrakTurleri.txt");
            _evrakTuru.Items.Add("");
            foreach (var evrakTuru in evrakTurleri)
            {
                _evrakTuru.Items.Add(evrakTuru);
            }

            if (_kontrolSeviyesi.SelectedIndex != -1)
                CiltGetir();
        }

        private string KontrolSeviyesi()
        {
            if (_kontrolSeviyesi.SelectedIndex == -1)
                return "";
            return _kontrolSeviyesi.SelectedItem.ToString().Substring(_kontrolSeviyesi.SelectedItem.ToString().IndexOf("[") + 1, _kontrolSeviyesi.SelectedItem.ToString().IndexOf("]") - _kontrolSeviyesi.SelectedItem.ToString().IndexOf("[") - 1);
        }

        private void CiltGetir()
        {
            _taranan.Image = null;

            var ciltId = "";
            var dbo = new DbObject();
            var parameters = new Parameter[2];
            parameters[0] = new Parameter("indeks", Util.GetName());

            var ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE INDEKS = :indeks AND CILT_DURUM = '" + KontrolSeviyesi() + "' ORDER BY CILT_ID", parameters);
            if (ds != null)
            {
                ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();

                //var dbo2 = new DbObject();
                //var parameters2 = new Parameter[1];
                //parameters2[0] = new Parameter("CiltId",ciltId);

                //var ds2 = dbo2.DataSet("SELECT COUNT(DATA_EVRAKTURU) AS EVRAKSAYI FROM SCAN_DOSYA WHERE CILT_ID =:CiltId AND DATA_EVRAKTURU IS NOT NULL",parameters2);
                //if (ds2 != null)
                //{
                //    if (Convert.ToInt32(ds2.Tables[0].Rows[0]["EVRAKSAYI"].ToString())!=0)
                //    {
                //        var dbo3 = new DbObject();
                //        var parameters3 = new Parameter[1];
                //        parameters3[0] = new Parameter("CiltId",ciltId);
                //        var ds3 = dbo3.DataSet("SELECT COUNT(DATA_ABONENO) AS ABONESAYI FROM SCAN_DOSYA WHERE CILT_ID =:CiltId AND DATA_ABONE IS NOT NULL",parameters3);
                //        if (ds3 != null)
                //        {
                //            if (Convert.ToInt32(ds3.Tables[0].Rows[0]["ABONESAYI"].ToString()) != 0)
                //            {

                //            }
                //        }
                //    }
                //}
            }
            else
            {
                ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE INDEKS IS NULL AND CILT_DURUM = '" + KontrolSeviyesi() + "' ORDER BY CILT_ID");
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

            _tarayanBilgileri.Text = dsCilt.Tables[0].Rows[0]["TARAYAN"] + " " + dsCilt.Tables[0].Rows[0]["TARAMA_TARIHI"];

            TarananGetir();
            new SoundPlayer(Resources.notify).Play();
            Invoke(new Action(() => _f.Close()));
        }

        private void TarananGetir()
        {
            File.WriteAllText(Path.GetTempPath() + TempKlasoru + _cilt + "\\data.txt", Tarananlar());
            string temp = Path.GetTempPath() + TempKlasoru + _cilt;
            var devam = false;
            for (var i = 0; i < _tarananlar.AllKeys.Length; i++)
            {
                if (!string.IsNullOrEmpty(_tarananlar[i]))
                    continue;

                devam = true;
                _selectedItem = _tarananlar.AllKeys[i];
                File.SetAttributes(temp, FileAttributes.Normal);
                File.SetAttributes(_selectedItem, FileAttributes.Normal);

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
                    //_aboneNo.Clear();
                    _aboneNo.Focus();
                }
                else
                {
                    _aboneNo.Text = _tarananlar[_selectedItem].Split('|')[0].Replace("|", "");
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

        private void KaydetClick(object sender, EventArgs e)
        {
            if (KontrolSeviyesi() != "BEK")
                //if (_evrakTuru.SelectedItem==null)
                //    return;

            if (KontrolSeviyesi() == "BE3")
            {
                if (string.IsNullOrEmpty(_aboneNo.Text))
                {
                    MessageBox.Show("Abone numarası olmadan kaydedemezsiniz.", "Onay", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //28.02.2013
                var ciltId = "";
                var dbo1 = new DbObject();
                var parameters1 = new Parameter[2];
                parameters1[0] = new Parameter("indeks", Util.GetName());

                var ds1 =
                    dbo1.DataSet(
                        "SELECT c.CILT_ID FROM SCAN_CILT c, SCAN_DOSYA d WHERE c.INDEKS = :indeks AND CILT_DURUM = '" +
                        KontrolSeviyesi() +
                        "' AND d.CILT_ID = c.CILT_ID AND d.DATA_EVRAKTURU IS NOT NULL ORDER BY CILT_ID", parameters1);
                if (ds1 != null)
                {
                    _tarananlar[_selectedItem] = _aboneNo.Text.Replace("|", "");
                    TarananGetir();
                    return;
                }
                //OSMAN YILMAZ 2-1-3
            }

            _tarananlar[_selectedItem] = _aboneNo.Text.Replace("|", "") + "|" + _evrakTuru.SelectedItem;
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
                    var dosyaDurum = "B";
                    if (KontrolSeviyesi() == "BE3")
                        dosyaDurum = "K";

                    var dosyaParam = new Parameter[13];
                    dosyaParam[0] = new Parameter("indeks", Util.GetName());
                    dosyaParam[1] = new Parameter("dataAboneno", val.Split('|')[0].Replace("|", ""));
                    //dosyaParam[2] = new Parameter("dataEvrakturu", val.Split('|')[1].Replace("|", ""));
                    dosyaParam[3] = new Parameter("ciltId", Convert.ToInt32(_ciltId));
                    dosyaParam[4] = new Parameter("dosyaPath", Path.GetFileName(file));
                    dbo.Execute(""
                                + "UPDATE SCAN_DOSYA "
                                + "  SET DOSYA_DURUM = '" + dosyaDurum + "', "
                                + "      INDEKS = :indeks, "
                                + "      DATA_ABONENO = :dataAboneno "
                                + "WHERE CILT_ID = :ciltId "
                                + "  AND DOSYA_PATH = :dosyaPath", dosyaParam);
                }
            }

            var ciltDurum = "BEK";
            switch (KontrolSeviyesi())
            {
                case "BEK":
                    ciltDurum = "BE2";
                    break;
                case "BE2":
                    ciltDurum = "BE3";
                    break;
                case "BE3":
                    ciltDurum = kontrol ? "KNT" : "SCN";
                    break;

            }
            dbo.Execute("UPDATE SCAN_CILT SET TARIH = '" + DateTime.Now.ToShortDateString() + "', CILT_DURUM = '" + ciltDurum + "' WHERE CILT_ID = :ciltId", param);
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
            if (_kontrolSeviyesi.SelectedIndex == -1) return;
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

            var dataAboneno = val.Split('|')[0].Replace("|", "");
            var dataEvrakturu = val.Split('|')[1].Replace("|", "");

            _aboneNo.Text = dataAboneno;
            _evrakTuru.SelectedItem = dataEvrakturu;
        }

        private bool _sendFileToServer = false;
        private void TarananMouseDown(object sender, MouseEventArgs e)
        {
            if (_mouseSelection == 1)
            {
                _mouseSelectionStartPoint = TranslateZoomMousePosition(e.Location);
                _mouseSelection = 2;
            }
            else
            {
                /*
                _mouseSelectionEndPoint = TranslateZoomMousePosition(e.Location);
                _mouseSelection = 1;

                var kirp = new Rectangle(_mouseSelectionStartPoint, new Size(_mouseSelectionEndPoint.X - _mouseSelectionStartPoint.X, _mouseSelectionEndPoint.Y - _mouseSelectionStartPoint.Y));
                if (kirp.Width < 0 || kirp.Height < 0)
                    return;

                AddToPrevList(new Bitmap(_taranan.Image));
                using (var kirpilmisResim = new Bitmap(_taranan.Image).Clone(kirp, _taranan.Image.PixelFormat))
                {
                    //_taranan.Image = kirpilmisResim;                    
                    _sendFileToServer = true;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 40);
                    kirpilmisResim.Save(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length), Util.GetImageEncoder("JPEG"), encoderParameters);                                                            
                }
                _taranan.Image = Image.FromFile(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length));                
                */

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


                    _taranan.Image.Save(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length), Util.GetImageEncoder(), Util.GetEncoderParameters());

                    var bmp = new Bitmap(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length));
                    try
                    {
                        if (_taranan.Image != null)
                        {
                            _taranan.Image.Dispose();
                            _taranan.Image = null;
                        }
                        orjResim.Dispose();
                        kirpilmisResim.Dispose();
                        bmp.Save(_selectedItem, Util.GetImageEncoder(), Util.GetEncoderParameters());
                        //geri dön kırp bmp yi save edemiyor
                        _taranan.Image = Image.FromFile(_selectedItem);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex + "temp Kayit");
                    }
                    finally
                    {

                        bmp.Dispose();

                    }
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

        private bool _createBlackBorders = false;
        private Bitmap Rotate(float aci)
        {
            AddToPrevList(new Bitmap(_taranan.Image));

            using (var bitmap = new Bitmap(_taranan.Image))
            {
                var tmpx = new Bitmap(bitmap);
                if (_createBlackBorders)
                {
                    var boyut = (int)Math.Sqrt(bitmap.Width * bitmap.Width + bitmap.Height * bitmap.Height) + 10;
                    tmpx = new Bitmap(boyut, boyut, bitmap.PixelFormat);
                    using (var gr = Graphics.FromImage(tmpx))
                    {
                        gr.FillRectangle(Brushes.Black, 0, 0, boyut, boyut);
                        gr.DrawImage(bitmap, (boyut - bitmap.Width) / 2, (boyut - bitmap.Height) / 2, bitmap.Width, bitmap.Height);
                    }
                    _createBlackBorders = false;
                }
                else
                {
                    tmpx = new Bitmap(_taranan.Image);
                }

                if (Math.Sign(aci) == 0)
                {
                    var sk = new gmseDeskew.gmseDeskew(bitmap);
                    aci = -(float)sk.GetSkewAngle();
                }
                var tmp = new Bitmap(tmpx.Width, tmpx.Height, tmpx.PixelFormat);
                tmp.SetResolution(tmpx.HorizontalResolution, tmpx.VerticalResolution);
                var g = Graphics.FromImage(tmp);
                try
                {
                    g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                    g.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                    g.RotateTransform(aci);
                    g.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                    g.DrawImage(tmpx, 0, 0);
                    tmpx.Dispose();
                    return tmp;
                    //return ImageProcessor.Crop(tmp, 33);                    
                }
                finally
                {
                    g.Dispose();
                }
            }
            _sendFileToServer = true;
        }

        private Bitmap ResmiKaydet(Bitmap img)
        {
            //_taranan.Image.Save(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length), Util.GetImageEncoder(), Util.GetEncoderParameters());
            // var bmpimg = new Bitmap(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length));
            _taranan.Image.Dispose();
            _taranan.Image = null;
            // bmpimg.Save(_selectedItem, Util.GetImageEncoder(), Util.GetEncoderParameters());

            var bmpIndexSave = (Bitmap)img.Clone();
            bmpIndexSave.Save(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length), Util.GetImageEncoder(), Util.GetEncoderParameters());
            bmpIndexSave.Dispose();
            var bmpTempSave = (Bitmap)img.Clone();
            img.Dispose();
            bmpTempSave.Save(_selectedItem, Util.GetImageEncoder(), Util.GetEncoderParameters());
            return bmpTempSave;

        }

        private void OtoEgimGiderClick(object sender, EventArgs e)
        {
            var bitmap = Rotate(0);
            var tmp = ResmiKaydet(bitmap);
            _taranan.Image = tmp;
        }

        private void SolaHassasDondurClick(object sender, EventArgs e)
        {
            var bitmap = Rotate(-1);
            var tmp = ResmiKaydet(bitmap);
            _taranan.Image = tmp;
        }

        private void SagaHassasDondurClick(object sender, EventArgs e)
        {
            var bitmap = Rotate(1);
            var tmp = ResmiKaydet(bitmap);
            _taranan.Image = tmp;
        }

        /*Not Eski sistem olduğu için iptal ettim*/
        //private void _90SolaDondur_Click(object sender, EventArgs e)
        //{
        //    var bitmap = Rotate(90);
        //    var tmp = ResmiKaydet(bitmap);
        //    _taranan.Image = tmp;
        //}

        //private void _180Dondur_Click(object sender, EventArgs e)
        //{
        //    var bitmap = Rotate(180);
        //    var tmp = ResmiKaydet(bitmap);
        //    _taranan.Image = tmp;
        //}

        private void _270SolaDondur_Click(object sender, EventArgs e)
        {
            _taranan.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
            _taranan.Image.Save(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length), Util.GetImageEncoder(), Util.GetEncoderParameters());
            _taranan.Image.Save(_selectedItem, Util.GetImageEncoder(), Util.GetEncoderParameters());
            _taranan.Image = Image.FromFile(_selectedItem);
            //var bitmap = Rotate(270);
            //var tmp = ResmiKaydet(bitmap);
            //_taranan.Image = tmp;
        }

        private void AciliDondurClick(object sender, EventArgs e)
        {
            var bitmap = Rotate(Convert.ToInt32(_aci.Text));
            var tmp = ResmiKaydet(bitmap);
            _taranan.Image = tmp;
        }

        private int _prevListLastIndex = 1;
        private void AddToPrevList(Bitmap bitmap)
        {
            if (_oncekiHaller == null)
                _oncekiHaller = new ArrayList();

            _oncekiHaller.Add(bitmap);
            _prevListLastIndex = _oncekiHaller.Count;
        }

        private void PrevImageClick(object sender, EventArgs e)
        {
            _prevListLastIndex = _prevListLastIndex - 1;
            if (_prevListLastIndex < 1)
                _prevListLastIndex = 1;
            _taranan.Image = (Bitmap)_oncekiHaller[_prevListLastIndex];
        }

        private void KapatClick(object sender, EventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
        }

        private void KontrolSeviyesiSelectedIndexChanged(object sender, EventArgs e)
        {
            CiltGetir();
        }

        private void _90Dondur_Click(object sender, EventArgs e)
        {
            _taranan.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
            _taranan.Image.Save(IndexKlasoru + _selectedItem.Remove(0, (Path.GetTempPath() + TempKlasoru).Length), Util.GetImageEncoder(), Util.GetEncoderParameters());
            _taranan.Image.Save(_selectedItem, Util.GetImageEncoder(), Util.GetEncoderParameters());
            _taranan.Image = Image.FromFile(_selectedItem);
        }
    }
}