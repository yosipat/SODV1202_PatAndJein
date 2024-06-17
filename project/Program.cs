using System;

namespace ConnectFour
{
    class Program
    {
        static char[,] board = new char[7, 6];
        static char currentPlayer = 'R'; // 'R' for Red, 'Y' for Yellow
        static string player1Name, player2Name;
        static bool isSinglePlayer = false;
        static Random random = new Random();

        static void Main(string[] args)
        {
            InitializeGame();
            InitializeBoard();

            bool gameWon = false;

            while (!gameWon && !IsBoardFull())
            {
                PrintBoard();
                int column;

                if (isSinglePlayer && currentPlayer == 'Y')
                {
                    // AI move
                    column = GetAIMove();
                    Console.WriteLine($"AI chooses column {column}");
                }
                else
                {
                    Console.WriteLine($"Player {currentPlayer}'s turn. Choose a column (0-6):");
                    while (!int.TryParse(Console.ReadLine(), out column) || column < 0 || column > 6 || !DropDisc(column))
                    {
                        Console.WriteLine("Invalid input. Please choose a valid column (0-6):");
                    }
                }

                if (CheckForWinner())
                {
                    gameWon = true;
                    PrintBoard();
                    Console.WriteLine($"Player {currentPlayer} wins!");
                }
                else
                {
                    currentPlayer = (currentPlayer == 'R') ? 'Y' : 'R';
                }
            }

            if (!gameWon)
            {
                PrintBoard();
                Console.WriteLine("It's a draw!");
            }

            Console.WriteLine("Do you want to play again? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                Main(args); // Restart the game
            }
        }

        static void InitializeGame()
        {
            Console.WriteLine("Welcome to Connect Four!");
            Console.WriteLine("Choose game mode: 1 for Single Player, 2 for Two Players");
            int mode;
            while (!int.TryParse(Console.ReadLine(), out mode) || (mode != 1 && mode != 2))
            {
                Console.WriteLine("Invalid input. Please choose 1 for Single Player or 2 for Two Players:");
            }

            isSinglePlayer = (mode == 1);

            Console.Write("Enter Player 1 name: ");
            player1Name = Console.ReadLine();
            player2Name = isSinglePlayer ? "AI" : GetPlayerName();

            Console.WriteLine($"Player 1: {player1Name} (R)");
            Console.WriteLine($"Player 2: {player2Name} (Y)");
        }

        static string GetPlayerName()
        {
            Console.Write("Enter Player 2 name: ");
            return Console.ReadLine();
        }

        static void InitializeBoard()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        static void PrintBoard()
        {
            for (int j = 5; j >= 0; j--)
            {
                for (int i = 0; i < 7; i++)
                {
                    Console.Write($"| {board[i, j]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----------------------------");
            Console.WriteLine("  0   1   2   3   4   5   6  ");
        }

        static bool DropDisc(int column)
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

        static int GetAIMove()
        {
            int column;
            do
            {
                column = random.Next(0, 7);
            } while (!DropDisc(column));
            return column;
        }

        static bool CheckForWinner()
        {
            // Check horizontal
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (board[i, j] == currentPlayer && board[i + 1, j] == currentPlayer &&
                        board[i + 2, j] == currentPlayer && board[i + 3, j] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            // Check vertical
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == currentPlayer && board[i, j + 1] == currentPlayer &&
                        board[i, j + 2] == currentPlayer && board[i, j + 3] == currentPlayer)
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

        static bool IsBoardFull()
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
}
