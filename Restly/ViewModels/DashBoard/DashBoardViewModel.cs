using MvvmCross;
using MvvmCross.Commands;
using Restly.Helper.HelperInterface;
using Restly.Models.ApiRequestResponse.Product;
using Restly.Models.ApiRequestResponse.Restaurant;
using Restly.NavigationParam;
using Restly.Services.Restaurant;
using Restly.Utility;
using Restly.ViewModels.Order;
using Restly.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Restly.ViewModels.DashBoard
{
    public class DashBoardViewModel : BaseViewModel
    {
        #region  GlobalVariables
        public Action ShowNotification;
        public Action HideKeyPad;
        #endregion

        #region Labels

        public string ClearText => "Clear";
        public string SearchHint => "Search for a product or restaurant";

        public string ExitAppTitle
        {
            get { return AppResources.Msg_ExitTheApp; }
        }
        public string YesTitle
        {
            get { return AppResources.Lbl_Yes; }
        }
        public string CancelTitle
        {
            get { return AppResources.Lbl_Cancel; }
        }
        #endregion

        #region Visibility
        private bool _searchVisibility = false;
        public bool SearchVisibility
        {
            get { return _searchVisibility; }
            set
            {
                _searchVisibility = value;
                RaisePropertyChanged(() => SearchVisibility);
            }
        }

        private bool _locationVisibility=false;
        public bool LocationVisibility
        {
            get { return _locationVisibility; }
            set
            {
                _locationVisibility = value;
                RaisePropertyChanged(() => LocationVisibility);
            }
        }
        private bool _showRestaurant = true;
        public bool ShowRestaurant
        {
            get { return _showRestaurant; }
            set
            {
                _showRestaurant = value;
                RaisePropertyChanged(() => ShowRestaurant);
            }
        }
        #endregion

        #region StringPropertyAndList
        private InitRestaurantsResponse _restaurantsData;
        public InitRestaurantsResponse RestaurantsData
        {
            get { return _restaurantsData; }
            set
            {
                _restaurantsData = value;
                RaisePropertyChanged(() => RestaurantsData);
            }
        }
        private ObservableCollection<RestaurantData> _restaurantsList;
        public ObservableCollection<RestaurantData> RestaurantsList
        {
            get { return _restaurantsList; }
            set
            {
                _restaurantsList = value;
                RaisePropertyChanged(() => RestaurantsList);
                NoDataLabel = (value != null && value.Count !=0) ? "" : AppResources.Lbl_NoData;
            }
        }
        
        private ObservableCollection<RestaurantData> _OGRestaurantsList;
        public ObservableCollection<RestaurantData> OGRestaurantsList
        {
            get { return _OGRestaurantsList; }
            set
            {
                _OGRestaurantsList = value;
                RaisePropertyChanged(() => OGRestaurantsList);
            }
        }
        private RestaurantData _selectedRestaurant;
        public RestaurantData SelectedRestaurant
        {
            get { return _selectedRestaurant; }
            set
            {
                _selectedRestaurant = value;
                RaisePropertyChanged(() => SelectedRestaurant);
                if (value==null)
                {
                    MenuTitle = "Food & Drinks";
                    LocationVisibility = false;
                    ShowRestaurant = true;
                }
                else
                {
                    MenuTitle = "Menu";
                    LocationVisibility = true;
                    ShowRestaurant = false;
                }
            }
        }
        private ObservableCollection<RestaurantsCategory> _categoryList;
        public ObservableCollection<RestaurantsCategory> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                RaisePropertyChanged(() => CategoryList);
            }
        }

        private RestaurantsCategory _selectedCategory;
        public RestaurantsCategory SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }
        private string _searchItem;
        public string SearchItem
        {
            get { return _searchItem; }
            set
            {
                _searchItem = value;
                RaisePropertyChanged(() => SearchItem);
                SearchVisibility = string.IsNullOrEmpty(value) ? false : true;
                if (string.IsNullOrEmpty(value))
                {
                    HideKeyPad?.Invoke();
                }
            }
        }

        private string _menuTitle = "Food & Drinks";
        public string MenuTitle
        {
            get { return _menuTitle; }
            set
            {
                _menuTitle = value;
                RaisePropertyChanged(() => MenuTitle);
            }
        }

        private ObservableCollection<MenuData> _menuList;
        public ObservableCollection<MenuData> MenuList
        {
            get { return _menuList; }
            set
            {
                _menuList = value;
                RaisePropertyChanged(() => MenuList);
                NoDataLabel = (value != null && value.Count != 0) ? "" : AppResources.Lbl_NoData;
            }
        }
        private ObservableCollection<MenuData> _OGMenuList;
        public ObservableCollection<MenuData> OGMenuList
        {
            get { return _OGMenuList; }
            set
            {
                _OGMenuList = value;
                RaisePropertyChanged(() => OGMenuList);
            }
        }

        private string _noDataLabel=AppResources.Lbl_PlzWait;
        public string NoDataLabel
        {
            get { return _noDataLabel; }
            set
            {
                _noDataLabel = value;
                RaisePropertyChanged(() => NoDataLabel);
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
        private ICommand _clearSearchCommand;
        public ICommand ClearSearchCommand
        {
            get
            {
                _clearSearchCommand = _clearSearchCommand ?? new MvxCommand(ProcessClearSearchCommand);
                return _clearSearchCommand;
            }
        }

        private ICommand _selectCategoryCommand;
        public ICommand SelectCategoryCommand
        {
            get
            {
                _selectCategoryCommand = _selectCategoryCommand ?? new MvxCommand<RestaurantsCategory>(ProcessSelectCategoryCommand);
                return _selectCategoryCommand;
            }
        }

        private ICommand _selectRestaurantCommand;
        public ICommand SelectRestaurantCommand
        {
            get
            {
                _selectRestaurantCommand = _selectRestaurantCommand ?? new MvxAsyncCommand<RestaurantData>(ProcessSelectRestaurantCommand);
                return _selectRestaurantCommand;
            }
        }
        private ICommand _selectMenuCommand;
        public ICommand SelectMenuCommand
        {
            get
            {
                _selectMenuCommand = _selectMenuCommand ?? new MvxCommand<MenuData>(ProcessSelectMenuCommand);
                return _selectMenuCommand;
            }
        }

        private ICommand _menuCommand;
        public ICommand MenuCommand
        {
            get
            {
                _menuCommand = _menuCommand ?? new MvxCommand(ProcessMenuCommand);
                return _menuCommand;
            }
        }

        private ICommand _orderCommand;
        public ICommand OrderCommand
        {
            get
            {
                _orderCommand = _orderCommand ?? new MvxCommand(ProcessOrderCommand);
                return _orderCommand;
            }
        }
        private ICommand _tabCommand;
        public ICommand TabCommand
        {
            get
            {
                _tabCommand = _tabCommand ?? new MvxCommand(ProcessTabCommand);
                return _tabCommand;
            }
        }
        private ICommand _accountCommand;
        public ICommand AccountCommand
        {
            get
            {
                _accountCommand = _accountCommand ?? new MvxCommand(ProcessAccountCommand);
                return _accountCommand;
            }
        }
        private ICommand _raisedHandCommand;
        public ICommand RaisedHandCommand
        {
            get
            {
                _raisedHandCommand = _raisedHandCommand ?? new MvxCommand(ProcessRaisedHandCommand);
                return _raisedHandCommand;
            }
        }


        #endregion

        #region  Constructor
        public override void ViewAppeared()
        {
            SelectedRestaurant = null;
            ShowRestaurant = true;
            LocationVisibility = false;
            RestaurantsList = new ObservableCollection<RestaurantData>();
            RestaurantsList = OGRestaurantsList;
            CategoryList = new ObservableCollection<RestaurantsCategory>();
            LoadData();
        }
       /// <summary>
       /// Initialising restaurant
       /// </summary>
        public async void LoadData()
        {
            try
            {
                Mvx.IoCProvider.Resolve<IAppLoader>().ShowIndicator();
                NoDataLabel = AppResources.Lbl_PlzWait;
                var response = await RestaurantService.ProcessToInitRestaurants();
                //var response1 = await RestaurantService.ProcessToGetRestaurants();
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                if (response != null)
                {
                    if (response != null)// && response.Code == AppConstants.SuccessCode)
                    {
                        OGRestaurantsList = new ObservableCollection<RestaurantData>();
                        RestaurantsList = new ObservableCollection<RestaurantData>();
                        CategoryList = new ObservableCollection<RestaurantsCategory>();
                        OGRestaurantsList = response.Data.RestaurantFirstPage.RestaurantsList;
                        RestaurantsList = OGRestaurantsList;
                        CategoryList = response.Data.RestaurantsCategories;
                        CategoryList.Insert(0, new RestaurantsCategory { Id = 0, Title = "All", ImageUrl = CategoryList[1].ImageUrl,SelectedBackgroundColor="AppColor",SelectedTextColor="WhiteColor" });
                    }
                    else
                    {
                       Mvx.IoCProvider.Resolve<IMessageBox>().ShowMessageBox(response.Message, null, false);
                    }
                }
                else
                {
                    var categoryList = new ObservableCollection<RestaurantsCategory>
                    {
                        new RestaurantsCategory { Id = 0, Title = "All", ImageUrl = new Uri("https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg"), SelectedBackgroundColor = "WhiteColor", SelectedTextColor = "BlackColor" },
                        new RestaurantsCategory { Id = 1, Title = "Desert", ImageUrl = new Uri("https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg"), SelectedBackgroundColor = "WhiteColor", SelectedTextColor = "BlackColor" },
                        new RestaurantsCategory { Id = 2, Title = "Pizza", ImageUrl = new Uri("https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg"), SelectedBackgroundColor = "WhiteColor", SelectedTextColor = "BlackColor" },
                        new RestaurantsCategory { Id = 3, Title = "Burger", ImageUrl = new Uri("https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg"), SelectedBackgroundColor = "WhiteColor", SelectedTextColor = "BlackColor" }
                    };
                     
                    CategoryList = categoryList;

                    var restaurants = new ObservableCollection<RestaurantData>
                    {
                        new RestaurantData { Id = 0, Title = "Jacks Burger", ImageUrl = new Uri("https://dadarestaurant.ro/wp-content/uploads/2019/03/cropped-dada-restaurant-bun-bucuresti-6.jpg"), Categories = "pizza, burger, desert",Rating=4},
                        new RestaurantData { Id = 1, Title = "Pizza place", ImageUrl = new Uri("https://dadarestaurant.ro/wp-content/uploads/2019/03/cropped-dada-restaurant-bun-bucuresti-6.jpg"), Categories = "pizza, burger",Rating=1},
                        new RestaurantData { Id = 2, Title = "Pizza second place", ImageUrl = new Uri("https://dadarestaurant.ro/wp-content/uploads/2019/03/cropped-dada-restaurant-bun-bucuresti-6.jpg"), Categories = "pizza, burger, desert",Rating=3},
                        new RestaurantData { Id = 3, Title = "Burger Joint", ImageUrl = new Uri("https://dadarestaurant.ro/wp-content/uploads/2019/03/cropped-dada-restaurant-bun-bucuresti-6.jpg"), Categories = "pizza, desert",Rating=5},
                    };
                    OGRestaurantsList = restaurants;
                    RestaurantsList = restaurants;
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }
        #endregion

        #region ProcessCommand
        private void ProcessBackCommand()
        {
            NavigationService.Close(this);
        }
        private async void ProcessClearSearchCommand()
        {
            try
            {
                SearchItem = null;
                await Initialize();
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }

        private void ProcessSelectCategoryCommand(RestaurantsCategory selectedCategory)
        {
            try
            {
                if (SelectedCategory?.Id== selectedCategory.Id)
                {
                    foreach (var item in CategoryList)
                    {
                        item.SelectedBackgroundColor = "WhiteColor";
                        item.SelectedTextColor = "BlackColor";
                    }
                }
                else
                {
                    SelectedCategory = selectedCategory;
                    foreach (var item in CategoryList)
                    {
                        if (item.Id == selectedCategory.Id)
                        {
                            item.SelectedBackgroundColor = "AppColor";
                            item.SelectedTextColor = "WhiteColor";
                        }
                        else
                        {
                            item.SelectedBackgroundColor = "WhiteColor";
                            item.SelectedTextColor = "BlackColor";
                        }

                    }
                }
                if (LocationVisibility)
                {
                    if (selectedCategory.Title == CategoryList[0].Title)
                    {
                        MenuList = OGMenuList;
                    }
                    else
                    {
                        //var rest = OGMenuList.Where(a => a.Description.ToLower().Contains(SelectedCategory.Title.ToLower()));
                        var rest = from p in OGMenuList
                                   where !string.IsNullOrEmpty(p.Description) && p.Description.ToLower().Contains(SelectedCategory.Title.ToLower())
                                   select p;
                        MenuList = new ObservableCollection<MenuData>(rest);
                    }
                }
                else
                {
                    if (selectedCategory.Title == CategoryList[0].Title)
                    {
                        RestaurantsList = OGRestaurantsList;
                        SelectedRestaurant = null;
                    }
                    else
                    {
                        //var rest = OGRestaurantsList.Where(a =>a.Categories.ToLower().Contains(SelectedCategory.Title.ToLower()));
                        var rest=from p in OGRestaurantsList
                                  where !string.IsNullOrEmpty(p.Categories) && p.Categories.ToLower().Contains(SelectedCategory.Title.ToLower()) select p;
                        RestaurantsList = new ObservableCollection<RestaurantData>(rest);
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }
        private async Task ProcessSelectRestaurantCommand(RestaurantData selectedRestaurant)
        {
            try
            {
                SelectedRestaurant = selectedRestaurant;
                Mvx.IoCProvider.Resolve<IAppLoader>().ShowIndicator();
                var response = await ProductService.ProcessInitRestaurantMenu(SelectedRestaurant);
                Mvx.IoCProvider.Resolve<IAppLoader>().StopIndicator();
                if (response != null)
                {
                    if (response != null)// && response.Code == AppConstants.SuccessCode)
                    {
                        MenuList = response.Data.MenuItemsFirstPage.Data;
                        OGMenuList = MenuList;
                        CategoryList = response.Data.RestaurantsCategories;
                        CategoryList.Insert(0, new RestaurantsCategory { Id = 0, Title = "All", ImageUrl = CategoryList[1].ImageUrl, SelectedBackgroundColor = "AppColor", SelectedTextColor = "WhiteColor" });
                    }
                    else
                    {
                        Mvx.IoCProvider.Resolve<IMessageBox>().ShowMessageBox(response.Message, null, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }

        private void ProcessSelectMenuCommand(MenuData selectedMenu)
        {
            try
            {
                NavigationService.Navigate<ProductDetailsViewModel, object>(new ProductNavigation { SelectedMenu =selectedMenu });
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }

        private void ProcessAccountCommand()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }

        private void ProcessTabCommand()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }
        private void ProcessMenuCommand()
        {
            try
            {
                SelectedRestaurant = null;
                ShowRestaurant = true;
                LocationVisibility = false;
                RestaurantsList = new ObservableCollection<RestaurantData>();
                RestaurantsList = OGRestaurantsList;
                CategoryList = new ObservableCollection<RestaurantsCategory>();
                LoadData();
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }
        private void ProcessOrderCommand()
        {
            try
            {
                NavigationService.Navigate<CartViewModel>();
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }

        private void ProcessRaisedHandCommand()
        {
            try
            {
                ShowNotification?.Invoke();
            }
            catch (Exception ex)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(DashBoardViewModel), ex);
            }
        }

        
        #endregion
    }
}
