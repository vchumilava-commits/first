using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.DapperTetstDTO
{
    public record UserDTO
        (
        long id,

        string firstName,

        string lastName,

        string email,

        string phone,

        string createdAt
        );
}