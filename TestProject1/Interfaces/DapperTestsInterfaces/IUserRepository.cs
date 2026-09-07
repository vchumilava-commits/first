using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.DTO.DapperTetstDTO;

namespace TestProject1.Interfaces.DapperTestsInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> GetUserByNameAndSurname(string firstName, string lastName);
    }
}