using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Demo.Core.Infrastructure
{
    public class KafkaClient
        : IKafkaClient
    {
        public KafkaClient(string server, string groupId)
        {
            //-----------------------------------------------------------------------------------------
            // Producer
            Dictionary<string, object> producerConfig = new Dictionary<string, object>
            {
                { "bootstrap.servers", server }
            };

            _producer = new Producer<string, string>(producerConfig, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8));

            //-----------------------------------------------------------------------------------------
            // Consumer
            Dictionary<string, object> consumerConfig = new Dictionary<string, object>
            {
                { "bootstrap.servers", server },
                { "group.id", groupId }
            };

            _consumer = new Consumer<string, string>(consumerConfig, new StringDeserializer(Encoding.UTF8), new StringDeserializer(Encoding.UTF8));
        }

        private readonly Producer<string, string> _producer;
        private readonly Consumer<string, string> _consumer;

        public async Task ProduceAsync<T>(string topic, string key, T obj)
        {
            string objValue = JsonConvert.SerializeObject(obj);

            Message<string, string> messageResponse = await _producer.ProduceAsync(topic, key, objValue);

            if (messageResponse.Error?.HasError == true)
                throw new Exception($"Error sending message. ErrorCode: {messageResponse.Error.Code}. Reason: {messageResponse.Error.Reason}.");
        }

        public T ConsumeAsync<T>(string topic)
        {
            Message<string, string> messageResponse = null;
            _consumer.OnMessage += (_, message) => messageResponse = message;

            _consumer.Subscribe(topic);
            _consumer.Poll(10 * 1000);

            if (messageResponse == null)
                return default(T);

            if (messageResponse.Error?.HasError == true)
                throw new Exception($"Error sending message. ErrorCode: {messageResponse.Error.Code}. Reason: {messageResponse.Error.Reason}.");

            T response = JsonConvert.DeserializeObject<T>(messageResponse.Value);
            return response;
        }
    }
}
