using CsvHelper;
using CsvHelper.Configuration;
using Loterias.Application.Models;
using Loterias.Application.Services.Interfaces;
using Loterias.Application.Utils.Csv.Models;
using Loterias.Application.Utils.Csv.Models.Maps;
using Loterias.Application.Utils.Exceptions;
using Loterias.Application.Utils.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterias.Application.Services
{
    public class CsvService : ICsvService
    {
        private readonly CsvConfiguration _config = new(CultureInfo.InvariantCulture) { Delimiter = ";", HasHeaderRecord = false };
        //TODO: Ajustar para obter caminho relativo
        private string _pathCsv;
        private CsvMap _csvMap;

        public CsvService(IOptions<GameInfo> tipoJogo, CsvMap csvMap)
        {
            SetPath(tipoJogo.Value);
            _csvMap = csvMap;
        }

        public async Task<IEnumerable<Game>> Read()
        {
            try
            {
                var data = await File.ReadAllLinesAsync(_pathCsv);
                List<Game> games = new();

                foreach (var line in data)
                {
                    Game game = line;
                    games.Add(game);
                }

                return games;
            }
            catch
            {
                throw new ErrorRetrievingDataException();
            }
        }

        public async Task Update(IEnumerable<Csv> games)
        {
            try
            {
                if (File.Exists(_pathCsv))
                {
                    var data = await File.ReadAllLinesAsync(_pathCsv);
                    DateTime? lastDateDrawn = ExtractDateLastDraw(data);
                    IEnumerable<Csv> gamesToBeInserted = lastDateDrawn != null ? games.Where(game => DateTime.Parse(game.DrawDate) > lastDateDrawn) : games;
                    await UpdateExistsFile(gamesToBeInserted);
                    return;
                }
                
                await Insert(games);
            }
            catch
            {
                throw;
            }
        }

        private async Task Insert(IEnumerable<Csv> games)
        {
            try
            {
                using var writer = new StreamWriter(_pathCsv, false, Encoding.UTF8);
                using var csv = new CsvWriter(writer, _config);
                csv.Context.RegisterClassMap(_csvMap);
                await csv.WriteRecordsAsync(games);
            }
            catch
            {
                throw new ErrorEnteringDataException("Error inserting data in csv.");
            }
        }

        private async Task UpdateExistsFile(IEnumerable<Csv> games)
        {
            try
            {
                using var writer = new StreamWriter(_pathCsv, true);
                using var csv = new CsvWriter(writer, _config);
                csv.Context.RegisterClassMap(_csvMap);
                await csv.WriteRecordsAsync(games);
            }
            catch
            {
                throw new ErrorEnteringDataException("Error updating data in csv.");
            }
        }

        private DateTime? ExtractDateLastDraw(string[] games)
        {
            try
            {
                DateTime.TryParse(games[0].Split(";")[0], out DateTime result);
                return result;
            }
            catch
            {
                return null;
            }
        }

        private void SetPath(GameInfo tipoJogo)
        {
            int indice = 0;

            if (!tipoJogo.IsMegaSena)
                indice = 1;

            _pathCsv = $"C:\\Projetos\\Pessoal\\Loterias\\loterias-robot\\Loterias\\Loterias.Application\\Files\\{tipoJogo.GameAcronym.Split(",")[indice].Trim()}.csv";
        }
    }
}
