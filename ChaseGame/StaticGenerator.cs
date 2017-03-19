using System;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public class StaticGenerator : IGenerator
    {
        private IGameBoard _gameBoard;

        private bool GameFieldShouldBeGrass(int x, int y)
        {
            return (x % 2 != 0) && (y % 2 != 0);
        }

        IGameBoard IGenerator.GenerateGameBoard(int boardSizeX, int boardSizeY)
        {
            _gameBoard = new GameBoard(boardSizeX, boardSizeY);
            for (int x = 0; x < _gameBoard.LengthX; x++)
            {
                for (int y = 0; y < _gameBoard.LengthY; y++)
                {
                    GameFieldType type = GameFieldShouldBeGrass(x, y) ? GameFieldType.Grass : GameFieldType.Road;
                    _gameBoard[x, y] = new GameField(type);
                }
            }
            return _gameBoard;
        }
    }
}
