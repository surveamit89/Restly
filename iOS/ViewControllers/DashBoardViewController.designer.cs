// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Restly.iOS
{
    [Register ("DashBoardViewController")]
    partial class DashBoardViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btn_search { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView cv_categories { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbl_title { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tf_search { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tv_restaurant { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btn_search != null) {
                btn_search.Dispose ();
                btn_search = null;
            }

            if (cv_categories != null) {
                cv_categories.Dispose ();
                cv_categories = null;
            }

            if (lbl_title != null) {
                lbl_title.Dispose ();
                lbl_title = null;
            }

            if (tf_search != null) {
                tf_search.Dispose ();
                tf_search = null;
            }

            if (tv_restaurant != null) {
                tv_restaurant.Dispose ();
                tv_restaurant = null;
            }
        }
    }
}