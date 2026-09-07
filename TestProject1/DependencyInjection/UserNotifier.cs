using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.Interfaces;

namespace TestProject1.DependencyInjection
{
    public class UserNotifier
    {
        private readonly IEmailSender sender;
        public UserNotifier(IEmailSender sender)
        {
            this.sender = sender;
        }

        public void Notify(int userId)
        {
            sender.Send("user@mail.com", $"Hello, user {userId}!");
        }
    }
}
