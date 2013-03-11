using System;
using System.Windows.Forms;

namespace ArtControl
{
    public partial class DialogForm : Form
    {
        public DialogForm()
        {
            InitializeComponent();
        }
        
        private void EvetClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_text.Text))
            {
                MessageBox.Show("Açıklama giriniz.");
                return;
            }
            
            ((MainForm) Owner).DResult = DialogResult.Yes;
            ((MainForm) Owner).DText = _text.Text;
            Dispose();
        }

        private void HayirClick(object sender, EventArgs e)
        {
            ((MainForm)Owner).DResult = DialogResult.No;
            ((MainForm)Owner).DText = "";
            Dispose();
        }
    }
}
