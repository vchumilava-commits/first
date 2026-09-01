using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.DapperTetstDTO
{
    public record OrderItemsDTO
        (
        long id,

        long orderId,

        long productId,

        long quantity,

        double unitPrice
        );
}