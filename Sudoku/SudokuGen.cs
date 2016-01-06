using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class SudokuGen
    {
        public Sudoku Sudoku;
        
        public Sudoku NewSudoku(SudokuDifficulty difficulty)
        {

            try
            {
                Sudoku = new Sudoku(difficulty);
                GenerateFullGrid();
            }
            catch (Exception e)
            {
                CreateBlankCells();
            }

            return Sudoku;

        }

        private void GenerateFullGrid(int row = 0, int col = 0)
        {
           var ints = Enumerable.Range(1, 9)
                    .Shuffle()
                    .ToArray()
                    .Where(x => Sudoku.IsValid(row, col, x));

                // Find valid number for the empty cell
                foreach (var x in ints)
                {
                    Sudoku[row, col] = x;

                if (row == 8 && col == 8 ) { throw new Exception("done"); } // <= must BE DFS !!!!!!! WTF

                if (col < 8)
                        GenerateFullGrid(row, col + 1);
                    else
                        GenerateFullGrid(row + 1);
                }

                // No valid number found, erase and return to caller
                Sudoku[row, col] = 0;
        }


        public void CreateBlankCells()
        {
            var temp = Sudoku.Clone();
            Random ran = new Random();

            // while erase cell is not ok
            // random cell where not blank
            // count number of possible ints
            // if more than one set it back
            // else set to à

            var cellToClean = (int) Sudoku.Difficulty*5;

            while (temp.EmptyCells.Count() < cellToClean)
            {
                var row = ran.Next(0, 9);
                var col = ran.Next(0, 9);
                if(temp.IsCellEmpty(row,col)) continue;

                var oldval = temp[row, col];
                temp[row, col] = Cell.EMPTY_VALUE;

                var possible = Enumerable.Range(1, 9).Where(x => temp.IsValid(row, col, x));

                if (possible.Count() != 1)
                {
                    temp[row, col] = oldval;
                }

            }
            Sudoku = temp;

            //var ints = Enumerable.Range(1, 9)
            //       .Shuffle()
            //       .ToArray()
            //       .Where(x => Sudoku.IsValid(row, col, x));

            ///* FOR EVERY BLOCK  , TAKE ONE OUT, REPEAT AS MANY NUMBER OF TIMES!!!!*/
            //Random ran = new Random();

            //var flag = 0;

            //while (flag == 0)
            //{
            //    var temp = temp1.Clone();

            //    /*Choose 'Number' as follows:
            //     * SET BY DEFAULT TO 6 at top
            //     * 
            //      for mid level: Number= 5 - 6
            //      for easy: 3-4 
            //      for hard: 6 to 8
            //     */

            //    for (int times = 0; times < (int)Difficulty; times++)
            //    {

            //        temp[ran.Next(0, 3), ran.Next(0, 3)] = 0;
            //        temp[ran.Next(0, 3), ran.Next(0, 3) + 3] = 0;
            //        temp[ran.Next(0, 3), ran.Next(0, 3) + 6] = 0;
            //        temp[ran.Next(0, 3) + 3, ran.Next(0, 3)] = 0;
            //        temp[ran.Next(0, 3) + 3, ran.Next(0, 3) + 3] = 0;
            //        temp[ran.Next(0, 3) + 3, ran.Next(0, 3) + 6] = 0;
            //        temp[ran.Next(0, 3) + 6, ran.Next(0, 3)] = 0;
            //        temp[ran.Next(0, 3) + 6, ran.Next(0, 3) + 3] = 0;
            //        temp[ran.Next(0, 3) + 6, ran.Next(0, 3) + 6] = 0;

            //    }
            //    SudokuBacktracking s0 = new SudokuBacktracking();
            //    SudokuBacktracking s1 = new SudokuBacktracking();
            //    SudokuBacktracking s2 = new SudokuBacktracking();

            //    s0.init(temp);
            //    s1.init(temp);
            //    s2.init(temp);

            //    s0.run();
            //    s1.run();
            //    s2.run();

            //    int c = 0;

            //    Console.WriteLine();

            //    for (int i = 0; i < 9; i++)
            //    {
            //        for (int j = 0; j < 9; j++)
            //        {
            //            if (s0.Sudoku[i, j] != s1.Sudoku[i, j]) { c = 1; break; }
            //        }
            //        if (c == 1) { break; }
            //    }
            //    if (c == 1)
            //    {
            //        flag = 0; // means that loop should repeat again since this grid-blank-assignment does not yield unique solution
            //    }
            //    else {

            //        flag = 1;

            //        for (int i = 0; i < 9; i++)
            //        {
            //            for (int j = 0; j < 9; j++)
            //            {
            //                if (s1.Sudoku[i, j] != s2.Sudoku[i, j]) { c = 1; break; }
            //            }
            //            if (c == 1) { break; }
            //        }
            //        if (c == 1) { flag = 0; }
            //        else {

            //            for (int i = 0; i < 9; i++)
            //            {
            //                for (int j = 0; j < 9; j++)
            //                {
            //                    //Console.Write(temp[i,j]+ " ");
            //                    Sudoku[i, j] = temp[i, j];
            //                }
            //                //Console.WriteLine();
            //            }
            //        }
            //    }


            //}

            Console.WriteLine("SUDOKU PUZZLE GENERATED: ");

            //return model;

            //throw new Exception("Solution found!");

        }

    }
}