using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solvers
{
    class DeepFirstSearchSolver : ISudokuSolver
    {
        public SudokuBoard Solve(SudokuBoard sourceBoard)
        {
            var visited = new Stack<Cell>();
            var toVisit = new Stack<Cell>();

            var start = new Random().Next(1, 10);
            toVisit.Push(new Cell(0, 0, start));

            while (toVisit.Any())
            {
                var current = toVisit.Pop();
                visited.Push(current);

                if (current.Row == SudokuBoard.SIZE - 1 && current.Col == SudokuBoard.SIZE - 1)
                {
                    return new SudokuBoard(visited);
                }

                current = current.Col < SudokuBoard.SIZE - 1
                    ? new Cell(current.Row, current.Col + 1)
                    : new Cell(current.Row + 1, 0);

                var sudoku = new SudokuBoard(visited);
                var neighbours = Enumerable.Range(1, 9)
                                    .Shuffle()
                                    .Where(x => sudoku.IsValid(new Cell(current.Row, current.Col, x)))
                                    .Select(x => new Cell(current.Row, current.Col, x))
                                    .ToList();

                // Backtracking part
                if (!neighbours.Any())
                {
                    if (!toVisit.Any()) { break; }

                    var next = toVisit.Peek();
                    while (visited.Any(cell => cell.Row == next.Row && cell.Col == next.Col))
                    {
                        visited.Pop();
                    }
                }
                // Classic DFS
                else
                {
                    neighbours.ForEach(toVisit.Push);
                }
            }

            return new SudokuBoard();
        }
    }


}
