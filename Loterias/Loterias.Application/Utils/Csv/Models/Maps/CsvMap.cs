using CsvHelper.Configuration;
using Loterias.Application.Utils.Settings;
using Microsoft.Extensions.Options;

namespace Loterias.Application.Utils.Csv.Models.Maps
{
    public sealed class CsvMap : ClassMap<Csv>
    {
        public CsvMap(IOptions<GameSettings> options)
        {
            Map(m => m.DrawDate).Index(0);
            Map(m => m.NumberOne).Index(1);
            Map(m => m.NumberTwo).Index(2);
            Map(m => m.NumberThree).Index(3);
            Map(m => m.NumberFour).Index(4);
            Map(m => m.NumberFive).Index(5);
            Map(m => m.NumberSix).Index(6);
            Map(m => m.NumberSeven).Index(7).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberEight).Index(8).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberNine).Index(9).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberTen).Index(10).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberEleven).Index(11).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberTwelve).Index(12).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberThirteen).Index(13).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberFourteen).Index(14).Ignore(options.Value.IsMegaSena);
            Map(m => m.NumberFiveteen).Index(15).Ignore(options.Value.IsMegaSena);
        }
    }
}
