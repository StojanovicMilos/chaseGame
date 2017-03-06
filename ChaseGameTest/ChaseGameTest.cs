using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaseGameNamespace;
using System.Windows.Forms;

namespace ChaseGameTest
{
    [TestClass]
    public class ChaseGameTest
    {
	    private int _numberOfPlayers;
	    private PictureBox[][] _pictureBoxes;
		
		public void Initialize(int numberOfPlayers, int boardSizeX, int boardSizeY)
		{
			_numberOfPlayers = numberOfPlayers;

			_pictureBoxes = new PictureBox[boardSizeX][];
			for (int i = 0; i < _pictureBoxes.Length; i++)
			{
				_pictureBoxes[i] = new PictureBox[boardSizeY];

				for (int j = 0; j < _pictureBoxes[i].Length; j++)
				{
					_pictureBoxes[i][j] = new PictureBox();
				}
			}
		}

		[TestMethod]
        public void ChaseGameConstructorValidInput()
        {
            // arrange
			const int numberOfPlayers = 5;
			const int boardSizeX = 80;
			const int boardSizeY = 45;
			Initialize(numberOfPlayers, boardSizeX, boardSizeY);
            
            // act  
            new ChaseGame(_pictureBoxes, _numberOfPlayers);

            // assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChaseGameConstructorInvalidX()
        {
			// arrange
			const int numberOfPlayers = 5;
			const int boardSizeX = 0;
			const int boardSizeY = 90;
			Initialize(numberOfPlayers, boardSizeX, boardSizeY);

			// act  
			new ChaseGame(_pictureBoxes, _numberOfPlayers);

            // assert 
        }

        [TestMethod]
        public void ChaseGameMapGeneration()
        {
	        const int numberOfIterations = 1000;
	        for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
				// arrange
				const int numberOfPlayers = 5;
				const int boardSizeX = 160;
				const int boardSizeY = 90;
				Initialize(numberOfPlayers, boardSizeX, boardSizeY);

				// act  
				ChaseGame chaseGame = new ChaseGame(_pictureBoxes, _numberOfPlayers);
				bool validGameBoard = chaseGame.CheckGameBoard();

                // assert  
                Assert.IsTrue(validGameBoard);
            }
        }
    }
}
