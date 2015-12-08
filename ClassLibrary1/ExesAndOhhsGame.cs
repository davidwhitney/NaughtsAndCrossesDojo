using System;
using System.Collections.Generic;
using System.Linq;
using ExesAndOhhs.Game;

namespace ExesAndOhhs
{
    public class ExesAndOhhsGame
    {
        public List<ITakeATurn> Players { get; }
        public ITakeATurn PlayerOh { get { return Players.Single(x => x.PlayerCharacter == 'o'); } }
        public ITakeATurn PlayerEx { get { return Players.Single(x => x.PlayerCharacter == 'x'); } }

        public ExesAndOhhsGame(ITakeATurn playerOh, ITakeATurn playerEx)
        {
            playerOh.PlayerCharacter = 'o';
            playerEx.PlayerCharacter = 'x';

            Players = new List<ITakeATurn>
            {
                playerOh,
                playerEx
            };
        }

        public List<GameBoard> SimulateGames(int numberOfGames)
        {
            var games = new List<GameBoard>();
            for (int i = 0; i < numberOfGames; i++)
            {
                var game = PlaySingleGame();

                var winner = GetWinner(game);
                PlayerOh.GameCompleted(game, winner);
                PlayerEx.GameCompleted(game, winner);

                games.Add(game);
            }

            return games;
        }

        private GameBoard PlaySingleGame()
        {
            _currentPlayer = null;
            var game = new GameBoard();
            var iterations = 0;

            while (!game.GameWon || iterations != 1000)
            {
                var player = PickPlayer();
                var choice = player.MakeSelection(game);

                var turnAttempts = 0;
                bool validTurn;
                do
                {
                    validTurn = game.TakeTurn(_currentPlayer.Value, choice.X, choice.Y);
                    ProcessInvalidTurn(validTurn, player, game, choice, turnAttempts);
                    turnAttempts++;

                } while (!validTurn);

                iterations++;
            }
            return game;
        }

        private void ProcessInvalidTurn(bool validTurn, ITakeATurn player, GameBoard game, Choice choice, int turnAttempts)
        {
            if (validTurn)
            {
                return;
            }

            player.InvalidTurnAttempted(game, choice);
            
            if (turnAttempts == 500)
            {
                throw new Exception(string.Format("Ai {0} can't work out the rules - made 500 invalid turns in a row. Choice: {1}", player.PlayerCharacter, choice));
            }
        }

        private ITakeATurn GetWinner(GameBoard gameBoard)
        {
            ITakeATurn winner = null;
            if (gameBoard.Winner == 'x')
            {
                winner = PlayerEx;
            }
            if (gameBoard.Winner == 'o')
            {
                winner = PlayerOh;
            }
            return winner;
        }

        private char? _currentPlayer;
        private ITakeATurn PickPlayer()
        {
            var player = _currentPlayer.GetValueOrDefault('x') == 'x' ? PlayerOh : PlayerEx;
            _currentPlayer = player == PlayerOh ? 'o' : 'x';
            return player;
        }
    }
}
