using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace AutoPickingSys
{
    class DrawLine
    {
        public Bitmap DraLine(Bitmap b, Point _StartPoint, Point _EndPoint)
        {
            Bitmap dstImage = (Bitmap)b.Clone();
            using (Graphics g = Graphics.FromImage(dstImage))
            {
                Pen _penRed = new Pen(Color.Red);
                g.DrawLine(new Pen(Color.Green, 3), _StartPoint, _EndPoint);
                g.Dispose();

                return dstImage;
            }

        }
    }
}
