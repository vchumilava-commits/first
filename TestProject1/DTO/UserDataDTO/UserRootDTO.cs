using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TestProject1.DTO.UserDataDTO;

public record UserRootDTO
(
   [property: JsonPropertyName("data")]
   List<UserDatumDTO> Data
);
