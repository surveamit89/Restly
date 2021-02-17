using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Plugin.CurrentActivity;
using Restly.Helper.HelperInterface;
using Restly.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restly.Droid.Helpers
{
    public class AppMessageBox : IMessageBox
    {
        public static BVMessageBox appMesaageBox;
        Handler handler;

        public void ShowMessageBox(string MessageText, string MessageTitle = "", bool ClosePage = true)
        {
            try
            {
                handler = CommonUtil.Handler;
                appMesaageBox = new BVMessageBox(CrossCurrentActivity.Current.Activity, MessageText, MessageTitle, ClosePage);
                appMesaageBox.Show();
            }
            catch (Exception e)
            {
                // Mvx.Resolve<IBVLogger>().DebugLog(typeof(BVLoader).Name, ex);
            }
        }

        public void CloseMessageBox()
        {
            try
            {
                if ((appMesaageBox != null))
                {
                    appMesaageBox.Dismiss();
                }
            }
            catch (Exception)
            {
                //Mvx.Resolve<IBVLogger>().DebugLog(typeof(BVLoader).Name, ex);
            }
        }
    }

    public class BVMessageBox : Dialog
    {
        public BVMessageBox(Context context, string message, string title, bool ClosePage) : base(context, Resource.Style.AppProgressDialog)
        {
            try
            {
                //ProgressBar pbar = new ProgressBar(Application.Context);
                SetCancelable(false);
                //SetContentView(Resource.Layout.common_messageBox);
                //ProgressBar pbar = new ProgressBar(Application.Context);
                //SetCancelable(false);
                //pbar.IndeterminateDrawable = context.GetDrawable(Resource.Animation.progress);

                SetContentView(Resource.Layout.common_messageBox);
                //SetContentView(dialog, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
                //Window.Attributes.Alpha = 0.4f;
                Window.SetGravity(GravityFlags.Center);
                Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
                TextView textMessage = FindViewById<TextView>(Resource.Id.textMessage);
                textMessage.Text = message;
                TextView textTitle = FindViewById<TextView>(Resource.Id.textTitle);
                textTitle.Text = title;
                TextView textDone = FindViewById<TextView>(Resource.Id.textDone);
                if (!ClosePage)
                {
                    textDone.Text = "OK";
                }
                textDone.Click += (s, e) =>
                {
                    Mvx.IoCProvider.Resolve<IMessageBox>().CloseMessageBox();
                    if (ClosePage)
                    {
                        Mvx.IoCProvider.Resolve<IMvxMessenger>().Publish(new ClosePageMessage(""));
                    }
                };
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(typeof(BVMessageBox).Name, ex);
            }
        }

    }
}