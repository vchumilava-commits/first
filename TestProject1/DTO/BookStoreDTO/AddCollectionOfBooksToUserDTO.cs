using System;
using System.Collections.Generic;
using System.Text;

namespace Tests1.DTO.BookStoreDTO
{
    public record AddCollectionOfBooksToUserDTO(
        string UserId,
        List<CollectionOfIsbnsDTO> Books
    );
}
