using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.UserDataDTO;

public record UserProfileDTO
(
    [property: JsonPropertyName("fullName")]
    string FullName,
    [property: JsonPropertyName("age")]
    int Age,
    [property: JsonPropertyName("address")]
    UserAdressDTO UserAddress,
    [property: JsonPropertyName("tags")]
    List<string> Tags
);
