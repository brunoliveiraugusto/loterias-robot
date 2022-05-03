using System;

namespace Loterias.Application.Models
{
    public class LastGame
    {
        public string[] Numbers { get; set; }
        public DateTime DrawDate { get; set; }

        public static implicit operator LastGame(Game game)
        {
            return new LastGame
            {
                Numbers = game.GameDrawn.Split("-"),
                DrawDate = game.DrawDate
            };
        }
    }
}
