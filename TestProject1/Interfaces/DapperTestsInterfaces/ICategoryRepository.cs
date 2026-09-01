using System;
using System.Collections.Generic;
using System.Text;
using TestProject1.DTO.DapperTetstDTO;

namespace TestProject1.Interfaces.DapperTestsInterfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
    }
}
