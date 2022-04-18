using Loterias.Application.Models;
using System;
using System.Linq;

namespace Loterias.Application.Utils.Email.Templates
{
    public class RecommendedGameHtml
    {
        public string Html { get; set; }

        public static implicit operator RecommendedGameHtml(RecommendedGame recommendedGame)
        {
            return new RecommendedGameHtml
            {
                Html = RecommendedGameTemplate.Game
                                .Replace("{DataProximoSorteio}", recommendedGame.NextDrawDate.Date.ToShortDateString())

                                .Replace("{PrimeiroNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(0).Number)
                                .Replace("{SegundoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(1).Number)
                                .Replace("{TerceiroNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(2).Number)
                                .Replace("{QuartoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(3).Number)
                                .Replace("{QuintoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(4).Number)
                                .Replace("{SextoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(5).Number)

                                .Replace("{PrimeiroSucessor}", recommendedGame.PossibleGames.ElementAt(0).PossibleNumbers.ElementAt(0))
                                .Replace("{SegundoSucessor}", recommendedGame.PossibleGames.ElementAt(1).PossibleNumbers.ElementAt(1))
                                .Replace("{TerceiroSucessor}", recommendedGame.PossibleGames.ElementAt(2).PossibleNumbers.ElementAt(2))
                                .Replace("{QuartoSucessor}", recommendedGame.PossibleGames.ElementAt(3).PossibleNumbers.ElementAt(3))
                                .Replace("{QuintoSucessor}", recommendedGame.PossibleGames.ElementAt(4).PossibleNumbers.ElementAt(4))
                                .Replace("{SextoSucessor}", recommendedGame.PossibleGames.ElementAt(5).PossibleNumbers.ElementAt(5))

                                .Replace("{QntdPrimeiroSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(0).PossibleNumbers.Count().ToString())
                                .Replace("{QntdSegundoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(1).PossibleNumbers.Count().ToString())
                                .Replace("{QntdTerceiroSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(2).PossibleNumbers.Count().ToString())
                                .Replace("{QntdQuartoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(3).PossibleNumbers.Count().ToString())
                                .Replace("{QntdQuintoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(4).PossibleNumbers.Count().ToString())
                                .Replace("{QntdSextoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(5).PossibleNumbers.Count().ToString())

                                .Replace("{PrimeiroRecomendado}", recommendedGame.Numbers.ElementAt(0).ToString())
                                .Replace("{SegundoRecomendado}", recommendedGame.Numbers.ElementAt(1).ToString())
                                .Replace("{TerceiroRecomendado}", recommendedGame.Numbers.ElementAt(2).ToString())
                                .Replace("{QuartoRecomendado}", recommendedGame.Numbers.ElementAt(3).ToString())
                                .Replace("{QuintoRecomendado}", recommendedGame.Numbers.ElementAt(4).ToString())
                                .Replace("{SextoRecomendado}", recommendedGame.Numbers.ElementAt(5).ToString())
            };
        }
    }
}
