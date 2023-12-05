/*****************************************************************************
*   Project:        城市轨道交通
*
*   Developed by:   Jphotonics Technology.
*                   Hangzhou, China
*
*   Developers:     Jphotonics   2010-10-20
*
*
*   Copyright:      (c) 2010 Jphotonics Technology. All rights reserved.
*****************************************************************************/


using Eenova.Chart.Elements;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Factories
{
    class ValueCalculatorFactory
    {
        public static ValueCalculator Create(Axis axis)
        {
            return axis.IsLogarithm ? CreateLogarithm(axis) : CreateCommon(axis);
        }

        private static ValueCalculator CreateCommon(Axis axis)
        {
            if (axis.IsMinValueAuto == false)
                if (axis.IsMaxValueAuto == false)
                    if (axis.IsMainUnitAuto == false)
                        return new CommonFFFValueCalculator(axis);
                    else
                        return new CommonFFTValueCalculator(axis);
                else
                    if (axis.IsMainUnitAuto == false)
                        return new CommonFTFValueCalculator(axis);
                    else
                        return new CommonFTTValueCalculator(axis);
            else
                if (axis.IsMaxValueAuto == false)
                    if (axis.IsMainUnitAuto == false)
                        return new CommonTFFValueCalculator(axis);
                    else
                        return new CommonTFTValueCalculator(axis);
                else
                    if (axis.IsMainUnitAuto == false)
                        return new CommonTTFValueCalculator(axis);
                    else
                        return new CommonTTTValueCalculator(axis);
        }

        private static ValueCalculator CreateLogarithm(Axis axis)
        {
            if (axis.IsMinValueAuto == false)
            {
                if (axis.IsMaxValueAuto == false)
                {
                    if (axis.IsMainUnitAuto == false)
                    {
                        return new LogFFFValueCalculator(axis);
                    }
                    else
                    {
                        return new LogFFTValueCalculator(axis);
                    }
                }
                else
                {
                    if (axis.IsMainUnitAuto == false)
                    {
                        return new LogFTFValueCalculator(axis);
                    }
                    else
                    {
                        return new LogFTTValueCalculator(axis);
                    }
                }
            }
            else
            {
                if (axis.IsMaxValueAuto == false)
                {
                    if (axis.IsMainUnitAuto == false)
                    {
                        return new LogTFFValueCalculator(axis);
                    }
                    else
                    {
                        return new LogTFTValueCalculator(axis);
                    }
                }
                else
                {
                    if (axis.IsMainUnitAuto == false)
                    {
                        return new LogTTFValueCalculator(axis);
                    }
                    else
                    {
                        return new LogTTTValueCalculator(axis);
                    }
                }
            }
        }
    }
}
