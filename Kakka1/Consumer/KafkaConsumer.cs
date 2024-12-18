using Confluent.Kafka;
using KafkaApi.Models;
using System.Text.Json;

namespace Consumer
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly ILogger<KafkaConsumer> _logger;

        public KafkaConsumer(ILogger<KafkaConsumer> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {

                _ = ConsumeAsync("order-topic", stoppingToken);
            }, stoppingToken);
        }


        public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
        {

            var config = new ConsumerConfig
            {
                GroupId = "order-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest

            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var consumerResult = consumer.Consume(stoppingToken);
                var order = JsonSerializer.Deserialize<Order>(consumerResult.Message.Value);

                if (order != null)
                {
                    _logger.LogInformation("new order {id}, {customer}", order.Id, order.CustomerName);
                }
                await Task.Delay(1000, stoppingToken);
            }
            consumer.Close();


        }
    }
}
