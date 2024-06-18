using System;
using System.Data.Common;

namespace ConnectFour_PatAndJein
{
    class Connect4
    {
        public string player1Name;
        public string player2Name;
        public bool singlePlayerMode;
        public char[,] board = new char[7, 6];
        public char currentPlayer = 'X';
        public string currentPlayerName;
        Random r = new Random();
        public bool Play()
        {
            Console.WriteLine();
            Console.WriteLine($"----- Player {currentPlayerName}'s turn. ({currentPlayer}) ----- Enter a number of column (0-6):");

            int c;
            if (singlePlayerMode && currentPlayerName == player2Name)
            {
                c = r.Next(0, 7);

            }
            else
            {
                while (!int.TryParse(Console.ReadLine(), out c) || c < 0 || c > 6)
                {
                    Console.WriteLine($"{currentPlayerName} enter an invalid input. Enter a number of column (0-6):");
                }
            }

            

         
                if (DropDisc(c)) {

                    PrintBoard();

                    if (CheckWin()) {
                    Console.WriteLine("Congratulations!");
                    Console.WriteLine($"{currentPlayerName} Win!");
                        return false;
                    }

                    if (IsBoardFull())
                    {
                        Console.WriteLine("Game over!");
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

        public bool DropDisc(int column)
        {
            for (int row = 0; row < 6; row++)
            {
                if (board[column, row] == ' ')
                {
                    board[column, row] = currentPlayer;
                    return true;
                }
            }
            return false;
        }

        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            currentPlayerName = (currentPlayer == 'X') ? player1Name : player2Name;
        }
        
        public void PrintBoard()
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

            Console.WriteLine($"Player 1: {player1Name} (X)");
            Console.WriteLine($"Player 2: {player2Name} (O)");

            // Print the game board

            Console.WriteLine("");
            for (int j = 5; j >= 0; j--)
            {
                for (int i = 0; i < 7; i++)
                {
                    Console.Write($"| {board[i, j]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  0   1   2   3   4   5   6  ");
            Console.WriteLine("");
        }

        public bool CheckWin()
        {
            // Check horizontal
            for (int j = 0; j < 6; j++)
            {
                int count = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (board[i, j] == currentPlayer)
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
                    if (board[i, j] == currentPlayer)
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
                    if (board[i, j] == currentPlayer && board[i + 1, j + 1] == currentPlayer &&
                        board[i + 2, j + 2] == currentPlayer && board[i + 3, j + 3] == currentPlayer)
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
                    if (board[i, j] == currentPlayer && board[i + 1, j - 1] == currentPlayer &&
                        board[i + 2, j - 2] == currentPlayer && board[i + 3, j - 3] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsBoardFull()
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

    }

    class Program
    {
        static string player1Name;
        static string player2Name;
        static bool singlePlayerMode;
        static char[,] board = new char[7, 6];

        static void Main(string[] args)
        {
            Connect4 game = new Connect4();

            SetupGame();
            CreateBoard();

            game.singlePlayerMode=singlePlayerMode;
            game.player1Name=player1Name;
            game.player2Name=player2Name;
            game.board= board;
            game.currentPlayerName = player1Name;
            game.PrintBoard();

            while (game.Play())
            {

            }
            
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
                    board[i, j] = ' ';
                }
            }
        }
    }
}

