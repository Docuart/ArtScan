using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using Point = System.Drawing.Point;

namespace Library.Image
{
    public class ImageProcessor
    {
        public static Bitmap Rotate(Bitmap bitmap, int degree)
        {
            var filter = new RotateBicubic(degree, true);
            return filter.Apply(bitmap);
        }


        public static Bitmap Crop(Bitmap bitmap)
        {
            return Crop(bitmap, 33);
        }

        public static Bitmap Crop(Bitmap bitmap, int rgbPoint)
        {
            // renk=R:rgbPoint G:rgbPoint B:rgbPoint
            const int step = 10; // kaçar piksel renk alımı yapılacağı
            var points = new[] { new Point(10000, 10000), new Point(10000, 10000), new Point(10000, 10000), new Point(0, 0) };  
            
            // SOL
            var a = new [] {800, 1000, 1200};
            var pa = new[] {new Point(), new Point(), new Point()};
            for(var i = 0; i < a.Length; i++)
            {
                if (bitmap.Height < a[i])
                    continue;
                for (var j = 0; j < bitmap.Width / 2; j = j + step)
                {                    
                    var color = bitmap.GetPixel(j, a[i]);
                    if (color.R > rgbPoint || color.G > rgbPoint || color.B > rgbPoint)
                    {
                        pa[i] = new Point(j + step, a[i]);
                        break;
                    }
                }
            }
            foreach (var t in pa)
            {
                if (t.X < points[0].X)
                    points[0] = t;
            }            

            // SAĞ
            var c = new[] { 800, 1000, 1200 };
            var pc = new[] { new Point(), new Point(), new Point() };
            for (var i = 0; i < c.Length; i++)
            {
                if (bitmap.Height < c[i])
                    continue;
                for (var j = 1; j < bitmap.Width / 2; j = j + step)
                {                    
                    var color = bitmap.GetPixel(bitmap.Width - j, c[i]);
                    if (color.R > rgbPoint || color.G > rgbPoint || color.B > rgbPoint)
                    {
                        pc[i] = new Point(bitmap.Width - j - step, c[i]);
                        break;
                    }
                }
            }
            foreach (var t in pc)
            {
                if (t.X < points[2].Y)
                    points[2] = t;
            }

            var mid = bitmap.Width / 2; //(bitmap.Width - points[0].X - (bitmap.Width - points[2].X))/2;
            
            // ÜST
            var oran1 = 600; 
            int oran2 = 400;
            if (mid < 600)
            {
                oran1 = Convert.ToInt32(mid * 0.6);
                oran2 = Convert.ToInt32(mid * 0.4);
            }
            
            var b = new[] { mid - oran1, mid - oran2, mid, mid + oran2, mid + oran1};
            var pb = new[] { new Point(), new Point(), new Point(), new Point(), new Point() };
            for (var i = 0; i < b.Length; i++)
            {
                for (var j = 0; j < 1500; j = j + step)
                {
                    var color = bitmap.GetPixel(b[i], j);
                    if (color.R > rgbPoint || color.G > rgbPoint || color.B > rgbPoint)
                    {
                        pb[i] = new Point(b[i], j + step);
                        break;
                    }
                }
            }
            foreach (var t in pb)
            {
                if (t.Y < points[1].Y)
                    points[1] = t;
            }

            // ALT
            var d = new[] { mid - 400, mid, mid + 400 };
            var pd = new[] { new Point(), new Point(), new Point() };
            for (var i = 0; i < d.Length; i++)
            {
                for (var j = 1; j < 1500; j = j + step)
                {
                    var color = bitmap.GetPixel(d[i], bitmap.Height - j);
                    if (color.R > rgbPoint || color.G > rgbPoint || color.B > rgbPoint)
                    {
                        pd[i] = new Point(d[i], bitmap.Height - j - step);
                        break;
                    }
                }
            }
            foreach (var t in pd)
            {
                if (t.Y > points[3].Y)
                    points[3] = t;
            }
            
            var r = new Rectangle(points[0].X, points[1].Y, points[2].X - points[0].X, points[3].Y - points[1].Y);
            if (r.Width <= 0) r.Width = bitmap.Width;
            if (r.Height <= 0) r.Height = bitmap.Height;

            var crop = new Crop(r);
            Bitmap bit;
            try
            {
                bit = crop.Apply(bitmap);
            } 
            catch
            {
                bit = bitmap;
            }
            bitmap.Dispose();
           
            return bit;
        }

        public enum SplitType { None, Sol, Sag, Cift }
        public static Bitmap[] Split(Bitmap bitmap, SplitType splitType)
        {
            var splitPoint = bitmap.Width / 2;
            var bitmaps = new Bitmap[2];
            if (splitType == SplitType.None)
                bitmaps[0] = bitmap;

            if (splitType == SplitType.Sol || splitType == SplitType.Cift)
            {
                var sol = new Rectangle(0, 0, splitPoint, bitmap.Height);
                bitmaps[0] = new Crop(sol).Apply(bitmap);
            }
            if (splitType == SplitType.Sag || splitType == SplitType.Cift)
            {
                var sag = new Rectangle(splitPoint, 0, splitPoint, bitmap.Height);
                bitmaps[1] = new Crop(sag).Apply(bitmap);
            }
            return bitmaps;
        }

        private static Bitmap Resize(Bitmap bitmap, Size size)
        {
            var sourceWidth = bitmap.Width;
            var sourceHeight = bitmap.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            
            // ReSharper disable PossibleLossOfFraction
            nPercentW = (size.Width / sourceWidth);
            nPercentH = (size.Height / sourceHeight);
            // ReSharper restore PossibleLossOfFraction

            nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var b = new Bitmap(destWidth, destHeight);
            var g = Graphics.FromImage(b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(bitmap, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }

        public static double GetSkewAngle(Bitmap bitmap)
        {
            var dsc = new DocumentSkewChecker();
            return dsc.GetSkewAngle(bitmap);
        }

        // Conver list of AForge.NET's points to array of .NET points
        private static Point[] ToPointsArray(List<IntPoint> points)
        {
            var array = new Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new Point(points[i].X, points[i].Y);
            }

            return array;
        }

        public static ArrayList DetectPaper(Bitmap image)
        {
            var pointArray = new ArrayList();
            // Open your image
            // locating objects
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.BackgroundThreshold = Color.Black;
            
            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = image.Width / 4;
            blobCounter.MinWidth = image.Width / 4;
            blobCounter.MaxHeight = image.Height - 100;
            blobCounter.MaxWidth = image.Width - 100;
            
            blobCounter.ProcessImage(image);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            // check for rectangles
            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

            foreach (var blob in blobs)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blob);
                List<IntPoint> cornerPoints;
                try
                {
                    // use the shape checker to extract the corner points
                    if (shapeChecker.IsQuadrilateral(edgePoints, out cornerPoints))
                    {
                        // only do things if the corners form a rectangle
                        if (shapeChecker.CheckPolygonSubType(cornerPoints) == PolygonSubType.Rectangle)
                        {
                            // here i use the graphics class to draw an overlay, but you
                            // could also just use the cornerPoints list to calculate your
                            // x, y, width, height values.
                            List<Point> Points = new List<Point>();
                            foreach (var point in cornerPoints)
                            {
                                Points.Add(new Point(point.X, point.Y));
                            }
                            pointArray.Add(Points);

                            //Graphics g = Graphics.FromImage(image);
                            //g.DrawPolygon(new Pen(Color.Red, 50.0f), Points.ToArray());
                        }
                    }
                } catch {}
            }
            return pointArray;

            /*
            var bitmap = bitmapGelen.Clone(new Rectangle(10, 10, bitmapGelen.Width - 100, bitmapGelen.Height - 100), bitmapGelen.PixelFormat);

            // locate objects using blob counter
            var blobCounter = new BlobCounter();
            blobCounter.ProcessImage(bitmap);
            var blobs = blobCounter.GetObjectsInformation();
            // create Graphics object to draw on the image and a pen
            var g = Graphics.FromImage(bitmap);
            var bluePen = new Pen(Color.Blue, 20);
            // check each object and draw circle around objects, which
            // are recognized as circles
            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                var edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                var corners = PointsCloud.FindQuadrilateralCorners(edgePoints);

                g.DrawPolygon(bluePen, ToPointsArray(corners));
            }
            
            bluePen.Dispose();
            g.Dispose();
            return bitmap;
            */
        }
    }
}
