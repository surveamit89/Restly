using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Utility
{
    public class AppConstants
    {
        public const int SuccessCode = 200;
        public static class RestApi
        {
            internal static readonly string InitRestaurants = "Restaurant/InitRestaurants";
            internal static readonly string GetRestaurants = "Restaurant/GetRestaurants";

            internal static readonly string InitRestaurantMenu = "Product/InitRestaurantMenu";
            internal static readonly string GetMenuList = "Product/GetMenuList";
            internal static readonly string GetProductById = "Product/GetProductById";
        }

        public static class SecureStorageKeys
        {
            public static readonly string AppCartData = "app_cart_data";
        }
    }
}
