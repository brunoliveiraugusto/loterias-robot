using Loterias.Application.Models;
using Loterias.Application.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Loterias.Application.Services
{
    public class CsvService : ICsvService
    {
        public IEnumerable<Game> Read()
        {
            var data = File.ReadLines($@"C:\Projetos\Pessoal\Loterias\loterias-robot\Loterias\Loterias.Application\FilesCsv\megasena.csv");
            var games = new List<Game>();

            foreach (var line in data)
            {
                Game game = line;
                games.Add(game);
            }

            return games;
        }
    }
}
