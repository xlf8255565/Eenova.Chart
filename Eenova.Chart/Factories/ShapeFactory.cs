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


using System.Windows;
using System.Windows.Shapes;
using Microsoft.Expression.Shapes;

namespace Eenova.Chart.Factories
{
    class ShapeFactory
    {
        public static Shape Create(ShapeType shapeType)
        {
            Shape shape;
            switch (shapeType)
            {
                default:
                case ShapeType.Circle:
                    shape = new Ellipse();
                    break;
                case ShapeType.Square:
                    shape = new Rectangle();
                    break;
                case ShapeType.CrossStar:
                    shape = new RegularPolygon() { StrokeThickness = 0, InnerRadius = 0.47, PointCount = 4 };
                    break;
                case ShapeType.Diamond:
                    shape = new RegularPolygon() { StrokeThickness = 0, InnerRadius = 1, PointCount = 4 };
                    break;
                case ShapeType.Hexagon:
                    shape = new RegularPolygon() { StrokeThickness = 0, InnerRadius = 1, PointCount = 6 };
                    break;
                case ShapeType.Pentagon:
                    shape = new RegularPolygon() { StrokeThickness = 0, InnerRadius = 1, PointCount = 5 };
                    break;
                case ShapeType.Star:
                    shape = new RegularPolygon() { StrokeThickness = 0, InnerRadius = 0.47211, PointCount = 5 };
                    break;
                case ShapeType.Triangle:
                    shape = new RegularPolygon() { StrokeThickness = 0, InnerRadius = 1, PointCount = 3 };
                    break;
            }
            return shape;
        }
    }
}
