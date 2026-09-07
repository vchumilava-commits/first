using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.DapperTetstDTO
{
    public record ReviewsDTO
        (
        long id,

        string userId,

        string productId,

        long rating,

        long comment,

        long createdAt
        );
}