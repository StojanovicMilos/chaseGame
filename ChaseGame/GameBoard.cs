using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    class GameBoard
    {
        private GameField[,] gameBoard;
        Random random = new Random();

        public GameBoard(PictureBox[,] pictureBoxes)
        {
            gameBoard = new GameField[pictureBoxes.GetLength(0), pictureBoxes.GetLength(1)];
            /*for (int i = 0; i < pictureBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                {
                    gameBoard[i, j] = new GameField(pictureBoxes[i, j]);
                }
            }*/
            GenerateRandomGameBoard(pictureBoxes);
            Draw();
        }

        private void Draw()
        {
            foreach (GameField gamefield in gameBoard)
                gamefield.Draw();
        }

        private void GenerateRandomGameBoard(PictureBox[,] pictureBoxes)
        {
            SetOutlineFieldsToRoads(pictureBoxes);
            GenerateRoads(pictureBoxes);
            FillRemainingFieldsWithGrass(pictureBoxes);
        }

        private void SetOutlineFieldsToRoads(PictureBox[,] pictureBoxes)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    if ((x == 0) || (y == 0) || (x == (gameBoard.GetLength(0) - 1)) || (y == (gameBoard.GetLength(1) - 1)))
                        gameBoard[x, y] = new RoadGameField(pictureBoxes[x,y]);
                }
            }
        }

        private void GenerateRoads(PictureBox[,] pictureBoxes)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    GenerateSurroundingFields(x, y, pictureBoxes);
                }
            }
        }

        private void FillRemainingFieldsWithGrass(PictureBox[,] pictureBoxes)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.GetLength(1); y++)
                {
                    if ((gameBoard[x, y] == null))// || (GetNumberOfNeighbourRoads(x, y) == 1))
                        gameBoard[x, y] = new GrassGameField(pictureBoxes[x,y]);
                }
            }
        }

        private void GenerateSurroundingFields(int x, int y, PictureBox[,] pictureBoxes)
        {
            if ((gameBoard[x, y] != null) && (gameBoard[x, y].GetType().Equals(typeof(RoadGameField))))
            {
                int numberOfNeighbourRoads = GetNumberOfNeighbourRoads(x, y);
                var possibleNewNeighbours = GetPossibleNewNeighbours(x, y);

                int numberOfNewNeighbourRoads = 0;

                while ((possibleNewNeighbours.Count > 0) && ((numberOfNeighbourRoads + numberOfNewNeighbourRoads) < 2))
                {
                    int index = random.Next(possibleNewNeighbours.Count);
                    var coordinates = possibleNewNeighbours[index];
                    gameBoard[coordinates.X, coordinates.Y] = new RoadGameField(pictureBoxes[coordinates.X, coordinates.Y]);
                    possibleNewNeighbours.RemoveAt(index);
                    numberOfNewNeighbourRoads++;
                }
                if ((possibleNewNeighbours.Count > 0) && (PercentChance(70)))
                {
                    int index = random.Next(possibleNewNeighbours.Count);
                    var coordinates = possibleNewNeighbours[index];
                    gameBoard[coordinates.X, coordinates.Y] = new RoadGameField(pictureBoxes[coordinates.X, coordinates.Y]);
                    possibleNewNeighbours.RemoveAt(index);
                    numberOfNewNeighbourRoads++;

                    if ((possibleNewNeighbours.Count > 0) && (PercentChance(10)))
                    {
                        index = random.Next(possibleNewNeighbours.Count);
                        coordinates = possibleNewNeighbours[index];
                        gameBoard[coordinates.X, coordinates.Y] = new RoadGameField(pictureBoxes[coordinates.X, coordinates.Y]);
                        possibleNewNeighbours.RemoveAt(index);
                        numberOfNewNeighbourRoads++;
                    }
                }
            }
        }

        private int GetNumberOfNeighbourRoads(int x, int y)
        {
            int numberOfNeighbourRoads = 0;
            if (TopNeighbourIsType(x, y, typeof(RoadGameField)))
                numberOfNeighbourRoads++;
            if (RightNeighbourIsType(x, y, typeof(RoadGameField)))
                numberOfNeighbourRoads++;
            if (BottomNeighbourIsType(x, y, typeof(RoadGameField)))
                numberOfNeighbourRoads++;
            if (LeftNeighbourIsType(x, y, typeof(RoadGameField)))
                numberOfNeighbourRoads++;
            return numberOfNeighbourRoads;
        }

        private List<Coordinates> GetPossibleNewNeighbours(int x, int y)
        {
            List<Coordinates> possibleNewNeighbours = new List<Coordinates>();

            //Check top
            if (
                !(TopLeftNeighbourIsType(x, y, typeof(RoadGameField)) && LeftNeighbourIsType(x, y, typeof(RoadGameField))) &&
                !(TopRightNeighbourIsType(x, y, typeof(RoadGameField)) && RightNeighbourIsType(x, y, typeof(RoadGameField)))
                )
            {
                if ((y > 0) && (gameBoard[x, y - 1] == null))
                    possibleNewNeighbours.Add(new Coordinates { X = x, Y = y - 1 });
            }

            //Check right
            if (
                !(TopRightNeighbourIsType(x, y, typeof(RoadGameField)) && TopNeighbourIsType(x, y, typeof(RoadGameField))) &&
                !(BottomRightNeighbourIsType(x, y, typeof(RoadGameField)) && BottomNeighbourIsType(x, y, typeof(RoadGameField)))
                )
            {
                if ((x < gameBoard.GetLength(0) - 1) && (gameBoard[x + 1, y] == null))
                    possibleNewNeighbours.Add(new Coordinates { X = x + 1, Y = y });
            }

            //Check bottom
            if (
                !(BottomRightNeighbourIsType(x, y, typeof(RoadGameField)) && RightNeighbourIsType(x, y, typeof(RoadGameField))) &&
                !(BottomLeftNeighbourIsType(x, y, typeof(RoadGameField)) && LeftNeighbourIsType(x, y, typeof(RoadGameField)))
                )
            {
                if ((y < gameBoard.GetLength(1) - 1) && (gameBoard[x, y + 1] == null))
                    possibleNewNeighbours.Add(new Coordinates { X = x, Y = y + 1 });
            }

            //Check left
            if (
                !(BottomLeftNeighbourIsType(x, y, typeof(RoadGameField)) && BottomNeighbourIsType(x, y, typeof(RoadGameField))) &&
                !(TopLeftNeighbourIsType(x, y, typeof(RoadGameField)) && TopNeighbourIsType(x, y, typeof(RoadGameField)))
                )
            {
                if ((x > 0) && (gameBoard[x - 1, y] == null))
                    possibleNewNeighbours.Add(new Coordinates { X = x - 1, Y = y });
            }

            return possibleNewNeighbours;
        }

        private bool FieldIsType(int x, int y, Type gameFieldType)
        {
            if (gameBoard[x, y].GetType().Equals(gameFieldType))
                return true;
            return false;
        }

        private bool TopNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((y > 0) && (gameBoard[x, y - 1] != null) && (gameBoard[x, y - 1].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool TopRightNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((x < gameBoard.GetLength(0) - 1) && (y > 0) && (gameBoard[x + 1, y - 1] != null) && (gameBoard[x + 1, y - 1].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool RightNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((x < gameBoard.GetLength(0) - 1) && (gameBoard[x + 1, y] != null) && (gameBoard[x + 1, y].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool BottomRightNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((x < gameBoard.GetLength(0) - 1) && (y < gameBoard.GetLength(1) - 1) && (gameBoard[x + 1, y + 1] != null) && (gameBoard[x + 1, y + 1].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool BottomNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((y < gameBoard.GetLength(1) - 1) && (gameBoard[x, y + 1] != null) && (gameBoard[x, y + 1].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool BottomLeftNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((x > 0) && (y < gameBoard.GetLength(1) - 1) && (gameBoard[x - 1, y + 1] != null) && (gameBoard[x - 1, y + 1].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool LeftNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((x > 0) && (gameBoard[x - 1, y] != null) && (gameBoard[x - 1, y].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool TopLeftNeighbourIsType(int x, int y, Type gameFieldType)
        {
            if ((x > 0) && (y > 0) && (gameBoard[x - 1, y - 1] != null) && (gameBoard[x - 1, y - 1].GetType().Equals(gameFieldType)))
                return true;
            return false;
        }

        private bool PercentChance(int percentage)
        {
            if (random.Next(100) < percentage)
            {
                return true;
            }
            return false;
        }

        private class Coordinates
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        internal bool checkGameBoard()
        {
            for (int x = 1; x < gameBoard.GetLength(0); x++)
            {
                for (int y = 1; y < gameBoard.GetLength(1); y++)
                {
                    if (FieldIsType(x, y, typeof(RoadGameField)) && LeftNeighbourIsType(x, y, typeof(RoadGameField)) && TopLeftNeighbourIsType(x, y, typeof(RoadGameField)) && TopNeighbourIsType(x, y, typeof(RoadGameField)))
                        return false;
                    if (FieldIsType(x, y, typeof(RoadGameField)) && TopNeighbourIsType(x, y, typeof(RoadGameField)) && TopRightNeighbourIsType(x, y, typeof(RoadGameField)) && RightNeighbourIsType(x, y, typeof(RoadGameField)))
                        return false;
                    if (FieldIsType(x, y, typeof(RoadGameField)) && RightNeighbourIsType(x, y, typeof(RoadGameField)) && BottomRightNeighbourIsType(x, y, typeof(RoadGameField)) && BottomNeighbourIsType(x, y, typeof(RoadGameField)))
                        return false;
                    if (FieldIsType(x, y, typeof(RoadGameField)) && BottomNeighbourIsType(x, y, typeof(RoadGameField)) && BottomLeftNeighbourIsType(x, y, typeof(RoadGameField)) && LeftNeighbourIsType(x, y, typeof(RoadGameField)))
                        return false;
                }   
            }
            return true;
        }
    }
}
