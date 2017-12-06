using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace AutoPickingSys
{
    class Contrast
    {

        //对比度扩展
        public void contrast(Bitmap b,int X, int Y, float R,out int GateColor)
        {
                Bitmap bm = (Bitmap)b.Clone();
                Bitmap dstImage = (Bitmap)b.Clone();
                int iw = bm.Width;
                int ih = bm.Height;
                int x1, y1, x2, y2;
                x1 = 90; y1 = 20; x2 =150; y2 = 220;
                    //int x1 = Convert.ToInt32(dialog.getX1);
                    //int y1 = Convert.ToInt32(dialog.getY1);
                    //int x2 = Convert.ToInt32(dialog.getX2);
                    //int y2 = Convert.ToInt32(dialog.getY2);

                    //计算灰度映射表
                 int[] pixMap = pixelsMap(x1, y1, x2, y2);

                    //线性拉伸

                 Color c = new Color();
                 int r;
                 long _edgeSum = 0;
                 for (int j = 0; j < ih; j++)
                 {
                     for (int i = 0; i < iw; i++)
                     {
                         if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                         {
                             c = bm.GetPixel(i, j);
                             r = pixMap[c.R];
                             if (r >= 255) r = 255;
                             if (r < 0) r = 0;
                             _edgeSum += (long)r;
                         }
                     }
                 }
                 GateColor = (int)(_edgeSum / (long)(R * R));


        }

        //计算灰度映射表
        public int[] pixelsMap(int x1, int y1, int x2, int y2)
        {
            int[] pMap = new int[256];            //映射表

            if (x1 > 0)
            {
                double k1 = y1 / x1;              //第1段钭率k1

                //按第1段钭率k1线性变换
                for (int i = 0; i <= x1; i++)
                    pMap[i] = (int)(k1 * i);
            }

            double k2 = (y2 - y1) / (x2 - x1);    //第2段钭率k2

            //按第2段钭率k2线性变换
            for (int i = x1 + 1; i <= x2; i++)
                if (x2 != x1)
                    pMap[i] = y1 + (int)(k2 * (i - x1));
                else
                    pMap[i] = y1;

            if (x2 < 255)
            {
                double k3 = (255 - y2) / (255 - x2);//第2段钭率k2

                //按第3段钭率k3线性变换
                for (int i = x2 + 1; i < 256; i++)
                    pMap[i] = y2 + (int)(k3 * (i - x2));
            }
            return pMap;
        }

        //对比度扩展
        //public Bitmap stretch(Bitmap bm, int[] map, int iw, int ih,int X,int Y,float R,out int GateColor)
        //{
        //    Color c = new Color();
        //    int r, g, b;
        //    long _edgeSum = 0;
        //    for (int j = 0; j < ih; j++)
        //    {
        //        for (int i = 0; i < iw; i++)
        //        {
        //            if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
        //            {
        //                c = bm.GetPixel(i, j);

        //                r = map[c.R];
        //                g = map[c.G];
        //                b = map[c.B];

        //                if (r >= 255) r = 255;
        //                if (r < 0) r = 0;
        //                //if (g >= 255) g = 255;
        //                //if (g < 0) g = 0;
        //                //if (b >= 255) b = 255;
        //                //if (b < 0) b = 0;
        //                _edgeSum += (long)r;
        //                bm.SetPixel(i, j, Color.FromArgb(r, r, r));
        //            }
        //        }
        //    }
        //    GateColor = (int)(_edgeSum / (long)(R * R));
        //    return bm;
        //}
    }
}
