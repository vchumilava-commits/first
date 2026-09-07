using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.DTO.DapperTetstDTO;

namespace TestProject1.Interfaces.DapperTestsInterfaces
{
    public interface IOrderRepository
    {
        Task<OrderDTO> GetOrderByUserId(int userId, int orderId);
    }
}