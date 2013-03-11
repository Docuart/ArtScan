using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Library.Scan;

namespace ArtControl
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private const string IndeksKlasoru = "I:\\";
        private DialogForm _dialogForm;
        public DialogResult DResult;
        public string DText;

        public DialogResult ShowTextDialog()
        {
            _dialogForm = new DialogForm();
            _dialogForm.ShowDialog(this);
            return DResult;
        }

        private void AraClick(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(_klasorNo.Text))
            {
                MessageBox.Show("Klasör bilgisi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_DURUM = 'KNT' AND CILT_PATH = upper('" + _klasorNo.Text + "')");
            dbo.Dispose();

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Aradığınız klasör sistemde " +
                                "bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dbo = new DbObject();
            //02.03.2013 OSMAN YILMAZ ORDER BY EKLENDİ (DOSYA ID YE GÖRE SIRALI GELMESİ İÇİN)
            var dsDosya = dbo.DataSet("SELECT DOSYA_ID, DATA_ABONENO, DATA_EVRAKTURU, ACIKLAMA FROM SCAN_DOSYA WHERE CILT_ID = " + ds.Tables[0].Rows[0]["CILT_ID"]+"ORDER BY DOSYA_ID");
            dbo.Dispose();

            if (dsDosya == null || dsDosya.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Aradığınız klasöre ait dosyalar sistemde bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _grid.DataSource = dsDosya.Tables[0];
        }

        private void GridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT * FROM SCAN_DOSYA D WHERE D.DOSYA_ID = " + _grid.SelectedRows[0].Cells[0].Value);
            dbo.Dispose();
            _taranan.Image = Image.FromFile(IndeksKlasoru + _klasorNo.Text + "\\" + ds.Tables[0].Rows[0]["DOSYA_PATH"]);            
        }

        private void DogruClick(object sender, EventArgs e)
        {
            const string keyword = "DOĞRU";

            var dbo = new DbObject();
            dbo.Execute("UPDATE SCAN_DOSYA SET KONTROL = sysdate, ACIKLAMA = '" + keyword + "' WHERE DOSYA_ID = " + _grid.SelectedRows[0].Cells[0].Value, null);
            dbo.Dispose();

            _grid.SelectedRows[0].Cells[3].Value = keyword;
        }

        private void YanlisClick(object sender, EventArgs e)
        {
            if (ShowTextDialog() == DialogResult.Yes)
            {
                string keyword = "YANLIŞ-" + DText.Replace("'", "`");

                var dbo = new DbObject();
                dbo.Execute("UPDATE SCAN_DOSYA SET KONTROL = sysdate, ACIKLAMA = '" + keyword + "' WHERE DOSYA_ID = " + _grid.SelectedRows[0].Cells[0].Value, null);
                dbo.Dispose();

                _grid.SelectedRows[0].Cells[3].Value = keyword;
            }            
        }
        //02.03.2013 OSMAN YILMAZ 
        private void GonderClick(object sender, EventArgs e)
        {
            if (_grid.SelectedRows[0].Cells[0] == null)
            {
                MessageBox.Show("Lütfen Dosya Seçiniz");
                return;
            }

            try
            {
                if (_evrakTuru.SelectedItem == null & _aboneNo.Text == "")
                {
                    MessageBox.Show("Lütfen Evrak Türü veya Abone No Giriniz");
                    return;
                    
                }
                if (_evrakTuru.SelectedItem != null & _aboneNo.Text != "")
                {
                    var dboEvrakAbone = new DbObject();
                    dboEvrakAbone.Execute(
                        "UPDATE SCAN_DOSYA SET KONTROL = sysdate, ACIKLAMA = 'DOĞRU',DATA_ABONENO ='" + _aboneNo.Text +
                        "',DATA_EVRAKTURU ='" + _evrakTuru.SelectedItem + "'  WHERE DOSYA_ID = " +
                        _grid.SelectedRows[0].Cells[0].Value, null);
                    dboEvrakAbone.Dispose();

                    _grid.SelectedRows[0].Cells[3].Value = "DOĞRU";
                    _grid.SelectedRows[0].Cells[1].Value = _aboneNo.Text;
                    _grid.SelectedRows[0].Cells[2].Value = _evrakTuru.SelectedItem;
                }
                else if (_evrakTuru.SelectedItem == null & _aboneNo.Text != "")
                {
                    var dboEvrak = new DbObject();
                    dboEvrak.Execute(
                        "UPDATE SCAN_DOSYA SET KONTROL = sysdate, ACIKLAMA = 'DOĞRU',DATA_ABONENO ='" + _aboneNo.Text +
                        "' WHERE DOSYA_ID = " + _grid.SelectedRows[0].Cells[0].Value, null);
                    dboEvrak.Dispose();

                    _grid.SelectedRows[0].Cells[3].Value = "DOĞRU";
                    _grid.SelectedRows[0].Cells[1].Value = _aboneNo.Text;
                }
                else if (_evrakTuru.SelectedItem != null & _aboneNo.Text == "")
                {
                    var dboAbone = new DbObject();
                    dboAbone.Execute(
                        "UPDATE SCAN_DOSYA SET KONTROL = sysdate, ACIKLAMA = 'DOĞRU',DATA_EVRAKTURU ='" +
                        _evrakTuru.SelectedItem + "'  WHERE DOSYA_ID = " + _grid.SelectedRows[0].Cells[0].Value, null);
                    dboAbone.Dispose();

                    _grid.SelectedRows[0].Cells[3].Value = "DOĞRU";
                    _grid.SelectedRows[0].Cells[2].Value = _evrakTuru.SelectedItem;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex+" Hatasını Yazılım Ekibine Bildiriniz");
            }
                

        }
        //Direkt Abone No ve/veya Evrak Turu girilmesi için  
        //02.03.2013 OSMAN YILMAZ
        private void MainForm_Load(object sender, EventArgs e)
        {
            _evrakTuru.Items.Clear();

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
        }
        //Combobox a türler eklendi

        //05.03.2013 OSMAN YILMAZ Renklendirme
        private void RenklendirClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if(clickedButton.Name == "_yesil")
            {
                _grid.SelectedRows[0].DefaultCellStyle.BackColor = Color.Lime;
            }
            else if (clickedButton.Name == "_sari")
            {
                _grid.SelectedRows[0].DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (clickedButton.Name == "_lacivert")
            {
                _grid.SelectedRows[0].DefaultCellStyle.BackColor = Color.Navy;
            }
            else if (clickedButton.Name == "_turkuaz")
            {
                _grid.SelectedRows[0].DefaultCellStyle.BackColor = Color.Aqua;
            }
            else if (clickedButton.Name == "_pembe")
            {
                _grid.SelectedRows[0].DefaultCellStyle.BackColor = Color.Fuchsia;
            }
            else if (clickedButton.Name == "_kirmizi")
            {
                _grid.SelectedRows[0].DefaultCellStyle.BackColor = Color.Red;
            }
        }
    }
}
