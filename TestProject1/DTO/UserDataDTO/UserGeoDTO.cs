using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.UserDataDTO;

public record UserGeoDTO
(
    [property: JsonPropertyName("lat")]
    decimal Lat,
    [property: JsonPropertyName("lng")]
    decimal Lng
);
