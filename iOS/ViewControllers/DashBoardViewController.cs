using System;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Restly.iOS.Cells;
using Restly.ViewModels.DashBoard;
using UIKit;

namespace Restly.iOS
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class DashBoardViewController : MvxViewController<DashBoardViewModel>
    {
        public DashBoardViewController() : base("DashBoardViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var categoriesViewSource = new CategoriesCollectionSource(cv_categories);
            cv_categories.Source = categoriesViewSource;

            var restaurantViewSource = new RestaurantCollectionSource(tv_restaurant);
            tv_restaurant.Source = restaurantViewSource;

            using (var bindingSet = this.CreateBindingSet())
            {
                bindingSet.Bind(categoriesViewSource).To(vm => vm.CategoryList);
                bindingSet.Bind(categoriesViewSource).For(x => x.SelectionChangedCommand).To(vm => vm.SelectCategoryCommand);
                bindingSet.Bind(lbl_title).To(vm => vm.MenuTitle);
                bindingSet.Bind(tf_search).To(vm => vm.SearchItem);
                bindingSet.Bind(btn_search).To(vm => vm.ClearSearchCommand);
                bindingSet.Bind(btn_search).For("Title").To(vm => vm.ClearText);
                bindingSet.Bind(tf_search).For(x => x.Placeholder).To(vm => vm.SearchHint);

                bindingSet.Bind(restaurantViewSource).To(vm => vm.RestaurantsList);
                bindingSet.Bind(restaurantViewSource).For(x => x.SelectionChangedCommand).To(vm => vm.SelectRestaurantCommand);
            }

            if (NavigationController.NavigationBar != null)
            {
                NavigationController.NavigationBarHidden = true;
                NavigationController.NavigationBar.Hidden = true;
            }

            tf_search.BackgroundColor = UIColor.White;
        }
    }

    public class CategoriesCollectionSource : MvxCollectionViewSource
    {
        public CategoriesCollectionSource(UICollectionView collectionView) : base(collectionView)
        {
            collectionView.RegisterNibForCell(UINib.FromName(CategoriesViewCell.Key, NSBundle.MainBundle), CategoriesViewCell.Key);
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            var castedCell = (CategoriesViewCell)collectionView.DequeueReusableCell(CategoriesViewCell.Key, indexPath);
            return castedCell;
        }
    }

    public class RestaurantCollectionSource : MvxTableViewSource
    {
        public RestaurantCollectionSource(UITableView collectionView) : base(collectionView)
        {
            collectionView.RegisterNibForCellReuse(UINib.FromName(RestaurantViewCell.Key, NSBundle.MainBundle), RestaurantViewCell.Key);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var castedCell = (RestaurantViewCell)tableView.DequeueReusableCell(RestaurantViewCell.Key, indexPath);
            return castedCell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 180f;
        }
    }
}

