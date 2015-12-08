using ExesAndOhhs.Bots;
using NUnit.Framework;

namespace ExesAndOhhs.Tests
{
    [TestFixture]
    public class LearningAiTests
    {
        [Test]
        public void SimulateGames_RunsGamesAskedFor()
        {
            var bender = new Ai();
            var calculon = new Ai();
            var runner = new ExesAndOhhsGame(bender, calculon);

            var games = runner.SimulateGames(1);

            Assert.That(games.Count, Is.EqualTo(1));
        }
    }
}