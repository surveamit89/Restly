using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Restly.Helper;
using Restly.Helper.HelperInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restly
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            ;
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            //Mvx.IoCProvider.Resolve<IMvxAppStart>();
            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMvxMessenger, MvxMessengerHub>();
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            //Mvx.IoCProvider.RegisterSingleton<INavigationParameter>(new NavigationParameter());
            Mvx.IoCProvider.RegisterSingleton<IPersistData>(new PersistData());
            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IMvxNavigationService, MvxNavigationService>();
            //RegisterAppStart<TutorialViewModel>();
            //RegisterCustomAppStart<<>>();
            //var startup = Mvx.IoCProvider.Resolve<IMvxAppStart>();
            //startup.Start();
            //RegisterCustomAppStart<AppStart>();
        }

        public override Task Startup()
        {
            return base.Startup();

        }

    }
}
