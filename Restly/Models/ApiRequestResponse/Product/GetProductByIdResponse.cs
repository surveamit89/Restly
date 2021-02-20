using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Restly.Models.ApiRequestResponse.Product
{
    public class GetProductByIdResponse : BaseResponse
    {
        [JsonProperty("data")]
        public ProductData Data { get; set; }
    }
    public partial class ProductData:MvxViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }


        [JsonProperty("options")]
        public ObservableCollection<ProductOption> Options { get; set; }

        [JsonProperty("frequentlyBoughtProducts")]
        public ObservableCollection<FrequentlyBoughtProduct> FrequentlyBoughtProducts { get; set; }

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
        public int CartProductCount { get; set; }
        //public Command RemoveItem { get; set; }

        //private IMvxCommand _removeItemCommand { get; set; }

        //public ICommand RemoveItemCommand
        //{
        //    get
        //    {
        //        return _removeItemCommand ?? (_removeItemCommand = new MvxCommand(ProcessRemoveItemCommand));
        //    }
        //}


        //private void ProcessRemoveItemCommand()
        //{
        //    RemoveItem?.Execute(this);
        //}

    }
    public partial class FrequentlyBoughtProduct
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }
    }
    public partial class ProductOption
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("options")]
        public ObservableCollection<OptionData> Options { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("min")]
        public long Min { get; set; }

        [JsonProperty("max")]
        public long Max { get; set; }

        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }
        public bool IsSingleChoice { get { return Type == 0; } }
        public bool IsMultiChoice { get { return !IsSingleChoice; } }
        public bool ShowOptionalLabel { get { return !IsRequired; } }
    }

    public partial class OptionData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("options")]
        public ObservableCollection<FrequentlyBoughtProduct> Options { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("min")]
        public long Min { get; set; }

        [JsonProperty("max")]
        public long Max { get; set; }

        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }
        public bool IsSingleChoice { get; set; }
        public bool IsMultiChoice { get { return !IsSingleChoice; } }
        public bool ShowOptionalLabel { get { return !IsRequired; } }
        public bool IsSelected { get; set; }
    }
}
