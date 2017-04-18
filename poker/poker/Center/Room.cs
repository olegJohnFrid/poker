﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    public class Room
    {
        private Chat chat;
        private IGame game;
        private List<IGame> pastGames;

        public Room(IGame game)
        {
            this.game = game;
            pastGames = new List<IGame>();
        }

        public IGame Game
        {
            get
            {
                return game;
            }

            set
            {
                if (game != null && game.isActive())
                {
                    game.finishGame();
                    pastGames.Add(game);
                }
                game = value;
            }
        }

        public List<IGame> PastGames
        {
            get
            {
                return pastGames;
            }
        }
    }
}
