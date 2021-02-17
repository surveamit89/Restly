using MvvmCross.Plugin.Color;
using System.Drawing;
using System.Globalization;

namespace Restly.Converter
{
    public class StringToColorValueConverter : MvxColorValueConverter
    {
        
        private static readonly Color BlackTextColor = Color.FromArgb(0, 0, 0);
        private static readonly Color WhiteTextColor = Color.FromArgb(255, 255, 255);
        private static readonly Color AppTextColor = Color.FromArgb(467, 157, 68);
        private static readonly Color Transparent = Color.Transparent;
        protected override Color Convert(object value, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "WhiteColor":
                    {
                        return WhiteTextColor;
                    }
                case "BlackColor":
                    {
                        return BlackTextColor;
                    }
                case "AppColor":
                    {
                        return AppTextColor;
                    }
                case "TransparentColor":
                    {
                        return Transparent;
                    }
                default:
                    {
                        return WhiteTextColor;
                    }
            }

        }
    }
}
