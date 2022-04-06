using Loterias.Application.Models;
using System.Collections.Generic;

namespace Loterias.Application.Services.Interfaces
{
    public interface IGameService
    {
        void ProcessRecommendedGame(IEnumerable<Game> games);
    }
}
