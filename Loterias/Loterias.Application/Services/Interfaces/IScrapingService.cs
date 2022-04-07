using Loterias.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loterias.Application.Services.Interfaces
{
    public interface IScrapingService
    {
        Task<IEnumerable<Game>> Read();
    }
}
