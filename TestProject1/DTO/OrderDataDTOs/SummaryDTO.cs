using System.Text.Json.Serialization;

namespace TestProject1.DTO.OrderDataDTOs;

public record SummaryDTO(
    [property: JsonPropertyName("itemsTotal")]
    decimal ItemsTotal,
    [property: JsonPropertyName("deliveryFee")]
    decimal DeliveryFee,
    [property: JsonPropertyName("discount")]
    decimal Discount,
    [property: JsonPropertyName("finalTotal")]
    decimal FinalTotal
);