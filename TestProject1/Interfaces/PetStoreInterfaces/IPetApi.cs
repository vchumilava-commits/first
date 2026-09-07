using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Refit;
using Tests1.DTO.PetStoreDTO;
using Tests1.Interfaces.PetStoreInterfaces;

namespace Tests1.Interfaces.PetStoreInterfaces
{
    public interface IPetApi
    {
        [Get("/pets")]
        Task<RootDTO> GetAllPetsAsync();

        [Get("/pets")]
        Task<RootDTO> GetAllPetsByMinAgeAndLimit100Async([Query] int minAge, [Query] int limit);

        [Get("/pets/{id}")]
        Task<PetDTO> GetPetByIdAsync(string id);
    }
}
