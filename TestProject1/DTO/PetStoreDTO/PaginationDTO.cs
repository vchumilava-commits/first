using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tests1.DTO.PetStoreDTO
{
    public record PaginationDTO(
        int Page,
        int Limit,
        int TotalItems,
        int TotalPages
    );
}
