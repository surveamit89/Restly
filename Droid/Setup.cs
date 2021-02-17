using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using Restly.Droid.Helpers;
using Restly.Helper;
using Restly.Helper.HelperInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Restly.Droid
{
    public class Setup : MvxAndroidSetup
    {
        //public Setup(Context applicationContext) : base()
        //{
        //}

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterSingleton<IPersistData>(new PersistData());
            Mvx.IoCProvider.RegisterSingleton<IAppLogger>(new AppLogger());
            Mvx.IoCProvider.RegisterSingleton<IAppLoader>(new AppLoader());
            Mvx.IoCProvider.RegisterSingleton<IMessageBox>(new AppMessageBox());
            //Mvx.IoCProvider.RegisterSingleton<IFirebaseAnalytics>(new AppFirebaseAnalytics());
            UserDialogs.Init(() => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity);

            //FacebookSdk.SdkInitialize();
            //FacebookSdk.FullyInitialize();
            //FacebookSdk.AutoInitEnabled = true;
            //FacebookSdk.AutoLogAppEventsEnabled = true;
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxAndroidViewPresenter((IEnumerable<System.Reflection.Assembly>)AndroidViewAssemblies);
            Mvx.IoCProvider.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(MvxRecyclerView).Assembly
        };

        public override Assembly ExecutableAssembly => base.ExecutableAssembly;

        //protected override IMvxTrace CreateDebugTrace()
        //{
        //    return new DebugTrace();
        //}
        protected override void FillValueConverters(MvvmCross.Converters.IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("Language", new MvvmCross.Localization.MvxLanguageConverter());
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }
    }
}