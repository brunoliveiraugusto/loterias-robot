using System;
using System.Collections.Generic;

namespace Loterias.Application.Models
{
    public class RecommendedGame
    {
        public IEnumerable<int> Numbers { get; set; }
        public DateTime NextDrawDate { get; set; }
        public IEnumerable<PossibleGame> PossibleGames { get; set; }

        public static RecommendedGame CreateRecommendedGame(IEnumerable<int> numbers, DateTime nextDrawDate, IEnumerable<PossibleGame> possibleGames)
        {
            return new RecommendedGame
            {
                Numbers = numbers,
                NextDrawDate = nextDrawDate,
                PossibleGames = possibleGames
            };
        }
    }
}
