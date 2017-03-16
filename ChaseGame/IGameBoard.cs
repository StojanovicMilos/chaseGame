using System.Collections.Generic;

namespace ChaseGameNamespace
{
    public interface IGameBoard
    {
        int LengthX { get; }
        int LengthY { get; }

        void Draw();
        GameField this[int x, int y]
        {
            get;
            set;
        }

        bool FieldIsType(int x, int y, GameFieldType type);
        bool TopNeighbourIsType(int x, int y, GameFieldType type);
        bool TopRightNeighbourIsType(int x, int y, GameFieldType type);
        bool RightNeighbourIsType(int x, int y, GameFieldType type);
        bool BottomRightNeighbourIsType(int x, int y, GameFieldType type);
        bool BottomNeighbourIsType(int x, int y, GameFieldType type);
        bool BottomLeftNeighbourIsType(int x, int y, GameFieldType type);
        bool LeftNeighbourIsType(int x, int y, GameFieldType type);
        bool TopLeftNeighbourIsType(int x, int y, GameFieldType type);

        bool ValidateGameBoard();

        int GetNumberOfNeighbourRoads(int x, int y);
        List<Coordinates> GetPossibleNewNeighbours(int x, int y);

        bool InvalidField(int x, int y);
        bool TopLeftInvalid(int x, int y);
        bool TopRightInvalid(int x, int y);
        bool BottomRightInvalid(int x, int y);
        bool BottomLeftInvalid(int x, int y);
    }
}