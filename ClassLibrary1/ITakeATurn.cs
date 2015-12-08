using ExesAndOhhs.Game;

namespace ExesAndOhhs
{
    public interface ITakeATurn
    {
        Choice MakeSelection(GameBoard gameBoard);
        void GameCompleted(GameBoard gameBoard, ITakeATurn winner);
        void InvalidTurnAttempted(GameBoard gameBoard, Choice choice);
    }
}