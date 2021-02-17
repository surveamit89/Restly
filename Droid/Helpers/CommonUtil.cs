using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restly.Droid.Helpers
{
    public class CommonUtil
    {
        public static Handler Handler { get; set; }
        public static AppDialogBox Progress { get; set; }
    }
}