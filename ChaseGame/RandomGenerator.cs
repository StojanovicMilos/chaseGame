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

        public RandomGenerator(ILogger logger)
        {
            this._logger = logger;
        }

        public IGameBoard GenerateGameBoard(PictureBox[][] pictureBoxes)
        {
            InitializeGameBoard(pictureBoxes);
            SetOutlineFieldsToRoads(pictureBoxes);
            GenerateRoads(pictureBoxes);
            FillRemainingFieldsWithGrass(pictureBoxes);
            return _gameBoard;
        }

        private void InitializeGameBoard(PictureBox[][] pictureBoxes)
        {
            _logger.Log("Inisializing game board");
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
                        _gameBoard[x,y] = new GameField(pictureBoxes[x][y], GameFieldType.Road);
                        _logger.Log("Outline field [" + x + "][" + y + "] is set to road");
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
                    if (_gameBoard[x,y]?.Type == GameFieldType.Road)
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
                    if ((_gameBoard[x,y] == null) || (_gameBoard.GetNumberOfNeighbourRoads(x, y) == 1))
                    {
                        _gameBoard[x,y] = new GameField(pictureBoxes[x][y], GameFieldType.Grass);
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
            int numberOfNewNeighbourRoads = 0;

            while ((_possibleNewNeighbours.Count > 0) && ((numberOfNeighbourRoads + numberOfNewNeighbourRoads) < 2))
            {
                //TODO Add more logging
                AddNeighbour(pictureBoxes);
                numberOfNewNeighbourRoads++;
            }

            if ((_possibleNewNeighbours.Count <= 0) || !PercentChance(70))
            {
                return;
            }

            AddNeighbour(pictureBoxes);

            if ((_possibleNewNeighbours.Count <= 0) || !PercentChance(10))
            {
                return;
            }

            AddNeighbour(pictureBoxes);
        }

        private void AddNeighbour(PictureBox[][] pictureBoxes)
        {
            int index= _random.Next(_possibleNewNeighbours.Count);
            Coordinates coordinates = _possibleNewNeighbours[index];
            _gameBoard[coordinates.X, coordinates.Y] = new GameField(pictureBoxes[coordinates.X][coordinates.Y], GameFieldType.Road);
            _possibleNewNeighbours.RemoveAt(index);
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
