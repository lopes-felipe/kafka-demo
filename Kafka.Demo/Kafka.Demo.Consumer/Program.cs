using Kafka.Demo.Core.Domain;
using Kafka.Demo.Core.Infrastructure;
using Newtonsoft.Json;
using System;

namespace Kafka.Demo.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            KafkaClient kafkaClient = new KafkaClient("localhost:9092", "Kafka.Demo.Consumer");

            var cancelled = false;

            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cancelled = true;
            };

            Console.WriteLine("Ctrl-C to exit.");

            while (!cancelled)
            {
                Behavior behavior = kafkaClient.ConsumeAsync<Behavior>("behaviors");

                string behaviorStr = JsonConvert.SerializeObject(behavior);
                Console.WriteLine(behaviorStr);
            }
        }
    }
}
