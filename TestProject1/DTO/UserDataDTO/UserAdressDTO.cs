using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.UserDataDTO;
    public record UserAdressDTO
    (
        [property: JsonPropertyName("street")]
        string Street,
        [property: JsonPropertyName("city")]
        string City,
        [property: JsonPropertyName("geo")]
        UserGeoDTO UserGeo
    );
