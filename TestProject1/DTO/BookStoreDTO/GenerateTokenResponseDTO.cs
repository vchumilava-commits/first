using System;
using System.Collections.Generic;
using System.Text;

namespace Tests1.DTO.BookStoreDTO
{
    public record GenerateTokenResponseDTO(
        string Token,
        string Expires,
        string Status,
        string Result
    );
}
