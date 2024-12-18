using Confluent.Kafka;
using KafkaApi.Kafka;
using KafkaApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KafkaApi.Services
{
    public class OrderService


    {
        private readonly IKafkaProducer _kafkaProducer;

        public OrderService(IKafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task<Order> CreateOrder(string customer)
        {
            Order order = new Order();
            order.Id = Guid.NewGuid();
            order.CreatedDate = DateTime.Now;
            order.State = OrderState.Created;
            order.CustomerName = customer;

            await _kafkaProducer.ProduceAsync("order-topic", new Message<string, string>
            {
                Key = order.Id.ToString(),
                Value = JsonSerializer.Serialize(order)
            });

            return order;
        }
    }
}
