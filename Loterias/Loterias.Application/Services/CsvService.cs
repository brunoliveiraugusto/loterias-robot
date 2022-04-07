using Loterias.Application.Models;
using Loterias.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Loterias.Application.Services
{
    public class CsvService : ICsvService
    {
        private const string PATH_CSV = "C:\\Projetos\\Pessoal\\Loterias\\loterias-robot\\Loterias\\Loterias.Application\\FilesCsv\\MS.csv";

        public async Task<IEnumerable<Game>> Read()
        {
            var data = await File.ReadAllLinesAsync(PATH_CSV);
            List<Game> games = new();

            foreach (var line in data)
            {
                Game game = line;
                games.Add(game);
            }

            return games;
        }

        public async Task Update(IEnumerable<Game> games)
        {
            if(File.Exists(PATH_CSV))
            {
                var data = await File.ReadAllLinesAsync(PATH_CSV);
                DateTime.TryParse(data[0].Split(";")[0], out DateTime result);
                DateTime lastDateDrawn = result;

                //TODO: pego a última data sorteada e pego na lista de games os games a partir daquela data para mais recente
            }
        }
    }
}
