using Bromano.Validation;

namespace ChaseGameNamespace
{
    public class ChaseGame
    {
        private Player[] _players;
        private readonly IGameBoard _gameBoard;

        public IGameBoard GetGameBoard()
        {
            return _gameBoard;
        }

        public ChaseGame(int boardSizeX, int boardSizeY, int numberOfPlayers, IGenerator generator)
        {
            ValidateInput(boardSizeX, boardSizeY, numberOfPlayers);
            _players = new Player[numberOfPlayers];
            _gameBoard = generator.GenerateGameBoard(boardSizeX, boardSizeY);
        }

        private void ValidateInput(int boardSizeX, int boardSizeY, int numberOfPlayers)
        {
            Validate.IsTrue(boardSizeX > 0);
            Validate.IsTrue(boardSizeY > 0);
            Validate.IsTrue(numberOfPlayers > 0);
        }

        public bool ValidateGameBoard()
        {
            return _gameBoard.ValidateGameBoard();
        }
    }
}
