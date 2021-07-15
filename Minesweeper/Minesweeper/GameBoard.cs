using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class GameBoard
    {
        private Block[,] board;
        private int size;
        private int bombs;
        private int signed;

        // Building function
        public GameBoard(int size, int bombs)
        {
            this.size = size;
            this.board = new Block[size, size];
            this.bombs = bombs;
            this.signed = 0;
            SetGameBoard();
        }

        // This returns the size
        public int GetSize()
        {
            return this.size;
        }
        // This returns the amount of bombs
        public int GetBombs()
        {
            return this.bombs;
        }

        private void SetGameBoard()
        {
            Random rnd = new Random();
            int row;
            int col;
            int count = 0;
            // Place the bombs
            while (count < this.bombs)
            {
                // Calculate random places fo the bombs
                row = rnd.Next(0, size);
                col = rnd.Next(0, size);
                if (this.board[row, col] == null)
                {

                    this.board[row, col] = new Block('*');
                    count++;
                }
            }
            ////////////////////////////////
            // Place the other numbers
            for (row = 0; row < size; row++)
            {
                for (col = 0; col < size; col++)
                {
                    // Check if the place is free(not a mine)
                    if (this.board[row, col] == null)//FIXED: !=->==
                    {
                        //Console.WriteLine("PLACE NUMBER: {0},{1}",row,col);
                        // Place the calculated value
                        this.board[row, col] = new Block(Count(row, col));
                    }
                }
            }
        }

        // This function gets a cordinats and counts how many bombs are around it
        private char Count(int i, int j)
        {
            int count = 0;
            // Look around the current block
            // Look left to the block, in the same collom and right to it
            for (int x = i - 1; x <= i + 1; x++)
            {
                // Look top to the block, in the same row and right to it
                for (int y = j - 1; y <= j + 1; y++)
                {

                    if ((x >= 0 && x < size) && (y >= 0 && y < size))
                    {
                        // Skip the block we sent and if its not empty and if the block we checking is the mine block
                        if (!(x == i && y == j) && this.board[x, y] != null && this.board[x, y].GetValue() == '*')
                        {
                            count++;
                        }
                    }

                }

            }
            return (char)(count + '0');
        }

        // ToString function that prints the current state of the game
        public override string ToString()
        {
            // All here is for design
            string str = " ";
            for (int i = 0; i < size; i++)
            {
                str += "   " + (i + 1);
            }
            str += "\n  ";
            for (int i = 0; i < size; i++)
            {
                str += "----";
            }
            str += "-\n";
            for (int i = 0; i < size; i++)
            {
                str += String.Format("{0,2}|", i + 1);
                for (int j = 0; j < size; j++)
                {
                    str += " " + this.board[i, j].Show() + " |";
                }
                str += "\n  ";
                for (int k = 0; k < size; k++)
                {
                    str += "----";
                }
                str += "-\n";
            }
            str += "Total flags: " + this.signed;
            return str;
        }

        // This function gets a cordinats and opens the square
        public void EasyOpen(int row, int col)
        {
            this.board[row, col].MakeVisible();
            Console.WriteLine(this);
        }

        //This function checks if the game finished, if continue the game return true
        public bool IsContinue()
        {
            int count = 0;
            bool flag = false;
            int i, j;
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    // If we opened any bomb
                    if (this.board[i, j].IsVisible() && this.board[i, j].GetValue() == '*')
                    {
                        flag = true;
                        Console.WriteLine("You lost!");
                    }
                    // count how many "Normal" blocks we opened
                    else if (this.board[i, j].IsVisible())
                    {
                        count++;
                    }
                }
                // Check if all the NOT BOMB blocks opened
                if (count == this.size * this.size - this.bombs)
                {
                    flag = true;
                    Console.WriteLine("You won!");
                }
            }
            return !flag;
        }

        // This function gets a cordinats and opens the square
        public void ChallengeOpen(int row, int col)
        {
            // If out of bounds
            if ((row >= size || row < 0 || col >= size || col < 0))
            {
                return;
            }
            // If not zero and check if not opened
            else if (this.board[row, col].GetValue() != '0' && !this.board[row, col].IsVisible())
            {
                // Check if the block signed and if does then decrease by one 
                if (this.board[row, col].IsSigned())
                {
                    this.signed--;
                }
                this.board[row, col].MakeVisible();
                return;
            }
            // If zero AND not opened
            else if (!this.board[row, col].IsVisible())
            {
                // Check if the block signed and if does then decrease by one 
                if (this.board[row, col].IsSigned())
                {
                    this.signed--;
                }
                // Open the block
                this.board[row, col].MakeVisible();
                // Check by recursion all the blocks around the current block
                ChallengeOpen(row - 1, col - 1);
                ChallengeOpen(row - 1, col);
                ChallengeOpen(row - 1, col + 1);
                ChallengeOpen(row, col - 1);
                ChallengeOpen(row, col + 1);
                ChallengeOpen(row + 1, col - 1);
                ChallengeOpen(row + 1, col);
                ChallengeOpen(row + 1, col + 1);
            }
        }

        //This makes the block to signed with "?"
        public void MakeSigned(int row, int col)
        {
            // Sign if its not visible(opened) and still not signed
            if (!this.board[row, col].IsVisible() && !this.board[row, col].IsSigned())
            {
                this.board[row, col].Sign();
                this.signed++;
            }
        }

        //This remove the "?"
        public void MakeUnSigned(int row, int col)
        {
            // Unsign if it is signed and not opened
            if (!this.board[row, col].IsVisible() && this.board[row, col].IsSigned())
            {
                this.board[row, col].UnSign();
                this.signed--;
            }
        }
    }
}
