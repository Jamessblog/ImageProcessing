using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class ImageShowingRegion
    {
        public Bitmap RegionShowing(Bitmap b, Point selectPoint, Color showColor,
            Dictionary<int, Dictionary<int, Pixel>> DEdgeSortPoints, Dictionary<int, Pixel> Pixels)
        {
            Bitmap dstImage = (Bitmap)b.Clone();
            int width = b.Width;
            int height = b.Height;
            //Color ColorCross = Color.Red;
            Bitmap showingImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            showingImage = (Bitmap)b.Clone();
            int buffer;
            Dictionary<int, Pixel> edgeSortPoints = new Dictionary<int, Pixel>();
            buffer = Pixels[selectPoint.X + selectPoint.Y * width].AreaTag;
           
            for (int i = 1; i <= DEdgeSortPoints.Count(); i++)
            {
                if (DEdgeSortPoints[i][1].AreaTag == buffer)
                {
                    edgeSortPoints = DEdgeSortPoints[i];
                    break;
                }
            }
            try
            {
                BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                                   ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData shgData = showingImage.LockBits(new Rectangle(0, 0, width, height),
                        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


                // 图像实际处理区域
                unsafe
                {
                    byte* src = (byte*)srcData.Scan0;
                    byte* shg = (byte*)shgData.Scan0;
                    byte* nsrc; byte* nshg;
                    int stride = srcData.Stride;
                    int offset = stride - width * 4;
                    byte* north; byte* south;
                    byte* west; byte* east;
                    nsrc = src;//保存左上角当前指针
                    nshg = shg;

                    for (int i = 0; i < width; i++)//
                    {
                        //纵向定位当前点指针
                        for (int j = 0; j < height; j++)//从上往下搜索
                        {
                            //dnum = 0;
                            src = nsrc + i * 4 + j * stride;
                            shg = nshg + i * 4 + j * stride;

                            shg[0] = src[0];
                            shg[1] = src[1];
                            shg[2] = src[2];
                        }
                    }
                    for (int i = 1; i <= edgeSortPoints.Count(); i++)
                    {
                        shg = nshg + edgeSortPoints[i].X * 4 + edgeSortPoints[i].Y * stride;
                        north = shg - stride;
                        south = shg + stride;
                        east = shg - 4;
                        west = shg + 4;

                        shg[0] = showColor.B;
                        shg[1] = showColor.G;
                        shg[2] = showColor.R;
                        north[0] = showColor.B;
                        north[1] = showColor.G;
                        north[2] = showColor.R;
                        south[0] = showColor.B;
                        south[1] = showColor.G;
                        south[2] = showColor.R;
                        east[0] = showColor.B;
                        east[1] = showColor.G;
                        east[2] = showColor.R;
                        west[0] = showColor.B;
                        west[1] = showColor.G;
                        west[2] = showColor.R;
                    }

                    //for (int i = -1 * width / 100; i <= 1 * width / 100; i++)//画十字
                    //{
                    //    shg = nshg + (selectPoint.X + i) * 4 + selectPoint.Y * stride;
                    //    north = shg - stride;
                    //    south = shg + stride;
                    //    east = shg - 4;
                    //    west = shg + 4;

                    //    shg[0] = ColorCross.B;
                    //    shg[1] = ColorCross.G;
                    //    shg[2] = ColorCross.R;
                    //    north[0] = ColorCross.B;
                    //    north[1] = ColorCross.G;
                    //    north[2] = ColorCross.R;
                    //    south[0] = ColorCross.B;
                    //    south[1] = ColorCross.G;
                    //    south[2] = ColorCross.R;
                    //    east[0] = ColorCross.B;
                    //    east[1] = ColorCross.G;
                    //    east[2] = ColorCross.R;
                    //    west[0] = ColorCross.B;
                    //    west[1] = ColorCross.G;
                    //    west[2] = ColorCross.R;

                    //shg = nshg + selectPoint.X * 4 + (selectPoint.Y + i) * stride;
                    //north = shg - stride;
                    //south = shg + stride;
                    //east = shg - 4;
                    //west = shg + 4;

                    //shg[0] = showColor.B;
                    //shg[1] = showColor.G;
                    //shg[2] = showColor.R;
                    //north[0] = showColor.B;
                    //north[1] = showColor.G;
                    //north[2] = showColor.R;
                    //south[0] = showColor.B;
                    //south[1] = showColor.G;
                    //south[2] = showColor.R;
                    //east[0] = showColor.B;
                    //east[1] = showColor.G;
                    //east[2] = showColor.R;
                    //west[0] = showColor.B;
                    //west[1] = showColor.G;
                    //west[2] = showColor.R;
                    //}
                }
                b.UnlockBits(srcData);
                showingImage.UnlockBits(shgData);
                b.Dispose();
                dstImage = showingImage;
            }
            catch(Exception ex)
            {
                GC.Collect();
            }
            return dstImage;
        }
    }
}
