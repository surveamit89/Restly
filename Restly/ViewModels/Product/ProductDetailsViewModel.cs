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

        private int _cartProductCount=1;
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
        private void ProcessAddToCartCommand()
        {
            try
            {
                BaseResponse result = ValidateRequest();
                if (result.Code==200)
                {
                    var myCartdata = Mvx.IoCProvider.Resolve<IPersistData>().GetCartData();
                    if (string.IsNullOrEmpty(myCartdata) || myCartdata == "null")
                    {
                        CartList = new ObservableCollection<ProductData>();
                        SelectedProduct.CartProductCount = CartProductCount;
                        CartList.Add(SelectedProduct);
                        Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(CartList));

                    }
                    else
                    {
                        CartList = JsonConvert.DeserializeObject<ObservableCollection<ProductData>>(myCartdata);
                        SelectedProduct.CartProductCount = CartProductCount;
                        CartList.Add(SelectedProduct);
                        Mvx.IoCProvider.Resolve<IPersistData>().SetCartData(JsonConvert.SerializeObject(CartList));
                    }


                    UserDialogs.Instance.Alert(new AlertConfig
                    {
                        Message = "Product added in cart successfully.",
                        OkText = AppResources.Lbl_OK,
                        OnAction = () =>
                        {
                            BackCommand?.Execute(this);
                        }
                    });
                    //Mvx.IoCProvider.Resolve<IMessageBox>().ShowMessageBox("Product added in cart Successfully.", null, true);
                    //NavigationService.Navigate<DashBoardViewModel>();
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

        private BaseResponse ValidateRequest()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                foreach (var item in SelectedProduct.Options)
                {
                    if (item.IsRequired && !item.Options.Any(a => a.IsSelected))
                    {
                        response.Code = 400;
                        response.Message = string.Format(IsRequiredErrorFormat, item.Name);
                        return response;
                    }

                    var validData =new ObservableCollection<OptionData>(item.Options.Where(a => a.IsSelected));
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
                    
                    else if(!item.IsRequired && validData?.Count>0)
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
                var response = await ProductService.ProcessToGetProductById(SelectedMenu);
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                if (response != null)
                {
                    if (response != null)// && response.Code == AppConstants.SuccessCode)
                    {
                        AssignData(response.Data);
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
                        //item.Min = 1;
                        //item.Max = 2;
                    }
                }

                SelectedProduct = data;
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
                if (CartProductCount!=1)
                {
                    CartProductCount--;
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
