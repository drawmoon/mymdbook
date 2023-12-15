namespace JsonPatch.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public OrderDetail[] OrderDetails { get; set; }
    }
}
