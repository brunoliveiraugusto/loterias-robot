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

        public static implicit operator Csv(List<HtmlNode> gameDrawn)
        {
            //TODO: INCLUIR FOR PARA PREENCHIMENTO DOS CAMPOS COM BASE NA VARIÁVEL INDICATIVA DA QUANTIDADE DE NÚMEROS DE JOGOS DO APPSETTINGS
            DateTime.TryParse(gameDrawn.ElementAt(0).InnerText, out DateTime result);
            DateTime drawDate = result;

            return new Csv
            {
                DrawDate = drawDate.Date.ToShortDateString(),
                NumberOne = gameDrawn[1].InnerText.TrimStart('0'),
                NumberTwo = gameDrawn[2].InnerText.TrimStart('0'),
                NumberThree = gameDrawn[3].InnerText.TrimStart('0'),
                NumberFour = gameDrawn[4].InnerText.TrimStart('0'),
                NumberFive = gameDrawn[5].InnerText.TrimStart('0'),
                NumberSix = gameDrawn[6].InnerText.TrimStart('0'),
            };
        }
    }
}
