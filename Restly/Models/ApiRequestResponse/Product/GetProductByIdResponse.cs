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
        public long CartProductCount { get; set; }

        [JsonIgnore]
        public IMvxCommand RemoveItemSelectedCommand { get; set; }
        [JsonIgnore]
        private IMvxCommand _removeItemCommand { get; set; }
        [JsonIgnore]
        public IMvxCommand RemoveItemCommand
        {
            get
            {
                return _removeItemCommand ?? (_removeItemCommand = new MvxCommand(ProcessRemoveItemCommand));
            }
        }


        private void ProcessRemoveItemCommand()
        {
            RemoveItemSelectedCommand?.Execute(this);
        }
    }
    public partial class FrequentlyBoughtProduct:MvxViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }


        public long CartProductCount { get; set; }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
                if (value)
                {
                    SelectedBackgroundColor = "AppColor";
                    SelectedTextColor = "WhiteColor";
                }
                else
                {
                    SelectedBackgroundColor = "WhiteColor";
                    SelectedTextColor = "BlackColor";
                }
            }
        }

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

    public partial class OptionData:MvxViewModel
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

        [JsonProperty("price")]
        public long Price { get; set; }


        public bool IsSingleChoice { get; set; }
        public bool IsMultiChoice { get { return !IsSingleChoice; } }
        public bool ShowOptionalLabel { get { return !IsRequired; } }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        [JsonIgnore]
        public IMvxCommand ItemSelectedCommand { get; set; }
        [JsonIgnore]
        private IMvxCommand _itemSelected { get; set; }
        [JsonIgnore]
        public IMvxCommand ItemSelected
        {
            get
            {
                return _itemSelected ?? (_itemSelected = new MvxCommand(ProcessItemSelected));
            }
        }


        private void ProcessItemSelected()
        {
            ItemSelectedCommand?.Execute(this);
        }

        public string DisplayPrice
        {
            get 
            {
                if (Price>0)
                {
                    return "+ $ "+Price;
                }
                else
                {
                    return "";
                }
                
            }
        }
    }
}
