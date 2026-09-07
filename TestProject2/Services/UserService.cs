using System;
using System.Collections.Generic;
using System.Text;
using TestProject2.Interfaces;
using TestProject2.Models;

namespace TestProject2.Services
{
    public class UserService: IUserService
    {
        private readonly IUserApi api;

        public UserService(IUserApi api)
        {
            this.api = api;
        }

        public User GetUser(int id)
        {
            return api.GetUser(id);
        }

        public bool IsUserValid(int id)
        {
            return true;
        }
    }
}
