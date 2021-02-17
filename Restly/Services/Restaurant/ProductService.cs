using MvvmCross;
using Restly.Helper.HelperInterface;
using Restly.Models.ApiRequestResponse.Product;
using Restly.Models.ApiRequestResponse.Restaurant;
using Restly.Utility;
using RestSharp.Portable;
using System;
using System.Threading.Tasks;

namespace Restly.Services.Restaurant
{
    public static class ProductService
    {
        public static async Task<InitMenuResponse> ProcessInitRestaurantMenu(RestaurantData selectedRestaurant)
        {
            try
            {
                var request = new RestRequest(AppConstants.RestApi.InitRestaurantMenu);
                request.AddParameter("restaurantId", selectedRestaurant.Id);
                var response = await BaseWebService.ExecuteGet<InitMenuResponse>(request);

                return response;
            }
            catch (Exception e)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductService), e);
                return null;
            }
        }
        public static async Task<GetProductByIdResponse> ProcessToGetProductById(MenuData selectedProduct)
        {
            try
            {
                var request = new RestRequest(AppConstants.RestApi.GetProductById);
                request.AddParameter("productId", selectedProduct.Id);
                var response = await BaseWebService.ExecuteGet<GetProductByIdResponse>(request);

                return response;
            }
            catch (Exception e)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(ProductService), e);
                return null;
            }
        }
    }
}
