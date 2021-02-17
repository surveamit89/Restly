using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restly.Helper.HelperInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restly.Droid.Helpers
{
    public class AppLoader : IAppLoader
    {
        private AppDialogBox progress;
        Handler handler;

        public AppLoader()
        {

        }

        public void ShowIndicator()
        {
            try
            {
                handler = CommonUtil.Handler;
                progress = CommonUtil.Progress;
                if (progress != null && !progress.IsShowing)
                {
                    progress.Show();
                }
            }
            catch (Exception)
            {
                //Mvx.IoCProvider.Resolve<IBVLogger>().DebugLog(typeof(BVLoader).Name, ex);
            }
        }

        public void StopIndicator()
        {
            try
            {
                if ((progress != null))
                {
                    progress.Dismiss();
                }
            }
            catch (Exception)
            {
                //Mvx.IoCProvider.Resolve<IBVLogger>().DebugLog(typeof(BVLoader).Name, ex);
            }
        }
    }
}