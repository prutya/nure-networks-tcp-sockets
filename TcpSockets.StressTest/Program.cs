﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TcpSockets.Common;

namespace TcpSockets.StressTest
{
    public class Program
    {
        private const string hackerString = "time";

        private static readonly IPAddress serverIpAddress = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 3000;

        public static void Main(string[] args)
        {
            Logger.Log("Okay, imagine yourself a cool hacker trying to kill the server.");

            serverPort = Input.PortPrompt();
            
            for (int i = 0; i < ushort.MaxValue; i++)
            {
                TcpClient client = new TcpClient();
                try
                {
                    client.Connect(serverIpAddress, serverPort);
                    Socket clientSocket = client.Client;

                    clientSocket.SendString(hackerString);

                    string response = clientSocket.RecieveString();
                    Logger.Log($"Server response is: \"{response}\"");
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                    client.Close();
                }
            }
            Console.Read();
        }
    }
}
