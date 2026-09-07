using System;
using System.Collections.Generic;
using System.Text;
using TestProject2.Models;

namespace TestProject2.Interfaces
{
    public interface IUserService
    {
        User GetUser(int id);
        bool IsUserValid(int id);
    }
}
