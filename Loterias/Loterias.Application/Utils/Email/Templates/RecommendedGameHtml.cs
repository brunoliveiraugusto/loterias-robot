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
                                .Replace("{DataProximoSorteio}", DateTime.Now.Date.ToShortDateString())
                                .Replace("{PrimeiroNumero}", recommendedGame.PossibleGames.ElementAt(0).Number)
                                .Replace("{SegundoNumero}", recommendedGame.PossibleGames.ElementAt(1).Number)
                                .Replace("{TerceiroNumero}", recommendedGame.PossibleGames.ElementAt(2).Number)
                                .Replace("{QuartoNumero}", recommendedGame.PossibleGames.ElementAt(3).Number)
                                .Replace("{QuintoNumero}", recommendedGame.PossibleGames.ElementAt(4).Number)
                                .Replace("{SextoNumero}", recommendedGame.PossibleGames.ElementAt(5).Number)

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

                                .Replace("{PrimeiroRecomendado}", recommendedGame.Numbers.ElementAt(0))
                                .Replace("{SegundoRecomendado}", recommendedGame.Numbers.ElementAt(1))
                                .Replace("{TerceiroRecomendado}", recommendedGame.Numbers.ElementAt(2))
                                .Replace("{QuartoRecomendado}", recommendedGame.Numbers.ElementAt(3))
                                .Replace("{QuintoRecomendado}", recommendedGame.Numbers.ElementAt(4))
                                .Replace("{SextoRecomendado}", recommendedGame.Numbers.ElementAt(5))
            };
        }
    }
}
