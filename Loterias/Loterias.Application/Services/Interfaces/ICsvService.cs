using Loterias.Application.Models;
using System.Collections.Generic;

namespace Loterias.Application.Services.Interfaces
{
    public interface ICsvService
    {
        IEnumerable<Game> Read();
    }
}
