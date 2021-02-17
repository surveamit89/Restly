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
    public class AppLogger : IAppLogger
    {
        public void DebugLog(string tag, string message)
        {
            try
            {
                //var trace = Mvx.IoCProvider.Resolve<IMvxTrace>();
                //trace.Trace(MvxTraceLevel.Diagnostic, tag, message);
            }
            catch (Exception)
            {
                //Mvx.IoCProvider.Resolve<IBVLogger>().DebugLog(typeof(BVLogger).Name, ex);
            }
        }

        public void DebugLog(string tag, Exception ex)
        {
            try
            {
                //Crashes.TrackError(ex);
                //var trace = Mvx.IoCProvider.Resolve<IMvxTrace>();
                //trace.Trace(MvxTraceLevel.Diagnostic, tag, ex.Message);
                //UserDialogs.Instance.Alert(ex.Message, "", "OK");
            }
            catch (Exception)
            {
                //Mvx.IoCProvider.Resolve<IBVLogger>().DebugLog(typeof(BVLogger).Name, en);
            }

        }
    }
}