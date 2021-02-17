using MvvmCross;
using MvvmCross.Commands;
using Restly.Helper.HelperInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Restly.ViewModels.Splash
{
    public class SplashViewModel : BaseViewModel
    {
        #region  GlobalVariables
        #endregion

        #region Labels
        #endregion

        #region Visibility
        #endregion

        #region StringPropertyAndList
        #endregion

        #region Command
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                _backCommand = _backCommand ?? new MvxCommand(ProcessBackCommand);
                return _backCommand;
            }
        }
        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                _nextCommand = _nextCommand ?? new MvxCommand(ProcessNextCommand);
                return _nextCommand;
            }
        }
        #endregion

        #region  Constructor
        #endregion

        #region ProcessCommand
        private void ProcessBackCommand()
        {
            NavigationService.Close(this);
        }
        private void ProcessNextCommand()
        {
            try
            {
                //NavigationService.Navigate<DashBoardViewModel>();
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(SplashViewModel), ex);
            }
        }
        #endregion
    }
}
