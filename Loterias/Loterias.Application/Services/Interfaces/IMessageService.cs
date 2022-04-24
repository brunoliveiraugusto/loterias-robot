using Loterias.Application.Models;

namespace Loterias.Application.Services.Interfaces
{
    public interface IMessageService<T> where T : class
    {
        public T GetMessage(RecommendedGame recommendedGame);
    }
}
