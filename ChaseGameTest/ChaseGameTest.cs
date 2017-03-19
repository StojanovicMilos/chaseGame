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
            const int numberOfPlayers = 5;
            const int boardSizeX = 80;
            const int boardSizeY = 45;
            IGenerator staticGenerator = new StaticGenerator();

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, numberOfPlayers, staticGenerator);

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
            IGenerator staticGenerator = new StaticGenerator();

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, numberOfPlayers, staticGenerator);

            // assert 
        }

        [TestMethod]
        public void ChaseGameStaticMapGeneration()
        {
            // arrange
            const int numberOfPlayers = 5;
            const int boardSizeX = 160;
            const int boardSizeY = 90;
            IGenerator staticGenerator = new StaticGenerator();

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, numberOfPlayers, staticGenerator);
            bool validGameBoard = chaseGame.ValidateGameBoard();

            // assert  
            Assert.IsTrue(validGameBoard);
        }

        [TestMethod]
        public void ChaseGameRandomMapGeneration()
        {
            const int numberOfIterations = 1000;
            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                // arrange
                const int numberOfPlayers = 5;
                const int boardSizeX = 160;
                const int boardSizeY = 90;
                IGenerator randomGenerator = new RandomGenerator(new DummyLogger());//TextLogger());

                // act  
                ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, numberOfPlayers, randomGenerator);
                bool validGameBoard = chaseGame.ValidateGameBoard();

                // assert  
                Assert.IsTrue(validGameBoard);
            }
        }
    }
}
