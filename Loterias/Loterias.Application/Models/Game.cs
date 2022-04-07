using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterias.Application.Models
{
    public class Game
    {
        public string GameDrawn { get; set; }
        public DateTime DrawDate { get; set; }

        public static implicit operator Game(string data)
        {
            var gameData = data.Split(";");
            var drawDate = gameData[0].Split("/").Select(x => int.TryParse(x, out int result) ? result : 0).ToArray();
            string gameDrawn = data.Replace(gameData[0] + ";", "").Replace(";", "-");

            return new Game
            {
                DrawDate = new DateTime(drawDate[2], drawDate[1], drawDate[0]),
                GameDrawn = gameDrawn
            };
        }

        public static implicit operator Game(List<HtmlNode> gameDrawn)
        {
            DateTime.TryParse(gameDrawn.ElementAt(0).InnerText, out DateTime result);
            DateTime drawDate = result;

            return new Game
            {
                DrawDate = drawDate,
                GameDrawn = string.Join(";", gameDrawn.Skip(1))
            };
        }
    }
}
