namespace SparkRoseDigital.Infrastructure.MessageBroker
{
    public interface IEventRegister
    {
        void RegisterEvent<T>(object newEvent)
            where T : class;
    }
}
