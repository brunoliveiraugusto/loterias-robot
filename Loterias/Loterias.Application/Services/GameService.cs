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

        public GameService(IOptions<GameSettings> options)
        {
            _isMegasena = options.Value.IsMegaSena;
        }

        public RecommendedGame ProcessRecommendedGame(IEnumerable<Game> games)
        {
            var listGames = games.ToList();
            var predictions = AddSubsequentNumbersToEachNumberDrawn(listGames);
            var updatedPredictions = GroupLaterNumbersForEachGameNumber(predictions);
            var lastGame = GetLastGame(listGames);
            var gamePreviousToCurrentGame = GetGamePreviousToCurrentGame(listGames);
            var proximity = CalculateProximityOfTheLastDrawnGame(updatedPredictions, gamePreviousToCurrentGame.Numbers, lastGame.Numbers);
            var predictionsNumbers = GetLaterNumbersPredictionBasedOnLastGameNumbers(updatedPredictions, lastGame.Numbers);
            return GetRecommendedGame(predictionsNumbers, proximity);

        }

        private Dictionary<string, List<string>> AddSubsequentNumbersToEachNumberDrawn(List<Game> games)
        {
            Dictionary<string, List<string>> predictions = new();

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

        private static ICollection<PossibleGame> GetLaterNumbersPredictionBasedOnLastGameNumbers(ICollection<PossibleGame> games, string[] lastGame)
        {
            return games.Where(x => lastGame.Contains(x.Number))
                        .Select(x => new PossibleGame
                        {
                            LaterNumbers = x.LaterNumbers,
                            PossibleNumbers = x.LaterNumbers.OrderByDescending(y => y.Count()).FirstOrDefault(),
                            Number = x.Number
                        })
                        .ToList();
        }

        private LastGame GetLastGame(ICollection<Game> games)
        {
            LastGame lastGame = games.LastOrDefault();
            return lastGame;
        }

        private LastGame GetGamePreviousToCurrentGame(ICollection<Game> games)
        {
            return games.ElementAt(games.Count() - 2);
        }

        private DateTime GetNextDrawDate()
        {
            if(_isMegasena)
            {
                return DateTime.Now.DayOfWeek switch
                {
                    DayOfWeek.Monday => DateTime.Now.AddDays(2),
                    DayOfWeek.Tuesday => DateTime.Now.AddDays(1),
                    DayOfWeek.Wednesday => DateTime.Now,
                    DayOfWeek.Thursday => DateTime.Now.AddDays(2),
                    DayOfWeek.Friday => DateTime.Now.AddDays(1),
                    DayOfWeek.Saturday => DateTime.Now,
                    DayOfWeek.Sunday => DateTime.Now.AddDays(3),
                    _ => DateTime.Now,
                };
            }

            return DateTime.Now.DayOfWeek switch
            {
                DayOfWeek.Sunday => DateTime.Now.AddDays(1),
                _ => DateTime.Now,
            };
        }

        private RecommendedGame GetRecommendedGame(ICollection<PossibleGame> possibleGames, Proximity proximity)
        {

            var recommendedGameNumbers = GetRecommendedNumbersWithoutRepeats(possibleGames);
            var possibleOrderedGames = possibleGames.OrderBy(pg => int.Parse(pg.Number)).AsEnumerable();

            return RecommendedGame.CreateRecommendedGame(recommendedGameNumbers, possibleOrderedGames, proximity, GetNextDrawDate(), _isMegasena);
        }

        private static IEnumerable<string> GetRecommendedNumbersWithoutRepeats(ICollection<PossibleGame> possibleGames)
        {
            List<string> recommendedGameNumbers = new();

            foreach (var possibleGame in possibleGames)
            {
                var possibleNumber = possibleGame.PossibleNumbers.FirstOrDefault();

                if (!recommendedGameNumbers.Any(rgn => rgn == possibleNumber))
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

            return recommendedGameNumbers.Select(rgn => int.Parse(rgn)).OrderBy(number => number).Select(rgn => rgn.ToString()).AsEnumerable();
        }

        private static Proximity CalculateProximityOfTheLastDrawnGame(ICollection<PossibleGame> games, string[] gamePreviousToCurrentGame, string[] currentDrawnGame)
        {
            Proximity proximity = Proximity.New(currentDrawnGame, gamePreviousToCurrentGame, new List<Successor>());

            var gamesRelatedOnlyToTheLastDrawNumbers = games.Where(game => gamePreviousToCurrentGame.Contains(game.Number));

            IEnumerable<string> gamePreviousToCurrentGameAux = gamePreviousToCurrentGame;

            var possibleGames = GetLaterNumbersPredictionBasedOnLastGameNumbers(games, gamePreviousToCurrentGame);
            var recommendedNumbers = GetRecommendedNumbersWithoutRepeats(possibleGames);

            for (var i = 0; i < gamesRelatedOnlyToTheLastDrawNumbers.Count(); i++)
            {
                proximity.Successors.Add(
                    Successor.New(
                        gamesRelatedOnlyToTheLastDrawNumbers.ElementAt(i).Number, 
                        "",
                        recommendedNumbers.ElementAt(i),
                        new List<string>())
                );

                var numbersSortedByTheMostDrawn = gamesRelatedOnlyToTheLastDrawNumbers.ElementAt(i).LaterNumbers.OrderByDescending(ln => ln.Count());

                foreach (var laterNumber in numbersSortedByTheMostDrawn)
                {
                    proximity.Successors.ElementAt(i).Numbers.Add(laterNumber.FirstOrDefault());

                    if (proximity.Successors.ElementAt(i).Numbers.Any(number => gamePreviousToCurrentGameAux.Contains(number)))
                    {
                        proximity.Successors.ElementAt(i).DrawnNumber = laterNumber.FirstOrDefault();
                        gamePreviousToCurrentGameAux = gamePreviousToCurrentGameAux.Where(cdg => cdg != laterNumber.FirstOrDefault());
                        break;
                    }
                }
            }

            return proximity;
        }
    }
}
