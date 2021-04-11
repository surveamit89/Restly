using System;
using UIKit;

namespace Restly.iOS.Helpers
{
    public class TintHelper
    {
        public static void ColorImage(UIImage image, UIImageView imageView, UIColor color)
        {
            if (image == null)
            {
                return;
            }
            var newImage = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            imageView.TintColor = color;
            imageView.Image = newImage;
        }
    }
}
