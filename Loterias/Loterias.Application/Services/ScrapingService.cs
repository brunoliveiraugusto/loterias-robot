using Loterias.Application.Services.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Loterias.Application.Utils.Settings;
using Loterias.Application.Utils.Request.Interfaces;
using Loterias.Application.Utils.Request.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Loterias.Application.Models;

namespace Loterias.Application.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly string _url;
        private readonly IHttpService _httpService;
        private Dictionary<string, string> _requestParams = new();

        public ScrapingService(IOptions<GameRequest> options, IHttpService httpService)
        {
            _url = options?.Value.Uri;
            _requestParams.Add(options?.Value.KeyParam, options?.Value.ValueParam);
            _httpService = httpService;
        }

        public async Task<IEnumerable<Game>> Read()
        {
            var data = await _httpService.Get<MegaSenaResponse>(_url, _requestParams);
            HtmlDocument doc = new();
            doc.LoadHtml(data.Html);
            return ExtractGames(doc);

        }

        private IEnumerable<Game> ExtractGames(HtmlDocument document)
        {
            var games = document.DocumentNode
                .SelectNodes("//tbody")
                    .ToArray()
                        .Select(tbody =>
                            tbody.SelectNodes("//tr")
                                .ToArray()
                                    .Select(tr =>
                                        tr.SelectNodes("//td").Skip(1).Take(7))).FirstOrDefault();

            List<Game> extractedGames = new();

            Parallel.ForEach(games, game =>
            {
                Game extractedGame = game.ToList();
                extractedGames.Add(extractedGame);
            });

            return extractedGames.OrderBy(extractedGame => extractedGame.DrawDate);
        }
    }
}
