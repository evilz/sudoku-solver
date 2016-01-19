using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Solvers;

namespace Sudoku
{
    public static class SudokuGenerator
    {
        private static ISudokuSolver _solverGenerator;

        public static SudokuBoard NewSudoku(SudokuDifficulty difficulty, ISudokuSolver solverGenerator = null)
        {
            _solverGenerator = solverGenerator ?? new DeepFirstSearchSolver();

            var blankboard = new SudokuBoard(difficulty);
            return _solverGenerator
                    .Solve(blankboard)
                    .CreateBlankCells();
        }
        
        public static SudokuBoard CreateBlankCells(this SudokuBoard sudokuBoard)
        {
            var clone = sudokuBoard.Clone();
            Random ran = new Random();
            
            var cellToClean = (int)sudokuBoard.Difficulty * 5;

            while (clone.EmptyCells.Count() < cellToClean)
            {
                var row = ran.Next(0, SudokuBoard.SIZE);
                var col = ran.Next(0, SudokuBoard.SIZE);
                if (clone.IsCellEmpty(row, col)) continue;

                var oldval = clone[row, col];
                clone[row, col] = Cell.EmptyValue;

                var possible = Enumerable.Range(1, SudokuBoard.SIZE).Where(x => clone.IsValid(new Cell(row, col, x)));

                if (possible.Count() != 1)
                {
                    clone[row, col] = oldval;
                }
            }

            return clone;
        }

    }
}