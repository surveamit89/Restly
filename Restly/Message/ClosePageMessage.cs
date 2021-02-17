using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restly.Message
{
    public class ClosePageMessage : MvxMessage
    {
        public ClosePageMessage(object sender) : base(sender)
        {
        }
    }
}
