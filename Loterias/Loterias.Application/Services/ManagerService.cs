using Loterias.Application.Services.Interfaces;

namespace Loterias.Application.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ICsvService _csvService;
        private readonly IGameService _gameService;

        public ManagerService(ICsvService csvService, IGameService gameService)
        {
            _csvService = csvService;
            _gameService = gameService;
        }

        public void Process()
        {
            var games = _csvService.Read();
            _gameService.ProcessRecommendedGame(games);
        }
    }
}
