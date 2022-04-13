using Loterias.Application.Models;
using System.Threading.Tasks;

namespace Loterias.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> ProcessEmailSubmission(RecommendedGame recommendedGame);
    }
}
