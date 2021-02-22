using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using Newtonsoft.Json;
using Restly.Helper.HelperInterface;
using Restly.Models.ApiRequestResponse.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Restly.ViewModels.Order
{
    public class CartViewModel:BaseViewModel
    {
        #region  GlobalVariables
        #endregion

        #region Labels
        #endregion

        #region Visibility
        #endregion

        #region StringPropertyAndList
        private ObservableCollection<ProductData> _cartList;
        public ObservableCollection<ProductData> CartList
        {
            get { return _cartList; }
            set
            {
                _cartList = value;
                RaisePropertyChanged(() => CartList);
            }
        }
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
        
        private IMvxCommand _removeItemCommand;
        public IMvxCommand RemoveItemCommand
        {
            get
            {
                _removeItemCommand = _removeItemCommand ?? new MvxCommand<ProductData>(ProcessRemoveItemCommand);
                return _removeItemCommand;
            }
        }


        #endregion

        #region  Constructor
        public override void ViewAppeared()
        {
            try
            {
                var myCartdata = Mvx.IoCProvider.Resolve<IPersistData>().GetCartData();
                if (string.IsNullOrEmpty(myCartdata) || myCartdata == "null")
                {
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Sorry, Cart is empty.",
                        OkText = AppResources.Lbl_OK,
                        OnAction = () =>
                        {
                            BackCommand?.Execute(this);
                        }
                    });
                }
                else
                {
                    CartList = JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(myCartdata);
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(CartViewModel), ex);
            }
        }
        #endregion

        #region ProcessCommand
        private void ProcessBackCommand()
        {
            if (CartList!=null && CartList?.Count>0)
            {
                Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(CartList));
            }
            else
            {
                Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(null));
            }
            NavigationService.Close(this);
        }
        private async void ProcessRemoveItemCommand(ProductData item)
        {
            try
            {
                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Message = "Do you want to remove item from cart?",
                    OkText = AppResources.Lbl_Yes,
                    CancelText = AppResources.Lbl_No
                });
                if (result)
                {
                    CartList.Remove(item);
                    //CartList.Remove(CartList.FirstOrDefault(a=>a.Id==item.Id));
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(CartViewModel), ex);
            }
        }
        #endregion
    }
}
