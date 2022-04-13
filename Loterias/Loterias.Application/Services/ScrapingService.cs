using Loterias.Application.Services.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Loterias.Application.Utils.Settings;
using Loterias.Application.Utils.Request.Interfaces;
using Loterias.Application.Utils.Request.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Loterias.Application.Utils.Csv.Models;
using System;

namespace Loterias.Application.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly string _url;
        private readonly TablePosition _tablePosition;
        private readonly IHttpService _httpService;
        private Dictionary<string, string> _requestParams = new();

        public ScrapingService(IOptions<GameRequest> optionsGameRequest, IOptions<TablePosition> optionsTablePosition, IHttpService httpService)
        {
            _url = optionsGameRequest?.Value.Uri;
            _tablePosition = optionsTablePosition?.Value;
            _requestParams.Add(optionsGameRequest?.Value.KeyParam, optionsGameRequest?.Value.ValueParam);
            _httpService = httpService;
        }

        public async Task<IEnumerable<Csv>> Read()
        {
            var data = await _httpService.Get<MegaSenaResponse>(_url, _requestParams);
            HtmlDocument doc = new();
            doc.LoadHtml(data.Html);
            return ExtractGames(doc);

        }

        private IEnumerable<Csv> ExtractGames(HtmlDocument document)
        {
            //TODO: Ajustar nome da classe tornando o scraping genérico
            var games = document.DocumentNode
                .SelectNodes("//table[@class='tabela-resultado megasena']/tbody/tr")
                .Select(tr => tr.SelectNodes(".//td").Skip(_tablePosition.Skip).Take(_tablePosition.Take).ToList());

            List<Csv> extractedGames = new();

            Parallel.ForEach(games, game =>
            {
                Csv extractedGame = game.ToList();
                extractedGames.Add(extractedGame);
            });

            return extractedGames.OrderBy(extractedGame => DateTime.Parse(extractedGame.DrawDate));
        }
    }
}
