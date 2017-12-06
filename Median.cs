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
    class Median
    {
        public Bitmap median(Bitmap b, int X, int Y, float R)
        {
            
            Bitmap s = (Bitmap)b.Clone();
            Bitmap dstImage = (Bitmap)b.Clone();
            int width = s.Width;
            int height = s.Height;
            int bytes = width * height;
        try{
            Bitmap expImage = (Bitmap)b.Clone(); 
            BitmapData srcData = s.LockBits(new Rectangle(0, 0, width, height),
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
                byte* aim, aim2, aim3, aim4, aim5, aim6, aim7, aim8, aim9, aim10, aim11, aim12, aim13, aim14, aim15, aim16, aim17, aim18, aim19,
                    aim20, aim21, aim22, aim23, aim24, aim25;
                // int dnw, dn, dne, dw, d, dest, dsw, ds, dse;

                nsrc = src;//保存左上角当前指针
                nexp = exp;

                for (int i = 2; i < width - 2; i++)
                {
                    int[] dt;
                    for (int j = 2; j < height - 2; j++)
                    {
                        if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)//如果x^2+y^2<r^2
                        {
                            int m = 0, r = 0;
                            dt = new int[25];
                            src = nsrc + i * 4 + j * stride;
                            exp = nexp + i * 4 + j * stride;

                            //取5X5块的25个象素
                            for (int k = -2; k < 3; k++)
                            {
                                for (int l = -2; l < 3; l++)
                                {
                                    //取(i+k,j+l)处的象素，赋于数组dt
                                    aim = src + (k) * 4 + (l) * stride;
                                    int xx;
                                    dt[m] = aim[0];
                                    m++;
                                }
                            }
                            //冒泡排序,输出中值
                            r = median_sorter(dt, 25); //中值           
                            exp[0] = exp[1] = exp[2] = (byte)r;
                        }
                    }
                }
            }
            s.UnlockBits(srcData);
            expImage.UnlockBits(expData);
            s.Dispose();
            dstImage = expImage;
        }
        catch (Exception ex)
        {
            GC.Collect();
        }
            return dstImage;

        }
        public int median_sorter(int[] dt, int m)
        {
            int tem;
            for (int k = m - 1; k >= 1; k--)
                for (int l = 1; l <= k; l++)
                    if (dt[l - 1] > dt[l])
                    {
                        tem = dt[l];
                        dt[l] = dt[l - 1];
                        dt[l - 1] = tem;
                    }
            return dt[(int)(m / 2)];
        }
    }
}
