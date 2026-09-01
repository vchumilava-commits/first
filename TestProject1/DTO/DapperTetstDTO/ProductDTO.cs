using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.DapperTetstDTO
{
    public record ProductDTO
        (
        long id,

        string name,

        string description,

        double price,

        long stock,

        long categoryId
        );
}