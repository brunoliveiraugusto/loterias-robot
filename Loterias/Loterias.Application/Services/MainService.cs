using Loterias.Application.Models;
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
        private readonly IMessageService<Email> _messageService;
        private readonly IMessageProducerService _messageProducerService;

        public MainService(ICsvService csvService, IGameService gameService, IScrapingService scrapingService, IMessageService<Email> messageService, IMessageProducerService messageProducer)
        {
            _csvService = csvService;
            _gameService = gameService;
            _scrapingService = scrapingService;
            _messageService = messageService;
            _messageProducerService = messageProducer;
        }

        public async Task Execute()
        {
            try
            {
                await _csvService.Update(await _scrapingService.Read());
                var games = await _csvService.Read();                
                var recommendedGame = _gameService.ProcessRecommendedGame(games);
                Email email = _messageService.GetMessage(recommendedGame);
                _messageProducerService.SendMessage(email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
