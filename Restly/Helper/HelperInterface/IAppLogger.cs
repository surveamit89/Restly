using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Helper.HelperInterface
{
    public interface IAppLogger
    {
        void DebugLog(string tag, string message);
        void DebugLog(string tag, Exception ex);

    }
}
