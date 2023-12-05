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


using System;

namespace Eenova.Chart.Helpers
{
    /// <summary>
    /// 算法集合
    /// </summary>
    class ValueCalculateAlgorithm
    {
        /// <summary>
        /// 通过最大值，刻度，最小数据界限计算最小值。
        /// </summary>
        /// <param name="max">最大值</param>
        /// <param name="unit">刻度</param>
        /// <param name="limit">最小数据界限</param>
        /// <returns>最小值</returns>
        public static double GetMin(double max, double unit, double limit)
        {
            if (limit > max)
                throw new ArgumentException("limit需要小于等于max");

            if (unit <= 0)
                throw new ArgumentException("unit需要大于0");

            //反复减去最大值，直到最小值小于等于下限。
            double min = 0;
            for (var i = 0; i >= 0; i++)
            {
                min = max - i * unit;
                if (min <= limit)
                    break;
            }
            return min;
        }

        /// <summary>
        /// 通过最小值，刻度，最大上限计算最大值。
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="unit">刻度</param>
        /// <param name="limit">最大上限</param>
        /// <returns>最大值</returns>
        public static double GetMax(double min, double unit, double limit)
        {
            if (min > limit)
                throw new ArgumentException("limit需要大于等于min");

            if (unit <= 0)
                throw new ArgumentException("unit需要大于0");

            //反复加加加，直到最大值大于等于上限。
            double max = 0;
            for (var i = 0; i >= 0; i++)
            {
                max = min + i * unit;
                if (max >= limit)
                    break;
            }
            return max;
        }

        public static double GetUnit(DataType dataType, double differ)
        {
            if (dataType == DataType.DateTime)
            {
                return GetDateTimeUnit(differ);
            }
            else
            {
                return GetNumbericUnit(differ);
            }
        }

        /// <summary>
        /// 数值型数据通过最大数据和最小数据的间隔计算刻度。
        /// </summary>
        /// <param name="differ">间隔</param>
        /// <returns>刻度</returns>
        public static double GetNumbericUnit(double differ)
        {
            if (differ == 0)
                return 10;

            //if (differ <= 0)
            //    throw new ArgumentException("differ需要大于0");

            //求数量级。0.1,0,10,100...
            var magnitude = Math.Log10(differ);

            double remainder = 0;//余数。
            int power = 0;
            if (magnitude >= 0)
            {
                remainder = magnitude % 1;
                power = (int)magnitude - 1;
            }
            else
            {
                remainder = 1 - magnitude % 1;
                power = (int)magnitude - 2;
            }

            double unit = 0;
            if (remainder > 0 && remainder <= Math.Log10(2))
            {
                //0-2区间
                unit = 2;
            }
            else if (remainder > Math.Log10(5) && remainder <= 1)
            {
                //5-10区间
                unit = 10;
            }
            else
            {
                //2-5区间
                unit = 5;
            }

            unit = unit * Math.Pow(10, power);
            return unit;
        }

        /// <summary>
        /// 时间型数据通过最大数据和最小数据的间隔计算刻度。
        /// </summary>
        /// <param name="differ">间隔</param>
        /// <returns>刻度</returns>
        public static double GetDateTimeUnit(double differ)
        {
            if (differ == 0)
                return 600;

            //if (differ <= 0)
            //    throw new ArgumentException("differ需要大于0");

            if (differ <= 300)
                return GetDateTimeSecondUnit(differ);
            else if (differ <= 60 * 60 * 6)//60秒*60分*6小时
                return GetDateTimeMiniteUnit(differ);
            else if (differ <= 60 * 60 * 24 * 7)//60秒*60分*24小时*7天
                return GetDateTimeHourUnit(differ);
            else
                return GetDateTimeDayUnit(differ);
        }

        private static double GetDateTimeSecondUnit(double differ)
        {
            if (differ <= 15)//15 second
                return 1;
            else if (differ <= 30)
                return 2;
            else if (differ <= 60)
                return 5;
            else if (differ <= 150)
                return 10;
            else
                return 20;
        }

        private static double GetDateTimeMiniteUnit(double differ)
        {
            //360分钟。
            differ = differ / 60;
            var unit = 0;

            if (differ <= 15)//15分钟
                unit = 1;
            else if (differ <= 30)//30分钟
                unit = 2;
            else if (differ <= 60)//60分钟
                unit = 5;
            else if (differ <= 120)//120分钟，2小时
                unit = 10;
            else if (differ <= 240)//240分钟，4小时
                unit = 20;
            else if (differ % 30 == 0)//4.5,5,5.5,60
                unit = 30;
            else
                unit = 50;//最大6小时,12格。

            return unit * 60;
        }

        private static double GetDateTimeHourUnit(double differ)
        {
            //一共7天。
            differ = differ / 3600;

            return ((int)(differ / 12)) * 3600;
        }

        private static double GetDateTimeDayUnit(double differ)
        {
            differ = differ / (24 * 3600);
            var magnitude = Math.Log10(differ);
            var remainder = magnitude % 1;
            var power = (int)magnitude - 1;

            double unit = 0;
            if (remainder > 0 && remainder <= Math.Log10(2))
            {
                //0-2区间
                unit = 2;
            }
            else if (remainder > Math.Log10(5) && remainder <= 1)
            {
                //5-10区间
                unit = 10;
            }
            else
            {
                //2-5区间
                unit = 5;
            }

            unit = unit * Math.Pow(10, power);
            return unit * 24 * 3600;

        }

        public static double GetLogMin(double unit, double limit)
        {
            if (limit <= 0)
                throw new ArgumentException("limit需大于0");

            if (unit < 2)
                throw new ArgumentException("unit需大于2");

            if (limit >= 1)
                return 1;

            var min = 0.00;
            for (int i = -1; i < 0; i--)
            {
                min = Math.Pow(unit, i);
                if (min <= limit)
                    break;
            }
            return min;
        }

        public static double GetLogMax(double unit, double limit)
        {
            if (limit <= 0)
                throw new ArgumentException("limit需大于0");

            if (unit < 2)
                throw new ArgumentException("unit需大于2");

            if (limit <= 1)
                return 1;

            var max = 0.00;
            for (int i = 1; i > 0; i++)
            {
                max = Math.Pow(unit, i);
                if (max >= limit)
                    break;
            }
            return max;
        }

        public static double GetLogUnit(double min, double max)
        {
            if (min <= 0)
                throw new ArgumentException("min需要大于0");

            if (max <= 0)
                throw new ArgumentException("max需要大于0");

            if (min >= max)
                throw new ArgumentException("min需要小于max");

            var differ = max - min;
            var unit = 4;
            if (differ <= Math.Pow(2, 13) && differ >= Math.Pow(2, -13))
            {
                unit = 2;
            }
            else if (differ > Math.Pow(2, 26) || differ < Math.Pow(2, -26))
            {
                unit = 8;
            }
            return unit;
        }
    }
}
