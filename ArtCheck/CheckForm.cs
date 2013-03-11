using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using ArtCheck.Properties;
using Library.Scan;

namespace ArtCheck
{
    public partial class CheckForm : Form
    {
        private const string TarananKlasoru = "S:\\"; 
        private const string KontrolKlasoru = "K:\\"; 
        private const string TempKlasoru = "ArtCheck\\"; 
        private NameValueCollection _tarananlar;

        public CheckForm()
        {
            InitializeComponent();
            _tarananList.DrawMode = DrawMode.OwnerDrawFixed;
            _tarananList.DrawItem += OnDrawItem;
        }

        void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, _tarananlar[((ListBox)sender).Items[e.Index].ToString()] == "E" ? Brushes.Black : Brushes.Red, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        } 

        private void CheckFormLoad(object sender, EventArgs e)
        {            
            CiltGetir();
        }

        private void CiltGetir()
        {
            _yukleniyor.Visible = true;
            _timer.Enabled = false;
            _tarananList.Items.Clear();
            _taranan.Image = null;
            _tarananlar = new NameValueCollection();

            var ciltler = Directory.GetDirectories(TarananKlasoru);
            if (ciltler.Length == 0)
            {
                _timer.Enabled = true;
                return;
            }
            Array.Sort(ciltler);
            var index = -1;
            for (var i = 0; i < ciltler.Length; i++)
            {
                if (File.Exists(ciltler[0] + "\\cilt.end"))
                    index = i;
            }
            if (index == -1)
            {
                _timer.Enabled = true;
                return;
            }
            _cilt.Text = ciltler[0].Substring(ciltler[index].LastIndexOf("\\") + 1);
            _ciltKlasoru.Text = ciltler[0];

            var infoFile = File.ReadAllText(ciltler[index] + "\\user.info");
            _kullanici.Text = infoFile;

            var files = Directory.GetFiles(_ciltKlasoru.Text, "*.jpg");
            Array.Sort(files);

            var folder = Path.GetTempPath() + TempKlasoru;
            if (Directory.Exists(folder))
                Directory.Delete(folder, true);
            Directory.CreateDirectory(folder);
            foreach (var file in files)
            {
                var image = Image.FromFile(file);
                image.Save(folder + Path.GetFileName(file));
                
                _tarananList.Items.Add(Path.GetFileName(file) ?? "");
                _tarananlar.Add(Path.GetFileName(file) ?? "", "H");
                //_tarananList.Items.Add(file);
                //_tarananlar.Add(file, "H");
            }
            _tarananList.SelectedIndex = 0;

            new SoundPlayer(Resources.notify).Play();
            _yukleniyor.Visible = false;
        }

        private void UygunClick(object sender, EventArgs e)
        {
            _tarananlar[_tarananList.SelectedItem.ToString()] = "E";
            for (var i = 0; i < _tarananList.Items.Count; i++)
            {
                if (_tarananlar[_tarananList.Items[i].ToString()] != "H") 
                    continue;

                _tarananList.SelectedIndex = i;
                return;
            }

            var sonuc = MessageBox.Show(_cilt.Text + @" Cilt kontrolü tamamlandı. İşleminizi onaylıyor musunuz?", @"İşlem Sonu", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (sonuc != DialogResult.Yes) 
                return;

            var files = Directory.GetFiles(_ciltKlasoru.Text);
            for (var i = 0; i < files.Length; i++)
                files[i] = Path.GetFileName(files[i]);

            _taranan.Image = null;
            if (File.Exists(KontrolKlasoru + "\\" + _cilt.Text + ".zip"))
            {
                MessageBox.Show(@"Bu cilde ait kontrol dosyası mevcut." + '\n' + @"Lütfen sistem yöneticinize başvurunuz.", @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ZipUtil.CompressFolder(_ciltKlasoru.Text, KontrolKlasoru + "\\" + _cilt.Text + ".zip");
            //var key = CryptoUtil.EncryptFile(KontrolKlasoru + "\\" + _cilt.Text + ".zip", KontrolKlasoru + "\\" + _cilt.Text + "-.zip");
            //File.AppendAllLines(KontrolKlasoru + "\\files.txt", new [] { key.Checksum + key.Key });
            Directory.Delete(_ciltKlasoru.Text, true);
            CiltGetir();
        }

        private void TarananListSelectedIndexChanged(object sender, EventArgs e)
        {
            var fileName = Path.GetTempPath() + TempKlasoru + _tarananList.Items[_tarananList.SelectedIndex];
            new FileInfo(fileName).Attributes = FileAttributes.Normal;
            _taranan.Image = Image.FromFile(fileName);
            _dosya.Text = _tarananList.Items[_tarananList.SelectedIndex].ToString();
        }

        private void TekrarClick(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
                    @"Seçtiğiniz dosya silinip tekrar taranacak." + '\n' + @"İşleminizi onaylıyor musunuz?", @"Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (sonuc == DialogResult.Yes)
            {
                var images = WIAScanner.Scan();
                File.Delete(_dosya.Text);
                images[0].Save(_dosya.Text);
            }
        }

        private void SilClick(object sender, EventArgs e)
        {
            var sonuc = MessageBox.Show(
                    @"Seçtiğiniz dosya silinecektir." + '\n' + @"İşleminizi onaylıyor musunuz?", @"Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (sonuc == DialogResult.Yes)
                File.Delete(_dosya.Text);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            CiltGetir();
            if (_yukleniyor.Text.EndsWith("..."))
                _yukleniyor.Text = _yukleniyor.Text.Substring(0, _yukleniyor.Text.Length - 3);
            else
                _yukleniyor.Text += '.';
        }
    }
}