using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AutoPickingSys
{
    public class Characteristic
    {
        public int Area { get; set; }

        public int AreaTage { get; set; }

        public int MajorAxis { get; set; }

        public int MinorAxis { get; set; }

        public float MajToMinAxisRatio {get;set; }
        public float Yita { get; set; }

        public int Perimeter { get; set; }

        public Point Centroid { get; set; }

        public Color CentreAcerageColor { get; set; }

        public float AreaToPerimeterRate { get;set; }

        public bool IsQualifiedColony { get; set; }

        public bool IsInvalidColony { get; set; }

        public bool IsNearPoint { get; set; }

        public bool IsFarPoint { get; set; }

        public bool IsInternal { get; set; }
        
    }
}
