using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class GetCross
    {
        public void getCrossPoint(Bitmap b, out Point CrossPoint)
        {
            int width = b.Width;
            int height = b.Height;
            Point _crossPoint = new Point(0, 0);
            bool getCrossP = false;
            try
            {
                if (b != null)
                {
                    BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    unsafe
                    {
                        byte* src = (byte*)srcData.Scan0;
                        byte* nsrc;
                        int stride = srcData.Stride;
                        int offset = stride - width * 4;
                        byte* aimx; byte* aimy; byte* aimr;
                        nsrc = src;//保存左上角当前指针

                        for (int i = 1; i < width / 2; i++)
                        {
                            for (int j = 1; j < height / 2; j++)
                            {
                                int sum = 0;
                                src = nsrc + i * 4 + j * stride;
                                for (int m = 0; m < 20; m++)
                                {
                                    aimx = src + m * 4;
                                    aimy = src + m * stride;
                                    aimr = src + (m + 1) * 4 + (m + 1) * stride;
                                    if (aimx[0] == 0 && aimy[0] == 0 && aimr[0] == 255)
                                    {
                                        sum++;
                                    }
                                    if (sum == 20)
                                    {
                                        getCrossP = true;
                                        _crossPoint = new Point(i, j);
                                        break;
                                    }
                                }
                            }
                            if (getCrossP)
                                break;
                        }
                        b.UnlockBits(srcData);
                        b.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            CrossPoint = _crossPoint; 
        }

        public void getStandardRatio(Bitmap b, Point CrossPoint,out float StandardRatio)
        {
            int width = b.Width;
            int height = b.Height;
            Point _crossPoint = new Point(0, 0);
            _crossPoint = CrossPoint;
            int x; int y;
            x = CrossPoint.X; y = CrossPoint.Y;
            int sum = 0;
            try
            {
                if (b != null)
                {
                    BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    unsafe
                    {
                        byte* src = (byte*)srcData.Scan0;
                        byte* nsrc;
                        int stride = srcData.Stride;
                        int offset = stride - width * 4;
                        nsrc = src;//保存左上角当前指针

                        for (int i = x; i < width / 2; i++)
                        {
                            //for (int j = y; j < height / 2; j++)
                            //{
                            src = nsrc + i * 4 + y * stride;
                            if (src[0] == 0)
                            {
                                sum++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    b.UnlockBits(srcData);
                    b.Dispose();
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            StandardRatio =(float) 1000 / (float)sum ;
        }


        //getStandardRatio

    }
}
