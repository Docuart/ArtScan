using System;
using System.Windows.Forms;
using Library.Scan;

namespace SamgazKontrol
{
    public partial class KontrolForm : Form
    {
        public KontrolForm()
        {
            InitializeComponent();
        }

        private void AraClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_klasorNo.Text))
            {
                MessageBox.Show("Klasör bilgisi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_DURUM = 'KNT' AND CILT_PATH = '" + _klasorNo.Text + "'");
            dbo.Dispose();

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Aradığınız klasör sistemde bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dbo = new DbObject();
            var dsDosya = dbo.DataSet("SELECT * FROM SCAN_DOSYA WHERE CILT_ID = " + ds.Tables[0].Rows[0]["CILT_ID"]);
            dbo.Dispose();
            
            if (dsDosya == null || dsDosya.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Aradığınız klasöre ait dosyalar sistemde bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _grid.DataSource = dsDosya;

        }

        
    }
}
