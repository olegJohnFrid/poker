﻿using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Center
{
    public class Room
    {
        private int id;
        private Chat chat;
        private IGame game;
        private bool haveActiveGame;

        public Room(int id, Chat chat, TexasGame game, bool haveActiveGame)
        {
            this.Id = id;
            this.chat = chat;
            this.game = game;
            this.haveActiveGame = haveActiveGame;
        }

        public int Id { get { return id; } set { id = value; } }
        public Chat Chat { get { return chat; } set { chat = value; } }
        public IGame Game { get { return game; } set { game = value; } }
        public bool HaveActiveGame { get { return haveActiveGame; } set { haveActiveGame = value; } }
    }
}
