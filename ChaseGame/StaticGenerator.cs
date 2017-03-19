using System;

namespace ChaseGameNamespace
{
    public class StaticGenerator : IGenerator
    {
        private IGameBoard _gameBoard;

        private bool GameFieldShouldBeGrass(int x, int y)
        {
            return (x % 2 != 0) && (y % 2 != 0);
        }

        IGameBoard IGenerator.GenerateGameBoard(int boardSizeX, int boardSizeY, Player[] players)
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

            Random random = new Random();
            for (int i = 0; i < players.Length; i++)
            {
                int x = random.Next(_gameBoard.LengthX);
                int y = random.Next(_gameBoard.LengthY);

                while ((_gameBoard[x, y].Type != GameFieldType.Road) || (_gameBoard[x, y].GetPlayer() != null))
                {
                    x = random.Next(_gameBoard.LengthX);
                    y = random.Next(_gameBoard.LengthY);
                }

                _gameBoard[x, y].SetPlayer(players[i]);
            }
            return _gameBoard;
        }
    }
}
