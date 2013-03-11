using System;
using System.Windows.Forms;

namespace ArtRename
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }       

        private void TimerTick(object sender, EventArgs e)
        {
            _info.Lines = LoadInfo.Instance.Info.Split('\n');
            _info.SelectionStart = _info.Text.Length;
            _info.ScrollToCaret();
        }
    }
}
