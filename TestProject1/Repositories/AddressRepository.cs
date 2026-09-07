using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using TestProject1.Interfaces.DapperTestsInterfaces;
using Dapper;
using TestProject1.DTO.DapperTetstDTO;

namespace TestProject1.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string connection;
        public AddressRepository(string connection)
        {
            this.connection = connection;
        }

        public async Task<AddressDTO> GetAddressByUserId(int userId)
        {
            using var db = new SqliteConnection(connection);
            var address = await db.QueryFirstOrDefaultAsync<AddressDTO>("SELECT * from Addresses " +
                "WHERE UserId = @userId", new { userId });
            return address;
        }
    }
}
