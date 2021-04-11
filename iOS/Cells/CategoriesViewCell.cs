using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Restly.iOS.Helpers;
using Restly.Models.ApiRequestResponse.Restaurant;
using UIKit;

namespace Restly.iOS.Cells
{
    public partial class CategoriesViewCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("CategoriesViewCell");
        public static readonly UINib Nib;

        static CategoriesViewCell()
        {
            Nib = UINib.FromName("CategoriesViewCell", NSBundle.MainBundle);
        }

        protected CategoriesViewCell(IntPtr handle) : base(handle)
        {
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                using (var bindingSet = this.CreateBindingSet<CategoriesViewCell, RestaurantsCategory>())
                {
                    bindingSet.Bind(lbl_category).To(vm => vm.Title);
                    bindingSet.Bind(iv_category).For(x => x.ImagePath).To(vm => vm.ImageUrl);
                    bindingSet.Bind(v_container).For(x => x.BackgroundColor).To(vm => vm.SelectedBackgroundColor).WithConversion("StringToColorValue");
                    bindingSet.Bind(lbl_category).For(x => x.TextColor).To(vm => vm.SelectedTextColor).WithConversion("StringToColorValue");
                }
            });
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            iv_category.ClipsToBounds = true;
            v_container.Layer.CornerRadius = 8;
            v_container.ClipsToBounds = true;

            Layer.ShadowColor = UIColor.Black.CGColor;
            Layer.ShadowRadius = 10f;
            Layer.ShadowOpacity = 0.4f;
            Layer.ShadowOffset = new CGSize(0, 0);
            Layer.MasksToBounds = false;

            ContentView.BackgroundColor = UIColor.Clear;
        }

        public override UIEdgeInsets LayoutMargins
        {
            get
            {
                return new UIEdgeInsets(8, 8, 8, 8);
            }
            set { }
        }
    }
}
