using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using Image = AForge.Imaging.Image;

namespace Tests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getCamList();
        }

        private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;

        // get the devices name
        private void getCamList()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                comboBox1.Items.Clear();
                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                DeviceExist = true;
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox1.Items.Add(device.Name);
                }
                comboBox1.SelectedIndex = 0; //make dafault to first cam
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                comboBox1.Items.Add("No capture device on your system");
            }
        }

        
        //toggle start and stop button
        private void start_Click(object sender, EventArgs e)
        {
            if (start.Text == "&Start")
            {
                if (DeviceExist)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                    videoSource.ProvideSnapshots = true;
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    videoSource.SnapshotFrame += new NewFrameEventHandler(video_NewSnapshot);
                    CloseVideoSource();
                    videoSource.DesiredFrameSize = new Size(1280, 720);
                    videoSource.DesiredFrameRate = 2;

                    videoSource.Start();
                    label2.Text = "Device running...";
                    start.Text = "&Stop";
                    timer1.Enabled = true;
                }
                else
                {
                    label2.Text = "Error: No Device selected.";
                }
            }
            else
            {
                if (videoSource.IsRunning)
                {
                    timer1.Enabled = false;
                    CloseVideoSource();
                    label2.Text = "Device stopped.";
                    start.Text = "&Start";
                }
            }
        }

        //eventhandler if new frame is ready
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            //do processing here
            pictureBox1.Image = img;
        }

        private void video_NewSnapshot(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            img.Save("C:\\Test\\" + Guid.NewGuid() + ".jpg");
        }

        //close the device safely
        private void CloseVideoSource()
        {
            if (videoSource != null)
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }

        //get total received frame at 1 second tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "Device running... " + videoSource.FramesReceived.ToString() + " FPS";
        }



        private void Form1_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            CloseVideoSource();
            Application.Exit();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;

            if (!Directory.Exists("C:\\Test"))
                Directory.CreateDirectory("C:\\Test");
            //pictureBox1.Image.Save("C:\\Test\\" + Guid.NewGuid() + ".jpg", ImageFormat.Jpeg);
            var b = new ResizeBicubic(3000, 1500);
            var bitmap = b.Apply((Bitmap)pictureBox1.Image);
            bitmap.Save("C:\\Test\\" + Guid.NewGuid() + ".jpg", ImageFormat.Jpeg);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            var image = Image.FromFile("C:\\a.jpg");
            for(var y = 0; y < image.Height; y++)
            {
                Color color1;
                Color color2;
                for(var x = 0; x < image.Width/2; x++)
                {
                    color1 = image.GetPixel(y, x);
                    color2 = image.GetPixel(y, image.Width - x);

                    MessageBox.Show(color1.ToArgb() + "-" + color2.ToArgb());
                    break;
                }
                break;
            }
            */
        }
    }
}
