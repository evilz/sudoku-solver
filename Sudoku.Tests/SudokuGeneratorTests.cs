using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sudoku.Tests
{
    public class SudokuGeneratorTests
    {
       
        [Test]
        public void Should_return_completed_board_when_difficulty_is_none()
        {
            var board = SudokuGenerator.NewSudoku(SudokuDifficulty.None);
            Assert.That(board.EmptyCells.Count(), Is.EqualTo(0));
            Assert.That(board.IsCompleted, Is.True);
        }
        
    }
}
