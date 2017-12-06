using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class RegionGrowing
    {
        public void RegionGrowingAlgorithm(Bitmap edgeBitmap ,Point BeginPosition,int GateColor,int X,int Y,int R,
            ref Dictionary<int, Pixel> Pixels,out Bitmap edgeSegmentBitmap)
        {
            Bitmap edgeSegment = (Bitmap)edgeBitmap.Clone();

            double rate = 0.7;
            int gate,gate1;
            gate= (int)(GateColor*2);
            gate1 =130;
            int width=edgeBitmap .Width ;
            int height=edgeBitmap.Height ;

            //for (int i=0 ;i<width ;i++)
            //{
            //    Pixels[i].BinaryzationValue = 0;
            //    Pixels[i + (height - 1) * width].BinaryzationValue = 0;
            //}

            //for (int i=0 ;i<height ;i++)
            //{
            //    Pixels[i * width].BinaryzationValue = 0;
            //    Pixels[width -1 + i * width].BinaryzationValue =0;
            //}

            Pixels[BeginPosition.X + BeginPosition.Y * width].BinaryzationValue = 0;

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
                    {
                        Pixels[i + j * width].BinaryzationValue = 1;
                    }
                }
            }
            //////////////////
            //for (int i = BeginPosition.X; i < width - 1; i++)
            //{
            //    for (int j = BeginPosition.Y; j < height - 1; j++)
            //    {
            //        if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
            //        {
            //            Pixels[i + j * width].BinaryzationValue = 0;
            //        }
            //        else
            //        {
            //            if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
            //                | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
            //                && Pixels[i + j * width].EdgeValue <= gate)
            //            {
            //                Pixels[i + j * width].BinaryzationValue = 0;
            //            }
            //        }
            //    }
            //}

            //for (int i = BeginPosition.X; i > 0; i--)
            //{
            //    for (int j = BeginPosition.Y; j < height - 1; j++)
            //    {
            //        if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
            //        {
            //            Pixels[i + j * width].BinaryzationValue = 0;
            //        }
            //        else
            //        {
            //            if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
            //                | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
            //                && Pixels[i + j * width].EdgeValue <= gate)
            //            {
            //                Pixels[i + j * width].BinaryzationValue = 0;
            //            }
            //        }
            //    }
            //}

            //for (int i = BeginPosition.X; i < width - 1; i++)
            //{
            //    for (int j = BeginPosition.Y; j >1; j--)
            //    {
            //        if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
            //        {
            //            Pixels[i + j * width].BinaryzationValue = 0;
            //        }
            //        else
            //        {
            //            if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
            //                | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
            //                && Pixels[i + j * width].EdgeValue <= gate)
            //            {
            //                Pixels[i + j * width].BinaryzationValue = 0;
            //            }
            //        }
            //    }
            //}

            //for (int i = BeginPosition.X; i >1; i--)
            //{
            //    for (int j = BeginPosition.Y; j >1; j--)
            //    {
            //        if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
            //        {
            //            Pixels[i + j * width].BinaryzationValue = 0;
            //        }
            //        else
            //        {
            //            if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
            //                | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
            //                && Pixels[i + j * width].EdgeValue <= gate)
            //            {
            //                Pixels[i + j * width].BinaryzationValue = 0;
            //            }
            //        }
            //    }
            //}

            //for (int i = 1; i < width - 1; i++)
            //{
            //    for (int j = 1; j < height - 1; j++)
            //    {
            //        if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
            //        {
            //            Pixels[i + j * width].BinaryzationValue = 0;
            //        }
            //        else
            //        {
            //            if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
            //                | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
            //                && Pixels[i + j * width].EdgeValue <= gate)
            //            {
            //                Pixels[i + j * width].BinaryzationValue = 0;
            //            }
            //        }
            //    }
            //}

            //for (int i = width -2; i > 0; i--)
            //{
            //    for (int j = BeginPosition.Y; j < height - 1; j++)
            //    {
            //        if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
            //        {
            //            Pixels[i + j * width].BinaryzationValue = 0;
            //        }
            //        else
            //        {
            //            if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
            //                | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
            //                && Pixels[i + j * width].EdgeValue <= gate)
            //            {
            //                Pixels[i + j * width].BinaryzationValue = 0;
            //            }
            //        }
            //    }
            //}

            //for (int i = 1; i < width - 1; i++)
            //{
            //    for (int j = height -2; j >0; j--)
            //    {
            //        if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
            //        {
            //            Pixels[i + j * width].BinaryzationValue = 0;
            //        }
            //        else
            //        {
            //            if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
            //                | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
            //                && Pixels[i + j * width].EdgeValue <= gate)
            //            {
            //                Pixels[i + j * width].BinaryzationValue = 0;
            //            }
            //        }
            //    }
            //}
            ///////////////////////////////////////////////////////////////////
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].EdgeValue <= gate)
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
                            && Pixels[i + j * width].EdgeValue <= gate)
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
                            && Pixels[i + j * width].EdgeValue <= gate)
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
                            && Pixels[i + j * width].EdgeValue <= gate)
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
                            && Pixels[i + j * width].EdgeValue <= gate)
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
                            && Pixels[i + j * width].EdgeValue <= gate)
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
                            && Pixels[i + j * width].EdgeValue <= gate)
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
                            && Pixels[i + j * width].EdgeValue <= gate)
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

                        //int aHr, aHg, aHb, aVr, aVg, aVb, aH, aV;
                        //double scale = 0.2;
                        //double A;



                        //byte* nw; byte* n; byte* ne;
                        //byte* w; byte* est;
                        //byte* sw; byte* s; byte* se;

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
                                    dst[0] = dst[1] = dst[2] = 255;
                                else
                                    dst[0] = dst[1] = dst[2] = 0;
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

        public void RegionGrowingAlgorithmRectangle(Bitmap edgeBitmap, Point BeginPosition, int GateColor, int X, int Y, int W, int H,
            ref Dictionary<int, Pixel> Pixels, out Bitmap edgeSegmentBitmap)
        {
            Bitmap edgeSegment = (Bitmap)edgeBitmap.Clone();

            double rate = 0.7;
            int gate;
            gate = (int)(GateColor * rate);

            int width = edgeBitmap.Width;
            int height = edgeBitmap.Height;

            for (int i = 0; i < width; i++)
            {
                Pixels[i].BinaryzationValue = 0;
                Pixels[i + (height - 1) * width].BinaryzationValue = 0;
            }

            for (int i = 0; i < height; i++)
            {
                Pixels[i * width].BinaryzationValue = 0;
                Pixels[width - 1 + i * width].BinaryzationValue = 0;
            }

            Pixels[BeginPosition.X + BeginPosition.Y * width].BinaryzationValue = 0;

            for (int i = BeginPosition.X; i < width - 1; i++)
            {
                for (int j = BeginPosition.Y; j < height - 1; j++)
                {
                    if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2<r^2
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                              | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                              && Pixels[i + j * width].EdgeValue <= gate)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                    else
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }

            for (int i = BeginPosition.X; i > 0; i--)
            {
                for (int j = BeginPosition.Y; j < height - 1; j++)
                {
                    if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2<r^2
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
                            | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
                            && Pixels[i + j * width].EdgeValue <= gate)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                    else
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }

            for (int i = BeginPosition.X; i < width - 1; i++)
            {
                for (int j = BeginPosition.Y; j > 1; j--)
                {
                    if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2<r^2
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
    | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
    && Pixels[i + j * width].EdgeValue <= gate)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                    else
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }

            for (int i = BeginPosition.X; i > 1; i--)
            {
                for (int j = BeginPosition.Y; j > 1; j--)
                {
                    if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2<r^2
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
    | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
    && Pixels[i + j * width].EdgeValue <= gate)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                    else
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2<r^2
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
    | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
    && Pixels[i + j * width].EdgeValue <= gate)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                    else
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }

            for (int i = width - 2; i > 0; i--)
            {
                for (int j = BeginPosition.Y; j < height - 1; j++)
                {
                    if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2<r^2
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
    | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
    && Pixels[i + j * width].EdgeValue <= gate)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                    else
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = height - 2; j > 0; j--)
                {
                    if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2<r^2
                    {
                        if ((Pixels[i - 1 + j * width].BinaryzationValue == 0 | Pixels[i + 1 + j * width].BinaryzationValue == 0
      | Pixels[i + (j - 1) * width].BinaryzationValue == 0 | Pixels[i + (j + 1) * width].BinaryzationValue == 0)
      && Pixels[i + j * width].EdgeValue <= gate)
                        {
                            Pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                    else
                    {
                        Pixels[i + j * width].BinaryzationValue = 0;
                    }
                }
            }
            try{
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

                    //int aHr, aHg, aHb, aVr, aVg, aVb, aH, aV;
                    //double scale = 0.2;
                    //double A;



                    //byte* nw; byte* n; byte* ne;
                    //byte* w; byte* est;
                    //byte* sw; byte* s; byte* se;

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
                                dst[0] = dst[1] = dst[2] = 255;
                            else
                                dst[0] = dst[1] = dst[2] = 0;
                        }
                    }
                }
                edgeBitmap.UnlockBits(srcData);
                edgeSegment.UnlockBits(dstData);
                edgeBitmap.Dispose();
            }
        }
                catch(Exception ex)
            {
                GC.Collect();
            }
            edgeSegmentBitmap = edgeSegment;
        }

    }
}
