using System;
using System.Data.Common;

namespace ConnectFour_PatAndJein
{
    class Player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }

        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }
    }

    class Board
    {
        private readonly char[,] board = new char[7, 6];

        public Board()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        public bool DropDisc(int column, char symbol)
        {
            for (int row = 0; row < 6; row++)
            {
                if (board[column, row] == ' ')
                {
                    board[column, row] = symbol;
                    return true;
                }
            }
            return false;
        }

        public bool IsFull()
        {
            foreach (char cell in board)
            {
                if (cell == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public char[,] GetBoard()
        {
            return board;
        }

        public void PrintBoard(Player player1, Player player2)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Connect4 : Final Project");
            Console.WriteLine();
            Console.WriteLine("Developed by");
            Console.WriteLine("Hyunjung Lim, Yosita Jasamut");
            Console.WriteLine();
            Console.WriteLine("SODV1202:Introduction to Object Oriented Programming-24MAYMNTR1");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine($"Player 1: {player1.Name} ({player1.Symbol})");
            Console.WriteLine($"Player 2: {player2.Name} ({player2.Symbol})");
            Console.WriteLine();

            for (int j = 5; j >= 0; j--)
            {
                for (int i = 0; i < 7; i++)
                {
                    Console.Write($"| {board[i, j]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  0   1   2   3   4   5   6  ");
            Console.WriteLine();
        }
    }

    class Connect4
    {
        private readonly Player player1;
        private readonly Player player2;
        private Player currentPlayer;
        private readonly Board board;
        private readonly bool singlePlayerMode;
        private readonly Random r = new Random();

        public Connect4(Player p1, Player p2, bool isSinglePlayerMode)
        {
            player1 = p1;
            player2 = p2;
            currentPlayer = player1;
            board = new Board();
            singlePlayerMode = isSinglePlayerMode;
        }

        public void StartGame()
        {
            board.InitializeBoard();
            board.PrintBoard(player1, player2);
            while (Play()) { }
        }

        private bool Play()
        {
            Console.WriteLine();
            Console.WriteLine($"----- Player {currentPlayer.Name}'s turn. ({currentPlayer.Symbol}) ----- Enter a number of column (0-6):");

            int c;
            if (singlePlayerMode && currentPlayer == player2)
            {
                c = r.Next(0, 7);
            }
            else
            {
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "exit")
                    {
                        Console.WriteLine("Thanks for playing!");
                        Environment.Exit(0);
                    }
                    if (input.Length == 1 && int.TryParse(input, out c) && c >= 0 && c <= 6)
                    {
                        break;
                    }
                    Console.WriteLine($"{currentPlayer.Name}, enter a valid number of column (0-6):");
                }
            }

            if (board.DropDisc(c, currentPlayer.Symbol))
            {
                board.PrintBoard(player1, player2);

                if (CheckWin())
                {
                    Console.WriteLine("Congratulations!");
                    Console.WriteLine($"{currentPlayer.Name} Wins!");
                    return false;
                }

                if (board.IsFull())
                {
                    Console.WriteLine("Game over! It's a draw!");
                    return false;
                }

                SwitchPlayer();
            }
            else
            {
                Console.WriteLine($"!!! Column {c} is full. Please select another column. !!!");
                Console.WriteLine();
            }

            return true;
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        private bool CheckWin()
        {
            char[,] b = board.GetBoard();

            // Check horizontal
            for (int j = 0; j < 6; j++)
            {
                int count = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (b[i, j] == currentPlayer.Symbol)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                    if (count == 4)
                    {
                        return true;
                    }
                }
            }

            // Check vertical
            for (int i = 0; i < 7; i++)
            {
                int count = 0;
                for (int j = 0; j < 6; j++)
                {
                    if (b[i, j] == currentPlayer.Symbol)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                    if (count == 4)
                    {
                        return true;
                    }
                }
            }

            // Check diagonal (bottom-left to top-right)
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (b[i, j] == currentPlayer.Symbol && b[i + 1, j + 1] == currentPlayer.Symbol &&
                        b[i + 2, j + 2] == currentPlayer.Symbol && b[i + 3, j + 3] == currentPlayer.Symbol)
                    {
                        return true;
                    }
                }
            }

            // Check diagonal (top-left to bottom-right)
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (b[i, j] == currentPlayer.Symbol && b[i + 1, j - 1] == currentPlayer.Symbol &&
                        b[i + 2, j - 2] == currentPlayer.Symbol && b[i + 3, j - 3] == currentPlayer.Symbol)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    class Game
    {
        public static void Start()
        {
            while (true)
            {
                // Set up game mode and player names
                Console.WriteLine("Connect 4 Game Development Project:");
                Console.WriteLine("Choose game mode: 1 (Single Player), 2 (Two Players)");

                bool singlePlayerMode;
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "exit")
                    {
                        Console.WriteLine("Thanks for playing!");
                        Environment.Exit(0);
                    }

                    if (int.TryParse(input, out int mode) && (mode == 1 || mode == 2))
                    {
                        singlePlayerMode = (mode == 1);
                        break;
                    }
                    Console.WriteLine("Invalid input. Please choose 1 (Single Player) OR 2 (Two Players) or type 'exit' to quit:");
                }

                // Set up players
                Console.Write("Enter Player 1 name: ");
                string player1Name = Console.ReadLine();
                string player2Name;

                if (singlePlayerMode)
                {
                    player2Name = "AI";
                }
                else
                {
                    Console.Write("Enter Player 2 name: ");
                    player2Name = Console.ReadLine();
                }

                Player player1 = new Player(player1Name, 'X');
                Player player2 = new Player(player2Name, 'O');

                // Create and start the game
                Connect4 game = new Connect4(player1, player2, singlePlayerMode);
                game.StartGame();

                // End game
                Console.Write("Do you want to play again? (yes/no) ");
                string response = Console.ReadLine();
                if (response.ToUpper() != "YES")
                {
                    Console.WriteLine("Thanks for playing!");
                    Environment.Exit(0);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game.Start();
        }
    }
}
