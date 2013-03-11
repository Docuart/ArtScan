using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Library.Image;
using Library.Scan;

namespace ArtCrop2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }        

        private readonly Otsu _ot = new Otsu();        

        private void MainFormLoad(object sender, EventArgs e)
        {
            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_DURUM = 'KRP' AND NVL(LOGS, '-') NOT LIKE 'AUTOCROP%' AND INDEKS IS NULL ORDER BY CILT_ID");
            dbo.Dispose();

            if (ds == null)
            {                
                Application.Exit();
                return;
            }

            var ciltId = ds.Tables[0].Rows[0]["CILT_ID"].ToString();
            var ciltPath = ds.Tables[0].Rows[0]["CILT_PATH"].ToString();

            dbo = new DbObject();
            dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'KRX' WHERE CILT_ID = " + ciltId, null);
            dbo.Dispose();

            var dir = @"D:\INDEKS\" + ciltPath;
            if (!Directory.Exists(dir.Replace("INDEKS", "INDEKS-TEMP")))
                Directory.CreateDirectory(dir.Replace("INDEKS", "INDEKS-TEMP"));

            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                try
                {                    
                    LogYaz(ciltPath, "Dosya işleniyor. " + file);
                    if (File.Exists(file.Replace("INDEKS", "INDEKS-TEMP")))
                    {
                        LogYaz(ciltPath, "Dosya mevcut olduğundan diğer dosyaya geçiliyor. ");
                        continue;
                    }

                    label1.Text = file;
                    Application.DoEvents();

                    using (var bitmap = new Bitmap(file))
                    {
                        LogYaz(ciltPath, "Resim okundu. Eğim gideriliyor. ");
                        var temp = (Bitmap)bitmap.Clone();
                        var sk = new gmseDeskew.gmseDeskew(temp);
                        var tmp = new Bitmap(temp.Width, temp.Height, temp.PixelFormat);
                        tmp.SetResolution(temp.HorizontalResolution, temp.VerticalResolution);
                        using (var g = Graphics.FromImage(tmp))
                        {
                            var angle = sk.GetSkewAngle();
                            LogYaz(ciltPath, "Eğim hesaplandı. Değer:" + angle + " Eğim gideriliyor.");

                            g.FillRectangle(Brushes.Black, 0, 0, temp.Width, temp.Height);
                            g.TranslateTransform((float)temp.Width / 2, (float)temp.Height / 2);
                            g.RotateTransform(-(float)angle);
                            g.TranslateTransform(-(float)temp.Width / 2, -(float)temp.Height / 2);
                            g.DrawImage(temp, 0, 0);
                        }
                        LogYaz(ciltPath, "Eğim giderildi. Dörtgenler tespit ediliyor.");

                        var thresTemp = (Bitmap)temp.Clone();
                        _ot.Convert2GrayScaleFast(thresTemp);
                        int otsuThreshold = _ot.getOtsuThreshold(thresTemp);
                        _ot.threshold(thresTemp, otsuThreshold);

                        LogYaz(ciltPath, "Threshold uygulandı. Değer:" + otsuThreshold + " Eğim hesaplanıyor.");

                        var pointArray = ImageProcessor.DetectPaper(thresTemp);
                        if (pointArray == null || pointArray.Count == 0)
                        {
                            temp.Save(file.Replace("INDEKS", "INDEKS-TEMP"));
                            temp.Dispose();
                            bitmap.Dispose();
                            LogYaz(ciltPath, "Dörtgenler tespit edilemedi. Yeni resme geçiliyor.");
                            continue;
                        }

                        var rectangle = new Rectangle();
                        if (pointArray.Count == 2)
                        {
                            LogYaz(ciltPath, "2 adet dörtgen tespit edildi. Sağdaki alınıyor.");
                            foreach (var pointValue in pointArray)
                            {
                                var points = (List<Point>) pointValue;
                                var top = points.Select(point => point.Y).Concat(new[] {9999999}).Min();
                                var left = points.Select(point => point.X).Concat(new[] {9999999}).Min();
                                var bottom = points.Select(point => point.Y).Concat(new[] {0}).Max();
                                var right = points.Select(point => point.X).Concat(new[] {0}).Max();

                                var rect = new Rectangle(left + 25, top + 25, right - left - 50, bottom - top - 50);
                                if (rect.X > rectangle.X)
                                    rectangle = rect;
                            }
                        }
                        if (pointArray.Count == 1)
                        {
                            LogYaz(ciltPath, "1 adet dörtgen tespit edildi.");
                            var points = (List<Point>) pointArray[0];
                            var top = points.Select(point => point.Y).Concat(new[] {9999999}).Min();
                            var left = points.Select(point => point.X).Concat(new[] {9999999}).Min();
                            var bottom = points.Select(point => point.Y).Concat(new[] {0}).Max();
                            var right = points.Select(point => point.X).Concat(new[] {0}).Max();

                            rectangle = new Rectangle(left + 25, top + 25, right - left - 50, bottom - top - 50);
                        }
                        if (pointArray.Count > 2)
                        {
                            LogYaz(ciltPath, "2 den fazla dörtgen tespit edildi. Tüm sayfa alınıyor.");
                            rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                        }

                        LogYaz(ciltPath, "Tespit edilen dörtgen değerleri: Top:" + rectangle.Top + " Left:" + rectangle.Left + " Width:" + rectangle.Width + " Height:" + rectangle.Height);

                        if (rectangle.Width > rectangle.Height)
                        {
                            rectangle = new Rectangle(10, 10, bitmap.Width - 20, bitmap.Height - 20);
                            LogYaz(ciltPath, "Tespit edilen dörtgenin eni boyundan büyük olduğundan tüm sayfa alınıyor: Top:" + rectangle.Top + " Left:" + rectangle.Left + " Width:" + rectangle.Width + " Height:" + rectangle.Height);
                        }

                        LogYaz(ciltPath, "Sonuç kaydediliyor.");
                        var sonuc = bitmap.Clone(rectangle, bitmap.PixelFormat);
                        sonuc.Save(file.Replace("INDEKS", "INDEKS-TEMP"));
                        sonuc.Dispose();

                        temp.Dispose();
                        bitmap.Dispose();

                        LogYaz(ciltPath, "Nesneler sonlandırıldı. Yeni sayfaya geçiliyor.");
                    }
                } 
                catch (Exception ex)
                {
                    LogYaz(ciltPath, "Hata oluştu. Orijinal dosya aktarılıyor. " + ex.Message + "-" + ex.StackTrace);

                    dbo = new DbObject();
                    dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'KRP', LOGS = 'AUTOCROP-HATA' WHERE CILT_ID = " + ciltId, null);
                    dbo.Dispose();

                    File.Copy(file, file.Replace("INDEKS", "INDEKS-TEMP"));
                }
            }

            LogYaz(ciltPath, "Klasörler taşınıyor. " + dir + " --> " + dir.Replace(Path.GetFileName(dir), "--" + Path.GetFileName(dir)));
            Directory.Move(dir, dir.Replace(Path.GetFileName(dir), "--" + Path.GetFileName(dir)));

            LogYaz(ciltPath, "Klasörler taşınıyor. " + dir.Replace("INDEKS", "INDEKS-TEMP") + " --> " + dir);
            Directory.Move(dir.Replace("INDEKS", "INDEKS-TEMP"), dir);

            LogYaz(ciltPath, "Cilt durum güncelleniyor.");
            dbo = new DbObject();
            dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'KRP', LOGS = 'AUTOCROP' WHERE CILT_ID = " + ciltId, null);
            dbo.Dispose();
            LogYaz(ciltPath, "Cilt durum güncellendi. Uygulamadan çıkış yapılıyor. Yeni uygulama başlatılıyor.");

            System.Diagnostics.Process.Start(Application.ExecutablePath);
            LogYaz(ciltPath, "Yeni uygulama başlatıldı.");
            Application.Exit();
        }    
    
        private void LogYaz(string ciltPath, string log)
        {
            ciltPath = ciltPath.Replace(" ", "_");
            File.AppendAllLines("D:\\LOGS\\" + ciltPath + ".log", new[] {DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "-" + log});
        }
    }
}
