using Loterias.Application.Utils.Csv.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loterias.Application.Services.Interfaces
{
    public interface IScrapingService
    {
        Task<IEnumerable<Csv>> Read();
    }
}
