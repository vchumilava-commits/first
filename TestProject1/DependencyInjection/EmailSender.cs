using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.Interfaces;

namespace TestProject1.DependencyInjection
{
    public class EmailSender : IEmailSender
    {
        public void Send(string to, string text)
        {
            // Логика отправки письма
            Console.WriteLine($"Sending mail to {to}: {text}");
        }
    }
}
