using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // int model[][] ;
            var input = Console.In;
            while (true)
            {
                Console.WriteLine("Would you like to: (Enter 1 or 2) \n1. Generate a Sudoku puzzle \n2. Solve a Sudoku Puzzle \n3. Exit");

                //If selects 1 ---> AFter generate sudoku puzzle, ask if you want to solve it.
                // If selects 2 --> Ask if wanna generate random puzzle first or input their own puzzle

                int x = int.Parse(input.ReadLine());

                if (x == 3) { break; }
                // Sudoku s0 = new Sudoku();
                SudokuGen s1 = new SudokuGen();
                SudokuBacktracking s2 = new SudokuBacktracking();
                SimulatedAnnealingSolver s3 = new SimulatedAnnealingSolver();

                if (x == 1)
                { // Generate
                    Console.WriteLine("\nSelect difficulty level? \n1. Easy \n2. Medium \n3. Hard");
                    int y = int.Parse(input.ReadLine());
                    if (y == 1) { s1.number = 3; }
                    if (y == 2) { s1.number = 5; }
                    if (y == 3) { s1.number = 7; }
                    s1.init();
                    s1.run();
                    // Console.WriteLine("Number of times grid-blank-assignment was done: " + s1.ctr);

                    Console.WriteLine("\nWould you like to solve this puzzle too? (Enter 1, 2 or 3) \n1.Yes, using Backtracking \n2.Yes, using Simulated Annealing \n3. No");
                    int x1 = int.Parse(input.ReadLine());
                    if (x1 == 1)
                    {
                        s2.init(s1.model); s2.run(); Console.WriteLine("\nSolution found. "); s2.updateView(); Console.WriteLine("\nOriginal puzzle was:");
                        for (int row = 0; row < 9; row++)
                        {
                            Console.WriteLine();
                            if (row == 0) { Console.WriteLine("\n -----------------------"); }
                            for (int col = 0; col < 9; col++)
                                if (s1.model[row,col] != 0)
                                {
                                    if (col == 0) { Console.Write("| "); }
                                    Console.Write(s1.model[row,col] + " ");
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
                    if (x1 == 2)
                    {
                        int[,] temp = new int[9,9];
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                temp[i,j] = s1.model[i,j];
                            }
                        }
                        s3.SimulatedAnnealingSolve(temp); Console.WriteLine("\nOriginal puzzle was:");
                        for (int row = 0; row < 9; row++)
                        {
                            Console.WriteLine();
                            if (row == 0) { Console.WriteLine("\n -----------------------"); }
                            for (int col = 0; col < 9; col++)
                                if (s1.model[row,col] != 0)
                                {
                                    if (col == 0) { Console.Write("| "); }
                                    Console.Write(s1.model[row,col] + " ");
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
                    Console.WriteLine("\nWould you like to: (Enter 1 or 2) \n1. Input a Sudoku puzzle to solve \n2. Generate a Sudoku puzzle to solve");
                    int y = int.Parse(input.ReadLine());

                    if (y == 1)
                    { //take sudoku input to solve
                        var temp = new int[9,9];
                        Console.WriteLine("\nEnter rows 1 to 9. Represent blanks as 0s.");
                        String a;
                        for (int i = 0; i < 9; i++)
                        {
                            Console.WriteLine("Enter row " + (i + 1) + ": ");
                            a =input.ReadLine();
                            for (int j = 0; j < 9; j++)
                            {
                                temp[i,j] = a[j] - '0'; //TEST THIS INPUT METHOD!!!! ~~~~~~~~~~~~~~~~~~~~~~
                            }
                        }

                        s2.init(temp); s2.run(); Console.WriteLine("\nSolution found. "); s2.updateView(); Console.WriteLine("\nOriginal puzzle was:");
                        for (int row = 0; row < 9; row++)
                        {
                            Console.WriteLine();
                            if (row == 0) { Console.WriteLine("\n -----------------------"); }
                            for (int col = 0; col < 9; col++)
                                if (temp[row,col] != 0)
                                {
                                    if (col == 0) { Console.Write("| "); }
                                    Console.Write(temp[row,col] + " ");
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

                    else { // that is, y==2, ie. Generate puzzle to solve

                        s1.init();
                        s1.run();

                        Console.WriteLine("\nWould you like to solve this puzzle using: (Enter 1 or 2) \n1. Backtracking \n2. Simulated Annealing");
                        int x1 = int.Parse(input.ReadLine());
                        if (x1 == 1)
                        {
                            s2.init(s1.model); s2.run();
                        }
                        else {
                            s3.SimulatedAnnealingSolve(s1.model);
                        }


                    }


                }


            }
        }
    }
}
