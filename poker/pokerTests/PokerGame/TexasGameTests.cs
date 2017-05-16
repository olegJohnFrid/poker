﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using poker.ServiceLayer;
using poker.Center;
using poker.Players;
using poker.PokerGame.Moves;
using poker.Data;

namespace poker.PokerGame.Tests
{
    [TestClass()]
    public class TexasGameTests
    {
        [TestMethod()]
        public void IsAllowSpectatingTest()  //TODO Split to two test: pass/fail
        {
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 100);
            GamePreferences prefDisallow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, false, 100);
            IGame game1 = new TexasGame(prefAllow);
            Assert.IsTrue(game1.IsAllowSpectating());
            IGame game2 = new TexasGame(prefDisallow);
            Assert.IsFalse(game2.IsAllowSpectating());
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "no free chair.")]
        public void JoinExistingGameTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 100);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            GamePlayer p4 = new GamePlayer(new Player(4, "oleg", "1234", "oleg@gmail.com", league), 1000);
            GamePlayer p5 = new GamePlayer(new Player(5, "eliran", "1234", "eliran@gmail.com", league), 1000);
            List<int> chairs = game1.GetFreeChairs();
            Random rnd = new Random();
            int chair = chairs.ElementAt(rnd.Next(chairs.Count));
            game1.Join(500, chair, p1);
            chairs = game1.GetFreeChairs();
            Assert.IsFalse(chairs.Contains(chair));
            Assert.IsTrue(chairs.Count == 3);
            chair = chairs.ElementAt(rnd.Next(chairs.Count));
            game1.Join(500, chair, p2);
            chairs = game1.GetFreeChairs();
            Assert.IsFalse(chairs.Contains(chair));
            Assert.IsTrue(chairs.Count == 2);
            chair = chairs.ElementAt(rnd.Next(chairs.Count));
            game1.Join(500, chair, p3);
            chairs = game1.GetFreeChairs();
            Assert.IsFalse(chairs.Contains(chair));
            Assert.IsTrue(chairs.Count == 1);
            chair = chairs.ElementAt(rnd.Next(chairs.Count));
            game1.Join(500, chair, p3);
            chairs = game1.GetFreeChairs();
            Assert.IsTrue(chairs.Contains(chair));
            Assert.IsTrue(chairs.Count == 1);
            chair = chairs.ElementAt(rnd.Next(chairs.Count));
            game1.Join(5000, chair, p4);
            chairs = game1.GetFreeChairs();
            Assert.IsTrue(chairs.Contains(chair));
            Assert.IsTrue(chairs.Count == 1);
            chair = chairs.ElementAt(rnd.Next(chairs.Count));
            game1.Join(500, chair, p4);
            chairs = game1.GetFreeChairs();
            Assert.IsFalse(chairs.Contains(chair));
            Assert.IsTrue(chairs.Count == 0);
            chair=chairs.ElementAt(rnd.Next(chairs.Count));//should throw exception- game is full.
            game1.Join(500, chair, p5);
        }

        [TestMethod()]
        public void CheckTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 100);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            p1.NextMove= new Check(p1);
            p2.NextMove = new Check(p2);
            p3.NextMove = new Check(p3);
            game1.Join(100, 0, p1);
            game1.Join(100, 1, p2);
            game1.Join(100, 2, p3);
            GamePlayer firstPlayer = game1.GetFirstPlayer();
            game1.StartGame();
            GamePlayer nextPlayer = game1.GetNextPlayer();
            game1.NextTurn();
            Assert.AreSame(p2, nextPlayer);
            nextPlayer = game1.GetNextPlayer();
            Assert.AreSame(p3, nextPlayer);
            game1.NextTurn();
        }

        [TestMethod()]
        public void CallRaiseFoldTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            game1.Join(100, 0, p1);
            game1.Join(100, 1, p2);
            game1.Join(100, 2, p3);
            ((TexasGame)game1).debug = true;
            game1.StartGame();
            GamePlayer currnetPlayer = game1.GetActivePlayer(); //p1
            currnetPlayer.NextMove = new Raise(10, currnetPlayer);
            game1.NextTurn();
            Assert.AreNotEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(990, currnetPlayer.Money);

            currnetPlayer = game1.GetActivePlayer(); //p2
            currnetPlayer.NextMove = new Check(currnetPlayer);
            game1.NextTurn(); // not need to do
            Assert.AreEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual (1000, currnetPlayer.Money);

            currnetPlayer.NextMove = new Call(5, currnetPlayer); // not  enough
            game1.NextTurn(); // not need to do
            Assert.AreEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(1000, currnetPlayer.Money);

            currnetPlayer.NextMove = new Call(10, currnetPlayer); // OK
            game1.NextTurn(); // need to do
            Assert.AreNotEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(990, currnetPlayer.Money);

            GamePlayer foldPlayer = game1.GetActivePlayer(); //p3
            foldPlayer.NextMove = new Fold(foldPlayer);
            game1.NextTurn();
            Assert.IsTrue(foldPlayer.IsFold());

            game1.NextRound();

            currnetPlayer = game1.GetActivePlayer(); //p1
            Assert.AreNotEqual(foldPlayer, currnetPlayer);
            currnetPlayer.NextMove = new Raise(1000, currnetPlayer); //not need to do
            game1.NextTurn();
            Assert.AreEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(990, currnetPlayer.Money);

            currnetPlayer.NextMove = new Raise(10, currnetPlayer); //OK
            game1.NextTurn();
            Assert.AreNotEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(980, currnetPlayer.Money);

            currnetPlayer = game1.GetActivePlayer(); //p2
            Assert.AreNotEqual(foldPlayer, currnetPlayer);
            currnetPlayer.NextMove = new Raise(20, currnetPlayer); //OK
            game1.NextTurn();
            Assert.IsNull(game1.GetActivePlayer());
            Assert.AreEqual(970, currnetPlayer.Money);

        }

        [TestMethod()]
        public void GetListActivePlayersTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            int maxPlayers = 6;
            int minPlayers = 2;
            int minBuyIn = 100;
            int maxBuyIn = 1000;
            bool allowSpectating = true;
            int bigBlind = 100;
            int playerAmount = 500;            
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, maxPlayers, minPlayers, minBuyIn, maxBuyIn, allowSpectating, bigBlind);
            IGame game1 = new TexasGame(prefAllow);
            
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            GamePlayer p4 = new GamePlayer(new Player(4, "oleg", "1234", "oleg@gmail.com", league), 1000);
            GamePlayer p5 = new GamePlayer(new Player(5, "eliran", "1234", "eliran@gmail.com", league), 1000);
            game1.Join(playerAmount, 0, p1);
            game1.Join(playerAmount, 1, p2);
            game1.StartGame();
            AddPlayerToGame(playerAmount, game1, p3);
            AddPlayerToGame(playerAmount, game1, p4);
            AddPlayerToGame(playerAmount, game1, p5);

            List<GamePlayer> accpectedAnswer1 = new List<GamePlayer>();
            List<GamePlayer> receivedAnswer1 = game1.GetListActivePlayers();
            accpectedAnswer1.Add(p1);
            accpectedAnswer1.Add(p2);
            accpectedAnswer1.Add(p3);
            accpectedAnswer1.Add(p4);
            accpectedAnswer1.Add(p5);
                     
            Assert.IsTrue(CompareLists(accpectedAnswer1,receivedAnswer1));
        }

        public static void AddPlayerToGame(int playerAmount, IGame gameAddTo, GamePlayer playerToAdd)
        {
            List<int> chairs = gameAddTo.GetFreeChairs();
            Random rnd = new Random();
            int chair = chairs.ElementAt(rnd.Next(chairs.Count));
            gameAddTo.Join(playerAmount, chair, playerToAdd);
        }

        public bool CompareLists<T>(List<T> listA, List<T> listB)
        {
            if (listA.Count != listB.Count) return false;
            foreach (T p1 in listA)
            {
                if ((listB.Find(x => x.Equals(p1))) == null)
                    return false;
            }
            return true;
        }


    }
}