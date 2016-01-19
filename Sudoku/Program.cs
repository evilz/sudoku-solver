using System;
using System.IO;
using System.Linq;
using Sudoku.Solvers;

namespace Sudoku
{
    static class Program
    {
        static readonly TextReader Input = Console.In;

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Would you like to: (Enter 1 or 2) \n1. Generate a Sudoku puzzle \n2. Solve a Sudoku Puzzle \n3. Exit");

                //If selects 1 ---> AFter generate sudoku puzzle, ask if you want to Solve it.
                // If selects 2 --> Ask if wanna generate random puzzle first or Input their own puzzle

                int x = int.Parse(Input.ReadLine());

                if (x == 3) { break; }

                var annealingSolver = new SimulatedAnnealingSolver();

                if (x == 1)
                {
                    // Generate

                    Console.WriteLine("\nSelect Difficulty level? \n1. Easy \n2. Medium \n3. Hard");
                    int y = int.Parse(Input.ReadLine());

                    SudokuDifficulty difficulty;
                    switch (y)
                    {
                        case 1: difficulty = SudokuDifficulty.Easy; break;
                        case 2: difficulty = SudokuDifficulty.Medium; break;
                        case 3: difficulty = SudokuDifficulty.Hard; break;

                        default: difficulty = SudokuDifficulty.Medium; break;
                    }

                    var sudoku = SudokuGenerator.NewSudoku(difficulty);
                    DisplaySudoku(sudoku);


                    Console.WriteLine("\nWould you like to Solve this puzzle too? (Enter 1, 2 or 3) \n1.Yes, using Backtracking \n2.Yes, using Simulated Annealing \n3. No");
                    var x1 = int.Parse(Input.ReadLine());
                    if (x1 == 1)
                    {
                        ISudokuSolver solver = new DeepFirstSearchSolver();
                        var solution = solver.Solve(sudoku);
                        var isOK = solution.IsCompleted;
                        Console.WriteLine("\nSolution found. ");
                        DisplaySudoku(solution);
                        Console.WriteLine("\nOriginal puzzle was:");
                        DisplaySudoku(sudoku);
                    }

                    if (x1 == 2)
                    {
                        var temp = sudoku.Clone();

                        annealingSolver.SimulatedAnnealingSolve(temp); Console.WriteLine("\nOriginal puzzle was:");
                        DisplaySudoku(sudoku);
                    }
                    else
                    {
                        Console.WriteLine("\nDone.");
                    }

                }

                else
                { //ie. x==2, that is, Solve
                    Console.WriteLine("\nWould you like to: (Enter 1 or 2) \n1. Input a Sudoku puzzle to Solve \n2. Generate a Sudoku puzzle to Solve");
                    int y = int.Parse(Input.ReadLine());

                    if (y == 1)
                    { //take sudoku Input to Solve
                        var temp = new SudokuBoard();
                        Console.WriteLine("\nEnter rows 1 to 9. Represent blanks as 0s.");
                        String a;
                        for (int i = 0; i < 9; i++)
                        {
                            Console.WriteLine("Enter row " + (i + 1) + ": ");
                            a = Input.ReadLine();
                            for (int j = 0; j < 9; j++)
                            {
                                temp[i, j] = a[j] - '0'; //TEST THIS INPUT METHOD!!!! ~~~~~~~~~~~~~~~~~~~~~~
                            }
                        }

                        var solver = new DeepFirstSearchSolver();
                        var solution = solver.Solve(temp);

                        Console.WriteLine("\nSolution found. ");
                        DisplaySudoku(solution);
                        Console.WriteLine("\nOriginal puzzle was:");

                        DisplaySudoku(temp);

                    }

                    else
                    { // that is, y==2, ie. Generate puzzle to Solve

                        var sudoku = SudokuGenerator.NewSudoku(SudokuDifficulty.Easy);

                        Console.WriteLine("\nWould you like to Solve this puzzle using: (Enter 1 or 2) \n1. Backtracking \n2. Simulated Annealing");
                        int x1 = int.Parse(Input.ReadLine());
                        if (x1 == 1)
                        {
                            var solver = new DeepFirstSearchSolver();
                            var solution = solver.Solve(sudoku);

                        }
                        else
                        {
                            annealingSolver.SimulatedAnnealingSolve(sudoku);
                        }
                    }
                }
            }
        }

        private static void DisplaySudoku(SudokuBoard sudokuBoard)
        {
            for (int i = 0; i < SudokuBoard.SIZE; i++)
            {
                if ((i) % 3 == 0)
                {
                    Console.WriteLine(" -----------------------");
                }

                var row = sudokuBoard[i];
                Console.Write("| ");
                DisplayNumber(row[0]);
                DisplayNumber(row[1]);
                DisplayNumber(row[2]);
                Console.Write("| ");
                DisplayNumber(row[3]);
                DisplayNumber(row[4]);
                DisplayNumber(row[5]);
                Console.Write("| ");
                DisplayNumber(row[6]);
                DisplayNumber(row[7]);
                DisplayNumber(row[8]);
                Console.WriteLine("|");

            }
            Console.WriteLine(" -----------------------");

        }

        private static void DisplayNumber(int num)
        {
            Console.ForegroundColor = (ConsoleColor)num;
            Console.Write(num + " ");
            Console.ResetColor();
        }
    }
}
