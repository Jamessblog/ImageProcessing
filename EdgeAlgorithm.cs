using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class EdgeAlgorithm
    {
        public void 
            EdgeBitmapCalculate(Bitmap bitmap, int X, int Y, float R, out Bitmap EdgeBitmap,out int gatecolor, ref Dictionary<int, Pixel> Pixels)
        {
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            long _edgeSum = 0;
            int countA = 256; int[] dt = new int[257]; int AA; int countB = 256;
            int countA1 = 256; int countB1 = 256; int sumA = 0, sumB = 0;
            int theCount = 0;
            for (int i = 0; i < 257; i++)
            {
                dt[i] = 0;
            }

                Bitmap edgeImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
                edgeImage = (Bitmap)bitmap.Clone();
                try
                {
                    if (bitmap != null)
                    {
                        BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                        BitmapData edgeData = edgeImage.LockBits(new Rectangle(0, 0, width, height),
                                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


                        unsafe
                        {
                            byte* src = (byte*)srcData.Scan0;
                            byte* edge = (byte*)edgeData.Scan0;
                            byte* nsrc; byte* nedge;
                            int stride = srcData.Stride;
                            int offset = stride - width * 4;

                            int aHr, aHg, aHb, aVr, aVg, aVb, aH, aV;
                            double scale = 0.062;//gaiguo
                            double A, Ar, Ag, Ab;

                            int a0, a45, a90, a135, a180, a225, a270, a315;
                            double A1, A2, A3, A4, A5, A6, A7;



                            byte* nw; byte* n; byte* ne;
                            byte* w; byte* est;
                            byte* sw; byte* s; byte* se;

                            nsrc = src;//保存左上角当前指针
                            nedge = edge;

                            for (int i = 0; i < width; i++)//将画布涂黑
                            {
                                for (int j = 0; j < height; j++)
                                {
                                    src = nsrc + i * 4 + j * stride;
                                    edge = nedge + i * 4 + j * stride;
                                    edge[0] = edge[1] = edge[2] = 255;

                                }
                            }
                            for (int i = 1; i < width - 1; i++)//
                            {
                                for (int j = 1; j < height - 1; j++)//从上往下搜索
                                {
                                    src = nsrc + i * 4 + j * stride;
                                    edge = nedge + i * 4 + j * stride;

                                    nw = src - stride - 4;   // (x-1, y-1)
                                    n = src - stride;          // (x  , y-1)
                                    ne = src - stride + 4;   // (x+1, y-1)

                                    w = src - 4;             // (x-1, y)
                                    est = src + 4;             // (x+1, y)

                                    sw = src + stride - 4;   // (x-1, y+1)
                                    s = src + stride;          // (x  , y+1)
                                    se = src + stride + 4;   // (x+1, y+1)

                                    if ((i - X) * (i - X) + (j - Y) * (j - Y) < R * R)//如果x^2+y^2<r^2
                                    {
                                        a0 = Math.Abs((nw[0] + n[0] * 2 + ne[0]) - (sw[0] + s[0] * 2 + se[0]));
                                        a45 = Math.Abs((w[0] + nw[0] * 2 + n[0]) - (s[0] + se[0] * 2 + est[0]));
                                        a90 = Math.Abs((nw[0] + w[0] * 2 + sw[0]) - (ne[0] + est[0] * 2 + se[0]));
                                        a135 = Math.Abs((w[0] + sw[0] * 2 + s[0]) - (n[0] + ne[0] * 2 + est[0]));
                                        a180 = Math.Abs((sw[0] + s[0] * 2 + se[0]) - (nw[0] + n[0] * 2 + ne[0]));
                                        a225 = Math.Abs((s[0] + se[0] * 2 + est[0]) - (w[0] + nw[0] * 2 + n[0]));
                                        a270 = Math.Abs((ne[0] + est[0] * 2 + se[0]) - (nw[0] + w[0] * 2 + sw[0]));
                                        a315 = Math.Abs((n[0] + ne[0] * 2 + est[0]) - (w[0] + sw[0] * 2 + s[0]));
                                        //A = (aH + aV) / 2;
                                        A1 = Math.Max(a0, a45);
                                        A2 = Math.Max(A1, a90);
                                        A3 = Math.Max(A2, a135);
                                        A4 = Math.Max(A3, a180);
                                        A5 = Math.Max(A4, a225);
                                        A6 = Math.Max(A5, a270);
                                        A7 = Math.Max(A6, a315);
                                        A = A7;


                                        if (A > 255)
                                        {
                                            A = 255;
                                        }
                                        if (A <= 20)
                                        {
                                            A = 0;
                                        }
                                        if (A > 0)
                                        {
                                            theCount++;
                                            _edgeSum += (long)A;
                                        }

                                        _edgeSum += (long)A;
                                        edge[0] = edge[1] = edge[2] = (byte)A;
                                        Pixels[i + j * width].EdgeValue = (byte)A;
                                        Pixels[i + j * width].BinaryzationValue = 1;
                                        Pixels[i + j * width].FirstEdge = 0;
                                        Pixels[i + j * width].SecondEdge = 0;
                                    }
                                }
                            }
                        }
                        b.UnlockBits(srcData);
                        edgeImage.UnlockBits(edgeData);
                        b.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    GC.Collect();
                }
            EdgeBitmap = edgeImage;
            gatecolor = (int)(_edgeSum / theCount);
        }


        public void EdgeBitmapCalculateRectangle(Bitmap bitmap, int X, int Y, int W, int H, out Bitmap EdgeBitmap, ref Dictionary<int, Pixel> Pixels)
        {
            Bitmap b = (Bitmap)bitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            long _edgeSum = 0;
            double scaleR = 3.14;

            Bitmap edgeImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            edgeImage = (Bitmap)bitmap.Clone();
            try
            {
                if (bitmap != null)//对源图像与目标图像进行锁存；
                {
                    BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    BitmapData edgeData = edgeImage.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


                    unsafe
                    {
                        byte* src = (byte*)srcData.Scan0;//源图像左上角当前指针；
                        byte* edge = (byte*)edgeData.Scan0;//目标图像左上角当前指针；
                        byte* nsrc; byte* nedge;
                        int stride = srcData.Stride;//扫描宽度；
                        int offset = stride - width * 4;

                        int aHr, aHg, aHb, aVr, aVg, aVb, aH, aV;
                        double scale = 0.2;
                        double A;



                        byte* nw; byte* n; byte* ne;
                        byte* w; byte* est;
                        byte* sw; byte* s; byte* se;

                        nsrc = src;//保存左上角当前指针
                        nedge = edge;

                        for (int i = 0; i < width; i++)//将画布涂白
                        {
                            for (int j = 0; j < height; j++)
                            {
                                src = nsrc + i * 4 + j * stride;
                                edge = nedge + i * 4 + j * stride;
                                edge[0] = edge[1] = edge[2] = 255;

                            }
                        }
                        for (int i = 1; i < width; i++)//
                        {
                            for (int j = 1; j < height; j++)//从上往下搜索
                            {
                                src = nsrc + i * 4 + j * stride;
                                edge = nedge + i * 4 + j * stride;

                                nw = src - stride - 4;   // (x-1, y-1)
                                n = src - stride;          // (x  , y-1)
                                ne = src - stride + 4;   // (x+1, y-1)

                                w = src - 4;             // (x-1, y)
                                est = src + 4;             // (x+1, y)

                                sw = src + stride - 4;   // (x-1, y+1)
                                s = src + stride;          // (x  , y+1)
                                se = src + stride + 4;   // (x+1, y+1)
                                if ((i - X > 0) & (i - X < W) & (j - Y > 0) & (j - Y < H))
                                {

                                    aHr = Math.Abs((nw[0] + w[0] * 2 + sw[0]) - (ne[0] + est[0] * 2 + se[0]));

                                    //计算红色分量垂直灰度差

                                    aVr = Math.Abs((nw[0] + n[0] * 2 + ne[0]) - (sw[0] + s[0] * 2 + se[0]));

                                    //计算绿色分量水平灰度差

                                    aHg = Math.Abs((nw[1] + w[1] * 2 + sw[1]) - (ne[1] + est[1] * 2 + se[1]));

                                    //计算绿色分量垂直灰度差

                                    aVg = Math.Abs((nw[1] + n[1] * 2 + ne[1]) - (sw[1] + s[1] * 2 + se[1]));

                                    //计算蓝色分量水平灰度差

                                    aHb = Math.Abs((nw[2] + w[2] * 2 + sw[2]) - (ne[2] + est[2] * 2 + se[2]));

                                    //计算蓝色分量垂直灰度差

                                    aVb = Math.Abs((nw[2] + n[2] * 2 + ne[2]) - (sw[2] + s[2] * 2 + se[2]));
                                    //计算水平综合灰度差

                                    aH = aHr + aHg + aHb;

                                    //计算垂直综合灰度差

                                    aV = aVr + aVg + aVb;

                                    A = (aH + aV) / 2;

                                    A = (int)(A * scale);//图像梯度值；

                                    _edgeSum += (long)A;

                                    edge[0] = edge[1] = edge[2] = (byte)A;

                                    Pixels[i + j * width].EdgeValue = (byte)A;//把对应点的梯度值写入字典；
                                    Pixels[i + j * width].BinaryzationValue = 1;//置对应点的二进制值为1；
                                }
                            }
                        }
                    }
                    b.UnlockBits(srcData);//解锁；
                    edgeImage.UnlockBits(edgeData);
                    b.Dispose();//释放；
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }

            EdgeBitmap = edgeImage;//得到图像直方图；
            //GateColor = (int)(_edgeSum / (long)(W * H / scaleR));//得到边缘阈值为梯度平均值；
        }

    }
}
