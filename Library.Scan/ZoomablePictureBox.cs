using System.Drawing;
using System.Windows.Forms;

namespace Library.Scan
{
    public class ZoomablePictureBox : PictureBox
    {
        private int _zmLevel = 1;
        private Point _zmPt;

        public ZoomablePictureBox()
        {
            MouseDown += ZoomablePictureBoxMouseDown;
        }

        void ZoomablePictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (_zmLevel == 1)
                _zmPt = new Point(e.X, e.Y);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _zmLevel += 1;
                    break;
                case MouseButtons.Right:
                    if (_zmLevel == 1)
                        break;
                    _zmLevel -= 1;
                    break;
            }
            Invalidate();
        }

        new public Image Image // overrides
        {
            get
            {
                return base.Image;
            }
            set
            {
                _zmLevel = 1;
                base.Image = value;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Image == null) return;
            Point loc;
            Size sz;
            if (_zmLevel != 1)
            {
                sz = new Size(Image.Width / _zmLevel, Image.Height / _zmLevel);
                // center on zmPt. Casts are needed so integer divide doesn't occur (intermediate double result)
                loc = new Point((int)(Image.Width * (_zmPt.X / (double)ClientRectangle.Width)) - sz.Width / 2,
                                (int)(Image.Height * (_zmPt.Y / (double)ClientRectangle.Height)) - sz.Height / 2);
            }
            else
            {
                loc = new Point(0, 0);
                sz = Image.Size;
            }
            var rectSrc = new Rectangle(loc, sz);
            // now draw the rect of the source picture in the entire client rect of MyPictureBox
            pe.Graphics.DrawImage(Image, ClientRectangle, rectSrc, GraphicsUnit.Pixel);
            
        }
    }
}
