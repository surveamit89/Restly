using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restly.Models.ApiRequestResponse.Restaurant
{
    public class InitRestaurantsResponse : BaseResponse
    {
        [JsonProperty("data")]
        public RestaurantsData Data { get; set; }
    }
    public partial class RestaurantsData
    {
        [JsonProperty("restaurantFirstPage")]
        public RestaurantFirstPage RestaurantFirstPage { get; set; }

        [JsonProperty("restaurantsCategories")]
        public ObservableCollection<RestaurantsCategory> RestaurantsCategories { get; set; }
    }

    public partial class RestaurantFirstPage
    {
        [JsonProperty("data")]
        public ObservableCollection<RestaurantData> RestaurantsList { get; set; }

        [JsonProperty("totalPages")]
        public long TotalPages { get; set; }

        [JsonProperty("currentPage")]
        public long CurrentPage { get; set; }

        [JsonProperty("activeIndex")]
        public long ActiveIndex { get; set; }

        [JsonProperty("totalElementsCount")]
        public long TotalElementsCount { get; set; }
    }

    public partial class RestaurantData
    {
        [JsonProperty("categories")]
        public string Categories { get; set; }

        [JsonProperty("rating")]
        public long Rating { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }
    }

    public partial class RestaurantsCategory: MvxViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        private string _selectedBackgroundColor = "WhiteColor";
        public string SelectedBackgroundColor
        {
            get { return _selectedBackgroundColor; }
            set
            {
                _selectedBackgroundColor = value;
                RaisePropertyChanged(() => SelectedBackgroundColor);
            }
        }
        private string _selectedTextColor = "BlackColor";
        public string SelectedTextColor
        {
            get { return _selectedTextColor; }
            set
            {
                _selectedTextColor = value;
                RaisePropertyChanged(() => SelectedTextColor);
            }
        }
    }
}
