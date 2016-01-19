using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Sudoku
{
    public class SudokuBoard : IEnumerable<Cell>
    {
        public const int SIZE = 9;
        private readonly int[][] _grid;

        public SudokuDifficulty Difficulty { get;  }

        public SudokuBoard(SudokuDifficulty difficulty = SudokuDifficulty.None)
        {
            Difficulty = difficulty;
            _grid = Enumerable.Range(0, SIZE)
                        .Select(_ => new int[SIZE])
                        .ToArray();
        }
        
        public SudokuBoard(IEnumerable<Cell> cells, SudokuDifficulty difficulty = SudokuDifficulty.None) : this(difficulty)
        {
            foreach (var cell in cells)
                this[cell.Row, cell.Col] = cell.Val;
        }

        public SudokuBoard(IEnumerable<int> values, SudokuDifficulty difficulty = SudokuDifficulty.None) 
            : this(GetCellsFromValues(values),difficulty) {}

        private static IEnumerable<Cell> GetCellsFromValues(IEnumerable<int> values)
        {
            var val = values.ToArray();
            var i = 0;

            var cells = from row in Enumerable.Range(0, SIZE)
                from col in Enumerable.Range(0, SIZE)
                select new Cell(row, col, val[i++]);
            return cells;
        }
        
        public IEnumerable<Cell> EmptyCells => this.Where(cell => cell.IsEmpty);

        public IEnumerable<int> AllValues => Enumerable.Range(0, SIZE);  
        
        public bool IsCellEmpty(int row, int col)
        {
            return new Cell(row,col, this[row, col]).IsEmpty;
        }

        public bool IsCompleted => this.All(IsValid);

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

        public int[] this[int row] => _grid[row];

        public SudokuBoard Clone()
        {
            return new SudokuBoard(this, Difficulty);
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

        public bool IsValid(Cell cell)
        {
            var tmp = this[cell.Row][cell.Col];
            this[cell.Row][cell.Col] = 0;
            var isValid = CheckValueRange(cell.Val) && CheckRow(cell.Row, cell.Val) && CheckCol(cell.Col, cell.Val) && CheckBox(cell.Row, cell.Col, cell.Val);
            this[cell.Row][cell.Col] = tmp;
            return isValid;
        }

        private bool CheckValueRange(int val)
        {
            return val > 0 && val <= SIZE;
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
