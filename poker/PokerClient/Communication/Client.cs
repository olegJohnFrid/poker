﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientPoker.Communication
{
    public class Client
    {
        private TcpClient client;
        private readonly object lock_ = new object();

        private StreamReader sReader;
        private StreamWriter sWriter;

        private Boolean isConnected;

        public Client(String ipAddress, int portNum)
        {
            client = new TcpClient();
            client.Connect(ipAddress, portNum);
            isConnected = true;

            sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);

            Thread listener = new Thread(new ThreadStart(this.Listener));
            listener.Start();
        }

        public void SendMessage(Command command)
        {
            sWriter.WriteLine(JsonConvert.SerializeObject(command));
            sWriter.Flush();
        }

        public Command GetMessage()
        {
            return JsonConvert.DeserializeObject<Command>(sReader.ReadLine());
        }

        public void Listener()
        {
            Command command;
            while (true)
            {
                string answer = sReader.ReadLine();
                command = JsonConvert.DeserializeObject<Command>(answer);
                if (command == null)
                    continue;
                Parser.Parse(command);
            }
        }

        public void CloseConnection()
        {
            isConnected = false;
            sReader.Close();
            sWriter.Close();
            client.Close();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}