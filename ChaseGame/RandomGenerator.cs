using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public class RandomGenerator : IGenerator
    {
        private ILogger _logger;
        private List<Coordinates> _possibleNewNeighbours;
        private IGameBoard _gameBoard;
        private readonly Random _random = new Random();
        private readonly int[] _neighboursProbabilities = { 100, 100, 70, 10, 0 };
        public RandomGenerator(ILogger logger)
        {
            _logger = logger;
        }

        public IGameBoard GenerateGameBoard(PictureBox[][] pictureBoxes)
        {
            InitializeGameBoard(pictureBoxes);
            SetOutlineFieldsToRoads(pictureBoxes);
            GenerateRoads(pictureBoxes);
            FillRemainingFieldsWithGrass(pictureBoxes);
            _gameBoard.SetBackgroundImages();
            return _gameBoard;
        }

        private void InitializeGameBoard(PictureBox[][] pictureBoxes)
        {
            _logger.Log("Initializing game board");
            _gameBoard = new GameBoard(pictureBoxes.Length, pictureBoxes[0].Length);
            _logger.Log("GameBoardSize = " + _gameBoard.LengthX + ", " + _gameBoard.LengthY);
        }

        private void SetOutlineFieldsToRoads(PictureBox[][] pictureBoxes)
        {
            for (int x = 0; x < _gameBoard.LengthX; x++)
            {
                for (int y = 0; y < _gameBoard.LengthY; y++)
                {
                    if (FieldIsOutline(x, y))
                    {
                        _gameBoard[x, y] = new GameField(pictureBoxes[x][y], GameFieldType.Road);
                        _logger.Log("Outline field at coordinates [" + x + "][" + y + "] is set to road");
                    }
                }
            }
        }

        private bool FieldIsOutline(int x, int y)
        {
            return (x == 0) || (y == 0) || (x == (_gameBoard.LengthX - 1)) || (y == (_gameBoard.LengthY - 1));
        }

        private void GenerateRoads(PictureBox[][] pictureBoxes)
        {
            for (int x = 0; x < _gameBoard.LengthX; x++)
            {
                for (int y = 0; y < _gameBoard.LengthY; y++)
                {
                    if (_gameBoard[x, y]?.Type == GameFieldType.Road)
                    {
                        _logger.Log("--------------------------------------------------");
                        _logger.Log("Generating surrounding fields for " + x + ", " + y);
                        GenerateSurroundingFields(x, y, pictureBoxes);
                        _logger.Log("Finished generating surrounding fields for " + x + ", " + y);
                    }
                }
            }
        }

        private void FillRemainingFieldsWithGrass(PictureBox[][] pictureBoxes)
        {
            for (int x = 0; x < _gameBoard.LengthX; x++)
            {
                for (int y = 0; y < _gameBoard.LengthY; y++)
                {
                    if ((_gameBoard[x, y] == null) || (_gameBoard.GetNumberOfNeighbourRoads(x, y) == 1))
                    {
                        _gameBoard[x, y] = new GameField(pictureBoxes[x][y], GameFieldType.Grass);
                        _logger.Log("field [" + x + "][" + y + "] is set to grass");
                    }
                }
            }
        }

        private void GenerateSurroundingFields(int x, int y, PictureBox[][] pictureBoxes)
        {
            int numberOfNeighbourRoads = _gameBoard.GetNumberOfNeighbourRoads(x, y);
            _logger.Log("numberOfNeighbourRoads = " + numberOfNeighbourRoads);
            _possibleNewNeighbours = _gameBoard.GetPossibleNewNeighbours(x, y);
            _logger.Log("_possibleNewNeighbours = " + _possibleNewNeighbours.ListEveryElement());

            while ((_possibleNewNeighbours.Count > 0) && (PercentChance(_neighboursProbabilities[numberOfNeighbourRoads])))
            {
                numberOfNeighbourRoads++;
                _logger.Log("adding neighbour number " + (numberOfNeighbourRoads).ToString());
                AddNeighbour(pictureBoxes);
                _logger.Log("added neighbour number " + (numberOfNeighbourRoads).ToString());
                _possibleNewNeighbours = _gameBoard.GetPossibleNewNeighbours(x, y);
                _logger.Log("_possibleNewNeighbours = " + _possibleNewNeighbours.ListEveryElement());
            }
        }

        private void AddNeighbour(PictureBox[][] pictureBoxes)
        {
            int index = _random.Next(_possibleNewNeighbours.Count);
            _logger.Log("adding neighbour with index = " + index + " from list.");
            Coordinates coordinates = _possibleNewNeighbours[index];
            _logger.Log("coordinates of neighbour with index " + index + " = " + coordinates.ToString());
            _gameBoard[coordinates.X, coordinates.Y] = new GameField(pictureBoxes[coordinates.X][coordinates.Y], GameFieldType.Road);
            _logger.Log("Field at coordinates " + coordinates.ToString() + " set to road");
            //_possibleNewNeighbours.RemoveAt(index);
        }

        private bool PercentChance(int percentage)
        {
            return _random.Next(100) < percentage;
        }

        public bool ValidateGameBoard()
        {
            return _gameBoard.ValidateGameBoard();
        }
    }
}
