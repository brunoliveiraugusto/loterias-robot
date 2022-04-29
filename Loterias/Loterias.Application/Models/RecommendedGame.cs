using System;
using System.Collections.Generic;

namespace Loterias.Application.Models
{
    public class RecommendedGame
    {
        public IEnumerable<int> Numbers { get; set; }
        public DateTime NextDrawDate { get; set; }
        public IEnumerable<PossibleGame> PossibleGames { get; set; }
        public LastDraw LastDraw { get; set; }
        public bool IsMegaSena { get; set; }

        public static RecommendedGame CreateRecommendedGame(IEnumerable<int> numbers, DateTime nextDrawDate, IEnumerable<PossibleGame> possibleGames, bool isMegaSena)
        {
            return new RecommendedGame
            {
                Numbers = numbers,
                NextDrawDate = nextDrawDate,
                PossibleGames = possibleGames,
                IsMegaSena = isMegaSena
            };
        }
    }
}
