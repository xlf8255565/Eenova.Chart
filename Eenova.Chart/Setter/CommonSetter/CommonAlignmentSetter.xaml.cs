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
using System.Windows.Controls;
using System.Windows.Media;

namespace Eenova.Chart.Setter
{
    public partial class CommonAlignmentSetter : BaseSetter
    {
        public CommonAlignmentSetter()
        {
            InitializeComponent();
            this.PreviewPanel.SizeChanged += (s, e) => this.SetClip();
        }

        private void SetClip()
        {
            this.PreviewPanel.Clip = new RectangleGeometry()
            {
                Rect = new Rect(0, 0, this.PreviewPanel.ActualWidth, this.PreviewPanel.ActualHeight)
            };
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbAlignment, AlignmentSelector.SHorizontalAlignmentProperty);
            this.AddBindingProperty(this.cbAlignment, AlignmentSelector.SVerticalAlignmentProperty);
            this.AddBindingProperty(this.cbOrientation, ComboBox.SelectedValueProperty);
            this.AddBindingProperty(this.nbTextRotation, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.psOrigin, PointSelector.PointProperty);
        }
    }
}
