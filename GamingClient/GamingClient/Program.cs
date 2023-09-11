using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

class Program
{
    static void Main()
    {
        TcpClient client = new TcpClient("127.0.0.1", 12345);
        NetworkStream stream = client.GetStream();
        var reader = new StreamReader(stream);
        var writer = new StreamWriter(stream) { AutoFlush = true };
        bool firstGame = true;

        while (true)
        {
            string response = reader.ReadLine();
            Console.WriteLine(response);

            if (response.StartsWith("Correct!"))
            {
                Console.Write("Play again? (Yes|No): ");
                string reponse = Console.ReadLine();

                if(reponse == "No")
                {
                    Console.Write("Thank you and see you soon!");
                    Thread.Sleep(3000);
                    break;
                }
            }

            if (firstGame)
            {
                firstGame = false;
                Console.Write("Enter your name: ");

                string input = Console.ReadLine();
                writer.WriteLine(input);
                
            }
            else
            {
                Console.Write("Enter your guess: ");

                string input = Console.ReadLine();
                int guess = int.Parse(input);
                writer.WriteLine(guess);
            }
        }

        client.Close();
    }
}