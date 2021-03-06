﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class SimExperter
    {
        public Bitmap ExpertTest1(Bitmap b, Color showColor,ref Dictionary<int, Characteristic> Characteristics,
                      Dictionary<int, Dictionary<int, Pixel>> DEdgeSortPoints, Dictionary<int, Pixel> Pixels)
        {
            Dictionary<int, Characteristic> _characteristics = new Dictionary<int, Characteristic>();
            _characteristics = Characteristics;


            int areamax = 0; int areamin = _characteristics[1].Area;
            float ratemax = 0; float ratemin = _characteristics[1].MajToMinAxisRatio;

            for (int i = 1; i <= _characteristics.Count(); i++)
            {
                if (_characteristics[i].IsQualifiedColony == true )
                {
                    if (_characteristics[i].Area >= areamax)
                        areamax = _characteristics[i].Area;
                    if (_characteristics[i].Area <= areamin)
                        areamin = _characteristics[i].Area;
                    if (_characteristics[i].MajToMinAxisRatio >= ratemax)
                        ratemax = _characteristics[i].MajToMinAxisRatio;
                    if (_characteristics[i].MajToMinAxisRatio <= ratemin)
                        ratemin = _characteristics[i].MajToMinAxisRatio;
                }
            }
            for (int i = 1; i <= _characteristics.Count(); i++)
            {
                _characteristics[i].IsQualifiedColony = false;
                if (_characteristics[i].Area <= areamax && _characteristics[i].Area >= areamin && _characteristics[i].MajToMinAxisRatio <= ratemax 
                    && _characteristics[i].MajToMinAxisRatio>=ratemin && _characteristics[i].IsInvalidColony == false)
                    _characteristics[i].IsQualifiedColony = true;

            }


            Bitmap dstImage = (Bitmap)b.Clone();
            int width = b.Width;
            int height = b.Height;


            Bitmap showingImage = new Bitmap(width, height);// (Bitmap)b.Clone();//
            showingImage = (Bitmap)b.Clone();

            //int buffer;

            //Dictionary<int, Characteristic> _characteristics = new Dictionary<int, Characteristic>();
            //_characteristics = Characteristics;
            try{

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


                for (int k = 1; k <= _characteristics.Count(); k++)
                {
                    //if (CharacteristicsValue[i].IsQualifiedColony == true)
                    if (_characteristics[k].IsQualifiedColony == true)
                    {
                        Dictionary<int, Pixel> edgeSortPoints = new Dictionary<int, Pixel>();
                        int edgeX = 0; int edgeY = 0;
                        edgeSortPoints = DEdgeSortPoints[k];
                        edgeX = _characteristics[k].Centroid.X;
                        edgeY = _characteristics[k].Centroid.Y;


                        for (int i = 1; i <= edgeSortPoints.Count(); i++)//画边缘；
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

                        //for (int i = -1 * width / 100; i <= 1 * width / 100; i++)//画十字；
                        //{
                        //    shg = nshg + (edgeX + i) * 4 + edgeY * stride;
                        //    north = shg - stride;
                        //    south = shg + stride;
                        //    east = shg - 4;
                        //    west = shg + 4;

                        //    shg[0] = showColor.B;
                        //    shg[1] = showColor.G;
                        //    shg[2] = showColor.R;
                        //    north[0] = showColor.B;
                        //    north[1] = showColor.G;
                        //    north[2] = showColor.R;
                        //    south[0] = showColor.B;
                        //    south[1] = showColor.G;
                        //    south[2] = showColor.R;
                        //    east[0] = showColor.B;
                        //    east[1] = showColor.G;
                        //    east[2] = showColor.R;
                        //    west[0] = showColor.B;
                        //    west[1] = showColor.G;
                        //    west[2] = showColor.R;

                        //    shg = nshg + edgeX * 4 + (edgeY + i) * stride;
                        //    north = shg - stride;
                        //    south = shg + stride;
                        //    east = shg - 4;
                        //    west = shg + 4;

                        //    shg[0] = showColor.B;
                        //    shg[1] = showColor.G;
                        //    shg[2] = showColor.R;
                        //    north[0] = showColor.B;
                        //    north[1] = showColor.G;
                        //    north[2] = showColor.R;
                        //    south[0] = showColor.B;
                        //    south[1] = showColor.G;
                        //    south[2] = showColor.R;
                        //    east[0] = showColor.B;
                        //    east[1] = showColor.G;
                        //    east[2] = showColor.R;
                        //    west[0] = showColor.B;
                        //    west[1] = showColor.G;
                        //    west[2] = showColor.R;

                        //}
                    }
                }
            }

            b.UnlockBits(srcData);
            showingImage.UnlockBits(shgData);
            b.Dispose();
            dstImage = showingImage;
            Characteristics = _characteristics;
        }
                catch(Exception ex)
            {
                GC.Collect();
            }
            return dstImage;

        }
    }
}
