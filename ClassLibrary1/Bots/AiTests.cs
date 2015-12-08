using ExesAndOhhs.Game;
using NUnit.Framework;

namespace ExesAndOhhs.Bots
{
    [TestFixture]
    public class AiTests
    {
        private GameBoard _board;
        private Ai _bot;

        [SetUp]
        public void SetUp()
        {
            _board = new GameBoard();
            _bot = new Ai();
        }

        [Test]
        public void MakeChoice_PicksNumberInValidRange()
        {
            var choice = _bot.MakeSelection(_board);

            Assert.That(choice.X, Is.InRange(0, 2));
            Assert.That(choice.Y, Is.InRange(0, 2));
        }

    }
}
