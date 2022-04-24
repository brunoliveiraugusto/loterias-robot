namespace Loterias.Application.Services.Interfaces
{
    public interface IMessageProducerService
    {
        void SendMessage<T>(T message) where T : class;
    }
}
