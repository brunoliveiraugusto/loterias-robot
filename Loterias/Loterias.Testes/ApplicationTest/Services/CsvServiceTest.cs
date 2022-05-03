using FluentAssertions;
using Loterias.Application.Models;
using Loterias.Application.Services;
using Loterias.Application.Services.Interfaces;
using Loterias.Test.Builders.Models;
using Loterias.Test.Builders.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Loterias.Test.ApplicationTest.Services
{
    public class CsvServiceTest
    {
        private readonly Mock<ICsvService> _csvServiceMock = new CsvServiceTestBuilder().Build();
        private readonly IEnumerable<Game> _gamesMock = new GameTestBuilder().Default().Build();

        private CsvService Build()
        {
            //TODO: Ajustar dependência
            return new CsvService(null);
        }

        [Fact(DisplayName = "Teste para o carregamento dos jogos")]
        public async void TestarCarregamentoDosJogosArquivoCsv()
        {
            #region Given
            CsvService service = Build();
            
            _csvServiceMock
                .Setup(s => s.Read())
                .Returns(Task.FromResult(_gamesMock));
            #endregion

            #region When
            var games = await service.Read();
            #endregion

            #region Then
            games.Should().NotBeNullOrEmpty();
            games.Should().HaveCountGreaterThan(0);
            #endregion
        }
    }
}
