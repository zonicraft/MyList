using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace MyList.UWP.Utils
{
    class ColorConverter
    {
        public static Brush GetBrushFromCode(string colorCode)
        {
            return new SolidColorBrush(GetColorFromCode(colorCode));
        }


        public static Brush InvertColorToBrush(string colorCode)
        {
            Color color = GetColorFromCode(colorCode);
            byte RGBMAX = 255;
            Color fc = Color.FromArgb(255, (byte)(RGBMAX - color.R),
             (byte)(RGBMAX - color.G), (byte)(RGBMAX - color.B));
            return new SolidColorBrush(fc);
        }

        public static Color GetColorFromCode(string colorCode)
        {

            colorCode = colorCode.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(colorCode.Substring(0, 2), 16));
            a = 255;
            byte r = (byte)(Convert.ToUInt32(colorCode.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(colorCode.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(colorCode.Substring(6, 2), 16));
            return Color.FromArgb(a, r, g, b);

        }
    }
}
