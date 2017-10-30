using System.Threading.Tasks;

namespace Kafka.Demo.Core.Infrastructure
{
    public interface IKafkaClient
    {
        Task ProduceAsync<T>(string topic, string key, T obj);

        T ConsumeAsync<T>(string topic);
    }
}
