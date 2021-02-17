using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Restly.ViewModels.Splash;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restly
{
    public class AppStart : MvxAppStart
    {
        private readonly IMvxNavigationService _navigationService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) : base(application, navigationService)
        {
            _navigationService = navigationService;
            //NavigateToFirstViewModel();
        }

        //public void Start(object hint = null)
        //{
        //    StartApp();
        //}

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<SplashViewModel>();
        }

    }
}
