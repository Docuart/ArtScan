using System;
using System.Windows.Forms;

namespace ArtData
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
            
            ((BaseForm) Owner).DResult = DialogResult.Yes;
            ((BaseForm) Owner).DText = _text.Text;
            Dispose();
        }

        private void HayirClick(object sender, EventArgs e)
        {
            ((BaseForm)Owner).DResult = DialogResult.No;
            ((BaseForm)Owner).DText = "";
            Dispose();
        }
    }
}
