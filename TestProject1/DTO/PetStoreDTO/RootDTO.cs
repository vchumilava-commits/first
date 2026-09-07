using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tests1.DTO.PetStoreDTO
{
    public record RootDTO(
        List<PetDTO> Data,
        PaginationDTO Pagination
    );

}