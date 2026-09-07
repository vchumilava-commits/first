using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.DapperTetstDTO;
using TestProject1.Interfaces.DapperTestsInterfaces;
using Dapper;

namespace TestProject1.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string connection;
        public CategoryRepository(string connection)
        {
            this.connection = connection;
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            using var db = new SqliteConnection(connection);
            var categories = await db.QueryAsync<CategoryDTO>("SELECT * from Categories");
            return categories;
        }
    }
}
