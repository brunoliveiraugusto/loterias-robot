using Loterias.Application.Models;
using System.Linq;

namespace Loterias.Application.Utils.Email.Templates
{
    public class RecommendedGameHtml
    {
        public string Html { get; set; }

        public static implicit operator RecommendedGameHtml(RecommendedGame recommendedGame)
        {
            //TODO: Adequar proximidade ao cenário de jogos com mais de 6 números.
            string html = RecommendedGameTemplate.Game
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

                                .Replace("{PrimeiroNumeroUltimoSorteio}", recommendedGame.Proximity.LastGame.ElementAt(0))
                                .Replace("{SegundoNumeroUltimoSorteio}", recommendedGame.Proximity.LastGame.ElementAt(1))
                                .Replace("{TerceiroNumeroUltimoSorteio}", recommendedGame.Proximity.LastGame.ElementAt(2))
                                .Replace("{QuartoNumeroUltimoSorteio}", recommendedGame.Proximity.LastGame.ElementAt(3))
                                .Replace("{QuintoNumeroUltimoSorteio}", recommendedGame.Proximity.LastGame.ElementAt(4))
                                .Replace("{SextoNumeroUltimoSorteio}", recommendedGame.Proximity.LastGame.ElementAt(5))

                                .Replace("{PrimeiroNumeroQueFoiRecomendadoPeloAlgoritmo}", recommendedGame.Proximity.Successors.OrderBy(s => int.Parse(s.RecommendedNumber)).ElementAt(0).RecommendedNumber)
                                .Replace("{SegundoNumeroQueFoiRecomendadoPeloAlgoritmo}", recommendedGame.Proximity.Successors.OrderBy(s => int.Parse(s.RecommendedNumber)).ElementAt(1).RecommendedNumber)
                                .Replace("{TerceiroNumeroQueFoiRecomendadoPeloAlgoritmo}", recommendedGame.Proximity.Successors.OrderBy(s => int.Parse(s.RecommendedNumber)).ElementAt(2).RecommendedNumber)
                                .Replace("{QuartoNumeroQueFoiRecomendadoPeloAlgoritmo}", recommendedGame.Proximity.Successors.OrderBy(s => int.Parse(s.RecommendedNumber)).ElementAt(3).RecommendedNumber)
                                .Replace("{QuintoNumeroQueFoiRecomendadoPeloAlgoritmo}", recommendedGame.Proximity.Successors.OrderBy(s => int.Parse(s.RecommendedNumber)).ElementAt(4).RecommendedNumber)
                                .Replace("{SextoNumeroQueFoiRecomendadoPeloAlgoritmo}", recommendedGame.Proximity.Successors.OrderBy(s => int.Parse(s.RecommendedNumber)).ElementAt(5).RecommendedNumber)

                                .Replace("{posicaoPrimeiroNumeroSorteadoCorretamente}", (recommendedGame.Proximity.Successors.ElementAt(0).Numbers.Count() - 1).ToString())
                                .Replace("{primeiroNumeroQueFoiEscolhido}", recommendedGame.Proximity.Successors.ElementAt(0).Numbers.FirstOrDefault())
                                .Replace("{posicaoDoPrimeiroNumeroQueDeveriaSerEscolhido}", (recommendedGame.Proximity.Successors.ElementAt(0).Numbers.Count() - 1).ToString())
                                .Replace("{primeiroNumeroCorreto}", recommendedGame.Proximity.Successors.ElementAt(0).Numbers.LastOrDefault())

                                .Replace("{posicaoSegundoNumeroSorteadoCorretamente}", (recommendedGame.Proximity.Successors.ElementAt(1).Numbers.Count() - 1).ToString())
                                .Replace("{segundoNumeroQueFoiEscolhido}", recommendedGame.Proximity.Successors.ElementAt(1).Numbers.FirstOrDefault())
                                .Replace("{posicaoDoSegundoNumeroQueDeveriaSerEscolhido}", (recommendedGame.Proximity.Successors.ElementAt(1).Numbers.Count() - 1).ToString())
                                .Replace("{segundoNumeroCorreto}", recommendedGame.Proximity.Successors.ElementAt(1).Numbers.LastOrDefault())

                                .Replace("{posicaoTerceiroNumeroSorteadoCorretamente}", (recommendedGame.Proximity.Successors.ElementAt(2).Numbers.Count() - 1).ToString())
                                .Replace("{terceiroNumeroQueFoiEscolhido}", recommendedGame.Proximity.Successors.ElementAt(2).Numbers.FirstOrDefault())
                                .Replace("{posicaoDoTerceiroNumeroQueDeveriaSerEscolhido}", (recommendedGame.Proximity.Successors.ElementAt(2).Numbers.Count() - 1).ToString())
                                .Replace("{terceiroNumeroCorreto}", recommendedGame.Proximity.Successors.ElementAt(2).Numbers.LastOrDefault())

                                .Replace("{posicaoQuartoNumeroSorteadoCorretamente}", (recommendedGame.Proximity.Successors.ElementAt(3).Numbers.Count() - 1).ToString())
                                .Replace("{quartoNumeroQueFoiEscolhido}", recommendedGame.Proximity.Successors.ElementAt(3).Numbers.FirstOrDefault())
                                .Replace("{posicaoDoQuartoNumeroQueDeveriaSerEscolhido}", (recommendedGame.Proximity.Successors.ElementAt(3).Numbers.Count() - 1).ToString())
                                .Replace("{quartoNumeroCorreto}", recommendedGame.Proximity.Successors.ElementAt(3).Numbers.LastOrDefault())

                                .Replace("{posicaoQuintoNumeroSorteadoCorretamente}", (recommendedGame.Proximity.Successors.ElementAt(4).Numbers.Count() - 1).ToString())
                                .Replace("{quintoNumeroQueFoiEscolhido}", recommendedGame.Proximity.Successors.ElementAt(4).Numbers.FirstOrDefault())
                                .Replace("{posicaoDoQuintoNumeroQueDeveriaSerEscolhido}", (recommendedGame.Proximity.Successors.ElementAt(4).Numbers.Count() - 1).ToString())
                                .Replace("{quintoNumeroCorreto}", recommendedGame.Proximity.Successors.ElementAt(4).Numbers.LastOrDefault())

                                .Replace("{posicaoSextoNumeroSorteadoCorretamente}", (recommendedGame.Proximity.Successors.ElementAt(5).Numbers.Count() - 1).ToString())
                                .Replace("{sextoNumeroQueFoiEscolhido}", recommendedGame.Proximity.Successors.ElementAt(5).Numbers.FirstOrDefault())
                                .Replace("{posicaoDoSextoNumeroQueDeveriaSerEscolhido}", (recommendedGame.Proximity.Successors.ElementAt(5).Numbers.Count() - 1).ToString())
                                .Replace("{sextoNumeroCorreto}", recommendedGame.Proximity.Successors.ElementAt(5).Numbers.LastOrDefault());




            if (recommendedGame.IsMegaSena)
            {
                html = html
                        .Replace("{TipoJogo}", "Mega Sena")
                        .Replace("{exibirTagsAdicionais}", "none")
                        .Replace(" - {SetimoRecomendado} -  {OitavoRecomendado} -  {NonoRecomendado} -  {DecimoRecomendado} -  {DecimoPrimeiroRecomendado} -  " +
                            "{DecimoSegundoRecomendado} -  {DecimoTerceiroRecomendado} -  {DecimoQuartoRecomendado} -  {DecimoQuintoRecomendado}", "");
            } 
            else
            {
                html = html
                        .Replace("{TipoJogo}", "Lotofácil")
                        .Replace("{exibirTagsAdicionais}", "block")

                        .Replace("{SetimoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(6).Number)
                        .Replace("{OitavoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(7).Number)
                        .Replace("{NonoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(8).Number)
                        .Replace("{DecimoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(9).Number)
                        .Replace("{DecimoPrimeiroNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(10).Number)
                        .Replace("{DecimoSegundoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(11).Number)
                        .Replace("{DecimoTerceiroNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(12).Number)
                        .Replace("{DecimoQuartoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(13).Number)
                        .Replace("{DecimoQuintoNumeroAnterior}", recommendedGame.PossibleGames.ElementAt(14).Number)

                        .Replace("{SetimoSucessor}", recommendedGame.PossibleGames.ElementAt(6).PossibleNumbers.ElementAt(0))
                        .Replace("{OitavoSucessor}", recommendedGame.PossibleGames.ElementAt(7).PossibleNumbers.ElementAt(1))
                        .Replace("{NonoSucessor}", recommendedGame.PossibleGames.ElementAt(8).PossibleNumbers.ElementAt(2))
                        .Replace("{DecimoSucessor}", recommendedGame.PossibleGames.ElementAt(9).PossibleNumbers.ElementAt(3))
                        .Replace("{DecimoPrimeiroSucessor}", recommendedGame.PossibleGames.ElementAt(10).PossibleNumbers.ElementAt(4))
                        .Replace("{DecimoSegundoSucessor}", recommendedGame.PossibleGames.ElementAt(11).PossibleNumbers.ElementAt(5))
                        .Replace("{DecimoTerceiroSucessor}", recommendedGame.PossibleGames.ElementAt(12).PossibleNumbers.ElementAt(5))
                        .Replace("{DecimoQuartoSucessor}", recommendedGame.PossibleGames.ElementAt(13).PossibleNumbers.ElementAt(5))
                        .Replace("{DecimoQuintoSucessor}", recommendedGame.PossibleGames.ElementAt(14).PossibleNumbers.ElementAt(5))

                        .Replace("{QntdSetimoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(6).PossibleNumbers.Count().ToString())
                        .Replace("{QntdOitavoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(7).PossibleNumbers.Count().ToString())
                        .Replace("{QntdNonoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(8).PossibleNumbers.Count().ToString())
                        .Replace("{QntdDecimoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(9).PossibleNumbers.Count().ToString())
                        .Replace("{QntdDecimoPrimeiroSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(10).PossibleNumbers.Count().ToString())
                        .Replace("{QntdDecimoSegundoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(11).PossibleNumbers.Count().ToString())
                        .Replace("{QntdDecimoTerceiroSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(12).PossibleNumbers.Count().ToString())
                        .Replace("{QntdDecimoQuartoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(13).PossibleNumbers.Count().ToString())
                        .Replace("{QntdDecimoQuintoSucessorSorteado}", recommendedGame.PossibleGames.ElementAt(14).PossibleNumbers.Count().ToString())

                        .Replace("{SetimoRecomendado}", recommendedGame.Numbers.ElementAt(6).ToString())
                        .Replace("{OitavoRecomendado}", recommendedGame.Numbers.ElementAt(7).ToString())
                        .Replace("{NonoRecomendado}", recommendedGame.Numbers.ElementAt(8).ToString())
                        .Replace("{DecimoRecomendado}", recommendedGame.Numbers.ElementAt(9).ToString())
                        .Replace("{DecimoPrimeiroRecomendado}", recommendedGame.Numbers.ElementAt(10).ToString())
                        .Replace("{DecimoSegundoRecomendado}", recommendedGame.Numbers.ElementAt(11).ToString())
                        .Replace("{DecimoTerceiroRecomendado}", recommendedGame.Numbers.ElementAt(12).ToString())
                        .Replace("{DecimoQuartoRecomendado}", recommendedGame.Numbers.ElementAt(13).ToString())
                        .Replace("{DecimoQuintoRecomendado}", recommendedGame.Numbers.ElementAt(14).ToString());
            }
            
            return new RecommendedGameHtml
            {
                Html = html
            };
        }
    }
}
