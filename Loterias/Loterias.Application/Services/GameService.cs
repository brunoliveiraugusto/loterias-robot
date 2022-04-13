using Loterias.Application.Models;
using Loterias.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterias.Application.Services
{
    public class GameService : IGameService
    {
        public RecommendedGame ProcessRecommendedGame(IEnumerable<Game> games)
        {
            var listGames = games.ToList();
            var predictions = AddSubsequentNumbersToEachNumberDrawn(listGames);
            var updatedPredictions = GroupLaterNumbersForEachGameNumber(predictions);
            var lastGame = GetLastGame(listGames);
            var predictionsNumbers = GetLaterNumbersPredictionBasedOnLastGameNumbers(updatedPredictions, lastGame);
            return GetRecommendedGame(predictionsNumbers);

        }

        private Dictionary<string, List<string>> AddSubsequentNumbersToEachNumberDrawn(List<Game> games)
        {
            Dictionary<string, List<string>> predictions = new Dictionary<string, List<string>>();

            for (int i = 0; i < games.Count - 1; i++)
            {
                var gameNumbers = games[i].GameDrawn.Split("-");
                var laterNumbersGame = games[i + 1].GameDrawn.Split("-");

                foreach (var number in gameNumbers)
                {
                    foreach (var laterNumber in laterNumbersGame)
                    {
                        if (predictions.Any(x => x.Key == number))
                        {
                            predictions.TryGetValue(number, out List<string> values);
                            values.Add(laterNumber);
                        }
                        else
                        {
                            predictions.Add(number, new List<string> { laterNumber });
                        }
                    }
                }
            }

            return predictions;
        }

        private List<PossibleGame> GroupLaterNumbersForEachGameNumber(Dictionary<string, List<string>> predictions)
        {
            return predictions.OrderBy(x => x.Key)
                                .Select(x => new PossibleGame
                                {
                                    Number = x.Key,
                                    LaterNumbers = x.Value.GroupBy(x => x)
                                        .Select(x => x.ToList())
                                        .ToList()
                                })
                                .ToList();
        }

        private List<PossibleGame> GetLaterNumbersPredictionBasedOnLastGameNumbers(List<PossibleGame> games, string[] lastGame)
        {
            var teste = games.Where(x => lastGame.Contains(x.Number))
                        .Select(x => new PossibleGame
                        {
                            LaterNumbers = x.LaterNumbers,
                            PossibleNumbers = x.LaterNumbers.OrderByDescending(y => y.Count()).FirstOrDefault(),
                            Number = x.Number
                        })
                        .ToList();

            return teste;
        }

        private string[] GetLastGame(List<Game> games)
        {
            return games.FirstOrDefault().GameDrawn.Split("-");
        }

        private DateTime GetNextDrawDate(DateTime dateLastDraw)
        {
            return dateLastDraw.DayOfWeek == DayOfWeek.Wednesday ?
                                    dateLastDraw.AddDays(3) : dateLastDraw.AddDays(4);
        }

        private RecommendedGame GetRecommendedGame(List<PossibleGame> possibleGames)
        {
            //TODO: Refatoração do método separando responsabilidades.
            var recommendedGameNumbers = new List<string>();

            foreach (var possibleGame in possibleGames)
            {
                var possibleNumber = possibleGame.PossibleNumbers.FirstOrDefault();

                Console.WriteLine($"Número anterior: {possibleGame.Number} - " +
                    $"Número que mais saiu posteriormente: {possibleNumber} - " +
                    $"Quantidade de vezes: {possibleGame.PossibleNumbers.Count()}");

                if(!recommendedGameNumbers.Any(rgn => rgn == possibleNumber))
                {
                    recommendedGameNumbers.Add(possibleNumber);
                }
                else
                {
                    recommendedGameNumbers.Add(possibleGame.LaterNumbers
                            .OrderByDescending(x => x.Count())
                                .Where(lns => !recommendedGameNumbers
                                    .Any(rgn => rgn
                                        .Contains(lns.FirstOrDefault())))
                                            .FirstOrDefault()
                                                .FirstOrDefault());
                }
            }

            return RecommendedGame.CreateRecommendedGame(recommendedGameNumbers, DateTime.Now, possibleGames);
        }
    }
}
