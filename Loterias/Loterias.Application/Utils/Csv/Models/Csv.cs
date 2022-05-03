using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterias.Application.Utils.Csv.Models
{
    public class Csv
    {
        public string DrawDate { get; set; }   
        public string NumberOne { get; set; }
        public string NumberTwo { get; set; }
        public string NumberThree { get; set; }
        public string NumberFour { get; set; }
        public string NumberFive { get; set; }
        public string NumberSix { get; set; }
        public string NumberSeven { get; set; }
        public string NumberEight { get; set; }
        public string NumberNine { get; set; }
        public string NumberTen { get; set; }
        public string NumberEleven { get; set; }
        public string NumberTwelve { get; set; }
        public string NumberThirteen { get; set; }
        public string NumberFourteen { get; set; }
        public string NumberFiveteen { get; set; }

        public static implicit operator Csv(List<HtmlNode> gameDrawn)
        {
            DateTime.TryParse(gameDrawn.ElementAt(0).InnerText, out DateTime result);
            DateTime drawDate = result;

            return new Csv
            {
                DrawDate = drawDate.Date.ToShortDateString(),
                NumberOne = gameDrawn.ElementAtOrDefault(1)?.InnerText.TrimStart('0'),
                NumberTwo = gameDrawn.ElementAtOrDefault(2)?.InnerText.TrimStart('0'),
                NumberThree = gameDrawn.ElementAtOrDefault(3)?.InnerText.TrimStart('0'),
                NumberFour = gameDrawn.ElementAtOrDefault(4)?.InnerText.TrimStart('0'),
                NumberFive = gameDrawn.ElementAtOrDefault(5)?.InnerText.TrimStart('0'),
                NumberSix = gameDrawn.ElementAtOrDefault(6)?.InnerText.TrimStart('0'),
                NumberSeven = gameDrawn.ElementAtOrDefault(7)?.InnerText.TrimStart('0'),
                NumberEight = gameDrawn.ElementAtOrDefault(8)?.InnerText.TrimStart('0'),
                NumberNine = gameDrawn.ElementAtOrDefault(9)?.InnerText.TrimStart('0'),
                NumberTen = gameDrawn.ElementAtOrDefault(10)?.InnerText.TrimStart('0'),
                NumberEleven = gameDrawn.ElementAtOrDefault(11)?.InnerText.TrimStart('0'),
                NumberTwelve = gameDrawn.ElementAtOrDefault(12)?.InnerText.TrimStart('0'),
                NumberThirteen = gameDrawn.ElementAtOrDefault(13)?.InnerText.TrimStart('0'),
                NumberFourteen = gameDrawn.ElementAtOrDefault(14)?.InnerText.TrimStart('0'),
                NumberFiveteen = gameDrawn.ElementAtOrDefault(15)?.InnerText.TrimStart('0'),

            };
        }
    }
}
