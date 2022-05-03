using Loterias.Application.Models;
using Loterias.Application.Services.Interfaces;
using Loterias.Application.Utils.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterias.Application.Services
{
    public class GameService : IGameService
    {
        private bool _isMegasena;

        public GameService(IOptions<GameInfo> options)
        {
            _isMegasena = options.Value.IsMegaSena;
        }

        public RecommendedGame ProcessRecommendedGame(IEnumerable<Game> games)
        {
            var listGames = games.ToList();
            var predictions = AddSubsequentNumbersToEachNumberDrawn(listGames);
            var updatedPredictions = GroupLaterNumbersForEachGameNumber(predictions);
            var lastGame = GetLastGame(listGames);
            var predictionsNumbers = GetLaterNumbersPredictionBasedOnLastGameNumbers(updatedPredictions, lastGame.Numbers);
            //var lastDraw = GetInfoLastDraw(listGames);
            return GetRecommendedGame(predictionsNumbers, lastGame.DrawDate);

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

        private LastGame GetLastGame(List<Game> games)
        {
            LastGame lastGame = games.LastOrDefault();
            return lastGame;
        }

        private DateTime GetNextDrawDate(DateTime? dateLastDraw)
        {
            DateTime lastDraw = dateLastDraw ?? DateTime.Now;

            if(_isMegasena)
            {
                return lastDraw.DayOfWeek switch
                {
                    DayOfWeek.Monday => lastDraw.AddDays(2),
                    DayOfWeek.Tuesday => lastDraw.AddDays(1),
                    DayOfWeek.Wednesday => lastDraw,
                    DayOfWeek.Thursday => lastDraw.AddDays(2),
                    DayOfWeek.Friday => lastDraw.AddDays(1),
                    DayOfWeek.Saturday => lastDraw,
                    DayOfWeek.Sunday => lastDraw.AddDays(3),
                    _ => lastDraw,
                };
            }

            return lastDraw.DayOfWeek switch
            {
                DayOfWeek.Sunday => lastDraw.AddDays(1),
                _ => lastDraw,
            };
        }

        private RecommendedGame GetRecommendedGame(List<PossibleGame> possibleGames, DateTime dateLastDraw)
        {
            List<string> recommendedGameNumbers = new();

            foreach (var possibleGame in possibleGames)
            {
                var possibleNumber = possibleGame.PossibleNumbers.FirstOrDefault();

                if(!recommendedGameNumbers.Any(rgn => rgn == possibleNumber))
                {
                    recommendedGameNumbers.Add(possibleNumber);
                    continue;
                }
                
                recommendedGameNumbers.Add(possibleGame.LaterNumbers
                        .OrderByDescending(x => x.Count())
                            .Where(lns => !recommendedGameNumbers
                                .Any(rgn => rgn
                                    .Contains(lns.FirstOrDefault())))
                                        .FirstOrDefault()
                                            .FirstOrDefault());
            }

            var orderedNumbers = recommendedGameNumbers.Select(rgn => int.Parse(rgn)).OrderBy(number => number).AsEnumerable();
            var possibleOrderedGames = possibleGames.OrderBy(pg => int.Parse(pg.Number)).AsEnumerable();

            return RecommendedGame.CreateRecommendedGame(orderedNumbers, GetNextDrawDate(dateLastDraw), possibleOrderedGames, _isMegasena);
        }

        //private LastDraw GetInfoLastDraw(IEnumerable<Game> games)
        //{
        //    var lastGameDrawn = games.ElementAt(games.Count() - 2);
        //}
    }
}
