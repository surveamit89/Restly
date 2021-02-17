using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Models.ApiRequestResponse
{
    public class SelectedItemData:MvxViewModel
    {
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
