using System.Text.Json.Serialization;

namespace Test
{
    public class Book
    {
        //[JsonPropertyName("AcqTime")]
        //DateTime AcqTime { get; set; }
        public List<OrderItem> Bids { get; set; }
        public List<OrderItem> Asks { get; set; }
    }

    public class OrderItem
    {
        public Order Order { get; set; }
    }

    public class Order
    {
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string Kind { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
    }
}
