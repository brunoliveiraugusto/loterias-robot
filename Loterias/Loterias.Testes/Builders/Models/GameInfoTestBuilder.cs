using Loterias.Application.Utils.Settings;

namespace Loterias.Test.Builders.Models
{
    public class GameInfoTestBuilder : BaseTestBuilder<GameInfo>
    {
        public GameInfoTestBuilder()
        {
            Model = new GameInfo();
        }

        public GameInfoTestBuilder Default()
        {
            Model = new GameInfo
            {
                GameAcronym = "MS",
                IsMegaSena = true
            };

            return this;
        }

        public GameInfoTestBuilder GetLotofacil()
        {
            Model = new GameInfo
            {
                GameAcronym = "LF",
                IsMegaSena = false
            };

            return this;
        }
    }
}
