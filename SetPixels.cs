using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class SetPixels
    {
        public int AreaTagCount;

        public void SetPixel(Bitmap bitmap, out Dictionary<int, Pixel> Pixels)
        {
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            Dictionary<int, Pixel> pixels=new Dictionary<int,Pixel> ();
            //pixels.Clear();
            try {
                if (bitmap != null)
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
                                src = nsrc + i * 4 + j * stride;
                                Pixel pixel = new Pixel();
                                pixel.X = i;
                                pixel.Y = j;
                                pixel.ColorB = src[0];
                                pixel.ColorG = src[1];
                                pixel.ColorR = src[2];
                                pixel.BinaryzationValue = 0;
                                pixel.BinaryzationValue2 = 0;
                                pixel.FirstEdge = 0;
                                pixel.SecondEdge = 0;
                                pixel.SegLine = 0;
                                pixel.AreaTag = 0;
                                pixel.EdgeTag = 0;
                                pixel.PerimeterTag = 0;
                                pixels.Add(i + j * width, pixel);

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
 
            Pixels = pixels;
        }

        public Dictionary<int, Pixel> SetBinaryValue(Bitmap bitmap, Dictionary<int, Pixel> Pixels,out int _time)
        {

            long start, end;	//时钟周期
            long frequency;		//时钟频率
            timecount.QueryPerformanceFrequency(out frequency);
            timecount.QueryPerformanceCounter(out start);//开始时间////////

            Dictionary<int, Pixel> pixels = Pixels;

            int width = bitmap.Width;
            int height = bitmap.Height;
            int r;

            if (bitmap != null)
            {
                BitmapData srcData = bitmap.LockBits(new Rectangle(0, 0, width, height),
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
                            src = nsrc + i * 4 + j * stride;
                            r = src[0];
                            if (r == 0 && pixels[i + j * width].SegLine == 0)
                                pixels[i + j * width].BinaryzationValue = 1;
                            else
                                pixels[i + j * width].BinaryzationValue = 0;
                        }
                    }
                }
            }
            timecount.QueryPerformanceCounter(out end);//结束时间/////////////
            //显示时间
            _time = (int)((end - start) * 1000 / frequency);
            return pixels;
        }

        public Dictionary<int, Pixel> SetAreaTag(Bitmap bitmap,int X, int Y, float R, Dictionary<int, Pixel> Pixels)
        {

            //long start, end;	//时钟周期
            //long frequency;		//时钟频率
            //timecount.QueryPerformanceFrequency(out frequency);
            //timecount.QueryPerformanceCounter(out start);//开始时间////////

            Dictionary<int, Pixel> pixels = Pixels;
            

            int width = bitmap.Width;
            int height = bitmap.Height;

            AreaTagCount = 1;

            for (int i = 1; i < width; i++)//
            {
                for (int j = 1; j < height; j++)//从上往下搜索
                {
                    if ((i - X) * (i - X) + (j - Y) * (j - Y) >= R * R)//如果x^2+y^2>=r^2
                    {
                        pixels[i + j * width].AreaTag = 0;
                    }
                    //else if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)
                    else
                    {
                        if (i == 1 && pixels[i + j * width].BinaryzationValue == 1)
                            pixels[i + j * width].AreaTag = AreaTagCount;
                        else if (pixels[i + j * width].BinaryzationValue == 1)
                            pixels[i + j * width].AreaTag = AreaTagCount;
                        else if (pixels[i + j * width].BinaryzationValue == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                        {
                            pixels[i + j * width].AreaTag = 0;
                            AreaTagCount++;
                        }
                        else if (pixels[i + j * width].BinaryzationValue == 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                            pixels[i + j * width].AreaTag = 0;

                        if (i == (width - 1) && pixels[i + j * width].BinaryzationValue == 1)
                        {
                            pixels[i + j * width].AreaTag = 0;
                            AreaTagCount++;
                        }
                    }
                }
            }
             for (int i = 0; i < width; i++)//
            {
                for (int j = 0; j < height; j++)//从上往下搜索
                {
                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);
                        }

                        if (j != 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i - 1 + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag));                       
                        }
                    }
                }
            }


            for (int i = width -1; i >=0; i--)//
            {
                for (int j = height -1; j >=0 ; j--)//从上往下搜索
                {
                    if (i != width -1)
                    {
                        if (j == height -1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);
                        }

                        if (j != height -1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag == 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i + 1 + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag));
                        }
                    }
                }
            }

            for (int i = 0; i < width; i++)//
            {
                for (int j = height -1; j >=0; j--)//从上往下搜索
                {
                    if (i != 0)
                    {
                        if (j == height -1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);
                        }

                        if (j != height -1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag == 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, 
                                    Math.Min(pixels[i - 1 + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag));
                        }
                    }
                }
            }

            for (int i = width -1; i >=0; i--)//
            {
                for (int j = 0; j < height; j++)//从上往下搜索
                {
                    if (i != width -1)
                    {
                        if (j == 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);
                        }

                        if (j != 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, 
                                    Math.Min(pixels[i + 1 + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag));
                        }
                    }
                }
            }
            for (int i = 0; i < width; i++)//
            {
                for (int j = 0; j < height; j++)//从上往下搜索
                {
                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);
                        }

                        if (j != 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i - 1 + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag));
                        }
                    }
                }
            }
            for (int i = 0; i < width; i++)//
            {
                for (int j = height - 1; j >= 0; j--)//从上往下搜索
                {
                    if (i != 0)
                    {
                        if (j == height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);
                        }

                        if (j != height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag == 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i - 1 + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag));
                        }
                    }
                }
            }
            for (int i = width - 1; i >= 0; i--)//
            {
                for (int j = height - 1; j >= 0; j--)//从上往下搜索
                {
                    if (i != width - 1)
                    {
                        if (j == height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);
                        }

                        if (j != height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag == 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i + 1 + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag));
                        }
                    }
                }
            }
            for (int i = width - 1; i >= 0; i--)//
            {
                for (int j = 0; j < height; j++)//从上往下搜索
                {
                    if (i != width - 1)
                    {
                        if (j == 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);
                        }

                        if (j != 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i + 1 + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag));
                        }
                    }
                }
            }

            //timecount.QueryPerformanceCounter(out end);//结束时间/////////////
            ////显示时间
            //_time = (int)((end - start) * 1000 / frequency);
            return pixels;
            //sum = AreaTagCount;
        }

        public Dictionary<int, Pixel> SetAreaTagRectangle(Bitmap bitmap, int X, int Y, int W, int H, Dictionary<int, Pixel> Pixels)
        {
            Dictionary<int, Pixel> pixels = Pixels;

            int width = bitmap.Width;
            int height = bitmap.Height;

            AreaTagCount = 1;

            for (int i = 0; i < width; i++)//
            {
                for (int j = 0; j < height; j++)//从上往下搜索
                {
                    if ((i - X <= 0) | (i - X >= W) | (j - Y <= 0) | (j - Y >= H))
                    {
                        pixels[i + j * width].AreaTag = 0;
                    }
                    else if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))
                    {
                        if (i == 0 && pixels[i + j * width].BinaryzationValue == 1)
                            pixels[i + j * width].AreaTag = AreaTagCount;
                        else if (pixels[i + j * width].BinaryzationValue == 1)
                            pixels[i + j * width].AreaTag = AreaTagCount;
                        else if (pixels[i + j * width].BinaryzationValue == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                            AreaTagCount++;
                        else if (pixels[i + j * width].BinaryzationValue == 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                            pixels[i + j * width].AreaTag = 0;

                        if (i == (width - 1) && pixels[i + j * width].BinaryzationValue == 1)
                            AreaTagCount++;
                    }
                }
            }

            for (int i = 0; i < width; i++)//
            {
                for (int j = 0; j < height; j++)//从上往下搜索
                {
                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);
                        }

                        if (j != 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i - 1 + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag));
                        }
                    }
                }
            }


            for (int i = width - 1; i >= 0; i--)//
            {
                for (int j = height - 1; j >= 0; j--)//从上往下搜索
                {
                    if (i != width - 1)
                    {
                        if (j == height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);
                        }

                        if (j != height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag == 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i + 1 + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag));
                        }
                    }
                }
            }

            for (int i = 0; i < width; i++)//
            {
                for (int j = height - 1; j >= 0; j--)//从上往下搜索
                {
                    if (i != 0)
                    {
                        if (j == height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);
                        }

                        if (j != height - 1)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i - 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag == 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i - 1 + j * width].AreaTag != 0 && pixels[i + (j + 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i - 1 + j * width].AreaTag, pixels[i + (j + 1) * width].AreaTag));
                        }
                    }
                }
            }

            for (int i = width - 1; i >= 0; i--)//
            {
                for (int j = 0; j < height; j++)//从上往下搜索
                {
                    if (i != width - 1)
                    {
                        if (j == 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);
                        }

                        if (j != 0)
                        {
                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag == 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + 1 + j * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag == 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag);

                            if (pixels[i + j * width].AreaTag != 0 && pixels[i + 1 + j * width].AreaTag != 0 && pixels[i + (j - 1) * width].AreaTag != 0)
                                pixels[i + j * width].AreaTag = Math.Min(pixels[i + j * width].AreaTag,
                                    Math.Min(pixels[i + 1 + j * width].AreaTag, pixels[i + (j - 1) * width].AreaTag));
                        }
                    }
                }
            }

            return pixels;
        }

    }
}
