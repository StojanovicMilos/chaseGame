using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MoreLinq;

namespace ChaseGameNamespace
{
    public class GameBoard
    {
        private GameField[][] _gameBoard;
        private readonly Random _random = new Random();

        public GameBoard(PictureBox[][] pictureBoxes)
        {
            InitializeGameBoard(pictureBoxes);

            GenerateRandomGameBoard(pictureBoxes);

            //GenerateFixedGameBoard(pictureBoxes);

            Draw();
        }

        private void InitializeGameBoard(PictureBox[][] pictureBoxes)
        {
            _gameBoard = new GameField[pictureBoxes.Length][];
            for (int x = 0; x < _gameBoard.Length; x++)
            {
                _gameBoard[x] = new GameField[pictureBoxes[x].Length];
            }
        }

        private void GenerateFixedGameBoard(PictureBox[][] pictureBoxes)
        {
            for (int x = 0; x < _gameBoard.Length; x++)
            {
                for (int y = 0; y < _gameBoard[x].Length; y++)
                {
                    GameFieldType type = GameFieldShouldBeGrass(x, y) ? GameFieldType.Grass : GameFieldType.Road;
                    _gameBoard[x][y] = new GameField(pictureBoxes[x][y], type);
                }
            }
        }

        private bool GameFieldShouldBeGrass(int x, int y)
        {
            return (x % 2 != 0) && (y % 2 != 0);
        }

        private void Draw()
        {
            _gameBoard.ForEach(row => row.ForEach(column => column.Draw()));
        }

        private void GenerateRandomGameBoard(PictureBox[][] pictureBoxes)
        {
            SetOutlineFieldsToRoads(pictureBoxes);
            GenerateRoads(pictureBoxes);
            FillRemainingFieldsWithGrass(pictureBoxes);
        }

        private void SetOutlineFieldsToRoads(PictureBox[][] pictureBoxes)
        {
            for (int x = 0; x < _gameBoard.Length; x++)
            {
                for (int y = 0; y < _gameBoard[x].Length; y++)
                {
                    if (FieldIsOutline(x, y))
                    {
                        _gameBoard[x][y] = new GameField(pictureBoxes[x][y], GameFieldType.Road);
                    }
                }
            }
        }

        private bool FieldIsOutline(int x, int y)
        {
            return (x == 0) || (y == 0) || (x == (_gameBoard.Length - 1)) || (y == (_gameBoard[x].Length - 1));
        }

        private void GenerateRoads(PictureBox[][] pictureBoxes)
        {
            for (int x = 0; x < _gameBoard.Length; x++)
            {
                for (int y = 0; y < _gameBoard[x].Length; y++)
                {
                    if (_gameBoard[x][y]?.Type == GameFieldType.Road)
                        GenerateSurroundingFields(x, y, pictureBoxes);
                }
            }
        }

        private void FillRemainingFieldsWithGrass(PictureBox[][] pictureBoxes)
        {
            for (int x = 0; x < _gameBoard.Length; x++)
            {
                for (int y = 0; y < _gameBoard[x].Length; y++)
                {
                    if ((_gameBoard[x][y] == null) || (GetNumberOfNeighbourRoads(x, y) == 1))
                    {
                        _gameBoard[x][y] = new GameField(pictureBoxes[x][y], GameFieldType.Grass);
                    }
                }
            }
        }

        private void GenerateSurroundingFields(int x, int y, PictureBox[][] pictureBoxes)
        {
            int numberOfNeighbourRoads = GetNumberOfNeighbourRoads(x, y);
            List<Coordinates> possibleNewNeighbours = GetPossibleNewNeighbours(x, y);

            int numberOfNewNeighbourRoads = 0;
            int index;
            Coordinates coordinates;

            while ((possibleNewNeighbours.Count > 0) && ((numberOfNeighbourRoads + numberOfNewNeighbourRoads) < 2))
            {
                index = _random.Next(possibleNewNeighbours.Count);
                coordinates = possibleNewNeighbours[index];
                _gameBoard[coordinates.X][coordinates.Y] = new GameField(pictureBoxes[coordinates.X][coordinates.Y], GameFieldType.Road);
                possibleNewNeighbours.RemoveAt(index);
                numberOfNewNeighbourRoads++;
            }

            if ((possibleNewNeighbours.Count <= 0) || !PercentChance(70))
            {
                return;
            }

            index = _random.Next(possibleNewNeighbours.Count);
            coordinates = possibleNewNeighbours[index];
            _gameBoard[coordinates.X][coordinates.Y] = new GameField(pictureBoxes[coordinates.X][coordinates.Y], GameFieldType.Road);
            possibleNewNeighbours.RemoveAt(index);

            if ((possibleNewNeighbours.Count <= 0) || !PercentChance(10))
            {
                return;
            }

            index = _random.Next(possibleNewNeighbours.Count);
            coordinates = possibleNewNeighbours[index];
            _gameBoard[coordinates.X][coordinates.Y] = new GameField(pictureBoxes[coordinates.X][coordinates.Y], GameFieldType.Road);
            possibleNewNeighbours.RemoveAt(index);
        }

        private int GetNumberOfNeighbourRoads(int x, int y)
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

        private List<Coordinates> GetPossibleNewNeighbours(int x, int y)
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
            _gameBoard[x][y] = new GameField(null, GameFieldType.Road);
            bool canBeRoad = !InvalidField(x, y);
            _gameBoard[x][y] = null;
            return canBeRoad;
        }



        private bool FieldIsType(int x, int y, GameFieldType type)
        {
            return _gameBoard[x][y].Type == type;
        }

        private bool TopNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (y > 0) && (_gameBoard[x][y - 1] != null) && (_gameBoard[x][y - 1].Type == type);
        }

        private bool TopRightNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x < _gameBoard.Length - 1) && (y > 0) && (_gameBoard[x + 1][y - 1] != null) && (_gameBoard[x + 1][y - 1].Type == type);
        }

        private bool RightNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x < _gameBoard.Length - 1) && (_gameBoard[x + 1][y] != null) && (_gameBoard[x + 1][y].Type == type);
        }

        private bool BottomRightNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x < _gameBoard.Length - 1) && (y < _gameBoard[x].Length - 1) && (_gameBoard[x + 1][y + 1] != null) && (_gameBoard[x + 1][y + 1].Type == type);
        }

        private bool BottomNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (y < _gameBoard[x].Length - 1) && (_gameBoard[x][y + 1] != null) && (_gameBoard[x][y + 1].Type == type);
        }

        private bool BottomLeftNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x > 0) && (y < _gameBoard[x].Length - 1) && (_gameBoard[x - 1][y + 1] != null) && (_gameBoard[x - 1][y + 1].Type == type);
        }

        private bool LeftNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x > 0) && (_gameBoard[x - 1][y] != null) && (_gameBoard[x - 1][y].Type == type);
        }

        private bool TopLeftNeighbourIsType(int x, int y, GameFieldType type)
        {
            return (x > 0) && (y > 0) && (_gameBoard[x - 1][y - 1] != null) && (_gameBoard[x - 1][y - 1].Type == type);
        }

        private bool PercentChance(int percentage)
        {
            return _random.Next(100) < percentage;
        }

        public bool ValidateGameBoard()
        {
            for (int x = 1; x < _gameBoard.Length; x++)
            {
                for (int y = 1; y < _gameBoard[x].Length; y++)
                {
                    if (InvalidField(x, y))
                        return false;
                }
            }
            return true;
        }

        private bool InvalidField(int x, int y)
        {
            return TopLeftInvalid(x, y) || TopRightInvalid(x, y) || BottomRightInvalid(x, y) || BottomLeftInvalid(x, y);
        }

        private bool TopLeftInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && LeftNeighbourIsType(x, y, GameFieldType.Road)
                                        && TopLeftNeighbourIsType(x, y, GameFieldType.Road) && TopNeighbourIsType(x, y, GameFieldType.Road);
        }

        private bool TopRightInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && TopNeighbourIsType(x, y, GameFieldType.Road)
                                        && TopRightNeighbourIsType(x, y, GameFieldType.Road) && RightNeighbourIsType(x, y, GameFieldType.Road);
        }

        private bool BottomRightInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && RightNeighbourIsType(x, y, GameFieldType.Road)
                                        && BottomRightNeighbourIsType(x, y, GameFieldType.Road) && BottomNeighbourIsType(x, y, GameFieldType.Road);
        }

        private bool BottomLeftInvalid(int x, int y)
        {
            return FieldIsType(x, y, GameFieldType.Road) && BottomNeighbourIsType(x, y, GameFieldType.Road)
                                        && BottomLeftNeighbourIsType(x, y, GameFieldType.Road) && LeftNeighbourIsType(x, y, GameFieldType.Road);
        }
    }
}
