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
        private readonly IEmailService _emailService;

        public MainService(ICsvService csvService, IGameService gameService, IScrapingService scrapingService, IEmailService emailService)
        {
            _csvService = csvService;
            _gameService = gameService;
            _scrapingService = scrapingService;
            _emailService = emailService;
        }

        public async Task Execute()
        {
            try
            {
                await _csvService.Update(await _scrapingService.Read());
                var games = await _csvService.Read();
                var recommendedGame = _gameService.ProcessRecommendedGame(games);
                await _emailService.ProcessEmailSubmission(recommendedGame);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
