using System;
using System.Collections.Generic;
using System.Text;

namespace Tests1.DTO.BookStoreDTO
{
    public record DeleteBookRequestDTO(
        string Isbn,
        string UserId
        );
}
