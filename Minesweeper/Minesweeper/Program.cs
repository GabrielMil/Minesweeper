using System;

namespace Minesweeper
{
    class Program
    {
        public const int GUESS = 1;
        public const int SIGN = 2;
        public const int UNSIGN = 3;
        static void Main(string[] args)
        {
            // Get the size of the board
            Console.Write("Enter the size of the board: ");
            int size = int.Parse(Console.ReadLine());
            // Get the amount of bombs on the board
            Console.Write("Enter the amount of the bombs: ");
            int bombs = int.Parse(Console.ReadLine());
            // Create the game board
            GameBoard gb = new GameBoard(size, bombs);
            int choice;
            // Start prints
            Console.WriteLine("Welcome to the game!");
            Console.WriteLine(gb);
            // While the game continues keep playing
            while (gb.IsContinue())
            {
                // Get from the user the choice
                choice = Menu();
                switch (choice)
                {
                    case GUESS:
                        //gb.EasyOpen(GetRow(), GetCol());
                        gb.ChallengeOpen(GetRow(gb), GetCol(gb));
                        break;
                    case SIGN:
                        gb.MakeSigned(GetRow(gb), GetCol(gb));
                        break;
                    case UNSIGN:
                        gb.MakeUnSigned(GetRow(gb), GetCol(gb));
                        break;
                    default:
                        // Its not possible to come here so: HOW?!
                        Console.WriteLine("HOW?");
                        break;
                }
                // After every move print the game board
                Console.WriteLine(gb);
            }
            Console.ReadLine();
        }

        // This function builds
        public static int Menu()
        {
            int choice;
            do
            {
                Console.WriteLine("1 - Guess");
                Console.WriteLine("2 - Sign block");
                Console.WriteLine("3 - Unsign block");
                choice = int.Parse(Console.ReadLine());
            } while (choice < 1 || choice > 3); // Check that the option that chosen is one of the options
            return choice;
        }

        // This function gets the row number
        public static int GetRow(GameBoard gb)
        {
            int row;
            do
            {
                Console.Write("Enter row(1-{0}): ", gb.GetSize());
                row = int.Parse(Console.ReadLine()) - 1;
            } while (row < 0 || row > gb.GetSize() - 1); // Check that the row is in bounds
            return row;
        }

        // This function gets the collom number
        public static int GetCol(GameBoard gb)
        {
            int col;
            do
            {
                Console.Write("Enter collom(1-{0}): ", gb.GetSize());
                col = int.Parse(Console.ReadLine()) - 1;
            } while (col < 0 || col > gb.GetSize() - 1); // Check that the row is in bounds
            return col;
        }
    }
}
