using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Canon.Eos.Framework;
using Canon.Eos.Framework.Eventing;
using Library.Image;
using Library.Scan;

namespace CopyArt
{
    public partial class FrmMain : Form
    {
        private readonly FrameworkManager _manager;
        private EosCamera _camera;
        private const string ImageSavePath = "D:\\Scanned\\";
        private const string ImageSaveAfterPath = "D:\\GÖNDERİLDİ\\";
        private const string ImageSaveErrorPath = "D:\\HATA\\";
        private const bool SaveToDatabase = true;
        private readonly NameValueCollection _programParameters = new NameValueCollection();
        private const int TekSayfaYuksekligi = 1800;

        public FrmMain(FrameworkManager manager)
        {
            InitializeComponent();
            _manager = manager;
            _manager.CameraAdded += OnCameraAdded;
        }

        private string GetImageHost(bool create = true)
        {
            var dir = ImageSavePath + _adi.Text + "\\";
            if (create && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        private string GetImageSplittedHost(bool create = true)
        {            
            var dir = GetImageHost() + "Splitted\\";
            if (create && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return GetImageHost(create) + "Splitted\\";
        }

        private void FrmMainLoad(object sender, EventArgs e)
        {
            LoadCamera();
            TimerBatteryTick(sender, e);
            SetNumericEnabledStatus();

            if (_sayfaNo.Text == "")
                _sayfaNo.Text = @"0";

            _log.Width = Width - _log.Left - 10;

            const string parameterFileName = "ProgramParameters.txt";
            if (!File.Exists(Application.StartupPath + "\\" + parameterFileName))
            {
                using (File.CreateText(Application.StartupPath + "\\" + parameterFileName)) { }
            }

            var programParameters = File.ReadAllLines(Application.StartupPath + "\\" + parameterFileName);
            foreach (var programParameter in programParameters)
            {
                if (programParameter.Contains("="))
                    _programParameters.Add(programParameter.Split('=')[0], programParameter.Split('=')[1]);
            }
            _adi.Text = _programParameters["ISIN_ADI"];
        }

        private void TearDown()
        {
            if (_camera != null && _camera.IsInLiveViewMode)
                _camera.StopLiveView();

            _manager.ReleaseFramework();
        }

        private static void ErrorSound()
        {            
            Application.DoEvents();
            /*
            Console.Beep(1000, 500);
            Console.Beep(1000, 500);
            Console.Beep(1000, 500);
            Console.Beep(1000, 500);            
            */
        }

        private void InfoSound()
        {
            if (_camera.IsInLiveViewMode)
                return;
            Application.DoEvents();
            /*
            Console.Beep(4000, 100);
            Console.Beep(4000, 100);
            Console.Beep(4000, 100);
            */
        }

        private string _applicationId;
        private void Log(string message)
        {
            try
            {
                if (_applicationId == null)
                    _applicationId = Guid.NewGuid().ToString();

                _log.Items.Add(message);
                _log.SelectedIndex = _log.Items.Count - 1;

                var s = _log.Items.Cast<object>().Aggregate("", (current, item) => current + (DateTime.Now + "." + DateTime.Now.Millisecond + " - " + item.ToString() + "\r\n"));
                File.WriteAllText(GetImageSplittedHost(false) + _applicationId.Substring(0, 7) + ".zlog", s);
            }
            catch { }
        }

        private void Log(Exception ex)
        {
            ErrorSound();
            Log("Exception: " + ex.Message + "\r\n" + ex.StackTrace);
        }

        private enum Mod { Timer, Manual, Calibration }
        private Mod GetMod()
        {
            if (_modTimer.Checked)
                return Mod.Timer;
            if (_modKalibrasyon.Checked)
                return Mod.Calibration;
            return Mod.Manual;
        }

        /*
        private ImageProcessor.SplitType GetSplitType()
        {
            if (_splitTypeNone.Checked)
                return ImageProcessor.SplitType.None;
            if (_splitTypeCift.Checked)
                return ImageProcessor.SplitType.Cift;
            if (_splitTypeSag.Checked)
                return ImageProcessor.SplitType.Sag;
            if (_splitTypeSol.Checked)
                return ImageProcessor.SplitType.Sol;
            return ImageProcessor.SplitType.None;
        }
        */
        private void SetNumericEnabledStatus()
        {
            _timer.Enabled = GetMod() == Mod.Timer;
            if (_camera == null)
                return;

            if (GetMod() == Mod.Calibration)
            {
                //SafeCall(() => _camera.StartLiveView(), ex => { });
                _adi.Text = "";
                _adi.Enabled = false;
            }
            else
            {
                _adi.Enabled = true;
                /*
                SafeCall(() =>
                {
                    if (_camera.IsInLiveViewMode) 
                        _camera.StopLiveView();
                }, ex => { });
                 */
            }
        }

        private void ModUserCheckedChanged(object sender, EventArgs e)
        {
            SetNumericEnabledStatus();
        }

        private void ModTimerCheckedChanged(object sender, EventArgs e)
        {
            SetNumericEnabledStatus();
        }

        private void OnCameraAdded(object sender, EventArgs e)
        {
            LoadCamera();
            Log("Kamera eklendi.");
        }

        private void LoadCamera()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LoadCamera));
            }
            else
            {
                Log("Kamera yükleniyor.");
                _baslat.Enabled = true;
                foreach (var cam in _manager.GetCameras())
                {
                    cam.Shutdown += OnCameraShutdown;
                    cam.PictureTaken += OnPictureUpdate;
                    cam.LiveViewUpdate += OnPictureUpdate;
                    _camera = cam;

                    Log("Kamera Bulundu. " + cam);
                }
                if (_camera == null)
                {
                    Log("Kamera bulunamadı.");
                    _baslat.Enabled = false;
                    ErrorSound();
                }
            }
        }

        private void OnCameraShutdown(object sender, EventArgs e)
        {
            Log("Kamera kapandı.");
        }

        private void OnPictureUpdate(object sender, EosImageEventArgs e)
        {
            Log("OnPictureUpdate Event.");
            UpdatePicture(e.GetImage());
            if (!_camera.IsInHostLiveViewMode)
                SavePicture(e.GetStream());
        }

        private string GetFileName()
        {
            return _adi.Text + "#" + _sayfaNo.Text.PadLeft(4, '0') + ".jpg";
        }

        private string GetNewFileName()
        {
            var sayfaNo = Convert.ToInt32(_sayfaNo.Text);
            return _adi.Text + "#" + (sayfaNo + 1).ToString(CultureInfo.InvariantCulture).PadLeft(4, '0') + ".jpg";
        }

        private void SavePicture(Stream s)
        {
            Log("Resim kaydediliyor.");

            Log("Resim dosyası adı: " + GetFileName());
            using (Stream file = File.OpenWrite(GetImageSplittedHost() + GetFileName()))
            {                
                CopyStream(s, file);
            }
            InfoSound();
        }

        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        public static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }


        private void UpdatePicture(Image image)
        {
            if (!_camera.IsInLiveViewMode)
                Log("Updating picture.");

            if (InvokeRequired)
                Invoke(new Action(() => UpdatePicture(image)));
            else
            {
                _picture.Image = image;
                if (GetMod() != Mod.Calibration)
                    ProcessImages();
            }
        }

        private void SafeCall(Action action, Action<Exception> exceptionHandler)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Log(ex);
                if (InvokeRequired) Invoke(exceptionHandler, ex);
                else exceptionHandler(ex);
            }
        }

        private void BaslatClick(object sender, EventArgs e)
        {
            Log("BaslatClick Event.");
            if (GetMod() != Mod.Calibration && _adi.Text.Trim() == "")
            {
                MessageBox.Show(@"İşin adı girilmelidir.", @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Log("Setting picture host.");
            _camera.SavePicturesToHost(GetImageHost());

            Log("Setting timer status.");
            if (GetMod() == Mod.Timer)
                _time.Enabled = !_time.Enabled;
            else
                TakePicture();
            _baslat.Text = _time.Enabled ? @"Durdur" : @"Başlat";
        }

        private void TakePicture()
        {
            SafeCall(() =>
            {
                try
                {
                    Log("Fotoğraf çekiliyor.");
                    Log("Kamera kilit durumu: " + _camera.IsLocked);
                    if (_camera.IsInHostLiveViewMode)
                        _camera.StopLiveView();

                    if (GetMod() != Mod.Calibration && File.Exists(GetImageSplittedHost() + GetNewFileName()))
                    {
                        ErrorSound();
                        MessageBox.Show(GetNewFileName() + " dosyası sistemde mevcut.\nKayıt işlemi gerçekleştirilmedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _camera.TakePicture();

                    if (GetMod() != Mod.Calibration)
                    {
                        _sayfaNo.Text = Convert.ToString(Convert.ToInt32(_sayfaNo.Text) + 1);
                        SayfaSayisiYaz();
                    }
                    Log("Fotoğraf çekildi.");
                    Log("Batarya durumu: %" + _camera.BatteryLevel);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    if (GetMod() != Mod.Calibration)
                        _sayfaNo.Text = Convert.ToString(Convert.ToInt32(_sayfaNo.Text) - 1);
                }
                //StartLiveView();
            }, ex => Log(ex.ToString()));
        }

        private int _seconds = -1;
        private void TimeTick(object sender, EventArgs e)
        {
            _seconds = _seconds + 1;
            Log((Convert.ToInt32(_timer.Text) - _seconds) + " sn sonra çekilecek.");
            if (_seconds.ToString(CultureInfo.InvariantCulture) == _timer.Text)
            {
                _seconds = -1;
                TakePicture();
                if (GetMod() == Mod.Manual)
                    _time.Enabled = false;
            }
        }

        private void SayfaSayisiYaz()
        {
            var file = Path.GetTempPath() + "copyart.txt";
            try
            {                
                if (!File.Exists(file))
                {
                    using (var sw = File.CreateText(file))
                    {
                        sw.Write(0);
                    }
                }
                else
                {
                    var lines = File.ReadAllLines(file);
                    var sayi = Convert.ToInt64(lines[0]) + 1;
                    File.WriteAllText(file, sayi.ToString(CultureInfo.InvariantCulture));
                }
            } 
            catch(Exception)
            {
                
            }
        }

        /*
        private void StartLiveView()
        {
            _timerLiveView.Enabled = true;
        }
        */

        private int _liveViewTimer;
        private void TimerLiveViewTick(object sender, EventArgs e)
        {
            _liveViewTimer = _liveViewTimer + 1;
            if (_liveViewTimer == 3)
            {
                _liveViewTimer = 0;
                _timerLiveView.Enabled = false;
                _camera.StartLiveView();
                Log("Canlı görüntüleme modu etkinleştirdi.");
            }
        }

        private void TimerBatteryTick(object sender, EventArgs e)
        {
            if (_camera != null)
            {
                string batteryLevel;
                if (_camera.BatteryLevel > 100)
                {
                    batteryLevel = "AC Güçte";
                    _timerBattery.Enabled = false;
                }
                else
                {
                    batteryLevel = "%" + _camera.BatteryLevel;
                    _timerBattery.Enabled = true;
                }

                _battery.Value = Convert.ToInt32(_camera.BatteryLevel > 100 ? 100 : _camera.BatteryLevel);
                _batteryDescription.Text = batteryLevel;
                _batteryDescription.ForeColor = Color.Black;
            }
            else
            {
                _batteryDescription.Text = @"Kamera Bulunamadı.";
                _batteryDescription.ForeColor = Color.Red;
            }
        }

        // ReSharper disable PossibleNullReferenceException
        private readonly NameValueCollection _images = new NameValueCollection();
        private void ProcessImages()
        {
            // Kilitlenmeli mi?
            //lock (WindowTarget)
            //{            
            /* TODO: İncelenecek
            Log("Processing Images.");
            var folder = GetImageHost();
            var files = Directory.GetFiles(folder, "*.JPG");
            foreach (var file in files)
            {
                try
                {
                    var left = string.Format("{0}{1}_1.JPG", GetImageSplittedHost(), Path.GetFileName(file).Replace(".JPG", ""));
                    var right = string.Format("{0}{1}_2.JPG", GetImageSplittedHost(), Path.GetFileName(file).Replace(".JPG", ""));
                    if (File.Exists(left) || File.Exists(right))
                        continue;

                    Log("Found new file: " + file);
                    _images.Add(file, file);
                    var bitmaps = ImageProcessor.Split(ImageProcessor.Crop(new Bitmap(file)), GetSplitType());
                    Log("Crop success. Saving Files...");
                    if (bitmaps[0] != null)
                        bitmaps[0].Save(left);
                    if (bitmaps[1] != null)
                        bitmaps[1].Save(right);
                    Log("All files saved.");
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            }
            */
            //}
        }
        // ReSharper restore PossibleNullReferenceException

        private void AdiLeave(object sender, EventArgs e)
        {
            if (_adi.Text == "")
                return;

            _adi.Text = _adi.Text.ToUpperInvariant();
            CreateUserInfoFile();

            if (_adi.Text != "" && Directory.Exists(GetImageHost(false)))
            {
                var files = Directory.GetFiles(GetImageSplittedHost(false), "*.jpg");
                if (files.Length == 0)
                {
                    _sayfaNo.Text = @"0";
                    return;
                }

                var dr = MessageBox.Show(_adi.Text + " işi sistemde kayıtlıdır.\nDevam ettiğinizde bu cilde eklenecektir!", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    return;

                Array.Sort(files);
                var fileName = files[files.Length - 1].Substring(files[files.Length - 1].LastIndexOf("\\") + 1);
                fileName = (Convert.ToInt32(fileName.Replace(_adi.Text + "#", "").Replace(".jpg", ""))).ToString();
                _sayfaNo.Text = fileName;

                _picture.Image = Image.FromFile(GetImageSplittedHost() + GetFileName());
            }
        }

        private void CreateUserInfoFile()
        {
            if (!File.Exists(GetImageSplittedHost() + "user.info"))
                File.WriteAllText(GetImageSplittedHost() + "user.info", Util.GetName());
        }

        private void PictureClick(object sender, EventArgs e)
        {
            if (_sayfaNo.Text == "0")
                return;

            if (MessageBox.Show("Bu resim silinecektir.\nOnaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _picture.Image.Dispose();
                File.Delete(GetImageSplittedHost(false) + GetFileName());
                _sayfaNo.Text = Convert.ToString(Convert.ToInt32(_sayfaNo.Text) - 1);

                if (_sayfaNo.Text != "0")
                    _picture.Image = Image.FromFile(GetImageSplittedHost() + GetFileName());
            }
        }

        private void _isSonu_Click(object sender, EventArgs e)
        {
            if (Directory.GetDirectories(ImageSaveErrorPath).Length != 0)
            {
                MessageBox.Show("Hata klasörü içerisinde bekleyen cilt var. İşlemi tamamlanmadan gün sonu veremezsiniz.");
                return;
            }
            if (!String.IsNullOrEmpty(_adi.Text) && MessageBox.Show("Sadece bu işi mi göndermek istiyorsunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IsSonu();
                MessageBox.Show("İşlem tamamlandı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                
            else if (MessageBox.Show("Tüm işleri mi göndermek istiyorsunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var mevcutCiltler = "";
                var directories = Directory.GetDirectories(ImageSavePath);
                foreach (var directory in directories)
                {
                    var parameters = new Parameter[1];
                    parameters[0] = new Parameter("adi", directory);
                    
                    var dbo = new DbObject();
                    var ds = dbo.DataSet("SELECT * FROM SCAN_CILT C WHERE C.CILT_PATH = :adi", parameters);
                    dbo.Dispose();

                    if (ds != null)
                        mevcutCiltler += directory + "\n";
                }

                if (mevcutCiltler != "")
                {
                    var dr = MessageBox.Show("Aşağıdaki ciltler veritabanında kayıtlıdır:\n" + mevcutCiltler + "\n\nİşleme devam etmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                        return;

                    GunSonu();
                    MessageBox.Show("İşlem tamamlandı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }   
                else
                {
                    GunSonu();
                    MessageBox.Show("İşlem tamamlandı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }                
        }

        private void GunSonu()
        {
            var directories = Directory.GetDirectories(ImageSavePath);

            /*
            var mesaj = "";
            foreach (var directory in directories)
            {
                var folders = directory.Split('\\');
                var ciltAdi = folders[folders.Length - 1];
                if (ciltAdi == "Splitted")
                    ciltAdi = folders[folders.Length - 2];

                var parameters = new Parameter[1];
                parameters[0] = new Parameter("ciltAdi", "ciltAdi");
                                
                var dbo = new DbObject();
                var ds = dbo.DataSet("SELECT COUNT(1) AS SAYI FROM SCAN_CILT C, SCAN_DOSYA D WHERE C.CILT_ID = D.CILT_ID AND C.CILT_PATH = :ciltAdi", parameters);
                dbo.Dispose();

                if (ds != null && ds.Tables[0].Rows[0][0].ToString() != "0")
                    mesaj += ciltAdi + " veritabanında kayıtlıdır. Dosya Sayısı: " + ds.Tables[0].Rows[0][0] + Environment.NewLine;                
            }            

            if (!string.IsNullOrEmpty(mesaj))
            {
                MessageBox.Show(mesaj + Environment.NewLine + Environment.NewLine + " Klasörleri kontrol edip tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */
            // TODO: Otomatik eğim gidermeyi kullanıcıya sorsun mu?
            //var drEgim = MessageBox.Show("Otomatik eğim giderme yapılsın mı?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            var drEgim = DialogResult.No;            
            var drKirp = MessageBox.Show("Kırpma yapılsın mı?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            foreach (var directory in directories)
            {
                try
                {
                    if (!Directory.Exists(directory + "\\Splitted"))
                        continue;
                    CiltSonu(directory + "\\Splitted", drEgim == DialogResult.Yes, drKirp == DialogResult.Yes);
                    if (!Directory.Exists(ImageSaveAfterPath))
                        Directory.CreateDirectory(ImageSaveAfterPath);
                    Directory.Move(directory, directory.Replace(ImageSavePath, ImageSaveAfterPath));
                } 
                catch
                {
                    if (!Directory.Exists(ImageSaveErrorPath))
                        Directory.CreateDirectory(ImageSaveErrorPath);
                    Directory.Move(directory, directory.Replace(ImageSavePath, ImageSaveErrorPath));
                }
            }
        }

        private void IsSonu()
        {
            if (!Directory.Exists(GetImageSplittedHost()))
            {
                MessageBox.Show("Kopyalama klasörü bulunamadı. " + GetImageSplittedHost());
                return;
            }
            var files = Directory.GetFiles(GetImageSplittedHost(), "*.JPG");
            if (files.Length == 0)
            {
                MessageBox.Show("Klasörde *.JPG uygun dosya bulunamadı.");
                return;
            }
            if (MessageBox.Show(files.Length + " adet dosya sisteme dahil edilecektir. Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var egimGider = MessageBox.Show("Otomatik eğim giderme yapılsın mı?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            var kirp = MessageBox.Show("Kırpma yapılsın mı?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            CiltSonu(GetImageSplittedHost(), egimGider == DialogResult.Yes, kirp == DialogResult.Yes);

            MessageBox.Show("İşlem tamamlandı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _adi.Text = "";
            _adi.Focus();
        }

        private bool _gonderimDevamEdiyor = false;
        private void CiltSonu(string ciltPath, bool egimGider = true, bool kirp = true)
        {
            _gonderimDevamEdiyor = true;
            _manager.ReleaseFramework();

            var folders = ciltPath.Split('\\');
            var ciltAdi = folders[folders.Length - 1];
            if (ciltAdi == "Splitted")
                ciltAdi = folders[folders.Length - 2];            

            var ciltId = "";
            if (SaveToDatabase)
            {
                var parameters = new Parameter[1];
                parameters[0] = new Parameter("adi", ciltAdi);

                Log("Cilt işleniyor. " + ciltAdi);                
                var dbo = new DbObject();
                var ds = dbo.DataSet("SELECT * FROM SCAN_CILT C WHERE C.CILT_PATH = :adi", parameters);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    ciltId = dbo.GetSequence("SCAN").ToString(CultureInfo.InvariantCulture);
                    var ciltParams = new Parameter[3];
                    ciltParams[0] = new Parameter("ciltId", ciltId);
                    ciltParams[1] = new Parameter("ciltPath", ciltAdi);
                    ciltParams[2] = new Parameter("tarayan", Util.GetName());
                    dbo.Execute("INSERT INTO SCAN_CILT (CILT_ID, KURUM_ID, CILT_PATH, CILT_DURUM, TARAYAN, TARAMA_TARIHI) VALUES (:ciltId, 1, :ciltPath, 'UPL', :tarayan, sysdate)",ciltParams);
                }
                else
                {
                    ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();
                }
                Log("Cilt işlendi. CiltId:" + ciltId);

                dbo.Execute("DELETE FROM SCAN_DOSYA WHERE CILT_ID = " + ciltId, null);
                dbo.Dispose();
            }

            var files = Directory.GetFiles(ciltPath, "*.JPG");
            foreach (var file in files)
            {
                Application.DoEvents();
                Log("Resim işleniyor: " + file);

                using (var image = new Bitmap(file))
                {
                    var baseBitmap = image;
                    if (egimGider)
                    {
                        Log("Resim eğikliği gideriliyor: " + file);
                        var sk = new gmseDeskew.gmseDeskew(image);
                        var skewAngle = sk.GetSkewAngle();
                        var tmp = new Bitmap(image.Width, image.Height, image.PixelFormat);
                        tmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                        var g = Graphics.FromImage(tmp);
                        try
                        {
                            Log("Eğim: " + skewAngle);
                            g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);
                            if (Math.Abs(skewAngle) > 0.5 && Math.Abs(skewAngle) < 5)
                            {
                                Log("Eğim gideriliyor: " + skewAngle);
                                g.TranslateTransform((float) image.Width/2, (float) image.Height/2);
                                g.RotateTransform(-(float) skewAngle);
                                g.TranslateTransform(-(float) image.Width/2, -(float) image.Height/2);
                            }
                            g.DrawImage(image, 0, 0);
                        }
                        finally
                        {
                            g.Dispose();
                        }
                        baseBitmap = tmp;
                    } 
                    else
                    {
                        baseBitmap = image;
                    }
                    Bitmap bitmap;
                    if (kirp)
                    {
                        Log("Resim kırpılıyor: " + file);
                        bitmap = ImageProcessor.Crop(baseBitmap, 33);
                        try
                        {
                            if (bitmap.Width > bitmap.Height)
                            {
                                if (bitmap.Height > TekSayfaYuksekligi)
                                {

                                    Log(" Sağ sayfa kırpılıyor: " + file);
                                    var rectSag = new Rectangle(Convert.ToInt32((bitmap.Width/2) - (bitmap.Width*0.05)), 0, bitmap.Width - Convert.ToInt32((bitmap.Width/2) - (bitmap.Width*0.05)), bitmap.Height);
                                    var imageSag = bitmap.Clone(rectSag, bitmap.PixelFormat);

                                    Log(" Sağ sayfa iyileştiriliyor: " + file);
                                    imageSag = ImageProcessor.Crop(imageSag);
                                    bitmap = imageSag;
                                }
                            }
                        } 
                        catch
                        {
                            bitmap = baseBitmap;
                        }
                    } 
                    else
                    {
                        bitmap = baseBitmap;
                    }                    
                    var indeksPath = "I:\\" + ciltAdi;
                    Directory.CreateDirectory(indeksPath);
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 40);
                    if (!File.Exists(indeksPath + "\\" + Path.GetFileName(file)))
                        bitmap.Save(indeksPath + "\\" + Path.GetFileName(file), Library.Scan.Util.GetImageEncoder("JPEG"), encoderParameters);
                    bitmap.Dispose();
                    baseBitmap.Dispose();
                    image.Dispose();
                }
                if (SaveToDatabase)
                {
                    var dosyaParams = new Parameter[2];
                    dosyaParams[0] = new Parameter("ciltId", ciltId);
                    dosyaParams[1] = new Parameter("dosyaPath", Path.GetFileName(file));
                    var dbox = new DbObject();
                    dbox.Execute("INSERT INTO SCAN_DOSYA (DOSYA_ID, CILT_ID, DOSYA_PATH, DOSYA_DURUM) VALUES (SCAN_SEQ.NEXTVAL, :ciltId, :dosyaPath, 'B')",dosyaParams);
                    dbox.Dispose();
                }
                Log("Resim işlendi.");
            }
            if (SaveToDatabase)
            {
                var paramCilt = new Parameter[1];
                paramCilt[0] = new Parameter("ciltId", ciltId);
                var dbox = new DbObject();
                dbox.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'BEK' WHERE CILT_ID = :ciltId", paramCilt);
                dbox.Dispose();
            }
            Log("Cilt tamamlandı. " + ciltAdi);
            _gonderimDevamEdiyor = false;
        }

        private void CikisClick(object sender, EventArgs e)
        {
            if (_gonderimDevamEdiyor)
            {
                MessageBox.Show("İş sonu devam ederken program kapatılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Programdan çıkmak istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.ExitThread();
                Application.Exit();                
            }                
        }

        // TEST AMAÇLI
        private void StartTestProcedure()
        {
            var f = new OpenFileDialog();
            f.ShowDialog();
            if (string.IsNullOrEmpty(f.FileName))
                return;
            
            var file = f.FileName;
            Log("Resim işleniyor: " + file);

            var stopwatch = Stopwatch.StartNew();
            new gmseDeskew.gmseDeskew(new Bitmap(file)).GetSkewAngle();
            stopwatch.Stop();
            MessageBox.Show(stopwatch.Elapsed + "");

            return;

            using (var image = new Bitmap(file))
            {
                var boyut = (int)Math.Sqrt(image.Width * image.Width + image.Height * image.Height) + 10;
                var tmpx = new Bitmap(boyut, boyut, image.PixelFormat);
                using (var gr = Graphics.FromImage(tmpx))
                {
                    gr.DrawImage(image, (boyut - image.Width) / 2, (boyut - image.Height) / 2, image.Width, image.Height);
                }                
                Log("Resim eğikliği gideriliyor: " + file);
                var sk = new gmseDeskew.gmseDeskew(image);
                var skewAngle = sk.GetSkewAngle();
                var tmp = new Bitmap(image.Width, image.Height, image.PixelFormat);
                tmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                var g = Graphics.FromImage(tmp);
                try
                {
                    Log("Eğim: " + skewAngle);
                    g.FillRectangle(Brushes.Black, 0, 0, image.Width, image.Height);
                    if (Math.Abs(skewAngle) > 0.5 && Math.Abs(skewAngle) < 5)
                    {
                        g.TranslateTransform((float) image.Width/2, (float) image.Height/2);
                        g.RotateTransform(-(float) skewAngle);
                        g.TranslateTransform(-(float) image.Width/2, -(float) image.Height/2);
                    }
                    g.DrawImage(image, 0, 0);
                }
                finally
                {
                    g.Dispose();
                }

                Log("Resim kırpılıyor: " + file);
                var bitmap = ImageProcessor.Crop(tmp, 33);

                var fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                if (string.IsNullOrEmpty(fbd.SelectedPath))
                    return;

                var indeksPath = fbd.SelectedPath;
                Directory.CreateDirectory(indeksPath);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 40);
                bitmap.Save(indeksPath + "\\" + Path.GetFileName(file), Library.Scan.Util.GetImageEncoder("JPEG"), encoderParameters);
                bitmap.Dispose();
                image.Dispose();
            }
            Log("Resim işlendi.");
        }

        private void AdiKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.H)
                StartTestProcedure();
        }

        private void HataliCiltGetir()
        {
            String HataTuru = "Yanlış-Bulanık";

            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT d.CILT_ID, d.DOSYA_ID, c.CILT_PATH, (SELECT COUNT(p.DOSYA_ID) FROM SCAN_DOSYA WHERE p.CILT_ID = d.CILT_ID) AS SAYI FROM SCAN_CILT c, SCAN_DOSYA d WHERE d.ACIKLAMA LIKE UPPER('YANLIŞ-BULANIK') AND d.CILT_ID = c.CILT_ID");
            dbo.Dispose();

            if (ds != null)
            {
                _adi.Text = ds.Tables[0].Rows[2].ToString();
                TakePicture();
                dbo = new DbObject();
                // ds.Tables[0].Rows[0];
            }
        }

    }
}