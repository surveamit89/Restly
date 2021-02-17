using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.ViewModels;
using MvvmCross.Platforms.Android.Views;
using System.Globalization;
using Restly.Droid.Helpers;
using Android.Views;
using Android.Views.InputMethods;
using Restly.Helper.HelperInterface;
using MvvmCross;
using Android.Content;

namespace Restly.Droid.Views
{
    [Activity(Theme = "@style/MasterDetailTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class BaseActivity<T> : MvxActivity<T> where T : MvxViewModel
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CommonUtil.Handler = new Handler(Looper.MainLooper);
            CommonUtil.Progress = new AppDialogBox(this);
            CultureInfo.GetCultures(CultureTypes.NeutralCultures);
        }
        public void HideSoftKeyboard(View view)
        {
            try
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
            catch (System.Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog("BaseActivity", ex);
            }
        }
    }
}