using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library.Scan;

namespace ArtData
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginFormLoad(object sender, EventArgs e)
        {
            _userName.Text = Util.GetName();
        }

        private void GirisClick(object sender, EventArgs e)
        {
            new DataForm().Show();            
            Visible = false;
        }

        private static bool LoginKontrol()
        {
            var dr = MessageBox.Show("Kontrol/İstatistik formunu açmak istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dr == DialogResult.Yes;
        }

        private void KontrolClick(object sender, EventArgs e)
        {
            if (!LoginKontrol()) return;
            new KontrolForm().Show();
            Visible = false;
        }
        private void IstatistikClick(object sender, EventArgs e)
        {
            if (!LoginKontrol()) return; 
            new StatForm().Show();
            Visible = false;
        }
    }
}
