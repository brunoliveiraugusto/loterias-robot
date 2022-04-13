using System;
using System.Collections.Generic;

namespace Loterias.Application.Models
{
    public class RecommendedGame
    {
        public IEnumerable<string> Numbers { get; set; }
        public DateTime NextDrawDate { get; set; }
        public IEnumerable<PossibleGame> PossibleGames { get; set; }

        public static RecommendedGame CreateRecommendedGame(List<string> numbers, DateTime nextDrawDate, List<PossibleGame> possibleGames)
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
