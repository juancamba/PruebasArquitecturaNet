using Confluent.Kafka;

namespace KafkaApi.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        IProducer<string, string> _producer;

        public KafkaProducer()
        {

            var config = new ProducerConfig
            {

                BootstrapServers = "localhost:9092",


            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public Task ProduceAsync(string topic, Message<string, string> message)
        {
            return _producer.ProduceAsync(topic, message);
        }
    }
}
