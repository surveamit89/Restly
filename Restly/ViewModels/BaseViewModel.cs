using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.ViewModels
{
    public class BaseViewModel : MvxViewModel<object>
    {
        public object NavigationObject;
        public static IMvxNavigationService NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        public override void Prepare(object navigationObject)
        {
            // receive and store the parameter here
            NavigationObject = navigationObject;
        }
    }
}
