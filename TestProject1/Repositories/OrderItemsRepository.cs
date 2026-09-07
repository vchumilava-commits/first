using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using TestProject1.DTO.DapperTetstDTO;
using TestProject1.Interfaces.DapperTestsInterfaces;
using Dapper;

namespace TestProject1.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly string connection;
        public OrderItemsRepository(string connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<OrderItemsDTO>> GetOrderItemsByOrderId(int orderId)
        {
            using var db = new SqliteConnection(connection);
            var items = await db.QueryAsync<OrderItemsDTO>("SELECT * from OrderItems " +
                "WHERE OrderId = @orderId", new { orderId });
            return items;
        }
    }
}
