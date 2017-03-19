using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaseGameNamespace;

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
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, players, staticGenerator);

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
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, players, staticGenerator);

            // assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChaseGameConstructorInvalidY()
        {
            // arrange
            const int numberOfPlayers = 5;
            const int boardSizeX = 160;
            const int boardSizeY = 0;
            IGenerator staticGenerator = new StaticGenerator();
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, players, staticGenerator);

            // assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChaseGameConstructorInvalidNumberOfPlayers()
        {
            // arrange
            const int numberOfPlayers = 0;
            const int boardSizeX = 160;
            const int boardSizeY = 0;
            IGenerator staticGenerator = new StaticGenerator();
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, players, staticGenerator);

            // assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChaseGameConstructorInvalidPlayer()
        {
            // arrange
            const int numberOfPlayers = 0;
            const int boardSizeX = 160;
            const int boardSizeY = 0;
            IGenerator staticGenerator = new StaticGenerator();
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = null;
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, players, staticGenerator);

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
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
            }

            // act  
            ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, players, staticGenerator);
            bool validGameBoard = chaseGame.ValidateGameBoard();

            // assert  
            Assert.IsTrue(validGameBoard);
        }

        [TestMethod]
        public void ChaseGameRandomMapGeneration()
        {
            const int numberOfIterations = 10;
            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                // arrange
                const int numberOfPlayers = 5;
                const int boardSizeX = 160;
                const int boardSizeY = 90;
                IGenerator randomGenerator = new RandomGenerator(new DummyLogger());//TextLogger());
                Player[] players = new Player[numberOfPlayers];
                for (int i = 0; i < players.Length; i++)
                {
                    players[i] = new Player();
                }

                // act  
                ChaseGame chaseGame = new ChaseGame(boardSizeX, boardSizeY, players, randomGenerator);
                bool validGameBoard = chaseGame.ValidateGameBoard();

                // assert  
                Assert.IsTrue(validGameBoard);
            }
        }
    }
}
