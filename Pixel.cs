using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AutoPickingSys
{
    public class Pixel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int ColorR { get; set; }

        public int ColorG { get; set; }

        public int ColorB { get; set; }

        public int BinaryzationValue { get; set; }
        public int BinaryzationValue2 { get; set; }
        public int FirstEdge { get; set; }
        public int SecondEdge { get; set; }
        public int SegLine { get; set; }

        public int AreaTag { get; set; }

        public int EdgeTag { get; set; }

        public float PerimeterTag { get; set; }

        public byte EdgeValue { get; set; }//边缘值
    }
}
