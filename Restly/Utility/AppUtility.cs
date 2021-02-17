using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Utility
{
    public static class AppUtility
    {
        public static bool IsConnectedToInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }

    }
}
