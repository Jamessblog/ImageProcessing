using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class PlottedCentroidPoint
    {
        public void PlottedCentroid(Bitmap bitmap, Dictionary<int, Characteristic> characteristics ,out Bitmap bitmapWithCentroid)
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

                        nsrc = src;//保存左上角当前指针

                        for (int i = 1; i <= characteristics.Count(); i++)
                        {
                            if (characteristics[i].IsQualifiedColony == true)
                            {
                                for (int j = -8; j < 9; j++)
                                {
                                    //for (int k = -1; k < 2; k++)
                                    //{
                                    dst = ndst + (characteristics[i].Centroid.X + j) * 4 + (characteristics[i].Centroid.Y) * stride;
                                    dst[0] = dst[1] = dst[2] = 0;

                                    dst = ndst + (characteristics[i].Centroid.X) * 4 + (characteristics[i].Centroid.Y + j) * stride;
                                    dst[0] = dst[1] = dst[2] = 0;
                                    //}
                                }
                            }
                        }
                    }

                    b.UnlockBits(srcData);
                    dstImage.UnlockBits(dstData);
                    b.Dispose();
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            bitmapWithCentroid = dstImage;
        }
    }
}
