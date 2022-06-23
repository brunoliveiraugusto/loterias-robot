using Loterias.Application.Utils.Settings;

namespace Loterias.Test.Builders.Models
{
    public class GameInfoTestBuilder : BaseTestBuilder<GameSettings>
    {
        public GameInfoTestBuilder()
        {
            Model = new GameSettings();
        }

        public GameInfoTestBuilder Default()
        {
            Model = new GameSettings
            {
                GameAcronym = "MS",
                IsMegaSena = true
            };

            return this;
        }

        public GameInfoTestBuilder GetLotofacil()
        {
            Model = new GameSettings
            {
                GameAcronym = "LF",
                IsMegaSena = false
            };

            return this;
        }
    }
}
