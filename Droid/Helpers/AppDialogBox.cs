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
    public class AppDialogBox : Dialog
    {
        public AppDialogBox(Context context) : base(context, Resource.Style.AppProgressDialog)
        {
            try
            {
                ProgressBar pbar = new ProgressBar(Application.Context);
                SetCancelable(false);
                //pbar.IndeterminateDrawable = context.GetDrawable(Resource.Animation.progress);
                SetContentView(pbar, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
                Window.Attributes.Alpha = 0.4f;
                Window.SetGravity(GravityFlags.Center);
                Window.SetBackgroundDrawableResource(Resource.Color.transparent);
            }
            catch (Exception)
            {
                //Mvx.Resolve<IBVLogger>().DebugLog(typeof(BVDialogBox).Name, ex);
            }
        }
    }
}