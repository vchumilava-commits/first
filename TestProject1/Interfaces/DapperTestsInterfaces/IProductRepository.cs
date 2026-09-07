using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.DTO.DapperTetstDTO;

namespace TestProject1.Interfaces.DapperTestsInterfaces
{
    public interface IProductRepository
    {
        Task<ProductDTO> GetProductById(int id);
    }
}