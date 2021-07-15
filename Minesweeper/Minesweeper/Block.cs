using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Block
    {
        // Class features:
        // value - bomb/number
        // isVisible
        private char value;
        private bool isVisible;
        private bool isSigned;

        // This is a building function
        public Block(char value)
        {
            this.value = value;
            this.isVisible = false;
        }

        // Returns the value of the block
        public char GetValue()
        {
            return this.value;
        }

        // This function changes the Block to visible
        public void MakeVisible()
        {
            this.isSigned = false;// If opened than remove sign
            this.isVisible = true;
        }

        // This funtion returns if the block is visible
        public bool IsVisible()
        {
            return this.isVisible;
        }

        //This changes the block to signed
        public void Sign()
        {
            this.isSigned = true;
        }

        //This changes the block to unsigned
        public void UnSign()
        {
            this.isSigned = false;
        }

        // This tells if the block is signed or not
        public bool IsSigned()
        {
            return this.isSigned;
        }

        // This function returns the string depending if the block is visible
        public char Show()
        {
            char chr;
            // If opened
            if (this.isVisible)
            {
                chr = this.value;
            }
            // If signed
            else if (this.isSigned)
            {
                chr = '?';
            }
            // if not signed and not opened
            else
            {
                chr = ' ';
            }
            return chr;
        }
    }
}
