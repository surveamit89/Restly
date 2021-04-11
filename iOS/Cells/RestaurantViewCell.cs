using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Restly.iOS.Helpers;
using Restly.Models.ApiRequestResponse.Restaurant;
using UIKit;

namespace Restly.iOS.Cells
{
    public partial class RestaurantViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("RestaurantViewCell");
        public static readonly UINib Nib;

        static RestaurantViewCell()
        {
            Nib = UINib.FromName("RestaurantViewCell", NSBundle.MainBundle);
        }

        protected RestaurantViewCell(IntPtr handle) : base(handle)
        {
            InitializeBinding();
        }

        private void InitializeBinding()
        {
            this.DelayBind(() =>
            {
                using (var bindingSet = this.CreateBindingSet<RestaurantViewCell, RestaurantData>())
                {
                    bindingSet.Bind(lbl_rest_name).To(vm => vm.Title);
                    bindingSet.Bind(lbl_rest_rating).To(vm => vm.Rating);
                    bindingSet.Bind(lbl_rest_description).To(vm => vm.Categories);
                    bindingSet.Bind(iv_rest_image).For(x=>x.ImagePath).To(vm => vm.ImageUrl);
                }
            });
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            TintHelper.ColorImage(iv_star.Image, iv_star, UIColor.Black);
        }
    }
}
