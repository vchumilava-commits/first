namespace TestProject1.DTO;

using System.Text.Json.Serialization;

public class CreateUserRequestDTO
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("job")]
    public string Job { get; set; }

}