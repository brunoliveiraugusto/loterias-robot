using Loterias.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loterias.Application.Services.Interfaces
{
    public interface ICsvService
    {
        Task<IEnumerable<Game>> Read();
        Task Update(IEnumerable<Game> games);
    }
}
