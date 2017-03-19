using System;
using System.Windows.Forms;
using MoreLinq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ChaseGameNamespace
{
    public class GameBoard : IGameBoard
    {
        GameField[][] _gameBoard;

        public GameBoard(int x, int y)
        {
            _gameBoard = new GameField[x][];
            for (int i = 0; i < _gameBoard.Length; i++)
            {
                _gameBoard[i] = new GameField[y];
            }
        }

        public int LengthX => _gameBoard.Length;

        public int LengthY => _gameBoard[0].Length;

        GameField IGameBoard.this[int x, int y] { get => _gameBoard[x][y]; set => _gameBoard[x][y] = value; }

        public bool FieldIsType(int x, int y, GameFieldType type)
        {
            return _gameBoard[x][y].Type == type;
        }

        public bool TopNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (y > 0) && (_gameBoard[x][y - 1] != null) && (_gameBoard[x][y - 1].Type == type);
        }

        public bool TopRightNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x < _gameBoard.Length - 1) && (y > 0) && (_gameBoard[x + 1][y - 1] != null) && (_gameBoard[x + 1][y - 1].Type == type);
        }

        public bool RightNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x < _gameBoard.Length - 1) && (_gameBoard[x + 1][y] != null) && (_gameBoard[x + 1][y].Type == type);
        }

        public bool BottomRightNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x < _gameBoard.Length - 1) && (y < _gameBoard[x].Length - 1) && (_gameBoard[x + 1][y + 1] != null) && (_gameBoard[x + 1][y + 1].Type == type);
        }

        public bool BottomNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (y < _gameBoard[x].Length - 1) && (_gameBoard[x][y + 1] != null) && (_gameBoard[x][y + 1].Type == type);
        }

        public bool BottomLeftNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x > 0) && (y < _gameBoard[x].Length - 1) && (_gameBoard[x - 1][y + 1] != null) && (_gameBoard[x - 1][y + 1].Type == type);
        }

        public bool LeftNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x > 0) && (_gameBoard[x - 1][y] != null) && (_gameBoard[x - 1][y].Type == type);
        }

        public bool TopLeftNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x > 0) && (y > 0) && (_gameBoard[x - 1][y - 1] != null) && (_gameBoard[x - 1][y - 1].Type == type);
        }

        public bool ValidateGameBoard()
        {
            for (int x = 1; x < _gameBoard.Length; x++)
            {
                for (int y = 1; y < _gameBoard[x].Length; y++)
                {
                    if (InvalidField(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool InvalidField(int x, int y)
        {
            return TopLeftInvalid(x, y) || TopRightInvalid(x, y) || BottomRightInvalid(x, y) || BottomLeftInvalid(x, y);
        }

        public bool TopLeftInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && LeftNeighbourIsType(x, y, GameFieldType.Road)
                                        && TopLeftNeighbourIsType(x, y, GameFieldType.Road) && TopNeighbourIsType(x, y, GameFieldType.Road);
        }

        public bool TopRightInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && TopNeighbourIsType(x, y, GameFieldType.Road)
                                        && TopRightNeighbourIsType(x, y, GameFieldType.Road) && RightNeighbourIsType(x, y, GameFieldType.Road);
        }

        public bool BottomRightInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && RightNeighbourIsType(x, y, GameFieldType.Road)
                                        && BottomRightNeighbourIsType(x, y, GameFieldType.Road) && BottomNeighbourIsType(x, y, GameFieldType.Road);
        }

        public bool BottomLeftInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && BottomNeighbourIsType(x, y, GameFieldType.Road)
                                        && BottomLeftNeighbourIsType(x, y, GameFieldType.Road) && LeftNeighbourIsType(x, y, GameFieldType.Road);
        }

        public List<Coordinates> GetPossibleNewNeighbours(int x, int y)
        {
            List<Coordinates> possibleNewNeighbours = new List<Coordinates>();

            if (TopCanBeNeighbour(x, y))
            {
                possibleNewNeighbours.Add(new Coordinates(x, y - 1));
            }

            if (RightCanBeNeighbour(x, y))
            {
                possibleNewNeighbours.Add(new Coordinates(x + 1, y));
            }

            if (BottomCanBeNeighbour(x, y))
            {
                possibleNewNeighbours.Add(new Coordinates(x, y + 1));
            }

            if (LeftCanBeNeighbour(x, y))
            {
                possibleNewNeighbours.Add(new Coordinates(x - 1, y));
            }

            return possibleNewNeighbours;
        }

        private bool TopCanBeNeighbour(int x, int y)
        {
            if (TopIsFree(x, y) && TopCanBeRoad(x, y))
                return true;
            return false;
        }

        private bool RightCanBeNeighbour(int x, int y)
        {
            if (RightIsFree(x, y) && RightCanBeRoad(x, y))
                return true;
            return false;
        }

        private bool BottomCanBeNeighbour(int x, int y)
        {
            if (BottomIsFree(x, y) && BottomCanBeRoad(x, y))
                return true;
            return false;
        }

        private bool LeftCanBeNeighbour(int x, int y)
        {
            if (LeftIsFree(x, y) && LeftCanBeRoad(x, y))
                return true;
            return false;
        }

        private bool TopIsFree(int x, int y)
        {
            return (y > 0) && (_gameBoard[x][y - 1] == null);
        }

        private bool RightIsFree(int x, int y)
        {
            return (x < _gameBoard.Length - 1) && (_gameBoard[x + 1][y] == null);
        }

        private bool BottomIsFree(int x, int y)
        {
            return (y < _gameBoard[x].Length - 1) && (_gameBoard[x][y + 1] == null);
        }

        private bool LeftIsFree(int x, int y)
        {
            return (x > 0) && (_gameBoard[x - 1][y] == null);
        }

        private bool TopCanBeRoad(int x, int y)
        {
            return CanBeRoad(x, y - 1);
        }

        private bool RightCanBeRoad(int x, int y)
        {
            return CanBeRoad(x + 1, y);
        }

        private bool BottomCanBeRoad(int x, int y)
        {
            return CanBeRoad(x, y + 1);
        }

        private bool LeftCanBeRoad(int x, int y)
        {
            return CanBeRoad(x - 1, y);
        }

        private bool CanBeRoad(int x, int y)
        {
            _gameBoard[x][y] = new GameField(GameFieldType.Road);
            bool canBeRoad = !InvalidField(x, y);
            _gameBoard[x][y] = null;
            return canBeRoad;
        }

        public int GetNumberOfNeighbourRoads(int x, int y)
        {
            int numberOfNeighbourRoads = 0;

            if (TopNeighbourIsType(x, y, GameFieldType.Road))
            {
                numberOfNeighbourRoads++;
            }

            if (RightNeighbourIsType(x, y, GameFieldType.Road))
            {
                numberOfNeighbourRoads++;
            }

            if (BottomNeighbourIsType(x, y, GameFieldType.Road))
            {
                numberOfNeighbourRoads++;
            }

            if (LeftNeighbourIsType(x, y, GameFieldType.Road))
            {
                numberOfNeighbourRoads++;
            }

            return numberOfNeighbourRoads;
        }
    }
}
