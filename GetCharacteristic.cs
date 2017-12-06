using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    class GetCharacteristic
    {
        public void AreaStatistics(Bitmap bitmap,int AreaTageCount ,ref Dictionary<int, Pixel> Pixels,out Dictionary<int, Dictionary<int, Pixel>> DEdgePoints,
            out Dictionary<int, Dictionary<int, Pixel>> DEdgeSortPoints,out Dictionary<int, Characteristic> Characteristics)
        {

            //long start, end;	//时钟周期
            //long frequency;		//时钟频率
            //timecount.QueryPerformanceFrequency(out frequency);
            //timecount.QueryPerformanceCounter(out start);//开始时间////////

            Dictionary<int, Characteristic> characteristics=new Dictionary<int,Characteristic> ();
            Dictionary<int, Dictionary<int, Pixel>> dEdgePoints = new Dictionary<int, Dictionary<int, Pixel>>();
            Dictionary<int, Dictionary<int, Pixel>> dEdgeSortPoints=new Dictionary<int,Dictionary<int,Pixel>> ();
            Dictionary<int, Dictionary<int, Pixel>> dAreaCollection = new Dictionary<int, Dictionary<int, Pixel>>();

            int areaSum = 0;
            int effectAreaCount = 0;
            int averageArea = 0;
            int sortTag = 1;
            int dAreaCollectionTage = 0;
            int thearea = 0; int maxareatag = 1;
            int[] maxarea = new int[AreaTageCount+1];
            int sum1 = 0, sum2 = 0;
            //for (int i = 0; i < AreaTageCount; i++)
            //{
            //    maxarea[i] = 0;
            //}//求面积直方图用到；

            for (int i = 1; i <= AreaTageCount; i++)
            {
                Dictionary<int, Pixel> areaCollection = new Dictionary<int, Pixel>();
                dAreaCollection.Add(i, areaCollection);
            }
//////////////////////////////////////////////////////以上耗时：0；
            int width = bitmap.Width;
            int height = bitmap.Height;


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (Pixels [i+j*width].AreaTag !=0)
                    dAreaCollection[Pixels[i + j * width].AreaTag].Add(i + j * width, Pixels[i + j * width]);
                }
            }
            /////////////////////////////////////////////////////以上耗时122；
            for (int i = 1; i <= AreaTageCount; i++)
            {

                if (dAreaCollection[i].Count() >= 5)
                {
                    areaSum += dAreaCollection[i].Count();
                    effectAreaCount++;
                }
            }

            if (effectAreaCount != 0)
                averageArea = areaSum / effectAreaCount;
            ///////////////////////////////////////////////////以上耗时0；
            //求面积直方图，耗时3936太长，暂时保留处理；
            ////////////////////////////////////////////////////////////////////////////////
            //for (int i = 1; i <= AreaTageCount; i++)
            //{
            //    thearea = dAreaCollection[i].Count();
            //    sum1 = (int)(thearea * 0.8); sum2 = (int)(thearea * 1.2);
            //    for (int j = 0; j <= AreaTageCount - i; j++)
            //    {
            //        if (dAreaCollection[i + j].Count() >= sum1 && dAreaCollection[i + j].Count() <= sum2&& thearea>=200)
            //        {
            //            maxarea[i]++;
            //        }
            //    }
            //}
            /////////////////////////////////////////////////////////以上耗时3936！！！！；

            //for (int i = 1; i <= AreaTageCount; i++)
            //{
            //    if (maxarea[i] > maxarea[maxareatag])
            //        maxareatag = i;
            //}
            //thearea = dAreaCollection[maxareatag].Count();//求面积直方图用；

            for (int i = 1; i <= AreaTageCount; i++)
            {
                //thearea = dAreaCollection[maxareatag].Count();
                if (dAreaCollection[i].Count() < averageArea / 15 || dAreaCollection[i].Count() > averageArea * 10)
                {
                    if (dAreaCollection[i].Count() != 0)
                    {
                        foreach (KeyValuePair<int, Pixel> item in dAreaCollection[i])
                        {
                            Pixels[item.Key].AreaTag = 0;
                        }
                    }
                    dAreaCollection.Remove(i);
                }
                else if (dAreaCollection[i].Count() != 0)
                {
                    int CentroidXSum = 0;
                    int CentroidYSum = 0;
                    int CentroidX = 0;
                    int CentroidY = 0;

                    foreach (KeyValuePair<int, Pixel> item in dAreaCollection[i])
                    {
                        CentroidXSum += Pixels[item.Key].X;
                        CentroidYSum += Pixels[item.Key].Y;
                    }

                    CentroidX = CentroidXSum / dAreaCollection[i].Count();
                    CentroidY = CentroidYSum / dAreaCollection[i].Count();
                    ////去除极不规则区域（质心在目标区域外）
                    //if (!dAreaCollection[i].ContainsKey(CentroidX + CentroidY * width))
                    //{
                    //    foreach (KeyValuePair<int, Pixel> item in dAreaCollection[i])
                    //    {
                    //        Pixels[item.Key].AreaTag = 0;
                    //    }
                    //    dAreaCollection.Remove(i);
                    //}
                }
            }
            while (sortTag <= dAreaCollection.Count() && dAreaCollectionTage <= AreaTageCount)
            {
                if (dAreaCollection.ContainsKey(dAreaCollectionTage))
                {
                    int edgeSortTag=1;
                    int CentroidXSum = 0;
                    int CentroidYSum = 0;
                    int CentroidX = 0;
                    int CentroidY = 0;
                    //int expX = 0; int expY = 0;
                    //float yita = 0;
                    Dictionary<int, Pixel> edgePoints = new Dictionary<int, Pixel>();
                    Dictionary<int, Pixel> edgeSortPoints = new Dictionary<int, Pixel>();
                    foreach (KeyValuePair<int, Pixel> item in dAreaCollection[dAreaCollectionTage])
                    {
                        if (!(dAreaCollection[dAreaCollectionTage].ContainsKey (item.Key -1) && dAreaCollection[dAreaCollectionTage].ContainsKey (item.Key +1)
                            && dAreaCollection[dAreaCollectionTage].ContainsKey (item.Key -width ) && dAreaCollection[dAreaCollectionTage].ContainsKey (item.Key +width)))
                        {
                            Pixels[item.Key].EdgeTag = sortTag;
                            edgePoints.Add(item.Key, Pixels[item.Key]);
                            edgeSortPoints.Add(edgeSortTag, Pixels[item.Key]);
                            edgeSortTag++;
                        }

                        Pixels[item.Key].AreaTag = sortTag;
                        CentroidXSum += Pixels[item.Key].X;
                        CentroidYSum += Pixels[item.Key].Y;
                        //expX = Pixels[item.Key].X; expY = Pixels[item.Key].Y;
                        //yita += (float)((Math.Exp(-(expX * expX + expY * expY) / 2)) / (1 / Math.Sqrt(2 * Math.PI)));
                    }
                    
                    dEdgePoints.Add(sortTag, edgePoints);
                    dEdgeSortPoints.Add(sortTag, edgeSortPoints);
                    //2016/4/27
                    CentroidX = CentroidXSum / dAreaCollection[dAreaCollectionTage].Count();
                    CentroidY = CentroidYSum / dAreaCollection[dAreaCollectionTage].Count();
                    Point Centroid = new Point(CentroidX, CentroidY);
                    Characteristic characteristic = new Characteristic();
                    characteristic.Area = dAreaCollection[dAreaCollectionTage].Count();
                    characteristic.AreaTage = dAreaCollectionTage;
                    characteristic.Centroid = Centroid;
                    characteristic.CentreAcerageColor = Color.White;
                    characteristic.MajorAxis = 0;
                    characteristic.MinorAxis = 0;
                    characteristic.MajToMinAxisRatio = 1;
                    //characteristic.Yita=yita;
                    characteristic.Perimeter = 0;
                    characteristic.IsQualifiedColony = false;
                    characteristic.IsInvalidColony = false;
                    characteristics.Add(sortTag, characteristic);
                    sortTag++;
                }

                dAreaCollectionTage++;
            }

            DEdgePoints = dEdgePoints;
            DEdgeSortPoints = dEdgeSortPoints;
            Characteristics = characteristics;

            //timecount.QueryPerformanceCounter(out end);//结束时间/////////////
            ////显示时间
            //_time = (int)((end - start) * 1000 / frequency);
        }

        public Dictionary<int, Characteristic> GetMaxMinAxis(Dictionary<int, Dictionary<int, Pixel>> DEdgeSortPoints, Dictionary<int, Characteristic> Characteristics)
        {

            //long start, end;	//时钟周期
            //long frequency;		//时钟频率
            //timecount.QueryPerformanceFrequency(out frequency);
            //timecount.QueryPerformanceCounter(out start);//开始时间////////

            Dictionary<int, Characteristic> _characteristic = Characteristics;

            for (int i = 1; i <= DEdgeSortPoints.Count(); i++)
            {
                int majorAxis=0;
                int minorAxis = 0;
                int distance = 0; int distanceD = 0; int distanceE = 0;
                int positiveDistance=0;
                int nagitiveDistance = 0;
                float majToMinAxisRatio=1;
                float _k=0;
                int sum = 0;
                Point A = new Point();
                Point B = new Point();
                Point C = new Point(); Point D = new Point(); Point E = new Point();
               
                C = new Point(_characteristic[i].Centroid.X, _characteristic[i].Centroid.Y);
                for (int j = 1; j <= DEdgeSortPoints[i].Count(); j++)
                {
                    for (int k = 1; k <= DEdgeSortPoints[i].Count(); k++)
                    {
                        distance = (int)Math.Sqrt((DEdgeSortPoints[i][j].X - DEdgeSortPoints[i][k].X) * (DEdgeSortPoints[i][j].X - DEdgeSortPoints[i][k].X) 
                            +(DEdgeSortPoints[i][j].Y - DEdgeSortPoints[i][k].Y) * (DEdgeSortPoints[i][j].Y - DEdgeSortPoints[i][k].Y));
                        if (distance > majorAxis)
                        {
                            majorAxis = distance;
                            A=new Point (DEdgeSortPoints[i][j].X,DEdgeSortPoints[i][j].Y);
                            B=new Point (DEdgeSortPoints[i][k].Y,DEdgeSortPoints[i][k].Y);
                        }
                    }
                }
                minorAxis = majorAxis;
                for (int j = 1; j <= DEdgeSortPoints[i].Count(); j++)
                {
                    for (int k = 1; k <= DEdgeSortPoints[i].Count(); k++)
                    {
                        distanceD = (int)Math.Sqrt((DEdgeSortPoints[i][j].X - C.X) * (DEdgeSortPoints[i][j].X - C.X)
                            + (DEdgeSortPoints[i][j].Y - C.Y) * (DEdgeSortPoints[i][j].Y - C.Y));
                        distanceE = (int)Math.Sqrt((C.X - DEdgeSortPoints[i][k].X) * (C.X - DEdgeSortPoints[i][k].X)
                            + (C.Y - DEdgeSortPoints[i][k].Y) * (C.Y - DEdgeSortPoints[i][k].Y));
                        distance = (int)Math.Sqrt((DEdgeSortPoints[i][j].X - DEdgeSortPoints[i][k].X) * (DEdgeSortPoints[i][j].X - DEdgeSortPoints[i][k].X) 
                            +(DEdgeSortPoints[i][j].Y - DEdgeSortPoints[i][k].Y) * (DEdgeSortPoints[i][j].Y - DEdgeSortPoints[i][k].Y));
                        if (distanceD + distanceE == distance)
                        {
                            if (distance <= minorAxis)
                            {
                                minorAxis = distance;
                                D = new Point(DEdgeSortPoints[i][j].X, DEdgeSortPoints[i][j].Y);
                                E = new Point(DEdgeSortPoints[i][k].Y, DEdgeSortPoints[i][k].Y);
                            }
                        }
                    }
                }
                ////////////////////////////////试图建立过质心垂直长径的直线，失败/////////////////////////////////////////////
                //_k=(A.Y-B.Y)/(A.X-B.X);
                //for (int j = 1; j <= DEdgeSortPoints[i].Count(); j++)
                //{
                //    if (Math.Abs((-1 / _k)*(DEdgeSortPoints[i][j].X - C.X) - (DEdgeSortPoints[i][j].Y-C.Y))<2)
                //    {
                //        D = new Point(DEdgeSortPoints[i][j].X, DEdgeSortPoints[i][j].Y);
                //        sum=j;
                //        break;
                //    }
                //}
                //for (int j = sum+1; j <= DEdgeSortPoints[i].Count(); j++)
                //{
                //    if ((-1 / _k) * (DEdgeSortPoints[i][j].X - C.X) == (DEdgeSortPoints[i][j].Y - C.Y))
                //    {
                //        E = new Point(DEdgeSortPoints[i][j].X, DEdgeSortPoints[i][j].Y);
                //        break;
                //    }
                //}
                //minorAxis =(int) Math.Sqrt((D.X - E.X) * (D.X - E.X) + (D.Y - E.Y) * (D.Y - E.Y));
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //for (int j = 1; j <= DEdgeSortPoints[i].Count(); j++)
                //{
                //    distance = ((A.X - B.X) * (DEdgeSortPoints[i][j].Y - A.Y) - (A.Y - B.Y) * (DEdgeSortPoints[i][j].X - A.X)) /(int) Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));

                //    if (distance >= 0&&distance >positiveDistance)
                //        positiveDistance = distance;
                //    else if (distance < 0&&Math .Abs (distance)>nagitiveDistance)
                //        nagitiveDistance = Math.Abs(distance);
                //}

                //minorAxis = positiveDistance + nagitiveDistance;
                if (minorAxis == 0)
                {
                    minorAxis = 2;
                }

                majToMinAxisRatio=(float)majorAxis /(float)minorAxis;

                _characteristic[i].MajorAxis = majorAxis;
                _characteristic[i].MinorAxis = minorAxis;
                _characteristic[i].MajToMinAxisRatio = majToMinAxisRatio;
                _characteristic[i].Perimeter = DEdgeSortPoints[i].Count();
                _characteristic[i].AreaToPerimeterRate = (float) _characteristic[i].Area /(float)DEdgeSortPoints[i].Count();
            }

            //timecount.QueryPerformanceCounter(out end);//结束时间/////////////
            ////显示时间
            //_time1 = (int)((end - start) * 1000 / frequency);
                return _characteristic;
        }

        public Dictionary<int, Characteristic> GetCentreAcerageColor(Bitmap bitmap, Dictionary<int, Characteristic> Characteristics)
        {
            //long start, end;	//时钟周期
            //long frequency;		//时钟频率
            //timecount.QueryPerformanceFrequency(out frequency);
            //timecount.QueryPerformanceCounter(out start);//开始时间////////

            Dictionary<int, Characteristic> _characteristicWithColor = Characteristics;
            Bitmap b = (Bitmap)bitmap.Clone();

            int rr, gg, bb;
            int RSum, GSum, BSum;
            int RAverage,GAverage,BAverage;
            int range=0;

            int width = b.Width;
            int height = b.Height;
            try
            {
                if (b != null)
                {
                    BitmapData srcData = b.LockBits(new Rectangle(0, 0, width, height),
                                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


                    for (int i = 1; i <= _characteristicWithColor.Count(); i++)
                    {
                        double Ranges = (double)_characteristicWithColor[i].Area / 16;
                        range = (int)Math.Sqrt(Math.Ceiling(Ranges));
                        RSum = GSum = BSum = 0;
                        RAverage = GAverage = BAverage = 0;

                        unsafe
                        {
                            byte* src = (byte*)srcData.Scan0;
                            byte* nsrc;
                            int stride = srcData.Stride;
                            nsrc = src;
                            if (range == 1)
                            {
                                src = nsrc + _characteristicWithColor[i].Centroid.X * 4 + _characteristicWithColor[i].Centroid.Y * stride;
                                bb = src[0];
                                gg = src[1];
                                rr = src[2];
                                BSum += bb;
                                GSum += gg;
                                RSum += rr;
                            }

                            else if (range > 1 && range % 2 != 0)
                            {
                                for (int j = _characteristicWithColor[i].Centroid.X - range / 2; j <= _characteristicWithColor[i].Centroid.X + range / 2; j++)
                                {
                                    for (int k = _characteristicWithColor[i].Centroid.Y - range / 2; k <= _characteristicWithColor[i].Centroid.Y + range / 2; k++)
                                    {
                                        src = nsrc + j * 4 + k * stride;
                                        bb = src[0];
                                        gg = src[1];
                                        rr = src[2];
                                        BSum += bb;
                                        GSum += gg;
                                        RSum += rr;
                                    }
                                }
                            }

                            else if (range > 1 && range % 2 == 0)
                            {
                                for (int j = _characteristicWithColor[i].Centroid.X - range / 2; j <= _characteristicWithColor[i].Centroid.X + range / 2 - 1; j++)
                                {
                                    for (int k = _characteristicWithColor[i].Centroid.Y - range / 2; k <= _characteristicWithColor[i].Centroid.Y + range / 2 - 1; k++)
                                    {
                                        src = nsrc + j * 4 + k * stride;
                                        bb = src[0];
                                        gg = src[1];
                                        rr = src[2];
                                        BSum += bb;
                                        GSum += gg;
                                        RSum += rr;
                                    }
                                }
                            }

                            RAverage = RSum / (range * range);
                            GAverage = GSum / (range * range);
                            BAverage = BSum / (range * range);
                        }
                        _characteristicWithColor[i].CentreAcerageColor = Color.FromArgb(RAverage, GAverage, BAverage);
                    }

                    b.UnlockBits(srcData);
                    b.Dispose();
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
            }
            //timecount.QueryPerformanceCounter(out end);//结束时间/////////////
            ////显示时间
            //_time2 = (int)((end - start) * 1000 / frequency);

            return _characteristicWithColor;
        }
    }
}
