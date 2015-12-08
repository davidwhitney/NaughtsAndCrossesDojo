using System;
using ExesAndOhhs.Game;

namespace ExesAndOhhs.Bots
{
    public class Ai : ITakeATurn
    {
        public char PlayerCharacter { get; set; }

        public Choice MakeSelection(GameBoard gameBoard)
        {
            return new Choice(0, 0);
        }

        public void GameCompleted(GameBoard gameBoard, ITakeATurn winner)
        {
        }

        public void InvalidTurnAttempted(GameBoard gameBoard, Choice choice)
        {
        }
    }
}