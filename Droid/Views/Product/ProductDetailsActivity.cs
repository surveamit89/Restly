using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restly.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restly.Droid.Views.Product
{
    [Activity(Theme = "@style/MasterDetailTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class ProductDetailsActivity : BaseActivity<ProductDetailsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_product_details);
            // Create your application here
        }
    }
}