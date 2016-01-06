using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class SimulatedAnnealingSolver
    {
        bool[] squaresUnchangable;

        public List<int> initNumbers(List<int> numbers)
        {
            numbers.Clear();
            for (int i = 1; i <= 9; i++)
                numbers.Add(i);

            return numbers;
        }

        //for input board, empty squares are = 0
        public void SimulatedAnnealingSolve(Sudoku sudoku)
        {
            squaresUnchangable = new bool[81];

            //determine which squares' values cannot be changed
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i,j] != 0)
                        squaresUnchangable[i * 9 + j] = true;
                }
            }

            //for every 3x3 square, calculate numbers remaining and fill them in randomly
            List<int> numbers = new List<int>();
            numbers = initNumbers(numbers);

            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.Remove(sudoku[row,col]);
                }
            
            numbers = numbers.Shuffle().ToList();
            
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 0; row < 3; row++)
                for (int col = 3; col < 6; col++)
                {
                    if (sudoku[row,col] != 0) { }
                        numbers.RemoveAt(sudoku[row,col]);
                }

            numbers = numbers.Shuffle().ToList();
            for (int row = 0; row < 3; row++)
                for (int col = 3; col < 6; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 0; row < 3; row++)
                for (int col = 6; col < 9; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.RemoveAt(sudoku[row,col]);
                }

            numbers = numbers.Shuffle().ToList();
            for (int row = 0; row < 3; row++)
                for (int col = 6; col < 9; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 3; row < 6; row++)
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.RemoveAt(sudoku[row,col]);
                }

            numbers = numbers.Shuffle().ToList();
            for (int row = 3; row < 6; row++)
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 3; row < 6; row++)
                for (int col = 3; col < 6; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.RemoveAt(sudoku[row,col]);
                }
            numbers = numbers.Shuffle().ToList();
            for (int row = 3; row < 6; row++)
                for (int col = 3; col < 6; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 3; row < 6; row++)
                for (int col = 6; col < 9; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.RemoveAt(sudoku[row,col]);
                }
            numbers = numbers.Shuffle().ToList();
            for (int row = 3; row < 6; row++)
                for (int col = 6; col < 9; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 6; row < 9; row++)
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.RemoveAt(sudoku[row,col]);
                }

            numbers = numbers.Shuffle().ToList();
            for (int row = 6; row < 9; row++)
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 6; row < 9; row++)
                for (int col = 3; col < 6; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.RemoveAt(sudoku[row,col]);
                }
            numbers = numbers.Shuffle().ToList();
            for (int row = 6; row < 9; row++)
                for (int col = 3; col < 6; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }

            numbers = initNumbers(numbers);

            for (int row = 6; row < 9; row++)
                for (int col = 6; col < 9; col++)
                {
                    if (sudoku[row,col] != 0)
                        numbers.RemoveAt(sudoku[row,col]);
                }
            numbers = initNumbers(numbers);
            for (int row = 6; row < 9; row++)
                for (int col = 6; col < 9; col++)
                {
                    if (sudoku[row,col] == 0)
                    {
                        sudoku[row,col] = numbers[0];
                        numbers.RemoveAt(0);
                    }
                }
            /*
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Console.Write(board[i][j] + " ");
                }
                Console.WriteLine();
            }
            */
            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine();
                if (row == 0) { Console.WriteLine("\n -----------------------"); }
                for (int col = 0; col < 9; col++)
                    if (sudoku[row,col] != 0)
                    {
                        if (col == 0) { Console.Write("| "); }
                        Console.Write(sudoku[row,col] + " ");
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


            recurseSolve(sudoku, .8, 0);
        }

        public int numConflicts(Sudoku board)
        {
            int num = 0;
            Dictionary<int, int> numbers = new Dictionary<int, int>();

            //count thru rows
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!numbers.ContainsKey(board[i,j]))
                        numbers.Add(board[i,j], 1);
                    else
                        numbers.Add(board[i,j], numbers[board[i,j]] + 1);
                }

                for (int j = 1; j <= 9; j++)
                {
                    if (numbers.ContainsKey(j) && numbers[j] > 1)
                    {
                        num += numbers[j] - 1;
                    }
                    numbers.Add(j, -1); //reset map for Next row
                }
            }
            //count thru columns
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    if (!numbers.ContainsKey(board[row,col]))
                        numbers.Add(board[row,col], 1);
                    else
                        numbers.Add(board[row,col], numbers[board[row,col]] + 1);
                }

                for (int j = 1; j <= 9; j++)
                {
                    if (numbers.ContainsKey(j)  && numbers[j] > 1)
                    {
                        num += numbers[j] - 1;
                    }
                    numbers.Add(j, -1); //reset map for Next column
                }
            }
            return num;
        }

        // cooling constant = .7
        public Sudoku recurseSolve(Sudoku board, double temperature, int iteration)
        {
            int initConflicts = numConflicts(board);
            var rng = new Random();
            int square = (int)(rng.NextDouble() * 9);
            int xOffset = 0;
            int yOffset = 0;

            if (initConflicts == 0)
            {
                Console.WriteLine("Solution found!");
                return board;
            }

            switch (square)
            {
                case 0:
                    xOffset = 0;
                    yOffset = 0;
                    break;
                case 1:
                    xOffset = 0;
                    yOffset = 3;
                    break;
                case 2:
                    xOffset = 0;
                    yOffset = 6;
                    break;
                case 3:
                    xOffset = 3;
                    yOffset = 0;
                    break;
                case 4:
                    xOffset = 3;
                    yOffset = 3;
                    break;
                case 5:
                    xOffset = 3;
                    yOffset = 6;
                    break;
                case 6:
                    xOffset = 6;
                    yOffset = 0;
                    break;
                case 7:
                    xOffset = 6;
                    yOffset = 3;
                    break;
                case 8:
                    xOffset = 6;
                    yOffset = 6;
                    break;
            }

            int x1, y1, x2, y2;
            do
            {
                x1 = (int)(rng.NextDouble() * 3);
                y1 = (int)(rng.NextDouble() * 3);
                x2 = (int)(rng.NextDouble() * 3);
                y2 = (int)(rng.NextDouble() * 3);
            } while (squaresUnchangable[(xOffset + x1) * 9 + (yOffset + y1)] || squaresUnchangable[(xOffset + x2) * 9 + (yOffset + y2)]);

            Console.WriteLine("x1: " + (xOffset + x1) + " y1: " + (yOffset + y1));
            Console.WriteLine("x2: " + (xOffset + x2) + " y2: " + (yOffset + y2));
            Console.WriteLine("iteration Number: " + iteration);
            iteration++;

            var boardCandidate = board.Clone();
           
            boardCandidate[xOffset + x1,yOffset + y1] = board[xOffset + x2,yOffset + y2];
            boardCandidate[xOffset + x2,yOffset + y2] = board[xOffset + x1,yOffset + y1];

            int newConflicts = numConflicts(boardCandidate);

            if (newConflicts < initConflicts)
                board = boardCandidate.Clone();
            else
            {
                double probability = Math.Exp((initConflicts - newConflicts) / temperature);
                double random = rng.NextDouble();
                if (random <= probability)
                    board = boardCandidate.Clone();
            }
            /*
            for(int i = 0; i < 9; i++) //print out the new board
            {
                for(int j = 0; j < 9; j++)
                {
                    Console.Write(board[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            */
            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine();
                if (row == 0) { Console.WriteLine("\n -----------------------"); }
                for (int col = 0; col < 9; col++)
                    if (board[row,col] != 0)
                    {
                        if (col == 0) { Console.Write("| "); }
                        Console.Write(board[row,col] + " ");
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



            if (iteration > 4450) //added. 20000 before, 4450
                return board;

            double nextTemperature = updateTemp(temperature);
            return recurseSolve(board, nextTemperature, iteration);
        }

        public void multiArrayCopy(int[,] source, int[,] destination)
        {
            source.CopyTo(destination,0);
        }

        public double updateTemp(double temperature)
        {
            temperature *= .8; //or .7 or .3
            return temperature;
        }
        
    }
}