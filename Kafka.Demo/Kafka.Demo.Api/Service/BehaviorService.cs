using Kafka.Demo.Core.Domain;
using Kafka.Demo.Core.Infrastructure;
using Nancy;
using Nancy.ModelBinding;

namespace Kafka.Demo.Api.Service
{
    public sealed class BehaviorService
        : NancyModule
    {
        public BehaviorService(IKafkaClient kafkaClient)
            : base("/behaviors")
        {
            Post("/", async parameters =>
            {
                Behavior behavior = this.Bind<Behavior>();
                await kafkaClient.ProduceAsync("behaviors", behavior.Id.ToString(), behavior);
            });
        }
    }
}
