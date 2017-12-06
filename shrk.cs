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
    class shrk
    {
        public Bitmap shrink(Bitmap b)
        {
            Bitmap e = (Bitmap)b.Clone();
            Bitmap dstImage = (Bitmap)b.Clone();
            int width = e.Width;
            int height = e.Height;
            // int graying;
            Bitmap expImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            expImage = (Bitmap)b.Clone();
            try{
            BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
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

                // 源图像周围八点指针
                byte* aim;

                // int dnw, dn, dne, dw, d, dest, dsw, ds, dse;

                nsrc = src;//保存左上角当前指针
                nexp = exp;

                for (int i = 1; i < width - 1; i++)
                {
                    for (int j = 1; j < height - 1; j++)
                    {
                        src = nsrc + i * 4 + j * stride;
                        exp = nexp + i * 4 + j * stride;
                        for (int m = 0; m < 3; m++)
                        {
                            for (int n = 0; n < 3; n++)
                            {
                                aim = src + (m - 1) * 4 + (n - 1) * stride;
                                if (aim[0] == 255)
                                {
                                    exp[0] = exp[1] = exp[2] = 255;
                                    break;
                                }
                            }
                        }
                    }
                }


            }

            b.UnlockBits(srcData);
            expImage.UnlockBits(expData);
            b.Dispose();
            dstImage = expImage;
        }
                catch(Exception ex)
            {
                GC.Collect();
            }
            return dstImage;
        }

        public Bitmap shrinkRectangle(Bitmap b)
        {
            Bitmap e = (Bitmap)b.Clone();
            Bitmap dstImage = (Bitmap)b.Clone();
            int width = e.Width;
            int height = e.Height;
            // int graying;

            Bitmap expImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            expImage = (Bitmap)b.Clone();
            try{
            BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
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

                // 源图像周围八点指针

                byte* aim;
                // int dnw, dn, dne, dw, d, dest, dsw, ds, dse;

                nsrc = src;//保存左上角当前指针
                nexp = exp;
                for (int i = 2; i < width - 2; i++)
                {
                    for (int j = 2; j < height - 2; j++)
                    {
                        src = nsrc + i * 4 + j * stride;
                        exp = nexp + i * 4 + j * stride;
                        for (int m = 0; m < 5; m++)
                        {
                            for (int n = 0; n < 5; n++)
                            {
                                aim = src + (m - 2) * 4 + (n - 2) * stride;
                                if (aim[0] == 255)
                                {
                                    exp[0] = exp[1] = exp[2] = 255;
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            b.UnlockBits(srcData);
            expImage.UnlockBits(expData);
            b.Dispose();
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
