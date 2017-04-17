﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Players.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        private Player player;

        [TestInitialize()]
        public void Initialize()
        {
            player = new Player(1, "yakir", "1234", "yakir@gmail.com", new Center.League(1, "level one"));
        }

        [TestCleanup()]
        public void Cleanup()
        {
            player = new Player(1, "yakir", "1234", "yakir@gmail.com", new Center.League(1, "level one"));
        }


        [TestMethod()]
        public void SetEmailTest()
        {
            String emailBefore = player.GetEmail();
            player.SetEmail("");
            Assert.AreEqual(emailBefore, player.GetEmail());

            player.SetEmail("yakir");
            Assert.AreEqual(emailBefore, player.GetEmail());

            player.SetEmail("yakir@gasdas@asdas.com");
            Assert.AreEqual(emailBefore, player.GetEmail());

            player.SetEmail("@walla.com");
            Assert.AreEqual(emailBefore, player.GetEmail());

            String newEmail = "maor@gmail.com";
            player.SetEmail(newEmail);
            Assert.AreEqual(newEmail, player.GetEmail());
            Assert.AreNotEqual(emailBefore, player.GetEmail());
        }

        [TestMethod()]
        public void SetPasswordTest()
        {
            String passBefore = player.GetPassword();
            player.SetPassword("");
            Assert.AreEqual(passBefore, player.GetEmail());
            String newPass = "12345";
            player.SetPassword(newPass);
            Assert.AreEqual(newPass, player.GetPassword());
            Assert.AreNotEqual(passBefore, player.GetPassword());
        }
    }
}