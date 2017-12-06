using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class ThresholdSegment
    {
        public void ThresholdSegmentAlgorithm(Bitmap bitmap, int X, int Y, float R, Color BackGroundColor, Color AverageColor, out Bitmap ThresholdBitmap)
        {
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;

            Bitmap srcImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            srcImage = (Bitmap)b.Clone();
            Bitmap dstImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            dstImage = (Bitmap)b.Clone();
            try{
            if (b != null)
            {
                BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
                        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                unsafe
                {
                    byte* src = (byte*)srcData.Scan0;
                    byte* dst = (byte*)dstData.Scan0;
                    byte* nsrc; byte* ndst;
                    int stride = srcData.Stride;
                    int offset = stride - width * 4;

                    nsrc = src;//保存左上角当前指针
                    ndst = dst;

                    for (int i = 0; i < width; i++)//
                    {
                        //纵向定位当前点指针


                        for (int j = 0; j < height; j++)//从上往下搜索
                        {
                            //dnum = 0;
                            src = nsrc + i * 4 + j * stride;
                            dst = ndst + i * 4 + j * stride;
                            if ((i - X) * (i - X) + (j - Y) * (j - Y) <= R * R)//如果x^2+y^2>=r^2
                            {
                                dst[0] = dst[1] = dst[2] = 255;
                            }
                            else if ((i - X) * (i - X) + (j - Y) * (j - Y) > R * R)//如果x^2+y^2<r^2
                            {
                                if ((src[0]<(BackGroundColor.B - Math.Abs(BackGroundColor.B - AverageColor.B))
                                    ||src [0]>(BackGroundColor.B + Math.Abs(BackGroundColor.B - AverageColor.B)))
                                    &&(src [1]<(BackGroundColor.G - Math.Abs(BackGroundColor.G - AverageColor.G))
                                    ||src [1]>(BackGroundColor.G + Math.Abs(BackGroundColor.G - AverageColor.G)))
                                    && (src[2] < (BackGroundColor.R - Math.Abs(BackGroundColor.R - AverageColor.R))
                                    ||src [2]>(BackGroundColor.R + Math.Abs(BackGroundColor.R - AverageColor.R)))
                                    )
                                    dst[0] = dst[1] = dst[2] = 0;
                                else
                                    dst[0] = dst[1] = dst[2] = 255;
                            }
                        }
                    }
                    b.UnlockBits(srcData);
                    dstImage.UnlockBits(dstData);
                    b.Dispose();
                }
            }
        }
                catch(Exception ex)
            {
                GC.Collect();
            }
            ThresholdBitmap = dstImage;
        }


        public void ThresholdSegmentAlgorithmRectangle(Bitmap bitmap, int X, int Y, int W, int H, Color BackGroundColor, Color AverageColor, out Bitmap ThresholdBitmap)
        {
          
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
       
            Bitmap srcImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            srcImage = (Bitmap)b.Clone();
            Bitmap dstImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            dstImage = (Bitmap)b.Clone();
             try
             {
            if (b != null)
            {
            
                    BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    BitmapData dstData = dstImage.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                
              
                unsafe
                {
                    byte* src = (byte*)srcData.Scan0;
                    byte* dst = (byte*)dstData.Scan0;
                    byte* nsrc; byte* ndst;
                    int stride = srcData.Stride;
                    int offset = stride - width * 4;

                    nsrc = src;//保存左上角当前指针
                    ndst = dst;

                    for (int i = 0; i < width; i++)//
                    {
                        //纵向定位当前点指针


                        for (int j = 0; j < height; j++)//从上往下搜索
                        {
                            //dnum = 0;
                            src = nsrc + i * 4 + j * stride;
                            dst = ndst + i * 4 + j * stride;
                            if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))//如果x^2+y^2>=r^2
                            {
                                dst[0] = dst[1] = dst[2] = 255;
                            }
                            else if ((i - X <= 0) | (i - X >= W) | (j - Y <= 0) | (j - Y >= H))//如果x^2+y^2<r^2
                            {
                                if ((src[0] < (BackGroundColor.B - Math.Abs(BackGroundColor.B - AverageColor.B))
                                    || src[0] > (BackGroundColor.B + Math.Abs(BackGroundColor.B - AverageColor.B)))
                                    && (src[1] < (BackGroundColor.G - Math.Abs(BackGroundColor.G - AverageColor.G))
                                    || src[1] > (BackGroundColor.G + Math.Abs(BackGroundColor.G - AverageColor.G)))
                                    && (src[2] < (BackGroundColor.R - Math.Abs(BackGroundColor.R - AverageColor.R))
                                    || src[2] > (BackGroundColor.R + Math.Abs(BackGroundColor.R - AverageColor.R)))
                                    )
                                    dst[0] = dst[1] = dst[2] = 0;
                                else
                                    dst[0] = dst[1] = dst[2] = 255;
                            }
                        }
                    }
                    b.UnlockBits(srcData);
                    dstImage.UnlockBits(dstData);
                    b.Dispose();    
                }
            }
             }
              catch(Exception ex)
                {
                    GC.Collect();
                }
             ThresholdBitmap = dstImage;
            }
        }
    }

