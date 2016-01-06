using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Sudoku
{

    public struct Cell
    {
        public const int EMPTY_VALUE = 0;

        public Cell(int row, int col, int val)
        {
            Row = row;
            Col = col;
            Val = val;
        }

        public int Row { get; set; }
        public int Col { get; set; }
        public int Val { get; set; }

        public bool IsEmpty => Val == EMPTY_VALUE;
    }

    public class Sudoku : IEnumerable<Cell>
    {
        private const int SIZE = 9;
        private readonly int[][] _grid; 

        public SudokuDifficulty Difficulty { get; }

        public Sudoku(SudokuDifficulty difficulty)
        {
            Difficulty = difficulty;
            _grid = Enumerable.Range(0, SIZE).Select(_ => new int[SIZE]).ToArray();
        }

        public IEnumerable<Cell> EmptyCells
        {
            get { return this.Where(cell => cell.IsEmpty); }
        }
        
        public bool IsCellEmpty(int row, int col)
        {
            return new Cell(row,col, this[row, col]).IsEmpty;
        }

        public int[,] GetMatrix()
        {
            var g = new int[SIZE, SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    g[i, j] = _grid[i][j];
                }
            }

            return g;
        }
        
        public int this[int row,int col]
        {
            get { return _grid[row][col]; }
            set
            {
                _grid[row][col] = value < 0 || value > 9 
                    ? 0 
                    : value;
            }
        }
        
        public Sudoku Clone()
        {
           return this.Aggregate(new Sudoku(Difficulty), (sudoku, cell) =>
           {
               sudoku[cell.Row, cell.Col] = cell.Val;
               return sudoku;
           });
        }

        public bool IsValid(int row, int col, int num)
        {
            return CheckRow(row, num) && CheckCol(col, num) && CheckBox(row, col, num);
        }

        private bool CheckRow(int row, int num)
        {
            for (var col = 0; col < SIZE; col++)
                if (this[row, col] == num)
                    return false;

            return true;
        }

        private bool CheckCol(int col, int num)
        {
            for (int row = 0; row < SIZE; row++)
                if (this[row, col] == num)
                    return false;

            return true;
        }

        private bool CheckBox(int row, int col, int num)
        {
            row = (row / 3) * 3;
            col = (col / 3) * 3;

            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (this[row + r, col + c] == num)
                        return false;

            return true;
        }

        

        public IEnumerator<Cell> GetEnumerator()
        {
           return ( from row in Enumerable.Range(0, SIZE)
                    from col in Enumerable.Range(0, SIZE)
                    select new Cell {Row = row, Col = col, Val = this[row, col]}
                  ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
}
