using CsvHelper.Configuration.Attributes;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterias.Application.Utils.Csv.Models
{
    public class Csv
    {
        [Index(0)]
        public string DrawDate { get; set; }
        
        [Index(1)]
        public string NumberOne { get; set; }
        
        [Index(2)]
        public string NumberTwo { get; set; }
        
        [Index(3)]
        public string NumberThree { get; set; }
        
        [Index(4)]
        public string NumberFour { get; set; }
        
        [Index(5)]
        public string NumberFive { get; set; }
        
        [Index(6)]
        public string NumberSix { get; set; }
        
        //[Index(7), Optional]
        //public string NumberSeven { get; set; }
        //[Index(8), Optional]
        //public string NumberEight { get; set; }
        //[Index(9), Optional]
        //public string NumberNine { get; set; }
        //[Index(10), Optional]
        //public string NumberTen { get; set; }
        //[Index(11), Optional]
        //public string NumberEleven { get; set; }
        //[Index(12), Optional]
        //public string NumberTwelve { get; set; }
        //[Index(13), Optional]
        //public string NumberThirteen { get; set; }
        //[Index(14), Optional]
        //public string NumberFourteen { get; set; }
        //[Index(15), Optional]
        //public string NumberFifteen { get; set; }

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

            };
        }
    }
}
