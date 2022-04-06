using System.Collections.Generic;

namespace Loterias.Application.Models
{
    public class PossibleGame
    {
        public string Number { get; set; }
        public IEnumerable<IEnumerable<string>> LaterNumbers { get; set; }
        public IEnumerable<string> PossibleNumbers { get; set; }

        public PossibleGame()
        {
            LaterNumbers = new List<List<string>>();
            PossibleNumbers = new List<string>();
        }
    }
}
