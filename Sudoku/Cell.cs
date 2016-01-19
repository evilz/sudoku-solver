using System.Diagnostics;

namespace Sudoku
{
    [DebuggerDisplay("Cell : {Row},{Col} = {Val}")]
    public struct Cell
    {
        public const int EmptyValue = 0;

        public Cell(int row, int col, int val = EmptyValue)
        {
            Row = row;
            Col = col;
            Val = val;
        }

        public int Row { get; } 
        public int Col { get; } 
        public int Val { get; }

        public bool IsEmpty => Val == EmptyValue;

        public override bool Equals(object obj)
        {
            return Equals((Cell) obj);
        }

        private bool Equals(Cell other)
        {
            return Row == other.Row && Col == other.Col && Val == other.Val;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Row;
                hashCode = (hashCode*397) ^ Col;
                hashCode = (hashCode*397) ^ Val;
                return hashCode;
            }
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !left.Equals(right);
        }
    }
}