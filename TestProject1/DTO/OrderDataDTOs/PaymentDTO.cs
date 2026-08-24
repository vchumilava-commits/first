namespace TestProject1.DTO;

using System.Text.Json.Serialization;

public record PaymentDTO(
    [property: JsonPropertyName("method")]
    string Method,
    [property: JsonPropertyName("status")]
    string Status,
    [property: JsonPropertyName("transactionId")]
    string TransactionId
);