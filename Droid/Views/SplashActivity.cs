using Restly.Droid.Views;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using MvvmCross;
using Restly.Helper.HelperInterface;
using Restly.ViewModels.Splash;
using System;
using System.Threading;
using Android.Content.PM;
using Restly.Droid.Views.DashBoard;

namespace Restly.Droid.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/SplashTheme", MainLauncher = true, WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : BaseActivity<SplashViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                var newIntent = new Intent(this, typeof(DashBoardActivity));
                newIntent.AddFlags(ActivityFlags.ClearTop);
                newIntent.AddFlags(ActivityFlags.SingleTop);
                StartActivity(newIntent);
            }
            catch (Exception ex)
            {
                var newIntent = new Intent(this, typeof(DashBoardActivity));
                newIntent.AddFlags(ActivityFlags.ClearTop);
                newIntent.AddFlags(ActivityFlags.SingleTop);
                StartActivity(newIntent);
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(SplashActivity), ex);
            }
        }
    }
}