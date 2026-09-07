using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tests1.DTO.PetStoreDTO
{
    public record MedicalInfoDTO(
        bool Vaccinated,
        bool SpayedNeutered,
        bool Microchipped,
        bool SpecialNeeds,
        string HealthNotes
    );
}
