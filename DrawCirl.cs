using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AutoPickingSys
{  
    class DrawCirl
    {
        public Bitmap DraCirl(Bitmap b, int x, int y,float  r)
        {
            Bitmap dstImage = (Bitmap)b.Clone();
            try
            {
                Graphics g = Graphics.FromImage(dstImage);
                Pen _penRed = new Pen(Color.Red);

                g.DrawEllipse(_penRed, x - r, y - r, 2 * r, 2 * r);
                g.DrawEllipse(_penRed, x - r + 1, y - r + 1, 2 * r - 2, 2 * r - 2);
                g.DrawEllipse(_penRed, x - r - 1, y - r - 1, 2 * r + 2, 2 * r + 2);
                g.Dispose();
            }
            catch (Exception ex)
            {
                GC.Collect();
            }

         
         
            return dstImage;


        }
    }
}
