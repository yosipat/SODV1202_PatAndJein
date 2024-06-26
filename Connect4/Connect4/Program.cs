using System;
using System.Data.Common;

namespace ConnectFour_PatAndJein
{
    abstract class Player
    {
        public string Name { get; set; }
        public char Symbol { get; set; }

        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public abstract int GetInput(Board board);
    }
    
    //Human Player
    class HumanPlayer : Player
    {
        public HumanPlayer(string name, char symbol) : base(name, symbol)
        {

        }

        public override int GetInput(Board board)
        {
            int c;
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Thanks for playing!");
                    Environment.Exit(0);
                }
                if (input.Length == 1 && int.TryParse(input, out c) && c >= 0 && c <= 6 && board.IsFullColumn(c))
                {
                    return c;
                }
                Console.WriteLine($"{Name}, enter a valid number of column (0-6):");
            }
        }
    }


    //AI Player
    class AIPlayer : Player
    {
        public AIPlayer(string name, char symbol): base(name, symbol)
        {

        }

        public override int GetInput(Board board)
        {
            int c;
            Random r = new Random();
            
            do
            {
                c = r.Next(0,7); // Generates a number between 0 and 6
            }
            while (!board.IsFullColumn(c)); 
            
            return c;
        }
    }

 
    //'7x6' grid game board
    class Board
    {
        private readonly char[,] board = new char[7, 6];

        public Board()
        {
            InitializeBoard();
        }
    //Initialize Board
        public void InitializeBoard() // define all cell empty
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        
        public bool IsFullColumn(int column) // check if the column has a empty cell
        {
            for (int row = 0; row < 6; row++)
            {
                if (board[column, row] == ' ')
                {
                    return true;
                }
            }
            return false;
        }

        public bool DropDisc(int column, char symbol) 
        {
            for (int row = 0; row < 6; row++)
            {
                if (board[column, row] == ' ') 
                {
                    board[column, row] = symbol; // set value on the cell that empty
                    return true;
                }
            }
            return false;
        }

        public bool IsFullBoard()
        {
            foreach (char cell in board)
            {
                if (cell == ' ') // check if there is empty cell.
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

        //Display the game header
      public static void DisplayHeader()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Connect4 Game Development : Final Project");
            Console.WriteLine();
            Console.WriteLine("Developed by");
            Console.WriteLine("Hyunjung Lim, Yosita Jasamut");
            Console.WriteLine();
            Console.WriteLine("SODV1202:Introduction to Object Oriented Programming-24MAYMNTR1");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();
        }

        public void PrintBoard(Player player1, Player player2)
        {
            DisplayHeader();

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

        public Connect4(Player p1, Player p2, bool isSinglePlayerMode)
        {
            player1 = p1;
            player2 = p2;
            currentPlayer = player1;
            board = new Board();
            singlePlayerMode = isSinglePlayerMode;
        }
    //Start Game
        public void StartGame()
        {
            board.InitializeBoard(); // set all cells empty
            board.PrintBoard(player1, player2);
            while (Play()) { }
        }

        private bool Play()
        {
            Console.WriteLine();
            Console.WriteLine($"----- Player {currentPlayer.Name}'s turn. ({currentPlayer.Symbol}) ----- Enter a number of column (0-6):");

            int c = currentPlayer.GetInput(board); // get column number from player's input

            board.DropDisc(c, currentPlayer.Symbol); // drop value that player select
            
            board.PrintBoard(player1, player2); // display board

                if (CheckWin()) // check win condition
                {
                    Console.WriteLine("Congratulations!");
                    Console.WriteLine($"{currentPlayer.Name} Wins!");
                    return false; // end game
                }

                if (board.IsFullBoard()) // check if the board is full
                {
                    Console.WriteLine("Game over! It's a draw!");
                    return false; // end game
                }

                SwitchPlayer();

            return true; // continue game
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1; // switch player
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
                Board.DisplayHeader();
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
                Player player1;
                Player player2;

                Console.Write("Enter Player 1 name: ");
                string player1Name = Console.ReadLine();
                string player2Name;

                player1 = new HumanPlayer(player1Name,'X'); // create humanplayer object for player1

                if (singlePlayerMode) // if it is 1player mode
                {
                    player2 = new AIPlayer("AI", 'O'); // create aiplayer object for player2
                }
                else
                {
                    Console.Write("Enter Player 2 name: ");
                    player2Name = Console.ReadLine();
                    player2 = new HumanPlayer(player2Name, 'O'); // create human player object for player2
                }

                // Create and start the game
                Connect4 game = new Connect4(player1, player2, singlePlayerMode);
                game.StartGame(); // start game loop

                // End game
                Console.Write("Do you want to play again? yes (1) / no(2) :");
                int response;
                while(true)
                {
                    try
                    {
                        response = int.Parse(Console.ReadLine());

                        if (response == 2)
                        {
                            Console.WriteLine("Thanks for playing!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                    catch(FormatException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Do you want to play again? yes (1) / no(2) :");
                    }
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
