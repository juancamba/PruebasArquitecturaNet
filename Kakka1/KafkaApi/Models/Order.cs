namespace KafkaApi.Models
{

    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public OrderState State { get; set; } = OrderState.Created;
        public string CustomerName { get; set; }

    }
}
