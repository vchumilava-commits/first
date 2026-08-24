namespace TestProject1.DTO;

using System.Text.Json.Serialization;

public record OrderDTO(
    [property: JsonPropertyName("orderId")]
    string OrderId,
    [property: JsonPropertyName("createdAt")]
    string CreatedAt,
    [property: JsonPropertyName("customer")]
    CustomerDTO Customer,
    [property: JsonPropertyName("items")]
    List<ItemDTO> Items,
    [property: JsonPropertyName("payment")]
    PaymentDTO Payment,
    [property: JsonPropertyName("delivery")]
    DeliveryDTO Delivery,
    [property: JsonPropertyName("summary")]
    SummaryDTO Summary
);