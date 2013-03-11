using System;
using System.IO;
using System.Windows.Forms;

namespace ArtRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _lastSelectedPath = "D:\\Scanned";
        private void GozatClick(object sender, EventArgs e)
        {            
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = _lastSelectedPath;
            folderBrowserDialog.ShowDialog();

            _lastSelectedPath = folderBrowserDialog.SelectedPath;
            _klasor.Text = _lastSelectedPath;

            var ciltAdi = _klasor.Text.Substring(_klasor.Text.LastIndexOf("\\")).Replace("\\", "");
            var ciltAdiList = ciltAdi.Split('-');
            _yeni.Text = ciltAdiList[ciltAdiList.Length - 1];
            for (var i = 0; i < ciltAdiList.Length - 1; i++)
                _yeni.Text += "-" + ciltAdiList[i];

            _lastSelectedPath = _klasor.Text.Substring(0, _klasor.Text.LastIndexOf("\\")) + "\\" + _yeni.Text;
            //_yeni.Text = _yeni.Text.Split('-')[1] + "-" + _yeni.Text.Split('-')[0];
        }

        private void _kopyala_Click(object sender, EventArgs e)
        {
            var ustKlasor = _klasor.Text.Substring(0, _klasor.Text.LastIndexOf("\\"));
            var klasor = _klasor.Text.Substring(_klasor.Text.LastIndexOf("\\")).Replace("\\", "");
            var yeniKlasor = Path.Combine(ustKlasor, _yeni.Text);
            if (!Directory.Exists(yeniKlasor))
                Directory.CreateDirectory(yeniKlasor);
            else
            {
                if (MessageBox.Show("Klasör mevcut! Devam edilsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
            }


            var t = new System.Threading.Thread(CreateForm);
            t.Start();

            var files = Directory.GetFiles(_klasor.Text);
            foreach (var file in files)
            {
                LoadInfo.Instance.AddInfo("Dosya kopyalanıyor." + file);
                LoadInfo.Instance.AddInfo("Dosya kopyalandı." + file.Replace(klasor, _yeni.Text));
                File.Move(file, file.Replace(klasor, _yeni.Text));
            }            
            Directory.Delete(_klasor.Text);
            Invoke(new Action(() => _f.Close()));
            MessageBox.Show("İşlem tamamlandı.");
        }

        private LoadingForm _f;
        private void CreateForm()
        {
            _f = new LoadingForm();
            _f.ShowDialog();
        }
    }
}
