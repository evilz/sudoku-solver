using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public static class Extension
    {
        static readonly Random rng = new Random();
        
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(_ => rng.Next());
        }
    }
}
