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
    [Register ("SplashViewController")]
    partial class SplashViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView iv_logo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel l_welcome_message { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (iv_logo != null) {
                iv_logo.Dispose ();
                iv_logo = null;
            }

            if (l_welcome_message != null) {
                l_welcome_message.Dispose ();
                l_welcome_message = null;
            }
        }
    }
}