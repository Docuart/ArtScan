using System;
using System.IO;
using System.Windows.Forms;

namespace ArtCrop
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }       

        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText("C:\\Log.txt", _info.Text);                                        
                if (LoadInfo.Instance.Info == null)
                    return;
                _info.Lines = LoadInfo.Instance.Info.Split('\n');
                _info.SelectionStart = _info.Text.Length;
                _info.ScrollToCaret();

                if (LoadInfo.Instance.Length != 0)
                {
                    _progress.Maximum = LoadInfo.Instance.Length;
                    _progress.Minimum = 0;
                    _progress.Step = 1;

                    _progress.Value = LoadInfo.Instance.Status;

                    Text = "Yükleniyor... %" + LoadInfo.Instance.Status*100/LoadInfo.Instance.Length;
                }
            }
            catch { }
        }
    }
}
