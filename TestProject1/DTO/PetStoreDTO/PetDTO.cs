using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tests1.DTO.PetStoreDTO
{
    public record PetDTO(
        string Id,
        string Name,
        string Species,
        string Breed,
        int AgeMonths,
        string Size,
        string Status,
        string Price,
        string Currency,
        bool GoodWithKids,
        string CreatedAt,
        string UpdatedAt,
        MedicalInfoDTO MedicalInfo
    );
}
