// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Restly.iOS.Cells
{
    [Register ("RestaurantViewCell")]
    partial class RestaurantViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FFImageLoading.Cross.MvxCachedImageView iv_rest_image { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView iv_star { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbl_rest_description { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbl_rest_name { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbl_rest_rating { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (iv_rest_image != null) {
                iv_rest_image.Dispose ();
                iv_rest_image = null;
            }

            if (iv_star != null) {
                iv_star.Dispose ();
                iv_star = null;
            }

            if (lbl_rest_description != null) {
                lbl_rest_description.Dispose ();
                lbl_rest_description = null;
            }

            if (lbl_rest_name != null) {
                lbl_rest_name.Dispose ();
                lbl_rest_name = null;
            }

            if (lbl_rest_rating != null) {
                lbl_rest_rating.Dispose ();
                lbl_rest_rating = null;
            }
        }
    }
}