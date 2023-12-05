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
using System.Xml.Linq;
using Eenova.Chart.Converters;

namespace Eenova.Chart.Styler
{
    class VerticalNoteStyler : XmlStyler
    {
        public VerticalNoteStyler(VerticalNote element)
            : this(element, "VerticalNote")
        {
        }

        public VerticalNoteStyler(VerticalNote element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => VerticalNote.ForegroundProperty, new Brush2ColorConverter());
            AddAttribute(() => VerticalNote.BackgroundProperty, new Brush2ColorConverter());
            AddAttribute(() => VerticalNote.FontSizeProperty);
            AddAttribute(() => VerticalNote.FontStyleProperty);
            AddAttribute(() => VerticalNote.FontWeightProperty);
            AddAttribute(() => VerticalNote.FontFamilyProperty);
            AddAttribute(() => VerticalNote.TextDecorationsProperty, new FontUnderlineConverter());
            AddAttribute(() => VerticalNote.XProperty);

            return _xml;
        }
    }
}
