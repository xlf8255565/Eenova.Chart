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
using System.Windows.Data;

namespace Eenova.Chart.Setter
{
    public partial class AxisFormatSetter :BaseSetter
    {
        FormatSelector _formatSelector;

        public AxisFormatSetter(DataType dataType)
        {
            InitializeComponent();
            //InitializeSelector(dataType);
        }

        private void InitializeSelector(DataType dataType)
        {
            if (_formatSelector != null)
            {
                _formatSelector.ClearValue(FormatSelector.FormatProperty);
            }

            switch (dataType)
            {
                default:
                case DataType.Numberic:
                    _formatSelector = new NumbericFormatSelector();
                    break;
                case DataType.DateTime:
                    _formatSelector = new DateTimeFormatSelector();
                    break;
                case DataType.Text:
                    _formatSelector = new TextFormatSelector();
                    break;
            }

            _formatSelector.SetBinding(FormatSelector.FormatProperty, new Binding("Format")
            {
                Source = this.DataContext,
                UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
                Mode = BindingMode.TwoWay
            });

            this.bdRegion.Child = _formatSelector;
        }

        protected override void AddBindingProperties()
        {
            //this.AddBindingProperty(_formatSelector, FormatSelector.FormatProperty);
        }
    }
}
