using ExesAndOhhs.Game;

namespace ExesAndOhhs
{
    public interface ITakeATurn
    {
        char PlayerCharacter { get; set; }
        Choice MakeSelection(GameBoard gameBoard);
        void GameCompleted(GameBoard gameBoard, ITakeATurn winner);
        void InvalidTurnAttempted(GameBoard gameBoard, Choice choice);
    }
}