using System;
using System.IO;

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
                // Sudoku s0 = new Sudoku();
                var generator = new SudokuGen();
                var backtracking = new SudokuBacktracking();
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
                    generator.NewSudoku(difficulty);
                    DisplaySudoku(generator.Sudoku);

                    Console.WriteLine("\nWould you like to Solve this puzzle too? (Enter 1, 2 or 3) \n1.Yes, using Backtracking \n2.Yes, using Simulated Annealing \n3. No");
                    var x1 = int.Parse(Input.ReadLine());
                    if (x1 == 1)
                    {
                        backtracking.init(generator.Sudoku);
                        backtracking.run();
                        Console.WriteLine("\nSolution found. ");
                        DisplaySudoku(backtracking.Sudoku);
                        Console.WriteLine("\nOriginal puzzle was:");

                        DisplaySudoku(generator.Sudoku);
                    }
                    if (x1 == 2)
                    {
                        var temp = generator.Sudoku.Clone();
                        
                        annealingSolver.SimulatedAnnealingSolve(temp); Console.WriteLine("\nOriginal puzzle was:");
                        for (int row = 0; row < 9; row++)
                        {
                            Console.WriteLine();
                            if (row == 0) { Console.WriteLine("\n -----------------------"); }
                            for (int col = 0; col < 9; col++)
                                if (generator.Sudoku[row, col] != 0)
                                {
                                    if (col == 0) { Console.Write("| "); }
                                    Console.Write(generator.Sudoku[row, col] + " ");
                                    if (col == 2 | col == 5 | col == 8) { Console.Write("| "); }
                                }
                                else {
                                    if (col == 0) { Console.Write("| "); }
                                    Console.Write("-" + " ");
                                    if (col == 2 | col == 5 | col == 8) { Console.Write("| "); }
                                }
                            if (row == 2 | row == 5 | row == 8) { Console.Write("\n -----------------------"); }
                        }
                        Console.WriteLine();
                    }
                    else {
                        Console.WriteLine("\nDone.");
                    }

                }

                else { //ie. x==2, that is, Solve
                    Console.WriteLine("\nWould you like to: (Enter 1 or 2) \n1. Input a Sudoku puzzle to Solve \n2. Generate a Sudoku puzzle to Solve");
                    int y = int.Parse(Input.ReadLine());

                    if (y == 1)
                    { //take sudoku Input to Solve
                        var temp = new Sudoku(SudokuDifficulty.Easy);
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

                        backtracking.init(temp); backtracking.run(); Console.WriteLine("\nSolution found. ");
                        DisplaySudoku(backtracking.Sudoku);
                        Console.WriteLine("\nOriginal puzzle was:");
                        for (int row = 0; row < 9; row++)
                        {
                            Console.WriteLine();
                            if (row == 0) { Console.WriteLine("\n -----------------------"); }
                            for (int col = 0; col < 9; col++)
                                if (temp[row, col] != 0)
                                {
                                    if (col == 0) { Console.Write("| "); }
                                    Console.Write(temp[row, col] + " ");
                                    if (col == 2 | col == 5 | col == 8) { Console.Write("| "); }
                                }
                                else {
                                    if (col == 0) { Console.Write("| "); }
                                    Console.Write("-" + " ");
                                    if (col == 2 | col == 5 | col == 8) { Console.Write("| "); }
                                }
                            if (row == 2 | row == 5 | row == 8) { Console.Write("\n -----------------------"); }
                        }
                        Console.WriteLine();
                    }

                    else { // that is, y==2, ie. Generate puzzle to Solve

                        var sudoku = generator.NewSudoku(SudokuDifficulty.Easy);

                        Console.WriteLine("\nWould you like to Solve this puzzle using: (Enter 1 or 2) \n1. Backtracking \n2. Simulated Annealing");
                        int x1 = int.Parse(Input.ReadLine());
                        if (x1 == 1)
                        {
                            backtracking.init(generator.Sudoku); backtracking.run();
                        }
                        else {
                            annealingSolver.SimulatedAnnealingSolve(generator.Sudoku);
                        }


                    }


                }


            }
        }

        private static void DisplaySudoku(Sudoku sudoku)
        {
            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine();
                if (row == 0) { Console.WriteLine("\n -----------------------"); }

                for (int col = 0; col < 9; col++)
                {
                    if (sudoku[row, col] != 0)
                    {
                        if (col == 0)
                        {
                            Console.Write("| ");
                        }
                        Console.Write(sudoku[row, col] + " ");
                        if (col == 2 | col == 5 | col == 8)
                        {
                            Console.Write("| ");
                        }
                    }
                    else
                    {
                        if (col == 0)
                        {
                            Console.Write("| ");
                        }
                        Console.Write("-" + " ");
                        if (col == 2 | col == 5 | col == 8)
                        {
                            Console.Write("| ");
                        }
                    }
                }
                if (row == 2 | row == 5 | row == 8)
                {
                    Console.Write("\n -----------------------");
                }
            }
            Console.WriteLine();
        }


    }
}
