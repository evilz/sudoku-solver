using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class SudokuBoard : IEnumerable<Cell>
    {
        private const int SIZE = 9;
        private readonly int[][] _grid;

        public SudokuDifficulty Difficulty { get; set; } = SudokuDifficulty.Easy;
        
        public SudokuBoard(IEnumerable<Cell> cells ) : this()
        {
            foreach (var cell in cells)
                this[cell.Row, cell.Col] = cell.Val;
        }

        public SudokuBoard()
        {
            _grid = Enumerable.Range(0, SIZE)
                        .Select(_ => new int[SIZE])
                        .ToArray();
        }

        public IEnumerable<Cell> EmptyCells
        {
            get { return this.Where(cell => cell.IsEmpty); }
        }
        
        public bool IsCellEmpty(int row, int col)
        {
            return new Cell(row,col, this[row, col]).IsEmpty;
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
        
        public SudokuBoard Clone()
        {
           return new SudokuBoard(this);
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return ( from row in Enumerable.Range(0, SIZE)
                from col in Enumerable.Range(0, SIZE)
                select new Cell(row, col, this[row, col])
                ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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


        // Debug stuff
        //public int[,] GetMatrix()
        //{
        //    var g = new int[SIZE, SIZE];
        //    for (int i = 0; i < SIZE; i++)
        //    {
        //        for (int j = 0; j < SIZE; j++)
        //        {
        //            g[i, j] = _grid[i][j];
        //        }
        //    }
        //    return g;
        //}
    }

}
