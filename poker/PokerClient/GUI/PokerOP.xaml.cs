﻿using poker.PokerGame;
using poker.PokerGame.Moves;
using PokerClient.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for PokerOP.xaml
    /// </summary>
    public partial class PokerOP : UserControl
    {
        private Poker poker;

        public PokerOP()
        {
            InitializeComponent();
            IsEnabled = false;
        }

        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (poker.room.Game.GetActivePlayer() == null || !((TexasGame)poker.room.Game).Active)
                {
                    IsEnabled = false;
                    return;
                }                 
                IsEnabled = poker.room.Game.GetActivePlayer().Player.Equals(MainInfo.Instance.Player);
                if (IsEnabled)
                {
                    Slider.Maximum = poker.room.Game.GetActivePlayer().Money;
                    int minimumToBet = poker.room.Game.GetHigestBet() - poker.room.Game.GetActivePlayer().CurrentBet;

                    Slider.Minimum = ((TexasGame)poker.room.Game).GamePreferences.BigBlind + minimumToBet;
                    Slider.Value = Slider.Minimum;
                    //you cant Check if minimum is not 0
                    CheckButton.IsEnabled = minimumToBet == 0;
                    //you cant Call if minimum is 0
                    CallButton.IsEnabled = minimumToBet != 0;
                    // you cant raise if you dont have enuogh money
                    RaiseButton.IsEnabled = Slider.Minimum <= poker.room.Game.GetActivePlayer().Money;
                }
            });       
        }

        public void SetOp(Poker poker)
        {
            this.poker = poker;
        }

        private void FoldButton_Click(object sender, RoutedEventArgs e)
        {
            if (poker.room.Game.GetActivePlayer().Player.Equals(MainInfo.Instance.Player))
                Service.Instance.SendMoveToGame(poker.room.Id + "", new Move("Fold", 0));

        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (poker.room.Game.GetActivePlayer().Player.Equals(MainInfo.Instance.Player))
                Service.Instance.SendMoveToGame(poker.room.Id + "", new Move("Check", 0));
        }

        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            if(poker.room.Game.GetActivePlayer().Player.Equals(MainInfo.Instance.Player))
                Service.Instance.SendMoveToGame(poker.room.Id + "", new Move("Call", (int)poker.room.Game.GetHigestBet() - poker.room.Game.GetActivePlayer().CurrentBet));
        }

        private void RaiseButton_Click(object sender, RoutedEventArgs e)
        {
            if (poker.room.Game.GetActivePlayer().Player.Equals(MainInfo.Instance.Player))
                Service.Instance.SendMoveToGame(poker.room.Id + "", new Move("Raise", (int)Slider.Value));
        }
    }
}
