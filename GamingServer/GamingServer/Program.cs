using GamingServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{

    static void Main()
    {
        var server = new TcpListener(IPAddress.Any, 12345);
        server.Start();
        Console.WriteLine("Server started. Waiting for players...");

        var clients = new List<TcpClient>();
        var clientThreads = new List<Thread>();

        TcpClient client;
        while (clients.Count < 2) // Wait for at least two players
        {
            client = server.AcceptTcpClient();
            clients.Add(client);

            GameSettings gameSettings = new GameSettings(1, 10);

            Console.WriteLine($"Magic number to {clients.IndexOf(client)}: {gameSettings.MagicNumber}");

            Thread clientThread = new Thread(() => HandleClient(new Player() { Id = clients.IndexOf(client) }, client, gameSettings));
            clientThreads.Add(clientThread);
            clientThread.Start();
        }

        Console.WriteLine("Game is ready with 2 players. Let the game begin!");

        foreach (var thread in clientThreads)
        {
            thread.Join(); // Wait for all client threads to finish
        }
    }

    static void HandleClient(Player player, TcpClient client, GameSettings settings)
    {
        NetworkStream stream = client.GetStream();
        var reader = new StreamReader(stream);
        var writer = new StreamWriter(stream) { AutoFlush = true };

        bool firtsText = true;

        writer.WriteLine($"Welcome to the Hi-Lo game! Guess a number between {settings.MinValue} and {settings.MaxValue}.");

        while (true)
        {
            string input = reader.ReadLine();

            if (firtsText)
            {
                player.Name = input;
                Console.WriteLine($"Player name: {input}");
                writer.WriteLine($"Welcome {input}!");
                firtsText = false;
            }
            else
            {
                if(String.IsNullOrEmpty(input))
                {
                    break; // End the game
                }

                int clientGuess = int.Parse(input);

                player.Status.IncreaseAttempts();
                if (clientGuess < settings.MagicNumber)
                    writer.WriteLine(clientGuess < settings.MinValue ? $"Number out of range. The min value is {settings.MinValue}" : "LO");
                else if (clientGuess > settings.MagicNumber)
                    writer.WriteLine(clientGuess > settings.MaxValue ? $"Number out of range. The max value is {settings.MaxValue}" : "HI");
                else
                {
                    player.Status.IncreaseScore();
                    writer.WriteLine($"Correct! Score: {player.Status.Score} Attempts: {player.Status.Attempts}");

                    settings.SetNewMagicNumber();
                    Console.WriteLine($"Magic number to {player.Id}: " + settings.MagicNumber);
                }
            }
        }
    }
}




