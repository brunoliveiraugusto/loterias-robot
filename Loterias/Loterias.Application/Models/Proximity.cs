using System.Collections.Generic;

namespace Loterias.Application.Models
{
    public class Proximity
    {
        public string[] CurrentDrawnGame { get; set; }
        public string[] LastGame { get; set; }
        public ICollection<Successor> Successors { get; set; }

        public static Proximity New(string[] current, string[] last, List<Successor> successors) 
            => new() 
            { 
                CurrentDrawnGame = current,
                LastGame = last,
                Successors = successors
            };
    }
}
