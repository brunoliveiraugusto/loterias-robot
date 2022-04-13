using Loterias.Application.Models;
using System.Collections.Generic;

namespace Loterias.Application.Services.Interfaces
{
    public interface IGameService
    {
        RecommendedGame ProcessRecommendedGame(IEnumerable<Game> games);
    }
}
