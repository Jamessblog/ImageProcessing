using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPickingSys
{
    class ScreenAlgorithm
    {
        public Dictionary<int, Characteristic> ScreenTheColony(ScreenOpition screenOpition,
            ScreenOpitionValue screenOpitionValue, Dictionary<int, Characteristic> characteristics)
        {
            Dictionary<int, Characteristic> _characteristic = characteristics;

            for (int i = 1; i <= _characteristic.Count(); i++)
            {
                characteristics[i].IsQualifiedColony = true;
            }

            if (screenOpition .IsUseAreaOpition ==true)
            _characteristic = AreaScreen(screenOpition, screenOpitionValue, _characteristic);

            if (screenOpition .IsUseAxisOpition ==true)
            _characteristic = AxisScreen(screenOpition, screenOpitionValue, _characteristic);

            if (screenOpition .IsUseColorOpition ==true)
            _characteristic = ColorScreen(screenOpition, screenOpitionValue, _characteristic);

            if (screenOpition .IsUseRateOpition ==true)
            _characteristic = APRateScreen(screenOpition, screenOpitionValue, _characteristic);

            return _characteristic;
        }

        private Dictionary<int, Characteristic> AreaScreen(ScreenOpition screenOpition,
            ScreenOpitionValue screenOpitionValue, Dictionary<int, Characteristic> characteristics)
        {
            Dictionary<int, Characteristic> _characteristic = characteristics;

            if (screenOpition.hasAreaLowerLimit == true && screenOpition.hasAreaSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].Area >= screenOpitionValue.AreaLowerLimit && 
                        characteristics[i].Area <= screenOpitionValue.AreaSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasAreaLowerLimit != true && screenOpition.hasAreaSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].Area <= screenOpitionValue.AreaSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasAreaLowerLimit == true && screenOpition.hasAreaSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].Area >= screenOpitionValue.AreaLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasAreaLowerLimit != true && screenOpition.hasAreaSuperiorLimit != true)
                _characteristic = characteristics;

            return _characteristic;
        }

        private Dictionary<int, Characteristic> AxisScreen(ScreenOpition screenOpition, 
            ScreenOpitionValue screenOpitionValue, Dictionary<int, Characteristic> characteristics)
        {
            Dictionary<int, Characteristic> _characteristic = characteristics;

            if (screenOpition.hasMajorAxisLowerLimit == true && screenOpition.hasMajorAxisSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MajorAxis >= screenOpitionValue.MajorAxisLowerLimit && 
                        characteristics[i].MajorAxis <= screenOpitionValue.MajorAxisSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMajorAxisLowerLimit != true && screenOpition.hasMajorAxisSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MajorAxis <= screenOpitionValue.MajorAxisSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMajorAxisLowerLimit == true && screenOpition.hasMajorAxisSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MajorAxis >= screenOpitionValue.MajorAxisLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMajorAxisLowerLimit != true && screenOpition.hasMajorAxisSuperiorLimit  != true)
                _characteristic = characteristics;


            if (screenOpition.hasMinorAxisLowerLimit == true && screenOpition.hasMinorAxisSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MinorAxis >= screenOpitionValue.MinorAxisLowerLimit && 
                        characteristics[i].MinorAxis <= screenOpitionValue.MinorAxisSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMinorAxisLowerLimit != true && screenOpition.hasMinorAxisSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MinorAxis <= screenOpitionValue.MinorAxisSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMinorAxisLowerLimit == true && screenOpition.hasMinorAxisSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MinorAxis >= screenOpitionValue.MinorAxisLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMinorAxisLowerLimit != true && screenOpition.hasMinorAxisSuperiorLimit != true)
                _characteristic = characteristics;


            if (screenOpition.hasMaxMinRateLowerLimit == true && screenOpition.hasMaxMinRateSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MajToMinAxisRatio >= screenOpitionValue.MaxMinRateLowerLimit && 
                        characteristics[i].MajToMinAxisRatio <= screenOpitionValue.MaxMinRateSuperiorLimit )
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMaxMinRateLowerLimit != true && screenOpition.hasMaxMinRateSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MajToMinAxisRatio <= screenOpitionValue.MaxMinRateSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMaxMinRateLowerLimit == true && screenOpition.hasMaxMinRateSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].MajToMinAxisRatio >= screenOpitionValue.MaxMinRateLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasMaxMinRateLowerLimit != true && screenOpition.hasMaxMinRateSuperiorLimit != true)
                _characteristic = characteristics;


            return _characteristic;
        }

        private Dictionary<int, Characteristic> ColorScreen(ScreenOpition screenOpition, 
            ScreenOpitionValue screenOpitionValue, Dictionary<int, Characteristic> characteristics)
        {
            Dictionary<int, Characteristic> _characteristic = characteristics;

            if (screenOpition.hasColorRedLowerLimit == true && screenOpition.hasColorRedSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.R >= screenOpitionValue.ColorRedLowerLimit && 
                        characteristics[i].CentreAcerageColor.R <= screenOpitionValue.ColorRedSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorRedLowerLimit != true && screenOpition.hasColorRedSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.R <= screenOpitionValue.ColorRedSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorRedLowerLimit == true && screenOpition.hasColorRedSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.R >= screenOpitionValue.ColorRedLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorRedLowerLimit != true && screenOpition.hasColorRedSuperiorLimit != true)
                _characteristic = characteristics;


            if (screenOpition.hasColorGreenLowerLimit == true && screenOpition.hasColorGreenSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.G >= screenOpitionValue.ColorGreenLowerLimit && 
                        characteristics[i].CentreAcerageColor.G <= screenOpitionValue.ColorGreenSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorGreenLowerLimit != true && screenOpition.hasColorGreenSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.G <= screenOpitionValue.ColorGreenSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorGreenLowerLimit == true && screenOpition.hasColorGreenSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.G >= screenOpitionValue.ColorGreenLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorGreenLowerLimit != true && screenOpition.hasColorGreenSuperiorLimit != true)
                _characteristic = characteristics;


            if (screenOpition.hasColorBlueLowerLimit == true && screenOpition.hasColorBlueSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.B >= screenOpitionValue.ColorBlueLowerLimit && 
                        characteristics[i].CentreAcerageColor.B <= screenOpitionValue.ColorBlueSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorBlueLowerLimit != true && screenOpition.hasColorBlueSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.B <= screenOpitionValue.ColorBlueSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorBlueLowerLimit == true && screenOpition.hasColorBlueSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].CentreAcerageColor.B >= screenOpitionValue.ColorBlueLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasColorBlueLowerLimit != true && screenOpition.hasColorBlueSuperiorLimit != true)
                _characteristic = characteristics;


            return _characteristic;
        }

        private Dictionary<int, Characteristic> APRateScreen(ScreenOpition screenOpition, 
            ScreenOpitionValue screenOpitionValue, Dictionary<int, Characteristic> characteristics)
        {
            Dictionary<int, Characteristic> _characteristic = characteristics;

            if (screenOpition.hasAreaPerimeterRateLowerLimit == true &&
                screenOpition.hasAreaPerimeterRateSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].AreaToPerimeterRate >= screenOpitionValue.AreaPerimeterRateLowerLimit && 
                        characteristics[i].AreaToPerimeterRate <= screenOpitionValue.AreaPerimeterRateSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasAreaPerimeterRateLowerLimit != true && 
                screenOpition.hasAreaPerimeterRateSuperiorLimit == true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].AreaToPerimeterRate <= screenOpitionValue.AreaPerimeterRateSuperiorLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasAreaPerimeterRateLowerLimit == true && 
                screenOpition.hasAreaPerimeterRateSuperiorLimit != true)
            {
                for (int i = 1; i <= characteristics.Count(); i++)
                {
                    if (characteristics[i].IsQualifiedColony == true && 
                        characteristics[i].AreaToPerimeterRate >= screenOpitionValue.AreaPerimeterRateLowerLimit)
                        characteristics[i].IsQualifiedColony = true;
                    else
                        characteristics[i].IsQualifiedColony = false;
                }
            }
            else if (screenOpition.hasAreaPerimeterRateLowerLimit != true && 
                screenOpition.hasAreaPerimeterRateSuperiorLimit != true)
                _characteristic = characteristics;

            return _characteristic;
        }
    }
}
