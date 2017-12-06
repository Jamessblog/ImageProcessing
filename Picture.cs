using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace AutoPickingSys
{
    public class Picture
    {
        private Bitmap _bitmap;
        public Dictionary<int, Pixel> _pixels;
        public Dictionary<int, Characteristic> Characteristics;
        private Dictionary<int, Dictionary<int, Pixel>> _dEdgePoints;
        public Dictionary<int, Dictionary<int, Pixel>> _dEdgeSortPoints;
        public Point BeginPosition;
        public Point CrossPoint;
        public Color BackGroundColor;
        public Color CrossBackGroundColor;
        public Color AverageColor;
        public Color CrossAverageColor;
        public Bitmap EdgeBitmap; public Bitmap NumBitmap;
        public Bitmap SharpenBitmap;
        public Bitmap SmoothBitmap;
        public Bitmap MedianBitmap;
        public Bitmap ContrastBitmap;
        public Bitmap ThresholdSegmentBitmap;
        public Bitmap EdgeSegmentBitmap;//边缘分割图像；
        public Bitmap RegionGrowingBitmap;
        public Bitmap BinaryImage;
        public Bitmap CrossBinaryImage;
        public Bitmap QualifiedBG;
        public Bitmap RegionShowingBG;
        public Bitmap ZoomImage;
        public Bitmap RegionShowingImage;
        public Bitmap RegionShowingImageAll;
        public Bitmap RegionShowingImageQualified;
        public Bitmap RegionShowingImageInvalid;
        public Bitmap ExpertImageTest1;
        public Bitmap theImage;
        private int _areaTagCount;
        public int GateColor;

        public int time1;
        public int time2;
        public int time3;
        public bool IsUseCirl=true;
        public bool IsUseRegionGrowing;
        public float StandardRatio;
        public int X { get; set;}
        public int Y { get; set;}
        public int R { get; set;}
        public int RX { get; set; }
        public int RY { get; set; }
        public int RH { get; set; }
        public int RW { get; set; }

        public Picture(Bitmap b)
        {
            _bitmap = b;
            _pixels = null;
            SetPixels setPixel = new SetPixels();
            setPixel.SetPixel(_bitmap, out _pixels);
            QualifiedBG = _bitmap;
            NumBitmap = null;
        }

        public void GetBackColor()
        {
            GetBackGround getBackGround = new GetBackGround();
            if (IsUseCirl == true)            
                getBackGround.BGPoint(_bitmap, X, Y, R, out BeginPosition, out BackGroundColor);
            if (IsUseCirl == false)
                getBackGround.BGPointRectangle(_bitmap, RX, RY, RW, RH, out BeginPosition, out BackGroundColor);
        }
        public void Gray()
        {
            Graying _gray = new Graying();
            theImage = _gray.graying(_bitmap);
        }
        public void theSmooth()
        {
            Smooth _smooth = new Smooth();
            SmoothBitmap = _smooth.smooth((Bitmap)theImage.Clone());
            theImage = SmoothBitmap;
        }
        public void theMedian()
        {
            Median _median = new Median();
            MedianBitmap = _median.median((Bitmap)theImage.Clone(), X, Y, R);
            theImage = MedianBitmap;
        }
        public void theEdgeCalculate()//边缘分割算法；
        {
            EdgeAlgorithm edgeAlgorithm = new EdgeAlgorithm();
            if (IsUseCirl == true)
            {
                edgeAlgorithm.EdgeBitmapCalculate((Bitmap)theImage.Clone(), X, Y, R, out EdgeBitmap,out GateColor, ref _pixels);
                theImage = EdgeBitmap;
            }
            if (IsUseCirl == false)
            {
                edgeAlgorithm.EdgeBitmapCalculateRectangle(_bitmap, RX, RY, RW, RH, out EdgeBitmap,  ref _pixels);
                theImage = EdgeBitmap;
            }
        }
        public void theContrast()
        {
            Contrast _contrast = new Contrast();
            _contrast.contrast((Bitmap)theImage.Clone(), X, Y, R, out GateColor);
        }
        public void theRegoinGrow()//区域生长；
        {
            RegionGrowing regoinGrowing = new RegionGrowing();
            //theEdgeCalculate();
            GetBackColor();
            if (IsUseCirl == true)
            {
                regoinGrowing.RegionGrowingAlgorithm(EdgeBitmap, BeginPosition, GateColor, X, Y, R, ref _pixels, out EdgeSegmentBitmap);
                BinaryImage = EdgeSegmentBitmap;
                theImage = EdgeSegmentBitmap;
            }
            if (IsUseCirl == false)
            {
                regoinGrowing.RegionGrowingAlgorithmRectangle(EdgeBitmap, BeginPosition, GateColor, RX, RY, RW, RH, ref _pixels, out EdgeSegmentBitmap);
                BinaryImage = EdgeSegmentBitmap;
            }
        }

        public void theBilateralThreshold()//区域生长；
        {
            BilateralThreshold bilateralthreshold = new BilateralThreshold();
            //theEdgeCalculate();
            GetBackColor();
            if (IsUseCirl == true)
            {
                bilateralthreshold.BilateralThresholdAlgorithm((Bitmap)theImage.Clone(), BeginPosition, GateColor, X, Y, R, ref _pixels, out EdgeSegmentBitmap);
                BinaryImage = EdgeSegmentBitmap;
                theImage = EdgeSegmentBitmap;
            }
            //if (IsUseCirl == false)
            //{
            //    regoinGrowing.RegionGrowingAlgorithmRectangle(EdgeBitmap, BeginPosition, GateColor, RX, RY, RW, RH, ref _pixels, out EdgeSegmentBitmap);
            //    BinaryImage = EdgeSegmentBitmap;
            //}
        }

        public void theShrk()
        {
            shrk _shrink = new shrk();
            Bitmap sksave = (Bitmap)BinaryImage.Clone();
            sksave = _shrink.shrink((Bitmap)theImage.Clone());
            theImage = sksave;
        }
        public void theExp()
        {
            exp _exp = new exp();
            Bitmap esave = (Bitmap)BinaryImage.Clone();
            esave = _exp.expand((Bitmap)theImage.Clone());
            theImage = esave;
        }
        public void SetBinaryValue()
        {
            SetPixels setPixel = new SetPixels();
            _pixels = setPixel.SetBinaryValue((Bitmap)theImage.Clone(), _pixels, out time1);
        }
        public void SetAreaTag()
        {

            SetBinaryValue();
            SetPixels setPixel = new SetPixels();
            if (IsUseCirl == true)
            {
                _pixels = setPixel.SetAreaTag(theImage, X, Y, R, _pixels);
                _areaTagCount = setPixel.AreaTagCount;
            }
            if (IsUseCirl == false)
            {
                _pixels = setPixel.SetAreaTagRectangle(theImage, RX, RY, RW, RH, _pixels);
                _areaTagCount = setPixel.AreaTagCount;
            }
        }

        public void GetCharacteristic()
        {
            SetAreaTag();
            GetCharacteristic getCharacteristic = new GetCharacteristic();
            getCharacteristic.AreaStatistics(theImage, _areaTagCount, ref _pixels, out _dEdgePoints, out _dEdgeSortPoints, out Characteristics);
            Characteristics = getCharacteristic.GetMaxMinAxis(_dEdgeSortPoints, Characteristics);
            Characteristics = getCharacteristic.GetCentreAcerageColor(_bitmap, Characteristics);
        }







        public void Smooth1()
        {
            Smooth _smooth = new Smooth();
            SmoothBitmap = _smooth.smooth(_bitmap);
        }
        public void Smooth2()
        {
            Smooth1();
            Smooth _smooth = new Smooth();
            SmoothBitmap = _smooth.smooth(SmoothBitmap);
        }
        public void Smooth3()
        {
            Smooth2();
            Smooth _smooth = new Smooth();
            SmoothBitmap = _smooth.smooth(SmoothBitmap);
        }
        public void Sharpen1()
        {
            Smooth3();
            //EdgeCalculate();
            //Bitmap sharp_image=(Bitmap)EdgeBitmap.Clone();
            Bitmap sharp_image = (Bitmap)SmoothBitmap.Clone();
            Sharpen _sharpen = new Sharpen();
            SharpenBitmap = _sharpen.sharpen(sharp_image);
        }

        public void Smooth4()
        {
            Sharpen1();
            Smooth _smooth = new Smooth();
            SmoothBitmap = _smooth.smooth(SharpenBitmap);
        }

        //public void EdgeCalculate()//边缘分割算法；
        //{
        //    //Sharpen1();
        //    Smooth4();
        //    EdgeAlgorithm edgeAlgorithm = new EdgeAlgorithm();
        //    if (IsUseCirl == true)
        //        edgeAlgorithm.EdgeBitmapCalculate(SmoothBitmap, X, Y, R, out EdgeBitmap, out GateColor, ref _pixels);
        //    if (IsUseCirl == false)
        //        edgeAlgorithm.EdgeBitmapCalculateRectangle(_bitmap, RX, RY, RW, RH, out EdgeBitmap, out GateColor, ref _pixels);
        //}

        //public void RegoinGrow()//区域生长；
        //{
        //    RegionGrowing regoinGrowing = new RegionGrowing();
        //    EdgeCalculate();
        //    GetBackColor();
        //    if (IsUseCirl == true)
        //    {
        //        regoinGrowing.RegionGrowingAlgorithm(EdgeBitmap, BeginPosition, GateColor, X, Y, R, ref _pixels, out EdgeSegmentBitmap);
        //        BinaryImage = EdgeSegmentBitmap;
        //    }
        //    if (IsUseCirl == false)
        //    {
        //        regoinGrowing.RegionGrowingAlgorithmRectangle(EdgeBitmap, BeginPosition, GateColor, RX, RY, RW, RH, ref _pixels, out EdgeSegmentBitmap);
        //        BinaryImage = EdgeSegmentBitmap;
        //    }
        //}

        public void ThresholdCalculate()//用作分割标定点；
        {
            ThresholdAlgorithm thresholdObtain = new ThresholdAlgorithm();
            if (IsUseCirl == true)
                thresholdObtain.ThresholdCalculate(_bitmap, X, Y, R, out CrossBackGroundColor, out CrossAverageColor);
            if (IsUseCirl == false)
                thresholdObtain.ThresholdCalculateRectangle(_bitmap, RX, RY, RW, RH, out CrossBackGroundColor, out CrossAverageColor);
        }

        public void ThresholdSegment()
        {
            ThresholdSegment thresholdSegment = new ThresholdSegment();
            ThresholdCalculate();
            if (IsUseCirl == true)
            {
                thresholdSegment.ThresholdSegmentAlgorithm(_bitmap, X, Y, R, CrossBackGroundColor, CrossAverageColor, out ThresholdSegmentBitmap);
                CrossBinaryImage = ThresholdSegmentBitmap;
            }
            if (IsUseCirl == false)
            {
                thresholdSegment.ThresholdSegmentAlgorithmRectangle(_bitmap, RX, RY, RW, RH, CrossBackGroundColor, CrossAverageColor, out ThresholdSegmentBitmap);
                CrossBinaryImage = ThresholdSegmentBitmap;
            }
        }

        //public void ImageZoom()
        //{
        //    //if (IsUseRegionGrowing == false)
        //    //{
        //    //    ThresholdSegment();
        //    //}
        //    //if (IsUseRegionGrowing == true)
        //    //{
        //    RegoinGrow();
        //    //}
        //    exp ep = new exp();
        //    shrk shk = new shrk();
        //    Bitmap esave =(Bitmap) BinaryImage.Clone();
        //    Bitmap sksave = (Bitmap)BinaryImage.Clone();
        //    if (IsUseCirl == true)
        //    {
        //        if (BinaryImage != null)
        //        {
        //            sksave = shk.shrink((Bitmap)BinaryImage.Clone());

        //            esave = ep.expand((Bitmap)sksave.Clone());
        //        }
        //        ZoomImage = esave;
        //    }
        //    if (IsUseCirl == false)
        //    {
        //        if (BinaryImage != null)
        //        {
        //            sksave = shk.shrinkRectangle((Bitmap)BinaryImage.Clone());

        //            esave = ep.expandRectangle((Bitmap)sksave.Clone());
        //        }
        //        ZoomImage = esave;
        //    }
        //}

        //public void SetBinaryValue()
        //{
        //    ImageZoom();
        //    SetPixels setPixel = new SetPixels();
        //    _pixels = setPixel.SetBinaryValue(ZoomImage, _pixels,out time1);
        //}

        //public void SetAreaTag()
        //{
            
        //    SetBinaryValue();
        //    SetPixels setPixel = new SetPixels();
        //    if (IsUseCirl == true)
        //    {
        //        _pixels = setPixel.SetAreaTag(ZoomImage, X, Y, R, _pixels);
        //        _areaTagCount = setPixel.AreaTagCount;
        //    }
        //    if (IsUseCirl == false)
        //    {
        //        _pixels = setPixel.SetAreaTagRectangle(ZoomImage, RX, RY, RW, RH, _pixels);
        //        _areaTagCount = setPixel.AreaTagCount;
        //    }
        //}

        //public void GetCharacteristic()
        //{
        //    SetAreaTag();
        //    GetCharacteristic getCharacteristic = new GetCharacteristic();
        //    getCharacteristic.AreaStatistics(ZoomImage, _areaTagCount, ref _pixels, out _dEdgePoints, out _dEdgeSortPoints, out Characteristics);
        //    Characteristics = getCharacteristic.GetMaxMinAxis(_dEdgeSortPoints, Characteristics);
        //    Characteristics = getCharacteristic.GetCentreAcerageColor(ZoomImage, Characteristics);
        //}

        //public void GetCross()
        //{
        //    ThresholdSegment();
        //    GetCross getCross = new GetCross();
        //    getCross.getCrossPoint((Bitmap)CrossBinaryImage.Clone(), out CrossPoint);
        //    getCross.getStandardRatio((Bitmap)CrossBinaryImage.Clone(), CrossPoint, out StandardRatio);
        //}

        public void ShowRegion(Point selectPoint, Color showColor)
        {

            //if (RegionShowingImageInvalid == null)
            //{
            //    RegionShowingBG = RegionShowingImageAll;
            //    ImageShowingRegion imageShowingRegion = new ImageShowingRegion();
            //    RegionShowingImage = imageShowingRegion.RegionShowing((Bitmap)RegionShowingBG.Clone(), selectPoint, showColor, _dEdgeSortPoints, _pixels);
            //}
            //else
            //{
                //RegionShowingBG = RegionShowingImageInvalid;
            if (NumBitmap != null)
            {
                RegionShowingBG = NumBitmap;
            }
            else
            {
                RegionShowingBG = QualifiedBG;
            }
                ImageShowingRegion imageShowingRegion = new ImageShowingRegion();
                RegionShowingImage = imageShowingRegion.RegionShowing((Bitmap)RegionShowingBG.Clone(), selectPoint, showColor, _dEdgeSortPoints, _pixels);
            //}
        }

        public void ShowRegionAll(Color showColor)
        {
            ImageShowingRegionAll imageShowingRegionAll = new ImageShowingRegionAll();
            RegionShowingImageAll = imageShowingRegionAll.RegionShowingAll((Bitmap)_bitmap.Clone(), showColor, Characteristics, _dEdgeSortPoints, _pixels);
            //QualifiedBG = RegionShowingImageAll;
        }

        public void ShowRegionQualified(Color showColor)
        {
            ImageShowingRegionQualified imageShowingRegionQualified = new ImageShowingRegionQualified();
            RegionShowingImageQualified = imageShowingRegionQualified.RegionShowingQualified((Bitmap)_bitmap.Clone(), showColor, Characteristics, _dEdgeSortPoints, _pixels);
            RegionShowingImageInvalid = imageShowingRegionQualified.RegionShowingInvalid((Bitmap)RegionShowingImageQualified.Clone(), Color.Purple, Characteristics, _dEdgeSortPoints, _pixels);
            if (RegionShowingImageInvalid != null)
            {
                QualifiedBG = RegionShowingImageInvalid;
            } 
        }

        public void SvmAlgorithm()
        {
            svmAlgorithm mysvmproject = new svmAlgorithm();
            mysvmproject.svmproject(Characteristics);
        }

        public Characteristic GetSelectedColony(int x, int y)
        {
            Characteristic SelectColony = new Characteristic();
            int width = _bitmap.Width;
            int height = _bitmap.Height;
            int buffer, lenth;

            lenth = _bitmap.Width * _bitmap.Height;

            buffer = _pixels[x + Math.Abs(y) * width].AreaTag;


            for (int i = 1; i <= Characteristics.Count(); i++)
            {
                if (_pixels[Characteristics[i].Centroid.X + Characteristics[i].Centroid.Y * width].AreaTag == buffer)
                {
                    SelectColony = Characteristics[i];
                    SelectColony.IsFarPoint = false;
                    SelectColony.IsInternal = true;
                    SelectColony.IsNearPoint = false;
                    return SelectColony;
                }
            }

            for (int i = 1; i <= Characteristics.Count(); i++)
            {
                int distance = (int)Math.Sqrt((Characteristics[i].Centroid.X - x) * (Characteristics[i].Centroid.X - x) +
                    (Characteristics[i].Centroid.Y - y) * (Characteristics[i].Centroid.Y - y));
                if (lenth > distance)
                {
                    lenth = distance;
                    SelectColony = Characteristics[i];
                    SelectColony.IsInternal = false;

                    if (distance <= Characteristics[i].MajorAxis * 3 / 2)
                    {
                        SelectColony.IsNearPoint = true;
                        SelectColony.IsFarPoint = false;
                    }
                    else
                    {
                        SelectColony.IsNearPoint = false;
                        SelectColony.IsFarPoint = true;
                    }
                }
            }

            return SelectColony;
        }


        public void SimExperter(Color showColor)
        {
            //GetCharacteristic getCharacteristic = new GetCharacteristic();
            SimExperter simExperter = new SimExperter();
            ExpertImageTest1 = simExperter.ExpertTest1((Bitmap)QualifiedBG.Clone(), showColor, ref Characteristics, _dEdgeSortPoints, _pixels);


            //getCharacteristic.AreaStatistics(_bitmap, _areaTagCount, ref _pixels, out _dEdgePoints, out _dEdgeSortPoints, out Characteristics);
            //Characteristics = getCharacteristic.GetMaxMinAxis(_dEdgeSortPoints, Characteristics);
            //Characteristics = getCharacteristic.GetCentreAcerageColor(_bitmap, Characteristics);
        }
    }
}
