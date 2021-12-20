using System.Threading.Tasks;

namespace SparkRoseDigital.Infrastructure.MessageBroker
{
    public interface IRegisteredEventPublisher
    {
        Task PublishAll();
    }
}
