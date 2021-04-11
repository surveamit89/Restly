using System;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross;
using MvvmCross.Converters;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;
using Restly.Converter;
using Restly.Helper.HelperInterface;
using Restly.iOS.Services;

namespace Restly.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        public Setup()
        {
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            InitDependencyInjection();
        }

        protected void InitDependencyInjection()
        {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAppLoader>(() => new AppLoader());
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAppLogger>(() => new AppLogger());

        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("StringToColorValue", new StringToColorValueConverter());
        }

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies;
                var list = new List<Assembly>();
                foreach(var assembly in toReturn)
                {
                    list.Add(assembly);
                }
                list.Add(typeof(StringToColorValueConverter).Assembly);
                return list;
            }
        }
    }
}
