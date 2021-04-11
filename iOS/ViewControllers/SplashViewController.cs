using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Restly.ViewModels.Splash;
using UIKit;

namespace Restly.iOS
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class SplashViewController : MvxViewController<SplashViewModel>
    {
        public SplashViewController() : base("SplashViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            using (var bindingSet = this.CreateBindingSet())
            {
                bindingSet.Bind(l_welcome_message).To(vm=> vm.WelcomeMessage);
            }
            l_welcome_message.TextColor = UIColor.White;
            View.BackgroundColor = UIColor.Clear.FromHex(ViewModel.BackgroundColor);

            if (NavigationController.NavigationBar !=null)
            {
                NavigationController.NavigationBarHidden = true;
                NavigationController.NavigationBar.Hidden = true;
            }
            // Perform any additional setup after loading the view, typically from a nib.

            ViewModel.NextCommand?.Execute(null);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

