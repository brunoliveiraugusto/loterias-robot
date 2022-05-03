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
        private readonly TableInfo _tableInfo;
        private readonly IHttpService _httpService;
        private Dictionary<string, string> _requestParams = new();
        private string _class;
        private int _take;
        private readonly bool _isMegasena;

        public ScrapingService(IOptions<GameRequest> optionsGameRequest, IOptions<TableInfo> optionsTablePosition, IHttpService httpService, IOptions<GameInfo> optionsGameInfo)
        {
            _url = optionsGameRequest?.Value.Uri;
            _tableInfo = optionsTablePosition?.Value;
            _httpService = httpService;
            _isMegasena = optionsGameInfo.Value.IsMegaSena;
            SetParamsGetRequest(optionsGameRequest.Value);
            SetTableInfo(_tableInfo);
        }

        public async Task<IEnumerable<Csv>> Read()
        {
            //TODO: Realizar a leitura apenas se a última data sorteada for diferente da última data do csv.
            var data = await _httpService.Get<MegaSenaResponse>(_url, _requestParams);
            HtmlDocument doc = new();
            doc.LoadHtml(data.Html);
            return ExtractGames(doc);

        }

        private IEnumerable<Csv> ExtractGames(HtmlDocument document)
        {
            var games = document.DocumentNode
                .SelectNodes($"//table[@class='tabela-resultado {_class}']/tbody/tr")
                .Select(tr => tr.SelectNodes(".//td").Skip(_tableInfo.Skip).Take(_take).ToList());

            IEnumerable<Csv> extractedGames = SetExtractedGames(games);

            return extractedGames.OrderBy(extractedGame => DateTime.Parse(extractedGame.DrawDate));
        }

        private void SetTableInfo(TableInfo tableInfo)
        {
            int index = 0;

            if (!_isMegasena)
                index = 1;

            _class = tableInfo.Class.Split(",")[index].Trim();
            _take = int.Parse(tableInfo.Take.Split(",")[index].Trim());
        }

        private IEnumerable<Csv> SetExtractedGames(IEnumerable<List<HtmlNode>> games)
        {
            List<Csv> extractedGames = new();

            Parallel.ForEach(games, game =>
            {
                Csv extractedGame = game.ToList();
                extractedGames.Add(extractedGame);
                 
            });

            return extractedGames;
        }

        private void SetParamsGetRequest(GameRequest gameRequest)
        {
            int index = 0;

            if (!_isMegasena)
                index = 1;

            _requestParams.Add(gameRequest.ParameterKey, gameRequest.ParameterValue.Split(",")[index].Trim());
        }
    }
}
