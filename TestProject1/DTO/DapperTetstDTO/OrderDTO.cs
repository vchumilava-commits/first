using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.DapperTetstDTO
{
    public record OrderDTO
        (
        long id,

        long userId,

        string orderDate,

        string status,

        double totalPrice
        );
}