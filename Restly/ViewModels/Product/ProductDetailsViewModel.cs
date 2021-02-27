using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using Newtonsoft.Json;
using Restly.Helper.HelperInterface;
using Restly.Models.ApiRequestResponse;
using Restly.Models.ApiRequestResponse.Product;
using Restly.NavigationParam;
using Restly.Services.Restaurant;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Restly.ViewModels.Product
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        #region  GlobalVariables
        public string IsRequiredErrorFormat = "Please select {0}.";
        public string IsMaxErrorFormat = "You can select max {0} option from {1}.";
        public string IsMinErrorFormat = "You must select min {0} option from {1}.";
        #endregion

        #region Labels
        #endregion

        #region Visibility

        private bool _enabledDecreaseCount = false;
        public bool EnabledDecreaseCount
        {
            get { return _enabledDecreaseCount; }
            set
            {
                _enabledDecreaseCount = value;
                RaisePropertyChanged(() => EnabledDecreaseCount);
            }
        }
        private bool _showSimilarItemDetail = false;
        public bool ShowSimilarItemDetail
        {
            get { return _showSimilarItemDetail; }
            set
            {
                _showSimilarItemDetail = value;
                RaisePropertyChanged(() => ShowSimilarItemDetail);
            }
        }
        private bool _enabledDecreaseCountForFrequent = false;
        public bool EnabledDecreaseCountForFrequent
        {
            get { return _enabledDecreaseCountForFrequent; }
            set
            {
                _enabledDecreaseCountForFrequent = value;
                RaisePropertyChanged(() => EnabledDecreaseCountForFrequent);
            }
        }
       
        private bool _isFromCart = false;
        public bool IsFromCart
        {
            get { return _isFromCart; }
            set
            {
                _isFromCart = value;
                RaisePropertyChanged(() => IsFromCart);
                IsNotFromCart = !value;
            }
        }
        private bool _isNotFromCart = true;
        public bool IsNotFromCart
        {
            get { return _isNotFromCart; }
            set
            {
                _isNotFromCart = value;
                RaisePropertyChanged(() => IsNotFromCart);
            }
        }

        #endregion

        #region StringPropertyAndList
        private ProductNavigation _navigationParam;
        public ProductNavigation NavigationParam
        {
            get { return _navigationParam; }
            set
            {
                _navigationParam = value;
                RaisePropertyChanged(() => NavigationParam);
            }
        }

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

        private ProductData _selectedProduct;
        public ProductData SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                RaisePropertyChanged(() => SelectedProduct);
            }
        }

        private ProductData _selectedFrequentProduct;
        public ProductData SelectedFrequentProduct
        {
            get { return _selectedFrequentProduct; }
            set
            {
                _selectedFrequentProduct = value;
                RaisePropertyChanged(() => SelectedFrequentProduct);
            }
        }

        private int _cartProductCount = 1;
        public int CartProductCount
        {
            get { return _cartProductCount; }
            set
            {
                _cartProductCount = value;
                RaisePropertyChanged(() => CartProductCount);
                EnabledDecreaseCount = value > 1 ? true : false;
            }
        }

        private int _selectedFrequentlyBoughtCount = 1;
        public int SelectedFrequentlyBoughtCount
        {
            get { return _selectedFrequentlyBoughtCount; }
            set
            {
                _selectedFrequentlyBoughtCount = value;
                RaisePropertyChanged(() => SelectedFrequentlyBoughtCount);
                EnabledDecreaseCountForFrequent = value > 1 ? true : false;
            }
        }
        private FrequentlyBoughtProduct _selectedFrequentlyBought;
        public FrequentlyBoughtProduct SelectedFrequentlyBought
        {
            get { return _selectedFrequentlyBought; }
            set
            {
                _selectedFrequentlyBought = value;
                RaisePropertyChanged(() => SelectedFrequentlyBought);
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
        private ICommand _addToCartCommand;
        public ICommand AddToCartCommand
        {
            get
            {
                _addToCartCommand = _addToCartCommand ?? new MvxCommand(ProcessAddToCartCommand);
                return _addToCartCommand;
            }
        }

        private ICommand _removeFromCartCommand;
        public ICommand RemoveFromCartCommand
        {
            get
            {
                _removeFromCartCommand = _removeFromCartCommand ?? new MvxCommand(ProcessRemoveFromCartCommand);
                return _removeFromCartCommand;
            }
        }

        private ICommand _increaseCountCommand;
        public ICommand IncreaseCountCommand
        {
            get
            {
                _increaseCountCommand = _increaseCountCommand ?? new MvxCommand(ProcessIncreaseCountCommand);
                return _increaseCountCommand;
            }
        }



        private ICommand _decreaseCountCommand;
        public ICommand DecreaseCountCommand
        {
            get
            {
                _decreaseCountCommand = _decreaseCountCommand ?? new MvxCommand(ProcessDecreaseCountCommand);
                return _decreaseCountCommand;
            }
        }
        private ICommand _selectMoreItemCommand;
        public ICommand SelectMoreItemCommand
        {
            get
            {
                _selectMoreItemCommand = _selectMoreItemCommand ?? new MvxCommand<FrequentlyBoughtProduct>(ProcessSelectMoreItemCommand);
                return _selectMoreItemCommand;
            }
        }

        private IMvxCommand _optionSelectedCommand;
        public IMvxCommand OptionSelectedCommand
        {
            get
            {
                _optionSelectedCommand = _optionSelectedCommand ?? new MvxCommand<OptionData>(ProcessOptionSelectedCommand);
                return _optionSelectedCommand;
            }
        }
        private ICommand _addToCartConfirmCommand;
        public ICommand AddToCartConfirmCommand
        {
            get
            {
                _addToCartConfirmCommand = _addToCartConfirmCommand ?? new MvxCommand(ProcessAddToCartConfirmCommand);
                return _addToCartConfirmCommand;
            }
        }
        private ICommand _hideSimilarItemDetailCommand;
        public ICommand HideSimilarItemDetailCommand
        {
            get
            {
                _hideSimilarItemDetailCommand = _hideSimilarItemDetailCommand ?? new MvxCommand(ProcessHideSimilarItemDetailCommand);
                return _hideSimilarItemDetailCommand;
            }
        }
        private ICommand _increaseFrequentCountCommand;
        public ICommand IncreaseFrequentCountCommand
        {
            get
            {
                _increaseFrequentCountCommand = _increaseFrequentCountCommand ?? new MvxCommand(ProcessIncreaseFrequentCountCommand);
                return _increaseFrequentCountCommand;
            }
        }



        private ICommand _decreaseFrequentCountCommand;
        public ICommand DecreaseFrequentCountCommand
        {
            get
            {
                _decreaseFrequentCountCommand = _decreaseFrequentCountCommand ?? new MvxCommand(ProcessDecreaseFrequentCountCommand);
                return _decreaseFrequentCountCommand;
            }
        }


        /// <summary>
        /// validating option with single selection and multi selection
        /// </summary>
        private void ProcessOptionSelectedCommand(OptionData obj)
        {
            try
            {
                if (obj.IsSingleChoice)
                {
                    foreach (var item in SelectedProduct.Options)
                    {
                        var found = item.Options.FirstOrDefault(a => a.Id == obj.Id);
                        if (found != null)
                        {
                            foreach (var subitem in item.Options)
                            {
                                if (subitem.Id == obj.Id)
                                {
                                    if (!subitem.IsSelected)
                                    {
                                        subitem.IsSelected = false;
                                    }
                                }
                                else
                                {
                                    subitem.IsSelected = false;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        #endregion

        #region  Constructor
        public override void Prepare(object parameter)
        {
            try
            {
                if (parameter != null)
                {
                    NavigationParam = parameter as ProductNavigation;

                    GetMenuDetails(NavigationParam.SelectedMenu);
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        #endregion

        #region ProcessCommand
        private void ProcessBackCommand()
        {
            NavigationService.Close(this);
        }
        /// <summary>
        /// add to cart item
        /// </summary>
        private void ProcessAddToCartCommand()
        {
            try
            {
                BaseResponse result = ValidateRequest(SelectedProduct);
                if (result.Code == 200)
                {
                    SelectedProduct.CartProductCount = CartProductCount;
                    UpdateDataInCart(SelectedProduct);
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Product added in cart successfully.",
                        OkText = AppResources.Lbl_OK,
                        OnAction = () =>
                        {
                            BackCommand?.Execute(this);
                        }
                    });
                }
                else
                {
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = result.Message,
                        OkText = AppResources.Lbl_OK,
                        OnAction = () =>
                        {

                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void UpdateDataInCart(ProductData selectedProduct)
        {
            try
            {
                var myCartdata = Mvx.IoCProvider.Resolve<IPersistData>().GetCartData();
                if (string.IsNullOrEmpty(myCartdata) || myCartdata == "null")
                {
                    CartList = new ObservableCollection<ProductData>();
                    
                    CartList.Add(selectedProduct);
                    Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(CartList));

                }
                else
                {
                    CartList = JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(myCartdata);
                    CartList.Add(selectedProduct);
                    Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(CartList));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// validfation
        /// </summary>
        private BaseResponse ValidateRequest(ProductData selectedProduct)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                foreach (var item in selectedProduct.Options)
                {
                    if (item.IsRequired && !item.Options.Any(a => a.IsSelected))
                    {
                        response.Code = 400;
                        response.Message = string.Format(IsRequiredErrorFormat, item.Name);
                        return response;
                    }

                    var validData = new ObservableCollection<OptionData>(item.Options.Where(a => a.IsSelected));
                    if (item.IsRequired && validData?.Count > 0)
                    {
                        if (item.Max != 0 && validData?.Count() > item.Max)
                        {
                            response.Code = 400;
                            response.Message = string.Format(IsMaxErrorFormat, new string[2] { item.Max.ToString(), item.Name });
                            return response;
                        }
                        else if (item.Min != 0 && validData?.Count() < item.Min)
                        {
                            response.Code = 400;
                            response.Message = string.Format(IsMinErrorFormat, new string[2] { item.Min.ToString(), item.Name });
                            return response;
                        }
                    }

                    else if (!item.IsRequired && validData?.Count > 0)
                    {
                        if (item.Max != 0 && validData?.Count() > item.Max)
                        {
                            response.Code = 400;
                            response.Message = string.Format(IsMaxErrorFormat, new string[2] { item.Max.ToString(), item.Name });
                            return response;
                        }
                        else if (item.Min != 0 && validData?.Count() < item.Min)
                        {
                            response.Code = 400;
                            response.Message = string.Format(IsMinErrorFormat, new string[2] { item.Min.ToString(), item.Name });
                            return response;
                        }
                    }
                }

                response.Code = 200;
                response.Message = "done";
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 400;
                response.Message = ex.Message;
                return response;
            }
        }

        private async void GetMenuDetails(MenuData SelectedMenu)
        {
            try
            {
                Mvx.IoCProvider.Resolve<IAppLoader>().ShowIndicator();
                var response = await ProductService.ProcessToGetProductById(SelectedMenu.Id);
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                if (response != null)
                {
                    if (response != null)// && response.Code == AppConstants.SuccessCode)
                    {
                        SelectedProduct = response.Data;
                        AssignData(SelectedProduct);
                        AssignFrequentDataDetails(SelectedProduct);
                        CheckIsFromCart(SelectedMenu.Id);
                    }
                    else
                    {
                        Mvx.IoCProvider.Resolve<IMessageBox>().ShowMessageBox(response.Message, null, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void AssignData(ProductData data)
        {
            try
            {
                foreach (var item in data.Options)
                {
                    foreach (var subitem in item.Options)
                    {
                        subitem.IsSingleChoice = item.IsSingleChoice;
                        subitem.ItemSelectedCommand = OptionSelectedCommand;
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void ProcessIncreaseCountCommand()
        {
            try
            {
                CartProductCount++;
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }
        private void ProcessDecreaseCountCommand()
        {
            try
            {
                if (CartProductCount != 1)
                {
                    CartProductCount--;
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private async void ProcessSelectMoreItemCommand(FrequentlyBoughtProduct obj)
        {
            try
            {
                var found = SelectedProduct.FrequentlyBoughtProducts.FirstOrDefault(a => a.Id == obj?.Id);
                if (found != null && found.IsSelected)
                {
                    found.IsSelected = false;
                    SelectedFrequentlyBought = null;
                    ShowSimilarItemDetail = false;
                    ProcessRemoveItemCommand(found.Id);
                }
                else
                {
                    SelectedFrequentlyBought = obj;
                    SelectedFrequentlyBoughtCount = 1;
                    Mvx.IoCProvider.Resolve<IAppLoader>().ShowIndicator();
                    var response = await ProductService.ProcessToGetProductById(obj.Id);
                    Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                    if (response != null)
                    {
                        if (response != null)// && response.Code == AppConstants.SuccessCode)
                        {
                            SelectedFrequentProduct = response.Data;
                            AssignData(SelectedFrequentProduct);
                            AssignFrequentDataDetails(SelectedFrequentProduct);
                        }
                        else
                        {
                            Mvx.IoCProvider.Resolve<IMessageBox>().ShowMessageBox(response.Message, null, false);
                        }
                    }
                    ShowSimilarItemDetail = true;
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }
        private void ProcessAddToCartConfirmCommand()
        {
            try
            {
                BaseResponse result = ValidateRequest(SelectedFrequentProduct);
                if (result.Code == 200)
                {
                    SelectedFrequentProduct.CartProductCount = SelectedFrequentlyBoughtCount;
                    UpdateDataInCart(SelectedFrequentProduct);
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Product added in cart successfully.",
                        OkText = AppResources.Lbl_OK,
                        OnAction = () =>
                        {
                            ShowSimilarItemDetail = false;
                            AssignFrequentDataDetails(SelectedProduct);
                        }
                    });
                }
                else
                {
                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = result.Message,
                        OkText = AppResources.Lbl_OK,
                        OnAction = () =>
                        {

                        }
                    });
                }



            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void ProcessHideSimilarItemDetailCommand()
        {
            try
            {
                ShowSimilarItemDetail = false;
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void AssignFrequentDataDetails(ProductData selected)
        {
            try
            {
                var myCartdata = Mvx.IoCProvider.Resolve<IPersistData>().GetCartData();
                if (!string.IsNullOrEmpty(myCartdata) && myCartdata != "null")
                {
                   var CartDataList = JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(myCartdata);
                    foreach (var item in CartDataList)
                    {
                        var found = selected.FrequentlyBoughtProducts.FirstOrDefault(a => a.Id == item?.Id);
                        if (found != null )
                        {
                            found.IsSelected = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void ProcessRemoveItemCommand(long item)
        {
            try
            {
                var myCartdata = Mvx.IoCProvider.Resolve<IPersistData>().GetCartData();
                if (!string.IsNullOrEmpty(myCartdata) && myCartdata != "null")
                {
                    var CartDataList = JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(myCartdata);
                    CartDataList.Remove(CartDataList.FirstOrDefault(a=>a.Id==item));
                    if (CartDataList != null && CartDataList?.Count > 0)
                    {
                        Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(CartDataList));
                    }
                    else
                    {
                        Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(null));
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void ProcessIncreaseFrequentCountCommand()
        {
            try
            {
                SelectedFrequentlyBoughtCount++;
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }
        private void ProcessDecreaseFrequentCountCommand()
        {
            try
            {
                if (SelectedFrequentlyBoughtCount != 1)
                {
                    SelectedFrequentlyBoughtCount--;
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private void CheckIsFromCart(long ID)
        {
            try
            {
                var myCartdata = Mvx.IoCProvider.Resolve<IPersistData>().GetCartData();
                if (!string.IsNullOrEmpty(myCartdata) && myCartdata != "null")
                {
                    var CartDataList = JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(myCartdata);
                    var found = CartDataList.FirstOrDefault(a => a.Id == ID);
                    if (found != null)
                    {
                        IsFromCart = true;
                    }
                    else
                    {
                        IsFromCart = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        private async void ProcessRemoveFromCartCommand()
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
                    var myCartdata = Mvx.IoCProvider.Resolve<IPersistData>().GetCartData();
                    if (!string.IsNullOrEmpty(myCartdata) && myCartdata != "null")
                    {
                        var CartDataList = JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(myCartdata);
                        var found = CartDataList.FirstOrDefault(a => a.Id == SelectedProduct.Id);
                        if (found != null)
                        {
                            CartDataList.Remove(found);
                            Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(CartDataList));
                            IsFromCart = false;
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductDetailsViewModel), ex);
            }
        }

        #endregion
    }
}
