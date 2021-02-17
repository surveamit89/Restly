using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.RecyclerView.Widget;
using MvvmCross;
using MvvmCross.DroidX.RecyclerView;
using Restly.Helper.HelperInterface;
using Restly.ViewModels.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restly.Droid.Views.DashBoard
{
    [Activity(Theme = "@style/MasterDetailTheme", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class DashBoardActivity : BaseActivity<DashBoardViewModel>
    {
        private bool doubleBackPressed = false;
        EditText editTextInput;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_dashBoard);
            editTextInput = FindViewById<EditText>(Resource.Id.editTextInput);
            // Create your application here
            ViewModel.ShowNotification += ShowNotification;
            ViewModel.HideKeyPad += () =>
            {
                HideSoftKeyboard(editTextInput);
            };
        }

        

    private void ShowNotification()
        {
            try
            {
                Toast.MakeText(this, "You have raised your hand. Please wait, a waiter will be with you soon.", ToastLength.Long).Show();
                Intent notificationIntent = new Intent(this, typeof(DashBoardActivity));
                PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.OneShot);

                NotificationCompat.Builder notificationBuilder;
                string channelId = "restly_notification_channel";
                string channelName = "Restly Notification Channel";
                var notificationManager = NotificationManager.FromContext(this);
                notificationManager.CancelAll();
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {

                    var notificationChannel = new NotificationChannel(channelId,
                                                                      CharSequence.ArrayFromStringArray(new string[] { channelName })[0],
                                                                      NotificationImportance.High);
                    notificationChannel.EnableLights(true);
                    notificationChannel.EnableVibration(true);
                    notificationManager.CreateNotificationChannel(notificationChannel);
                    notificationBuilder = new NotificationCompat.Builder(this, channelId);
                }
                else
                {
                    notificationBuilder = new NotificationCompat.Builder(this, channelId);
                }
                notificationBuilder.SetContentTitle("Restly Notifications");
                notificationBuilder.SetContentText("You have raised your hand. Please wait, a waiter will be with you soon.");

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    notificationBuilder.SetSmallIcon(Resource.Drawable.Icon);
                    notificationBuilder.SetColor(Resource.Color.white_color);
                }
                else
                {
                    notificationBuilder.SetSmallIcon(Resource.Drawable.Icon);
                    notificationBuilder.SetLargeIcon(BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.Icon));
                }

                notificationBuilder.SetStyle(new NotificationCompat.BigTextStyle());
                notificationBuilder.SetAutoCancel(true);
                notificationBuilder.SetContentIntent(pendingIntent);
                notificationBuilder.SetVisibility((int)NotificationVisibility.Public);
                notificationBuilder.SetPriority((int)NotificationCompat.PriorityHigh);


                var notification = notificationBuilder.Build();
                notificationManager.Notify(0, notification);
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardActivity), ex);
            }
        }
       
        public override void OnBackPressed()
        {
            try
            {
                if (doubleBackPressed)
                {
                    ExitApp();
                    return;
                }

                this.doubleBackPressed = true;

                Handler h = new Handler();
                Action myAction = () =>
                {
                    doubleBackPressed = false;
                };

                h.PostDelayed(myAction, 2000);
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(typeof(DashBoardActivity).Name, ex);
            }
        }

        private async void ExitApp()
        {
            try
            {
                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Message = ViewModel.ExitAppTitle,
                    OkText = ViewModel.YesTitle,
                    CancelText = ViewModel.CancelTitle
                });
                if (result)
                {
                    var a = new Intent(Intent.ActionMain);
                    a.AddCategory(Intent.CategoryHome);
                    a.SetFlags(ActivityFlags.NewTask);
                    StartActivity(a);
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(typeof(DashBoardActivity).Name, ex);
            }
        }
    }
}