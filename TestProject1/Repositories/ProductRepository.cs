using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.DapperTetstDTO;
using TestProject1.Interfaces.DapperTestsInterfaces;
using Dapper;

namespace TestProject1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connection;
        public ProductRepository(string connection)
        {
            this.connection = connection;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            using var db = new SqliteConnection(connection);
            var productById = await db.QueryFirstOrDefaultAsync<ProductDTO>("SELECT * from Products " +
                "WHERE Id = @id", new { id });
            return productById;
        }
    }
}
