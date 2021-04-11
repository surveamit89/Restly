using Foundation;
using MvvmCross.Platforms.Ios.Core;
using UIKit;

namespace Restly.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}

