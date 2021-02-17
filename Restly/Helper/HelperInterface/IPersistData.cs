using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Helper.HelperInterface
{
    public interface IPersistData
    {
        string GetCartData();
        void SetCartData(string cartData);
    }
}
