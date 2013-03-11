using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library.Image;
using WIA;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = System.Drawing.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var start = DateTime.Now;
            var bitmap = ImageProcessor.Rotate(new Bitmap("D:\\CopyArt\\Temp\\IMG_0001.JPG"), 180);
            var bit = ImageProcessor.Crop(bitmap);
            var bits = ImageProcessor.Split(bit, ImageProcessor.SplitType.Cift);
            if (bits[0] != null)
                bits[0].Save("D:\\CopyArt\\Temp\\" + Guid.NewGuid() + ".jpg", ImageFormat.Jpeg);
            if (bits[1] != null)
                bits[1].Save("D:\\CopyArt\\Temp\\" + Guid.NewGuid() + ".jpg", ImageFormat.Jpeg);
            label1.Text = (start - DateTime.Now).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var d = DateTime.Now;
            var files = Directory.GetFiles("C:\\yamuk\\");
            foreach (var file in files)
            {
                AutoRotate(file, "C:\\yamuk2\\");
            }
            var dd = DateTime.Now.Subtract(d);
            MessageBox.Show(dd.TotalMilliseconds + "-" + dd.TotalSeconds);
        }

        private static void AutoRotate(string file, string outFolder)
        {
            if (!outFolder.EndsWith("\\"))
                outFolder = outFolder + "\\";
            using (var bitmap = new Bitmap(file))
            {
                var sk = new gmseDeskew.gmseDeskew(bitmap);
                var tmp = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
                tmp.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                var g = Graphics.FromImage(tmp);
                try
                {
                    g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                    g.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                    g.RotateTransform(-(float)sk.GetSkewAngle());
                    g.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                    g.DrawImage(bitmap, 0, 0);
                    tmp.Save(outFolder + Path.GetFileName(file));
                }
                finally
                {
                    g.Dispose();
                }
            }
        }

        private void Rotate(int deg)
        {
            var bitmap = pictureBox1.Image;
            var tmp = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            tmp.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            var g = Graphics.FromImage(tmp);
            try
            {
                g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                g.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                g.RotateTransform(deg);
                g.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);                
                g.DrawImage(bitmap, 0, 0);
                pictureBox1.Image = tmp;
            }
            finally
            {
                g.Dispose();
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Rotate(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rotate(-1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\yamuk\\ABONE TALEPLERİ(BONO YAPILAN ABONELERİ)17012011-03042012#0006.jpg");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (string.IsNullOrEmpty(ofd.FileName))
                return;

            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (string.IsNullOrEmpty(fbd.SelectedPath))
                return;

            AutoRotate(ofd.FileName, fbd.SelectedPath);
            MessageBox.Show("OK");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            const string filePath = "D:\\2006-MR-00764\\x.jpg";
            using (var bitmap = new Bitmap(filePath))
            {
                int boyut = (int)Math.Sqrt(bitmap.Width * bitmap.Width + bitmap.Height * bitmap.Height) + 10;
                
                var tmpx = new Bitmap(boyut, boyut, bitmap.PixelFormat);
                using (var gr = Graphics.FromImage(tmpx))
                {
                    gr.DrawImage(bitmap, (boyut - bitmap.Width) / 2, (boyut - bitmap.Height) / 2, bitmap.Width, bitmap.Height);                    
                }
                
                var sk = new gmseDeskew.gmseDeskew(tmpx);
                var tmp = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
                tmp.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                var g = Graphics.FromImage(tmp);                
                try
                {
                    g.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);
                    g.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
                    g.RotateTransform(-(float)sk.GetSkewAngle());
                    g.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
                    g.DrawImage(bitmap, 0, 0);

                    tmp.Save("C:\\deneme.jpg");
                }
                finally
                {
                    g.Dispose();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            if (string.IsNullOrEmpty(openFileDialog.FileName))
                return;

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();

            CryptoUtil.CryptoUtil.DosyaGonder(openFileDialog.FileName, saveFileDialog.FileName);
            MessageBox.Show("OK");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            const string sourceFile = "D:\\2006-MR-00764";
            const string destinationFile = "D:\\PDFs\\deneme.pdf";

            var document = new Document();
            var pdfWriter = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
            pdfWriter.SetFullCompression();
            pdfWriter.StrictImageSequence = true;
            pdfWriter.SetLinearPageMode();

            var images = Directory.GetFiles(sourceFile);

            for (var i = 0; i < images.Count(); i++)
            {
                // Define the page size here, _before_ you start the page.
                // You can easily switch from landscape to portrait to whatever
                document.SetPageSize(new Rectangle(Image.FromFile(images[i]).Width, Image.FromFile(images[i]).Height));
                document.SetMargins(0, 0, 0, 0);
                if (!document.IsOpen())
                {
                    document.Open();
                }

                document.NewPage();
                var myImage = iTextSharp.text.Image.GetInstance(images[i]);
                myImage.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                document.Add(myImage);
            }

            document.Close();
        }
    }
}
