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
    class Sharpen
    {
        public Bitmap sharpen(Bitmap b)
        {
            Bitmap s2 = (Bitmap)b.Clone();
            Bitmap dstImage = (Bitmap)b.Clone();
            int width = s2.Width;
            int height = s2.Height;
            int bytes = width * height;

            Bitmap expImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            expImage = (Bitmap)b.Clone();
            try{
            BitmapData srcData = s2.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData expData = expImage.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            // 图像实际处理区域
            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                byte* exp = (byte*)expData.Scan0;
                byte* nsrc; byte* nexp;
                int stride = srcData.Stride;
                int offset = stride - width * 4;
                byte* aim1, aim2, aim3, aim4, aim;
                nsrc = src;//保存左上角当前指针
                nexp = exp;

                for (int i = 1; i < width - 1; i++)
                {
                    for (int j = 1; j < height - 1; j++)
                    {
                        src = nsrc + i * 4 + j * stride;
                        exp = nexp + i * 4 + j * stride;
                        aim = src;
                        aim1 = src + (-1) * 4 + (0) * stride;
                        aim2 = src + (0) * 4 + (-1) * stride;
                        aim3 = src + (0) * 4 + (1) * stride;
                        aim4 = src + (1) * 4 + (0) * stride;
                        exp[0] = (byte)(aim[0] * 5 - (aim1[0] + aim2[0] + aim3[0] + aim4[0]));
                        exp[1] = (byte)(aim[1] * 5 - (aim1[1] + aim2[1] + aim3[1] + aim4[1]));
                        exp[2] = (byte)(aim[2] * 5 - (aim1[2] + aim2[2] + aim3[2] + aim4[2]));
                        //exp[0] =(byte) (aim[0] * 4 - (aim1[0] + aim2[0] + aim3[0] + aim4[0])+aim[0]);
                        //exp[1] = (byte)(aim[1] * 4 - (aim1[1] + aim2[1] + aim3[1] + aim4[1])+aim[1]);
                        //exp[2] = (byte)(aim[2] * 4 - (aim1[2] + aim2[2] + aim3[2] + aim4[2])+aim[2]);
                    }
                }
            }
            s2.UnlockBits(srcData);
            expImage.UnlockBits(expData);
            s2.Dispose();
            dstImage = expImage;
        }
                catch(Exception ex)
            {
                GC.Collect();
            }
            return dstImage;
        }
    }
}
