using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restly.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restly.Droid.Views.Product
{
    [Activity(Theme = "@style/MasterDetailTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class CartActivity : BaseActivity<CartViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_cart);
            // Create your application here
        }
        public override void OnBackPressed()
        {
            ViewModel.BackCommand?.Execute(this);
        }
    }
}