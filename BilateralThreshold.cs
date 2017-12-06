using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class BilateralThreshold
    {
        public void BilateralThresholdAlgorithm(Bitmap edgeBitmap, Point BeginPosition, int GateColor, int X, int Y, int R,
         ref Dictionary<int, Pixel> Pixels, out Bitmap edgeSegmentBitmap)
        {
            Bitmap edgeSegment = (Bitmap)edgeBitmap.Clone();

            double rate = 1.5;
            int gate, gate1,_gate,_gate1,gatemin;
            gate = (int)(GateColor*3);

            _gate = (int)(GateColor * 0.8);
            gatemin = (int)(GateColor * 0.4);

            gate1 = 120;
            _gate1 = 100;
            int width = edgeBitmap.Width;
            int height = edgeBitmap.Height;

            for (int i = 1; i < width; i++)
            {
                for (int j = 1; j < height; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if (Pixels[i + j * width].EdgeValue >= _gate)
                        {
                            Pixels[i + j * width].FirstEdge = 1;
                            Pixels[i + j * width].SecondEdge = 1;
                        }
                    }
                }
            }

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + (j - 1) * width].FirstEdge == 1 | Pixels[i + (j - 1) * width].FirstEdge == 1 | Pixels[i + 1 + (j - 1) * width].FirstEdge == 1 | Pixels[i - 1 + j * width].FirstEdge == 1
                              | Pixels[i + 1 + j * width].FirstEdge == 1 | Pixels[i - 1 + (j + 1) * width].FirstEdge == 1 | Pixels[i + (j + 1) * width].FirstEdge == 1 | Pixels[i + 1 + (j + 1) * width].FirstEdge == 1)
                              && Pixels[i + j * width].EdgeValue > gatemin && Pixels[i + j * width].EdgeValue < _gate)
                        {
                            Pixels[i + j * width].FirstEdge = 1;
                            Pixels[i + j * width].SecondEdge = 1;
                        }
                    }
                }
            }

            for (int i = width - 2; i > 1; i--)
            {
                for (int j = height - 2; j > 1; j--)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + (j - 1) * width].FirstEdge == 1 | Pixels[i + (j - 1) * width].FirstEdge == 1 | Pixels[i + 1 + (j - 1) * width].FirstEdge == 1 | Pixels[i - 1 + j * width].FirstEdge == 1
                              | Pixels[i + 1 + j * width].FirstEdge == 1 | Pixels[i - 1 + (j + 1) * width].FirstEdge == 1 | Pixels[i + (j + 1) * width].FirstEdge == 1 | Pixels[i + 1 + (j + 1) * width].FirstEdge == 1)
                              && Pixels[i + j * width].EdgeValue > gatemin && Pixels[i + j * width].EdgeValue < _gate)
                        {
                            Pixels[i + j * width].FirstEdge = 1;
                            Pixels[i + j * width].SecondEdge = 1;
                        }
                    }
                }
            }

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) == R * R)//如果x^2+y^2<r^2
                    {
                        Pixels[i + j * width].SecondEdge = 1;
                        Pixels[i + j * width].BinaryzationValue = 1;
                    }
                }
            }

            Pixels[BeginPosition.X + BeginPosition.Y * width].BinaryzationValue = 0;
            Pixels[X-R + Y * width].BinaryzationValue = 0;

       


            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                    if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                        | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                        && Pixels[i + j * width].SecondEdge == 0)
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                    }
                }
            }
            for (int i = width - 2; i > 1; i--)
            {
                for (int j = height - 2; j > 1; j--)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].SecondEdge == 0)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                }
            }
   
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = height - 2; j > 1; j--)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].SecondEdge == 0)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                }
            }

            for (int i = width - 2; i > 1; i--)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].SecondEdge == 0)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                }
            }

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].SecondEdge == 0)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                }
            }
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = height - 2; j > 1; j--)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].SecondEdge == 0)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                }
            }

            for (int i = width - 2; i > 1; i--)
            {
                for (int j = height - 2; j > 1; j--)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].SecondEdge == 0)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                }
            }

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }
            try
            {
                if (edgeBitmap != null)
                {
                    BitmapData srcData = edgeBitmap.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    BitmapData dstData = edgeSegment.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                    unsafe
                    {
                        byte* src = (byte*)srcData.Scan0;
                        byte* dst = (byte*)dstData.Scan0;
                        byte* nsrc; byte* ndst;
                        int stride = srcData.Stride;
                        int offset = stride - width * 4;
                        byte* aim;
                        nsrc = src;//保存左上角当前指针
                        ndst = dst;
                        for (int i = 0; i < width; i++)//将画布涂黑
                        {
                            for (int j = 0; j < height; j++)
                            {
                                src = nsrc + i * 4 + j * stride;
                                dst = ndst + i * 4 + j * stride;
                                dst[0] = dst[1] = dst[2] = 255;

                            }
                        }
                        for (int i = 0; i < width; i++)//
                        {
                            for (int j = 0; j < height; j++)//从上往下搜索
                            {
                                src = nsrc + i * 4 + j * stride;
                                dst = ndst + i * 4 + j * stride;

                                if (Pixels[i + j * width].BinaryzationValue == 0)
                                {
                                    dst[0] = dst[1] = dst[2] = 255;
                                }
                                else
                                {
                                    dst[0] = dst[1] = dst[2] = 0;
                                    //if (Pixels[i + j * width].EdgeValue < 1 )
                                    //{
                                    //    dst[0] = dst[1] = dst[2] = 255;
                                    //}
                                }

                            }
                        }
                    }
                    edgeBitmap.UnlockBits(srcData);
                    edgeSegment.UnlockBits(dstData);
                    edgeBitmap.Dispose();
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            edgeSegmentBitmap = edgeSegment;
        }
    }
}
