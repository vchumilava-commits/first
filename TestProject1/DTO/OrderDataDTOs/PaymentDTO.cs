using System.Text.Json.Serialization;

namespace TestProject1.DTO.OrderDataDTOs;

public record PaymentDTO(
    [property: JsonPropertyName("method")]
    string Method,
    [property: JsonPropertyName("status")]
    string Status,
    [property: JsonPropertyName("transactionId")]
    string TransactionId
);