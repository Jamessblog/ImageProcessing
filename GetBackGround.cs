using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class GetBackGround
    {
        public void BGPoint(Bitmap b, int X, int Y, float R, out Point positionPoint,out Color BackGroundColor)
        { 
            Point _positionPoint=new Point (0,0);
            Color _backGroundColor;
             //Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            int rr, gg, bb;
            int Rbg, Gbg, Bbg;

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

            if (b != null)
            {
                bool getBG = false;
                Bitmap dstImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
                dstImage = (Bitmap)b.Clone();

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
                            if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)//如果x^2+y^2<r^2
                            {
                                bb = src[0];
                                gg = src[1];
                                rr = src[2];
                                gg = gg / 3;
                                rr = rr / 3;
                                bb = bb / 3;
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

                    for (int i = 0; i < width; i++)//
                    {
                        //纵向定位当前点指针


                        for (int j = 0; j < height; j++)//从上往下搜索
                        {
                            //dnum = 0;
                            src = nsrc + i * 4 + j * stride;
                            if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)//如果x^2+y^2<r^2
                            {
                                src = nsrc + i * 4 + j * stride;
                                bb = src[0];
                                gg = src[1];
                                rr = src[2];
                                if ((Math.Abs(rr - Rbg) < 3) & (Math.Abs(gg - Gbg) < 3) & (Math.Abs(bb - Bbg) < 3))
                                {
                                    _positionPoint = new Point(i, j);
                                    getBG = true;
                                    break;
                                }
                            }
                        }
                        if (getBG)
                            break;
                    }
                }
            }
            positionPoint = _positionPoint;
            _backGroundColor = Color.FromArgb(Rbg, Gbg, Bbg);
            BackGroundColor = _backGroundColor;
        }        
        
        public void BGPointRectangle(Bitmap bitmap, int X, int Y, int W, int H, out Point positionPoint, out Color BackGroundColor)
        {
            Point _positionPoint = new Point(0, 0);
            Color _backGroundColor;
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            int rr, gg, bb;
            int Rbg, Gbg, Bbg;

            Rbg = Gbg = Bbg = 0;//初始化；

            int[] Rc = new int[256];//定义Rc的容量为256；
            int[] Gc = new int[256];
            int[] Bc = new int[256];
            for (int i = 0; i < 256; i++)//全部置0；
            {
                Rc[i] = 0;
                Gc[i] = 0;
                Bc[i] = 0;
            }

            if (b != null)
            {
                bool getBG = false;//设定getBG初始布尔值为false;
                Bitmap dstImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
                dstImage = (Bitmap)b.Clone();

                BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                unsafe
                {
                    byte* src = (byte*)srcData.Scan0;//源图像左上角当前指针；
                    byte* nsrc;
                    int stride = srcData.Stride;//扫描宽度；
                    int offset = stride - width * 4;
                    nsrc = src;//保存左上角当前指针

                    for (int i = 0; i < width; i++)//
                    {
                        //纵向定位当前点指针
                        for (int j = 0; j < height; j++)//从上往下搜索
                        {
                            //dnum = 0;
                            src = nsrc + i * 4 + j * stride;
                            if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))
                            {
                                bb = src[0];
                                gg = src[1];
                                rr = src[2];
                                gg = gg / 3;
                                rr = rr / 3;
                                bb = bb / 3;
                                Rc[rr]++;
                                Gc[gg]++;
                                Bc[bb]++;
                            }
                        }
                    }
                    for (int i = 0; i < 256; i++)
                    {
                        if (Rc[i] > Rc[Rbg])//一一比较，得到最大值；
                            Rbg = i;
                        if (Gc[i] > Gc[Gbg])
                            Gbg = i;
                        if (Bc[i] > Bc[Bbg])
                            Bbg = i;
                    }
                    Rbg = Rbg * 3;
                    Gbg = Gbg * 3;
                    Bbg = Bbg * 3;
                    _backGroundColor = Color.FromArgb(Rbg, Gbg, Bbg);//得到背景色的RGB值；

                    for (int i = 0; i < width; i++)//
                    {
                        //纵向定位当前点指针
                        for (int j = 0; j < height; j++)//从上往下搜索
                        {
                            //dnum = 0;
                            src = nsrc + i * 4 + j * stride;
                            if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))
                            {
                                src = nsrc + i * 4 + j * stride;
                                bb = src[0];
                                gg = src[1];
                                rr = src[2];
                                if ((Math.Abs(rr - Rbg) < 3) & (Math.Abs(gg - Gbg) < 3) & (Math.Abs(bb - Bbg) < 3))//相差3个灰度级；
                                {
                                    _positionPoint = new Point(i, j);
                                    getBG = true;
                                    break;
                                }
                            }
                        }
                        if (getBG)
                            break;
                    }
                }
            }
            positionPoint = _positionPoint;
            _backGroundColor = Color.FromArgb(Rbg, Gbg, Bbg);
            BackGroundColor = _backGroundColor;
        }

    }
}
