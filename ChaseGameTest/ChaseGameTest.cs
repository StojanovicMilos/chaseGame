using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaseGameNamespace;
using System.Windows.Forms;

namespace ChaseGameTest
{
    [TestClass]
    public class ChaseGameTest
    {
        [TestMethod]
        public void ChaseGameConstructorValidInput()
        {
            // arrange  
            int numberOfPlayers = 5;
            int boardSizeX = 80;
            int boardSizeY = 45;
            PictureBox[,] pictureBoxes = new PictureBox[boardSizeX, boardSizeY];
            for (int i = 0; i < pictureBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                {
                    pictureBoxes[i, j] = new PictureBox();
                }
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(pictureBoxes, numberOfPlayers);

            // assert  
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChaseGameConstructorInvalidX()
        {
            // arrange  
            int numberOfPlayers = 5;
            int boardSizeX = 0;
            int boardSizeY = 90;
            PictureBox[,] pictureBoxes = new PictureBox[boardSizeX, boardSizeY];
            for (int i = 0; i < pictureBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                {
                    pictureBoxes[i, j] = new PictureBox();
                }
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(pictureBoxes, numberOfPlayers);

            // assert  

        }

        [TestMethod]
        public void ChaseGameMapGeneration()
        {
            for (int numberOfIterations = 0; numberOfIterations < 1000; numberOfIterations++)
            {
                // arrange  
                int numberOfPlayers = 5;
                int boardSizeX = 160;
                int boardSizeY = 90;
                PictureBox[,] pictureBoxes = new PictureBox[boardSizeX, boardSizeY];
                for (int i = 0; i < pictureBoxes.GetLength(0); i++)
                {
                    for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                    {
                        pictureBoxes[i, j] = new PictureBox();
                    }
                }

                // act  
                ChaseGame chaseGame = new ChaseGame(pictureBoxes, numberOfPlayers);
                bool validGameBoard = chaseGame.CheckGameBoard();

                // assert  
                Assert.IsTrue(validGameBoard);
            }
        }
    }
}
