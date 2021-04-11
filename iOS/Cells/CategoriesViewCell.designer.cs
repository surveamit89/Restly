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
    [Register ("CategoriesViewCell")]
    partial class CategoriesViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FFImageLoading.Cross.MvxCachedImageView iv_category { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbl_category { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView v_container { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (iv_category != null) {
                iv_category.Dispose ();
                iv_category = null;
            }

            if (lbl_category != null) {
                lbl_category.Dispose ();
                lbl_category = null;
            }

            if (v_container != null) {
                v_container.Dispose ();
                v_container = null;
            }
        }
    }
}