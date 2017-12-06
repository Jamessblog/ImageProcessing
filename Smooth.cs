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
    class Smooth
    {
        public Bitmap smooth(Bitmap b)
        {
            //long start, end;	//时钟周期
            //long frequency;		//时钟频率
            //timecount.QueryPerformanceFrequency(out frequency);
            //timecount.QueryPerformanceCounter(out start);//开始时间////////

            Bitmap s1 = (Bitmap)b.Clone();
            Bitmap dstImage = (Bitmap)b.Clone();
            int width = s1.Width;
            int height = s1.Height;
            int bytes = width * height;

            Bitmap expImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            expImage = (Bitmap)b.Clone();
            try
            {
                BitmapData srcData = s1.LockBits(new Rectangle(0, 0, width, height),
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
                    byte* aim1, aim2, aim3, aim4, aim5, aim6, aim7, aim8, aim9, aim10, aim11, aim12, aim13, aim14, aim15, aim16, aim17, aim18, aim19,
                        aim20, aim21, aim22, aim23, aim24, aim25;
                    // int dnw, dn, dne, dw, d, dest, dsw, ds, dse;

                    nsrc = src;//保存左上角当前指针
                    nexp = exp;

                    for (int i = 2; i < width - 2; i++)
                    {
                        for (int j = 2; j < height - 2; j++)
                        {
                            src = nsrc + i * 4 + j * stride;
                            exp = nexp + i * 4 + j * stride;
                            aim1 = src + (-2) * 4 + (-2) * stride;
                            aim2 = src + (-1) * 4 + (-2) * stride;
                            aim3 = src + (0) * 4 + (-2) * stride;
                            aim4 = src + (1) * 4 + (-2) * stride;
                            aim5 = src + (2) * 4 + (-2) * stride;
                            aim6 = src + (-2) * 4 + (-1) * stride;
                            aim7 = src + (-1) * 4 + (-1) * stride;
                            aim8 = src + (0) * 4 + (-1) * stride;
                            aim9 = src + (1) * 4 + (-1) * stride;
                            aim10 = src + (2) * 4 + (-1) * stride;
                            aim11 = src + (-2) * 4 + (0) * stride;
                            aim12 = src + (-1) * 4 + (0) * stride;
                            aim13 = src + (0) * 4 + (0) * stride;
                            aim14 = src + (1) * 4 + (0) * stride;
                            aim15 = src + (2) * 4 + (0) * stride;
                            aim16 = src + (-2) * 4 + (1) * stride;
                            aim17 = src + (-1) * 4 + (1) * stride;
                            aim18 = src + (0) * 4 + (1) * stride;
                            aim19 = src + (1) * 4 + (1) * stride;
                            aim20 = src + (2) * 4 + (1) * stride;
                            aim21 = src + (-2) * 4 + (2) * stride;
                            aim22 = src + (-1) * 4 + (2) * stride;
                            aim23 = src + (0) * 4 + (2) * stride;
                            aim24 = src + (1) * 4 + (2) * stride;
                            aim25 = src + (2) * 4 + (2) * stride;
                            exp[0] = (byte)((aim1[0] + aim2[0] * 4 + aim3[0] * 7 + aim4[0] * 4 + aim5[0]
                                + aim6[0] * 4 + aim7[0] * 16 + aim8[0] * 26 + aim9[0] * 16 + aim10[0] * 4
                                + aim11[0] * 7 + aim12[0] * 26 + aim13[0] * 41 + aim14[0] * 26 + aim15[0] * 7
                                + aim16[0] * 4 + aim17[0] * 16 + aim18[0] * 26 + aim19[0] * 16 + aim20[0] * 4
                                + aim21[0] + aim22[0] * 4 + aim23[0] * 7 + aim24[0] * 4 + aim25[0]) / 273);
                            //exp[1]=exp[2]=exp[0];
                            exp[1] = (byte)((aim1[1] + aim2[1] * 4 + aim3[1] * 7 + aim4[1] * 4 + aim5[1]
                                + aim6[1] * 4 + aim7[1] * 16 + aim8[1] * 26 + aim9[1] * 16 + aim10[1] * 4
                                + aim11[1] * 7 + aim12[1] * 26 + aim13[1] * 41 + aim14[1] * 26 + aim15[1] * 7
                                + aim16[1] * 4 + aim17[1] * 16 + aim18[1] * 26 + aim19[1] * 16 + aim20[1] * 4
                                + aim21[1] + aim22[1] * 4 + aim23[1] * 7 + aim24[1] * 4 + aim25[1]) / 273);
                            exp[2] = (byte)((aim1[2] + aim2[2] * 4 + aim3[2] * 7 + aim4[2] * 4 + aim5[2]
                                + aim6[2] * 4 + aim7[2] * 16 + aim8[2] * 26 + aim9[2] * 16 + aim10[2] * 4
                                + aim11[2] * 7 + aim12[2] * 26 + aim13[2] * 41 + aim14[2] * 26 + aim15[2] * 7
                                + aim16[2] * 4 + aim17[2] * 16 + aim18[2] * 26 + aim19[2] * 16 + aim20[2] * 4
                                + aim21[2] + aim22[2] * 4 + aim23[2] * 7 + aim24[2] * 4 + aim25[2]) / 273);

                        }
                    }
                }
                s1.UnlockBits(srcData);
                expImage.UnlockBits(expData);
                s1.Dispose();
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
