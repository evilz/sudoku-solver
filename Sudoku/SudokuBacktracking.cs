using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class SudokuBacktracking
    {
        public SudokuBoard SudokuBoard;
        
        void createModel(SudokuBoard sudokuBoard)
        {
            SudokuBoard = sudokuBoard;
        }
        

        /** creates representation of Sudoku grid */

        public SudokuBoard init(SudokuBoard sudokuBoard)
        {
            createModel(sudokuBoard);
            // updateView() ;
            return SudokuBoard;
        }
        

        /** Checks if num is an acceptable value for the given row */

        protected bool checkRow(int row, int num)
        {
            for (int col = 0; col < 9; col++)
                if (SudokuBoard[row, col] == num)
                    return false;

            return true;
        }

        /** Checks if num is an acceptable value for the given column */

        protected bool checkCol(int col, int num)
        {
            for (int row = 0; row < 9; row++)
                if (SudokuBoard[row, col] == num)
                    return false;

            return true;
        }

        /** Checks if num is an acceptable value for the box around row and col */

        protected bool checkBox(int row, int col, int num)
        {
            row = (row/3)*3;
            col = (col/3)*3;

            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (SudokuBoard[row + r, col + c] == num)
                        return false;

            return true;
        }


        public void run()
        {
            try
            {
                //  updateView();

                // Start to Solve the puzzle in the left upper corner
                solve(0, 0);
            }
            catch (Exception e)
            {
            }
        }

        public int ctr = 0;
        /** Recursive function to find a valid Number for one single cell */

        public void solve(int row, int col)
        {
            // Throw an exception to stop the process if the puzzle is solved
            if (row > 8)
            {
                //Console.WriteLine("\nSolution found!");
                //updateView(); //------ FINAL SOLUTION IF YOU WANT TO PRINT
                throw new Exception("Solution found!");
            }

            ctr += 1;



            // If cell is not empty, continue with Next cell
            if (SudokuBoard[row, col] != 0)
                next(row, col);
            else
            {
                List<int> ints = new List<int>();
                for (int i = 1; i <= 9; i++)
                {
                    ints.Add(i);
                }
                var rng = new Random();
                ints = ints.OrderBy(a => rng.Next()).ToList();

                // Find valid Number for the empty cell
                for (int num = 1; num < 10; num++)
                {
                    int x = ints[num - 1];
                    if (checkRow(row, x) && checkCol(col, x) && checkBox(row, col, x))
                    {
                        SudokuBoard[row, col] = x;
                        //  updateView() ;

                        // Delegate work on the Next cell to a recursive call
                        next(row, col);
                    }
                }

                // No valid Number found, erase and return to caller
                SudokuBoard[row, col] = 0;
                // updateView() ;
                // Console.WriteLine();

            }
        }

        /** Calls Solve for the Next cell */

        public void next(int row, int col)
        {
            if (col < 8)
                solve(row, col + 1);
            else
                solve(row + 1, 0);
        }
    }
}