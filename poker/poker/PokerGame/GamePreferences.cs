﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame
{
    public class GamePreferences
    {
        private int maxPlayers;
        private int minPlayers;
        private int minBuyIn;
        private int maxBuyIn;
        private bool allowSpectating;
        private int smallBlind;
        private int bigBlind;
        private GameTypePolicy gameTypePolicy;
        public enum GameTypePolicy { LIMIT, NO_LIMIT, POT_LIMIT};

        public GameTypePolicy GetGameType()
        {
            return GameTypePolicy1;
        }
        public int MaxPlayers
        {
            get
            {
                return maxPlayers;
            }

            set
            {
                maxPlayers = value;
            }
        }

        public int MinPlayers
        {
            get
            {
                return minPlayers;
            }

            set
            {
                minPlayers = value;
            }
        }

        public GameTypePolicy getGameTypePolicy()
        {
            return GameTypePolicy1;
        }

        public int MinBuyIn
        {
            get
            {
                return minBuyIn;
            }

            set
            {
                minBuyIn = value;
            }
        }

        public int BigBlind
        {
            get
            {
                return bigBlind;
            }

            set
            {
                bigBlind = value;
            }
        }

        public int SmallBlind
        {
            get
            {
                return smallBlind;
            }

            set
            {
                smallBlind = value;
            }
        }

        public int MaxBuyIn
        {
            get
            {
                return maxBuyIn;
            }

            set
            {
                maxBuyIn = value;
            }
        }

        public GamePreferences(GameTypePolicy gamePolicy ,int maxPlayers,int minPlayers, int minBuyIn, int maxBuyIn, bool allowSpectating, int bigBlind)
        {
            GameTypePolicy1 = gamePolicy;
            this.maxPlayers = maxPlayers;
            SetMinPlayers(minPlayers);
            this.minBuyIn = minBuyIn;
            this.maxBuyIn = maxBuyIn;
            this.allowSpectating = allowSpectating;
            this.bigBlind = bigBlind;
            this.smallBlind = bigBlind / 2;
        }

        public void SetMinPlayers(int minPlayers)
        {
            if(minPlayers>=2 && minPlayers <= MaxPlayers)
            {
                this.minPlayers = minPlayers;

            }
        }

        public int GetMinPlayers()
        {
            return minPlayers;
        }

        public bool AllowSpectating { get { return allowSpectating; } set { allowSpectating = value; } }

        public GameTypePolicy GameTypePolicy1 { get => gameTypePolicy; set => gameTypePolicy = value; }
    }
}
