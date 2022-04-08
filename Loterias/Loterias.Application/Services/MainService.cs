using Loterias.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Loterias.Application.Services
{
    public class MainService : IMainService
    {
        private readonly ICsvService _csvService;
        private readonly IGameService _gameService;
        private readonly IScrapingService _scrapingService;

        public MainService(ICsvService csvService, IGameService gameService, IScrapingService scrapingService)
        {
            _csvService = csvService;
            _gameService = gameService;
            _scrapingService = scrapingService;
        }

        public async Task Execute()
        {
            try
            {
                await _csvService.Update(await _scrapingService.Read());
                var games = await _csvService.Read();
                _gameService.ProcessRecommendedGame(games);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
