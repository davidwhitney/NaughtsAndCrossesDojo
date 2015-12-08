using ExesAndOhhs.Game;
using NUnit.Framework;

namespace ExesAndOhhs.Tests
{
    [TestFixture]
    public class GameRuleTests
    {
        [Test]
        public void TakeTurn_GivenValidBoard_ReturnsState()
        {
            var board = new GameBoard();

            board.TakeTurn('o', 0, 0);

            Assert.That(board.ToString().Trim(), Is.EqualTo(@"
---
---
o--".Trim()));
        }

        [Test]
        public void CanMove_GivenSquareAlreadyOccupied_ReturnsFalse()
        {
            var board = new GameBoard();
            board.TakeTurn('o', 0, 0);

            var result = board.CanMove('x', 0, 0);

            Assert.That(result, Is.False);
        }

        [Test]
        public void TakeTurn_MoveWinsGameDueToRow_GameStateIdentifiesWin()
        {
            var board = new GameBoard(@"
---
xx-
oo-");
  
            board.TakeTurn('o', 2, 0);

            Assert.That(board.GameWon, Is.True);
        }

        [Test]
        public void TakeTurn_MoveWinsGameDueToColumn_GameStateIdentifiesWin()
        {
            var board = new GameBoard(@"
ox-
ox-
---");

            board.TakeTurn('o', 0, 0);

            Assert.That(board.GameWon, Is.True);
        }

        [Test]
        public void TakeTurn_MoveWinsGameDueToDiagonal_GameStateIdentifiesWin()
        {
            var board = new GameBoard(@"
ox-
xo-
---");

            board.TakeTurn('o', 2, 0);

            Assert.That(board.GameWon, Is.True);
        }

        [Test]
        public void TakeTurn_MoveWinsGameDueToOtherDiagonal_GameStateIdentifiesWin()
        {
            var board = new GameBoard(@"
xx-
xo-
o--");

            board.TakeTurn('o', 2, 2);

            Assert.That(board.GameWon, Is.True);
        }

        [Test]
        public void CanMove_LastMoveIsANaughtGivenANaught_ReturnsFalse()
        {
            var board = new GameBoard();
            board.TakeTurn('o', 0, 0);

            var result = board.CanMove('o', 1, 0);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanMove_GivenSquareNotOccupied_ReturnsTrue()
        {
            var board = new GameBoard();

            var result = board.CanMove('o', 0, 0);

            Assert.That(result, Is.True);
        }
    }
}
