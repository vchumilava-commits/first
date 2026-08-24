using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.UserDataDTO;

public record UserDatumDTO
(
    [property: JsonPropertyName("id")]
        int Id,
    [property: JsonPropertyName("username")]
        string UserName,
    [property: JsonPropertyName("profile")]
        UserProfileDTO UserProfile,
    [property: JsonPropertyName("roles")]
        List<string> Roles
);
