using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace RelationMap.Utility
{
    public static class ImageHelper
    {
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        public static void CropImage(int x, int y, int width, int height)
        {
            string imagePath = @"C:\Users\Admin\Desktop\test.jpg";
            Bitmap croppedImage;

            // Here we capture the resource - image file.
            using (var originalImage = new Bitmap(imagePath))
            {
                Rectangle crop = new Rectangle(x, y, width, height);

                // Here we capture another resource.
                croppedImage = originalImage.Clone(crop, originalImage.PixelFormat);

            } // Here we release the original resource - bitmap in memory and file on disk.

            // At this point the file on disk already free - you can record to the same path.
            croppedImage.Save(imagePath, ImageFormat.Jpeg);

            // It is desirable release this resource too.
            croppedImage.Dispose();
        }
        public static Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }
        public static void DrawRectangle(Graphics g, Pen penToUse, Point start, Point end)
        {
            
            Rectangle rect = GetRectangle(start, end);
            Region r = new Region(rect);
            r.Xor(g.ClipBounds);
            g.DrawRectangle(penToUse, rect);
        }

        public static Rectangle GetConstrainedRectangle(Point p1, Point p2, float ratio)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);

            int w = Math.Max(p1.X, p2.X) - x;
            int h = Math.Max(p1.Y, p2.Y) - y;

            if (ratio > 1)
            {
                h = (int)(1f * w / ratio);
            }
            else
            {
                w = (int)(1f * h * ratio);
            }

            return new Rectangle(x, y, w, h);
            //Rectangle r = new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y),
            //    Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            //return r;
        }
        public static void DrawConstrainedRectangle(Graphics g, Pen penToUse, Point start, Point end, float ratio = 0.33f)
        {

            Rectangle rect = GetConstrainedRectangle(start, end, ratio);
            Region r = new Region(rect);
            r.Xor(g.ClipBounds);
            g.DrawRectangle(penToUse, rect);
        }
        public static Bitmap CropToRectangle(Image srcImage, PointF start, PointF end, int newWidth, int newHeight, int sw, int sh)
        {
            //Scale of the Points are to the Control the image is in, not to the image - unless the scale is identical
            //gs.VisibleClipBounds vs srcImage.Width and SrcImage.Height.
            //float 

            float imageRatio = srcImage.Width / (float)srcImage.Height;
            float containerRatio = sw / (float)sh;

            double scaleX = (double)srcImage.Width / (double)sw;
            double scaleY = (double)srcImage.Height / (double)sh;

            if (imageRatio >= containerRatio)
            {
                //horizontal image
                float scaleFactor = sw / (float)srcImage.Width;
                float scaledHeight = srcImage.Height * scaleFactor;
                // calculate gap between top of container and top of image
                float filler = Math.Abs(sh - scaledHeight) / 2;
                start.X = (int)(start.X / scaleFactor);
                start.Y = (int)((start.Y - filler) / scaleFactor);
                end.X = (int)(end.X / scaleFactor);
                end.Y = (int)((end.Y - filler) / scaleFactor);

            }
            else
            {
                //vertical image
                float scaleFactor = sh / (float)srcImage.Height;
                float scaledWidth = srcImage.Width * scaleFactor;
                float filler = Math.Abs(sw - scaledWidth) / 2;
                start.X = (int)((start.X - filler) / scaleFactor);
                start.Y = (int)(start.Y / scaleFactor);
                end.X = (int)((end.X - filler) / scaleFactor);
                end.Y = (int)(end.Y / scaleFactor);
            }


            ////Need to bounding Rectangle/Square of the Circle produced
            float left = start.X;
            float top = start.Y;
            float ssWidth = end.X - start.X;
            float ssHight = end.Y - start.Y;



            Bitmap dstImage = new Bitmap(newWidth, newHeight, srcImage.PixelFormat);

            Graphics g = Graphics.FromImage(dstImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //GraphicsPath path = new GraphicsPath();
            Rectangle destRect = new Rectangle(0, 0, newWidth, newHeight);
            RectangleF sourceRect = new RectangleF(left, top, ssWidth, ssHight);

            g.DrawImage(srcImage, destRect, sourceRect, GraphicsUnit.Pixel);
            

            return dstImage;
        }
        public static void DrawCircle(Graphics g, Pen penToUse, Point center, int radius)
        {
            Rectangle rect = new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(penToUse, rect);
        }
        public static void DrawCircle(Graphics g, Pen penToUse, Point center, Point endPoint)
        {
            //Need to convert center to endPoint into a radius using sin and cosine
            double dx = endPoint.X - center.X;
            double dy = endPoint.Y - center.Y;
            double radius = Math.Sqrt(dx * dx + dy * dy);
            DrawCircle(g, penToUse, center, (int)radius);
        }

        public static Image CropToCircle(Graphics gs, Image srcImage, Point center, Point endPoint)
        {
            //TODO -implement similar to Crop to rectangle to account for stretched images.
            //Scale of the Points are to the Control the image is in, not to the image - unless the scale is identical
            //gs.VisibleClipBounds vs srcImage.Width and SrcImage.Height.
            double scaleX = srcImage.Width / gs.VisibleClipBounds.Width;
            double scaleY = srcImage.Height / gs.VisibleClipBounds.Height;
            center.X = (int)(center.X * scaleX);
            center.Y = (int)(center.Y * scaleY);
            endPoint.X = (int)(endPoint.X * scaleX);
            endPoint.Y = (int)(endPoint.Y * scaleY);


            double dx = endPoint.X - center.X;
            double dy = endPoint.Y - center.Y;
            double radius = Math.Sqrt(dx * dx + dy * dy);
            var diameter = (int)(radius * 2) ; //Diameter
            //Need to bounding Rectangle/Square of the Circle produced
            int left =  (center.X - (int)radius);
            int top =  (center.Y - (int)radius);

            //Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, PixelFormat.Format32bppArgb); //For same size as original
            Image dstImage = new Bitmap(diameter+2, diameter+2, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(dstImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            using (Brush br = new SolidBrush(Color.Transparent))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }
            GraphicsPath path = new GraphicsPath();
            
            //path.AddEllipse(left, top, diameter, diameter); // for Original Placement
            path.AddEllipse(0, 0, diameter, diameter ); //Top Left
            
            //g.DrawEllipse(Pens.Red, left, top, diameter, diameter); // for Original Placement
            
            //g.DrawEllipse(Pens.Red, 0, 0, diameter, diameter);
            g.SetClip(path);
            //g.DrawImage(srcImage, 0,0); 
            Rectangle destRect = new Rectangle(0, 0, diameter, diameter);
            Rectangle sourceRect = new Rectangle(left, top, diameter, diameter);
            g.DrawImage(srcImage, destRect, sourceRect, GraphicsUnit.Pixel);
            
            return dstImage;
        }


        public static Image CropToEllipse(Image srcImage)
        {
            //Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(dstImage);
            using (Brush br = new SolidBrush(Color.Transparent))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }
            GraphicsPath path = new GraphicsPath();
            
            path.AddEllipse(0, 0, dstImage.Width, dstImage.Height); //Just frames the image in the center..
            g.SetClip(path, CombineMode.Intersect);
            g.DrawImage(srcImage, 0, 0);

            return dstImage;
        }
    }
}
