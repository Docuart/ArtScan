using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArtDeskew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void EgimGiderClick(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(_klasor1.Text))
                    throw new DirectoryNotFoundException();
                if (!Directory.Exists(_klasor2.Text))
                    Directory.CreateDirectory(_klasor2.Text);
            } 
            catch
            {
                MessageBox.Show("Klasör seçiniz. Seçtiğiniz Klasör Bulunamadı.");
            }

            if (!_klasor2.Text.EndsWith("\\"))
                _klasor2.Text += "\\";

            var klasorListesi = Directory.GetDirectories(_klasor1.Text);
            _progressHepsi.Maximum = klasorListesi.Length;
            _progressHepsi.Value = 0;
            foreach (var klasor in klasorListesi)
            {                
                Application.DoEvents();

                Directory.CreateDirectory(_klasor2.Text + Path.GetFileName(klasor));

                var dosyaListesi = Directory.GetFiles(klasor, "*.jpg");
                _progressKlasor.Maximum = dosyaListesi.Length;
                _progressKlasor.Value = 0;
                foreach (var dosya in dosyaListesi)
                {                    
                    Application.DoEvents();

                    var bitmap = new Bitmap(dosya);
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

                        tmp.Save(_klasor2.Text + Path.GetFileName(klasor) + "\\" + Path.GetFileName(dosya));
                    }
                    finally
                    {
                        g.Dispose();
                    }

                    _progressKlasor.Value = _progressKlasor.Value + 1;
                }
                _progressHepsi.Value = _progressHepsi.Value + 1;
            }
        }

        private void KlasorSec1Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            _klasor1.Text = folderBrowserDialog.SelectedPath;
        }

        private void KlasorSec2Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            _klasor2.Text = folderBrowserDialog.SelectedPath;
        }

        
    }
}
