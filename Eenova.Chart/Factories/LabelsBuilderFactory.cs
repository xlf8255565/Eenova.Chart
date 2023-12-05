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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Eenova.Chart.Elements;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Factories
{
    class LabelsBuilderFactory
    {
        public static LabelsBuilder Create(Axis axis)
        {
            switch (axis.DataType)
            {
                case DataType.Numberic:
                    return new NumbericLabelsBuilder(axis);
                case DataType.DateTime:
                    return new DateTimeLabelsBuilder(axis);
                case DataType.Text:
                    return new TextLabelsBuilder(axis);
            }

            return null;
        }
    }
}
