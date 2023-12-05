using System;
using System.Windows.Media;

namespace Eenova.Chart.Helpers
{
    public class ColorOperator
    {
        public static Color ConvertFromString(string argb)
        {
            try
            {
                var alpha = argb.Substring(0, 2);
                var red = argb.Substring(2, 2);
                var green = argb.Substring(4, 2);
                var blue = argb.Substring(6, 2);

                var alphaByte = Convert.ToByte(alpha, 16);
                var redByte = Convert.ToByte(red, 16);
                var greenByte = Convert.ToByte(green, 16);
                var blueByte = Convert.ToByte(blue, 16);
                return Color.FromArgb(alphaByte, redByte, greenByte, blueByte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
