using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using TestProject2.Models;

namespace TestProject2.Interfaces
{
    public interface IUserApi
    {
        [Get("users/{id}")]
        User GetUser(int id);
    }
}
