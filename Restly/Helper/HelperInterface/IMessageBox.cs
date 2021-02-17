using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Helper.HelperInterface
{
    public interface IMessageBox
    {
        void ShowMessageBox(string MessageText, string MessageTitle, bool ClosePage = true);
        void CloseMessageBox();
    }
}
