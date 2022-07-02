using System.Collections.Generic;

namespace Loterias.Application.Models
{
    public class Successor
    {
        public string Number { get; set; }
        public string DrawnNumber { get; set; }
        public string RecommendedNumber { get; set; }
        public ICollection<string> Numbers { get; set; }

        public static Successor New(string number, string drawnNumber, string recommendedNumber, ICollection<string> numbers)
            => new()
            {
                Number = number,
                DrawnNumber = drawnNumber,
                RecommendedNumber = recommendedNumber,
                Numbers = numbers
            };
    }
}
