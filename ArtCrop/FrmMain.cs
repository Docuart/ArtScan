using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Windows.Forms;
using ArtCrop.Properties;
using Library.Scan;

namespace ArtCrop
{
    public partial class FrmMain : Form
    {
        private const bool VeritabaniAktif = false;
        //private const string CroppedFilePath = "I:\\ANKARA\\ANKARA\\ASKI\\";
        private const string CroppedFilePath = "D:\\Cropped\\";

        public FrmMain()
        {
            InitializeComponent();
            _durum.Text = @"Kırp butonuna tıklayınız.";            
        }

        private void KirpClick(object sender, EventArgs e)
        {            
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            var dirs = Directory.GetDirectories(folderBrowserDialog.SelectedPath);
            Array.Sort(dirs);

            foreach (var dir in dirs)
            {
                //Kirp(dir);

                var ciltId = "";
                if (VeritabaniAktif)
                {
                    var dbo = new DbObject();
                    ciltId = dbo.GetSequence("SCAN").ToString();
                    dbo.Dispose();
                }

                SmartCrop(ciltId, dir);

                if (VeritabaniAktif)
                {
                    var dbo = new DbObject();
                    dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'BEK' WHERE CILT_ID = " + ciltId, null);
                    dbo.Dispose();
                }
            }
            MessageBox.Show("İşlem tamamlandı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SmartCrop(string ciltId, string selectedPath)
        {
            var cilt = selectedPath.Substring(selectedPath.LastIndexOf("\\"));
            if (!Directory.Exists(CroppedFilePath + cilt + "\\"))
                Directory.CreateDirectory(CroppedFilePath + cilt + "\\");
            else
            {
                if (MessageBox.Show("Bu klasör mevcut devam etmek istiyor musunuz?\nMevcut klasör içeriği silinerek devam edecektir.", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            if (VeritabaniAktif)
            {
                var dbo = new DbObject();
                var ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE CILT_PATH = '" + cilt.Replace("\\", "") + "'");
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    var parameters = new Parameter[5];
                    parameters[0] = new Parameter("ciltId", ciltId);
                    parameters[1] = new Parameter("kurumId", "1");
                    parameters[2] = new Parameter("ciltPath", cilt.Replace("\\", ""));
                    parameters[3] = new Parameter("ciltDurum", "UPL");
                    parameters[4] = new Parameter("tarayan", "---");

                    LoadInfo.Instance.AddInfo("Cilt veritabanına kaydediliyor.");
                    dbo.Execute("INSERT INTO SCAN_CILT (CILT_ID, KURUM_ID, CILT_PATH, CILT_DURUM, TARAYAN) VALUES (:ciltId, :kurumId, :ciltPath, :ciltDurum, :tarayan)", parameters);
                    LoadInfo.Instance.AddInfo("Cilt veritabanına kaydedildi. CILT_ID:" + ciltId);
                }
                else
                {
                    ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();
                }
                dbo.Dispose();
            }

            if (Directory.Exists(selectedPath + "\\Splitted"))
                selectedPath = selectedPath + "\\Splitted";            

            var files = Directory.GetFiles(selectedPath, "*.JPG", SearchOption.TopDirectoryOnly);
            Array.Sort(files);

            var t = new System.Threading.Thread(CreateForm);
            t.Start();
            LoadInfo.Instance.SetLength(files.Length);

            int rgbPointInt;
            if (!int.TryParse(rgbPoint.Text, out rgbPointInt))
            {
                rgbPointInt = 33;
            }

            var index = 0;
            const int tekSayfaYuksekligi = 1800;
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Resim kenarları kırpılıyor: " + file, i);
                var image = Library.Image.ImageProcessor.Crop(new Bitmap(file), Convert.ToInt32(rgbPointInt));                
                string dosyaAdi;                               

                // Yükseklik genişlikten fazla ise, dikey tek bir sayfa var demektir. Parçalara ayırmadan kaydedilir.
                if (image.Height > image.Width || (image.Height < tekSayfaYuksekligi && image.Width > image.Height))
                {
                    index = index + 1;
                    dosyaAdi = cilt + "#" + index.ToString().PadLeft(4, '0') + ".jpg";
                    ResimKaydet(image, CroppedFilePath + cilt + "\\" + dosyaAdi);
                    VeritabaninaKaydet(ciltId, dosyaAdi);
                    continue;
                }

                if (image.Width > image.Height)
                {
                    if (image.Height > tekSayfaYuksekligi)
                    {
                        // Sol sayfanın ayrılması
                        index = index + 1;
                        LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sol sayfa kırpılıyor: " + file);
                        var rectSol = new Rectangle(0, 0, Convert.ToInt32((image.Width / 2) + (image.Width * 0.05)), image.Height);
                        var imageSol = image.Clone(rectSol, image.PixelFormat);

                        LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sol sayfa iyileştiriliyor: " + file);
                        imageSol = Library.Image.ImageProcessor.Crop(imageSol, Convert.ToInt32(rgbPointInt));

                        dosyaAdi = cilt + "#" + index.ToString().PadLeft(4, '0') + ".jpg";
                        if (imageSol.Width > 1 && imageSol.Height > 1)
                            ResimKaydet(imageSol, CroppedFilePath + cilt + "\\" + dosyaAdi);
                        
                        imageSol.Dispose();
                        VeritabaninaKaydet(ciltId, dosyaAdi);

                        // Sağ sayfanın ayrılması
                        index = index + 1;
                        LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sağ sayfa kırpılıyor: " + file);
                        var rectSag = new Rectangle(Convert.ToInt32((image.Width / 2) - (image.Width * 0.05)), 0, image.Width - Convert.ToInt32((image.Width / 2) - (image.Width * 0.05)), image.Height);
                        var imageSag = image.Clone(rectSag, image.PixelFormat);

                        LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sağ sayfa iyileştiriliyor: " + file);
                        imageSag = Library.Image.ImageProcessor.Crop(imageSag, Convert.ToInt32(rgbPointInt));

                        dosyaAdi = cilt + "#" + index.ToString().PadLeft(4, '0') + ".jpg";
                        if (imageSag.Width > 1 && imageSag.Height > 1)
                            ResimKaydet(imageSag, CroppedFilePath + cilt + "\\" + dosyaAdi);

                        imageSag.Dispose();
                        VeritabaninaKaydet(ciltId, dosyaAdi);
                    }
                }
                #region Eski Kod - Ortadan İkiye Bölme
                /* ORTADAN İKİYE BÖLME
                if (_kirpmaTuru.SelectedItem.ToString().StartsWith("SOL") || _kirpmaTuru.SelectedItem.ToString().StartsWith("IKI"))
                {
                    index++;
                    LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sol sayfa kırpılıyor: " + file);
                    var rectSol = new Rectangle(0, 0, Convert.ToInt32((image.Width / 2) + (image.Width * 0.05)), image.Height);
                    var imageSol = image.Clone(rectSol, image.PixelFormat);

                    LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sol sayfa iyileştiriliyor: " + file);
                    imageSol = Library.Image.ImageProcessor.Crop(imageSol, Convert.ToInt32(rgbPointInt));

                    var dosyaAdi = cilt + "#" + (index + 1).ToString().PadLeft(4, '0') + ".jpg";
                    if (imageSol.Width > 1 && imageSol.Height > 1)
                        imageSol.Save(CroppedFilePath + cilt + "\\" + dosyaAdi);

                    imageSol.Dispose();

                    VeritabaninaKaydet(ciltId, dosyaAdi);
                }

                if (_kirpmaTuru.SelectedItem.ToString().StartsWith("SAG") || _kirpmaTuru.SelectedItem.ToString().StartsWith("IKI"))
                {
                    index++;
                    LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sağ sayfa kırpılıyor: " + file);
                    var rectSag = new Rectangle(Convert.ToInt32((image.Width / 2) - (image.Width * 0.05)), 0, image.Width - Convert.ToInt32((image.Width / 2) - (image.Width * 0.05)), image.Height);
                    var imageSag = image.Clone(rectSag, image.PixelFormat);

                    LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sağ sayfa iyileştiriliyor: " + file);
                    imageSag = Library.Image.ImageProcessor.Crop(imageSag, Convert.ToInt32(rgbPointInt));

                    var dosyaAdi = cilt + "#" + (index + 1).ToString().PadLeft(4, '0') + ".jpg";
                    if (imageSag.Width > 1 && imageSag.Height > 1)
                        imageSag.Save(CroppedFilePath + cilt + "\\" + dosyaAdi);

                    imageSag.Dispose();

                    VeritabaninaKaydet(ciltId, dosyaAdi);
                }
                */
                #endregion
                image.Dispose();
            }
        }

        private void Kirp(string selectedPath)
        {
            var cilt = selectedPath.Substring(selectedPath.LastIndexOf("\\"));
            if (!Directory.Exists(CroppedFilePath + cilt + "\\"))
                Directory.CreateDirectory(CroppedFilePath + cilt + "\\");
            else
            {
                if (MessageBox.Show("Bu klasör mevcut devam etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            var files = Directory.GetFiles(selectedPath, "*.JPG", SearchOption.TopDirectoryOnly);            
            var t = new System.Threading.Thread(CreateForm);
            t.Start();
            LoadInfo.Instance.SetLength(files.Length);

            var index = -1;
            for (var i = 0; i < files.Length; i++)
            {
                // ReSharper disable PossibleLossOfFraction
                var file = files[i];
                LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Resim kenarları kırpılıyor: " + file, i);
                var image = Library.Image.ImageProcessor.Crop(new Bitmap(file));

                if (_kirpmaTuru.SelectedItem.ToString().StartsWith("SOL") || _kirpmaTuru.SelectedItem.ToString().StartsWith("IKI"))
                {
                    index++;
                    LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sol sayfa kırpılıyor: " + file);
                    var rectSol = new Rectangle(0, 0, Convert.ToInt32((image.Width/2) + (image.Width*0.05)), image.Height);
                    var imageSol = image.Clone(rectSol, image.PixelFormat);
                    imageSol.Save(CroppedFilePath + cilt + "\\" + cilt + "#" + (index + 1).ToString().PadLeft(4, '0') + ".jpg");
                    imageSol.Dispose();
                }

                if (_kirpmaTuru.SelectedItem.ToString().StartsWith("SAG") || _kirpmaTuru.SelectedItem.ToString().StartsWith("IKI"))
                {
                    index++;
                    LoadInfo.Instance.AddInfo(i + "/" + files.Length + " Sağ sayfa kırpılıyor: " + file);
                    var rectSag = new Rectangle(Convert.ToInt32((image.Width/2) - (image.Width*0.05)), 0, image.Width - Convert.ToInt32((image.Width/2) - (image.Width*0.05)), image.Height);
                    var imageSag = image.Clone(rectSag, image.PixelFormat);
                    imageSag.Save(CroppedFilePath + cilt + "\\" + cilt + "#" + (index + 1).ToString().PadLeft(4, '0') + ".jpg");
                    imageSag.Dispose();
                }
                image.Dispose();
                // ReSharper restore PossibleLossOfFraction
            }

            Invoke(new Action(() => _f.Close()));
            new SoundPlayer(Resources.notify).Play();
            
        }

        private void ResimKaydet(Bitmap image, string path)
        {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 40);
            image.Save(path, Util.GetImageEncoder("JPEG"), encoderParameters);
        }

        private void VeritabaninaKaydet(string ciltId, string dosyaPath)
        {
            if (!VeritabaniAktif)
                return;
            dosyaPath = dosyaPath.Replace("\\", "");

            var parameters = new Parameter[2];
            parameters[0] = new Parameter("ciltId", ciltId);
            parameters[1] = new Parameter("dosyaPath", dosyaPath);

            var dbo = new DbObject();
            dbo.Execute("INSERT INTO SCAN_DOSYA (DOSYA_ID, CILT_ID, DOSYA_PATH, DOSYA_DURUM) VALUES (SCAN_SEQ.NEXTVAL, :ciltId, :dosyaPath, 'B')", parameters);
            dbo.Dispose();

            LoadInfo.Instance.AddInfo("Dosya veritabanına kaydedildi. " + dosyaPath);
        }

        private LoadingForm _f;
        private void CreateForm()
        {
            _f = new LoadingForm();
            _f.ShowDialog();
        }
    }
}