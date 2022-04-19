using Loterias.Application.Models;
using System;
using System.Collections.Generic;

namespace Loterias.Test.Builders.Models
{
    public class GameTestBuilder : BaseTestBuilder<IEnumerable<Game>>
    {
        public GameTestBuilder()
        {
            Model = new List<Game>();
        }

        public GameTestBuilder Default()
        {
            Model = new List<Game>()
            {
                new Game()
                {
                    DrawDate = DateTime.Now,
                    GameDrawn = "01-02-03-04-05-06"
                },
                new Game()
                {
                    DrawDate = DateTime.Now,
                    GameDrawn = "07-08-09-10-11-12"
                },
                new Game()
                {
                    DrawDate = DateTime.Now,
                    GameDrawn = "13-14-15-16-17-18"
                }
            };

            return this;
        }
    }
}
