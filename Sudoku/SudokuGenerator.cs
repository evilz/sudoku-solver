using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public static class SudokuGenerator
    {
        public static SudokuBoard NewSudoku(SudokuDifficulty difficulty)
        {
            return GenerateFullGrid()
                    .CreateBlankCells(difficulty);
        }
        
        // DFS implementation
        private static SudokuBoard GenerateFullGrid()
        {
            var visited = new Stack<Cell>();
            var toVisit = new Stack<Cell>();

            var start = new Random().Next(1, 10);
            toVisit.Push(new Cell(0, 0, start));

            while (toVisit.Any())
            {
                var current = toVisit.Pop();
                visited.Push(current);
                if (current.Row == 8 && current.Col == 8)
                {
                    return new SudokuBoard(visited);
                }

                current = current.Col < 8
                    ? new Cell(current.Row, current.Col + 1)
                    : new Cell(current.Row + 1, 0);

                var sudoku = new SudokuBoard(visited);
                var neighbours = Enumerable.Range(1, 9)
                                    .Shuffle()
                                    .Where(x => sudoku.IsValid(current.Row, current.Col, x))
                                    .Select(x => new Cell(current.Row, current.Col, x))
                                    .ToList();
                
                if (!neighbours.Any())
                {
                    if (!toVisit.Any()) { break; }

                    var next = toVisit.Peek();
                    while (visited.Any(cell => cell.Row == next.Row && cell.Col == next.Col))
                    {
                        visited.Pop();
                    }
                }
                else
                {
                    neighbours.ForEach(toVisit.Push);
                }
            }

            return new SudokuBoard();
        }
        
        public static SudokuBoard CreateBlankCells(this SudokuBoard sudokuBoard, SudokuDifficulty difficulty)
        {
            var clone = sudokuBoard.Clone();
            Random ran = new Random();
            
            var cellToClean = (int)difficulty * 5;

            while (clone.EmptyCells.Count() < cellToClean)
            {
                var row = ran.Next(0, 9);
                var col = ran.Next(0, 9);
                if (clone.IsCellEmpty(row, col)) continue;

                var oldval = clone[row, col];
                clone[row, col] = Cell.EmptyValue;

                var possible = Enumerable.Range(1, 9).Where(x => clone.IsValid(row, col, x));

                if (possible.Count() != 1)
                {
                    clone[row, col] = oldval;
                }
            }

            return clone;
        }

    }
}