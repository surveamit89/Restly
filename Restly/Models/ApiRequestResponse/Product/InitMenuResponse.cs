using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Restly.Models.ApiRequestResponse.Restaurant;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restly.Models.ApiRequestResponse.Product
{
    public class InitMenuResponse : BaseResponse
    {
        [JsonProperty("data")]
        public MenuData Data { get; set; }
    }
    public partial class MenuData
    {
        [JsonProperty("menuItemsFirstPage")]
        public MenuItemsFirstPage MenuItemsFirstPage { get; set; }

        [JsonProperty("menuCategories")]
        public ObservableCollection<RestaurantsCategory> RestaurantsCategories { get; set; }

    }

    public partial class MenuItemsFirstPage
    {
        [JsonProperty("data")]
        public ObservableCollection<MenuData> Data { get; set; }

        [JsonProperty("totalPages")]
        public long TotalPages { get; set; }

        [JsonProperty("currentPage")]
        public long CurrentPage { get; set; }

        [JsonProperty("activeIndex")]
        public long ActiveIndex { get; set; }

        [JsonProperty("totalElementsCount")]
        public long TotalElementsCount { get; set; }
    }

    public partial class MenuData : MvxViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rating")]
        public long Rating { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }


        //public ObservableCollection<string>  { get; set; }

        private ObservableCollection<string> _allergens;
        [JsonProperty("allergens")]
        public ObservableCollection<string> Allergens
        {
            get { return _allergens; }
            set
            {
                _allergens = value;
                RaisePropertyChanged(() => Allergens);
                CreateTagList(value);
            }
        }

        private void CreateTagList(ObservableCollection<string> allergens)
        {
            try
            {
                TagList = new ObservableCollection<TagData>();
                if (allergens != null && allergens.Count > 0)
                {
                    foreach (var item in Allergens)
                    {
                        TagList.Add(new TagData { TagName = item });
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private ObservableCollection<TagData> _tagList;
        public ObservableCollection<TagData> TagList
        {
            get { return _tagList; }
            set
            {
                _tagList = value;
                RaisePropertyChanged(() => TagList);
            }
        }
    }
    public partial class TagData
    {
        public string TagName { get; set; }
    }
}
