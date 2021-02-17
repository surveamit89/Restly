using MvvmCross;
using Restly.Helper.HelperInterface;
using Restly.Models.ApiRequestResponse;
using Restly.Models.ApiRequestResponse.Product;
using Restly.Models.ApiRequestResponse.Restaurant;
using Restly.Utility;
using RestSharp.Portable;
using System;
using System.Threading.Tasks;

namespace Restly.Services.Restaurant
{
    public static class RestaurantService
    {
        public static async Task<InitRestaurantsResponse> ProcessToInitRestaurants()
        {
            try
            {
                var request = new RestRequest(AppConstants.RestApi.InitRestaurants);

                var response = await BaseWebService.ExecutePost<InitRestaurantsResponse>(request);

                return response;
            }
            catch (Exception e)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(RestaurantService), e);
                return null;
            }
        }

        public static async Task<BaseResponse> ProcessToGetRestaurants()
        {
            try
            {
                var request = new RestRequest(AppConstants.RestApi.GetRestaurants);

                var response = await BaseWebService.ExecutePost<BaseResponse>(request);

                return response;
            }
            catch (Exception e)
            {
                Mvx.IoCProvider.Resolve<IAppLogger>().DebugLog(nameof(RestaurantService), e);
                return null;
            }
        }
        
    }
}
