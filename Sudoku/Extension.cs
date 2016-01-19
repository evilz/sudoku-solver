using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sudoku
{
    public static class Extension
    {
        static readonly Random rng = new Random();
        
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(_ => rng.Next());
        }


        public static void Add(this List<Cell> list,int row,int col)
        {
            list.Add(new Cell(row,col));
        }
    }
}
