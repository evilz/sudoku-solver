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

            sourceBoard.Where(cell => !cell.IsEmpty)
                .ToList()
                .ForEach(cell => visited.Push(cell));

            var sudoku = new SudokuBoard(visited);
            var start = new Random().Next(1, 10);

            if (!sudoku.EmptyCells.Any()) return sudoku;

            var firstEmpty = sudoku.EmptyCells.First();

            toVisit.Push(new Cell(firstEmpty.Row, firstEmpty.Col, start));

            while (toVisit.Any())
            {
                var current = toVisit.Pop();
                visited.Push(current);

                sudoku = new SudokuBoard(visited);
                if (!sudoku.EmptyCells.Any()) return sudoku;

                current = sudoku.EmptyCells.First();

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

            return sudoku;
        }
    }


}
