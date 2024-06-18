using System;

namespace ConnectFour_PatAndJein
{
    class Program
    {
        static string player1Name;
        static string player2Name;
        static bool singlePlayerMode;
        static char[,] board = new char[7, 6];

        static void Main(string[] args)
        {
            SetupGame();
            CreateBoard();
            PrintBoard(); 
        }

        static void SetupGame()
        {
            Console.WriteLine("Connect 4 Game Development Project:");
            // Set up MODE (1 OR 2)
            Console.WriteLine("Choose game mode: 1 (Single Player), 2 (Two Players)");
            int mode;
            while (!int.TryParse(Console.ReadLine(), out mode) || (mode != 1 && mode != 2))
            {
                Console.WriteLine("Invalid input. Please choose 1 (Single Player) OR 2 (Two Players):");
            }

            singlePlayerMode = (mode == 1);

            // Player Name set-up
            Console.Write("Enter Player 1 name: ");
            player1Name = Console.ReadLine();

            if (singlePlayerMode)
            {
                player2Name = "AI";
            }
            else
            {
                player2Name = PromptPlayerName();
            }

            Console.WriteLine($"Player 1: {player1Name} (X)");
            Console.WriteLine($"Player 2: {player2Name} (O)");
        }

        static string PromptPlayerName()
        {
            Console.Write("Enter Player 2 name: ");
            return Console.ReadLine();
        }

        static void CreateBoard()
        {
            // Create the game board (create a 7x6 grid)
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    board[i, j] = '#';
                }
            }
        }

        static void PrintBoard()
        {
            // Print the game board
            for (int j = 5; j >= 0; j--)
            {
                for (int i = 0; i < 7; i++)
                {
                    Console.Write($"| {board[i, j]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  0   1   2   3   4   5   6  ");
        }
    }
}
