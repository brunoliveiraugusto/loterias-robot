using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loterias.Application.Utils.Request.Interfaces
{
    public interface IHttpService
    {
        Task<T> Get<T>(string endpoint, Dictionary<string, string> requestParams) where T : class;
    }
}
