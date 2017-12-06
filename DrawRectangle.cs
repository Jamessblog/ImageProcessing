using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class DrawRectangle
    {
        public Bitmap DraRect(Bitmap b, int x, int y, int w, int h)
        {

            Bitmap dstImage = (Bitmap)b.Clone();
            
            //using (Graphics g = Graphics.FromImage(dstImage))
            //{
            //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //    Pen _penRed = new Pen(Color.Red);
            //    g.DrawRectangle(_penRed, x, y, w, h);
                
            //    g.Dispose();
            //}
            Graphics g = Graphics.FromImage(dstImage);
            Pen _penRed = new Pen(Color.Red);
            g.DrawRectangle(_penRed, x, y, w, h);
            g.Dispose();
            return dstImage;
        }
        
    }
}
