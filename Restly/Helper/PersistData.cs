using MvvmCross;
using Restly.Helper.HelperInterface;
using Restly.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Restly.Helper
{
    public class PersistData : IPersistData
    {
        public string GetCartData()
        {
            return Preferences.Get(AppConstants.SecureStorageKeys.AppCartData, null);
        }

        public void SetCartData(string userData)
        {
            try
            {
                Preferences.Set(AppConstants.SecureStorageKeys.AppCartData, userData);

            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(PersistData), ex);
            }

        }
    }
}
