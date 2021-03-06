﻿using PokerClient.Communication;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            IPadress.Text = Client.GetLocalIPAddress();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            if (!MainInfo.Instance.ConnectToServer(IPadress.Text))
            {
                MessageBox.Show("Cannot Connect To Server...","Connection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Service.Instance.DoLogin(usernameBox.Text, passwordBox.Password);       
        }

        public void LoginFaild()
        {
            this.Dispatcher.Invoke(() =>
            {
                MainWindow.ShowMessage("Error with loggin, please try again");            
            });

        }

        private void newUserButton_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            this.Content = reg;
        }
    }
}
