namespace TestProject1.DTO;

using System.Text.Json.Serialization;


public class CreateUserResponseDTO
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("job")]
    public string Job { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; } // nullable так как иначе возникала ошибка при ассерте в тест 3
}