using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.DapperTetstDTO
{
    public record CategoryDTO
        (
        long id,

        string name
        );
}