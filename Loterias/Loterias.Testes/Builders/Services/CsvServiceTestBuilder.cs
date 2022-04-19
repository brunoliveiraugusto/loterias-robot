using Loterias.Application.Services.Interfaces;
using Moq;

namespace Loterias.Test.Builders.Services
{
    public class CsvServiceTestBuilder : BaseTestBuilder<Mock<ICsvService>>
    {
        public CsvServiceTestBuilder()
        {
            Model = new Mock<ICsvService>();
        }
    }
}
