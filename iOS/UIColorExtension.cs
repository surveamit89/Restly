using System;
using UIKit;

namespace Restly.iOS
{
    public static class UIColorExtension
    {
		public static UIColor FromHex(this UIColor color, int hexValue)
		{
			try
			{
				return UIColor.FromRGB(
					(((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
					(((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
					(((float)(hexValue & 0xFF)) / 255.0f)
				);
			}
			catch (Exception ex)
			{
			}
			return UIColor.Red;
		}
	}
}
