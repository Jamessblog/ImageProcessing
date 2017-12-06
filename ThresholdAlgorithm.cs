using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class ThresholdAlgorithm
    {
        public void ThresholdCalculate(Bitmap bitmap, int X, int Y, float R, out Color CrossBackGroundColor, out Color CrossAverageColor)
        {
            Color _averageColor;
            Color _backGroundColor;
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            int rr, gg, bb;
            int Rbg, Gbg, Bbg;
            double RSum, GSum, BSum;

            RSum = BSum = GSum = 0;
            Rbg = Gbg = Bbg = 0;

            int[] Rc = new int[256];
            int[] Gc = new int[256];
            int[] Bc = new int[256];
            for (int i = 0; i < 255; i++)
            {
                Rc[i] = 0;
                Gc[i] = 0;
                Bc[i] = 0;
            }
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

                        for (int i = 0; i < X; i++)//
                        {
                            //纵向定位当前点指针


                            for (int j = 0; j < Y; j++)//从上往下搜索
                            {
                                //dnum = 0;
                                src = nsrc + i * 4 + j * stride;
                                if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
                                {
                                    bb = src[0];
                                    gg = src[1];
                                    rr = src[2];
                                    gg = gg / 3;
                                    rr = rr / 3;
                                    bb = bb / 3;
                                    RSum += rr;
                                    GSum += gg;
                                    BSum += bb;
                                    Rc[rr]++;
                                    Gc[gg]++;
                                    Bc[bb]++;
                                }
                            }
                        }

                        for (int i = 0; i < 255; i++)
                        {
                            if (Rc[i] > Rc[Rbg])
                                Rbg = i;
                            if (Gc[i] > Gc[Gbg])
                                Gbg = i;
                            if (Bc[i] > Bc[Bbg])
                                Bbg = i;
                        }
                        Rbg = Rbg * 3;
                        Gbg = Gbg * 3;
                        Bbg = Bbg * 3;
                        _backGroundColor = Color.FromArgb(Rbg, Gbg, Bbg);

                    }
                    b.UnlockBits(srcData);
                    b.Dispose();
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            RSum = RSum / (R * R);
            BSum = BSum / (R * R);
            GSum = GSum / (R * R);
            _averageColor = Color.FromArgb((int)RSum ,(int)GSum ,(int)BSum );//////////////////////////////////////////////
            _backGroundColor = Color.FromArgb(Rbg, Gbg, Bbg);
            CrossAverageColor = _averageColor;
            CrossBackGroundColor = _backGroundColor;
        }

        public void ThresholdCalculateRectangle(Bitmap bitmap, int X, int Y, int W, int H, out Color CrossBackGroundColor, out Color CrossAverageColor)
        {
            Color _averageColor;
            Color _backGroundColor;
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            int rr, gg, bb;
            int Rbg, Gbg, Bbg;
            double RSum, GSum, BSum;

            RSum = BSum = GSum = 0;
            Rbg = Gbg = Bbg = 0;

            int[] Rc = new int[256];
            int[] Gc = new int[256];
            int[] Bc = new int[256];
            for (int i = 0; i < 255; i++)
            {
                Rc[i] = 0;
                Gc[i] = 0;
                Bc[i] = 0;
            }
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

                        for (int i = 0; i < width; i++)//
                        {
                            //纵向定位当前点指针


                            for (int j = 0; j < height; j++)//从上往下搜索
                            {
                                //dnum = 0;
                                src = nsrc + i * 4 + j * stride;
                                if ((i - X <= 0) | (i - X >= W) | (j - Y <= 0) | (j - Y >= H))
                                {
                                    bb = src[0];
                                    gg = src[1];
                                    rr = src[2];
                                    gg = gg / 3;
                                    rr = rr / 3;
                                    bb = bb / 3;
                                    RSum += rr;
                                    GSum += gg;
                                    BSum += bb;
                                    Rc[rr]++;
                                    Gc[gg]++;
                                    Bc[bb]++;
                                }
                            }
                        }

                        for (int i = 0; i < 255; i++)
                        {
                            if (Rc[i] > Rc[Rbg])
                                Rbg = i;
                            if (Gc[i] > Gc[Gbg])
                                Gbg = i;
                            if (Bc[i] > Bc[Bbg])
                                Bbg = i;
                        }
                        Rbg = Rbg * 3;
                        Gbg = Gbg * 3;
                        Bbg = Bbg * 3;
                        _backGroundColor = Color.FromArgb(Rbg, Gbg, Bbg);
                    }
                    b.UnlockBits(srcData);
                    b.Dispose();
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            RSum = RSum / (W * H / 3);
            BSum = BSum / (W * H / 3);
            GSum = GSum / (W * H / 3);
            _averageColor = Color.FromArgb((int)RSum, (int)GSum, (int)BSum);
            _backGroundColor = Color.FromArgb(Rbg, Gbg, Bbg);
            CrossAverageColor = _averageColor;
            CrossBackGroundColor = _backGroundColor;
        }

    }
}
