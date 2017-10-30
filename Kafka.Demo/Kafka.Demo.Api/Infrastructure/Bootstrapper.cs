using Kafka.Demo.Core.Infrastructure;
using Nancy;
using Nancy.TinyIoc;

namespace Kafka.Demo.Api.Infrastructure
{

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var kafkaClient = new KafkaClient("localhost:9092", "Kafka.Demo.Api");
            container.Register<IKafkaClient, KafkaClient>(kafkaClient);
        }
    }
}
