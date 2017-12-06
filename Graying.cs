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
    class Graying
    {
        public Bitmap graying(Bitmap b)
        {
            Bitmap gray = (Bitmap)b.Clone();
            Bitmap dstgrayImage = (Bitmap)b.Clone();
            int width = gray.Width;
            int height = gray.Height;
            int bytes = width * height;

            Bitmap expImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            expImage = (Bitmap)b.Clone();
            try
            {
                BitmapData srcData = gray.LockBits(new Rectangle(0, 0, width, height),
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
                    nsrc = src;//保存左上角当前指针
                    nexp = exp;
                    int _mean;
                    for (int i = 1; i < width; i++)
                    {
                        for (int j = 1; j < height; j++)
                        {
                            src = nsrc + i * 4 + j * stride;
                            exp = nexp + i * 4 + j * stride;
                            _mean = (int)(src[0] * 0.114 + src[1] * 0.587 + src[2] * 0.299);
                            exp[0] = exp[1] = exp[2];
                        }
                    }
                }
                gray.UnlockBits(srcData);
                expImage.UnlockBits(expData);
                gray.Dispose();
                dstgrayImage = expImage;
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            return dstgrayImage;
        }
    }
}
