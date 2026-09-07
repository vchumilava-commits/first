using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1.Interfaces
{
    public interface IEmailSender
    {
        void Send(string to, string text);
    }
}
