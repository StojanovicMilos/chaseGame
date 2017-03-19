using Bromano.Validation;

namespace ChaseGameNamespace
{
    public class ChaseGame
    {
        private readonly IGameBoard _gameBoard;

        public IGameBoard GetGameBoard()
        {
            return _gameBoard;
        }

        public ChaseGame(int boardSizeX, int boardSizeY, Player[] players, IGenerator generator)
        {
            ValidateInput(boardSizeX, boardSizeY, players);
            _gameBoard = generator.GenerateGameBoard(boardSizeX, boardSizeY, players);
        }

        private void ValidateInput(int boardSizeX, int boardSizeY, Player[] players)
        {
            Validate.IsTrue(boardSizeX > 0);
            Validate.IsTrue(boardSizeY > 0);
            Validate.NotNull(players);
            foreach (Player player in players)
                Validate.NotNull(player);
        }

        public bool ValidateGameBoard()
        {
            return _gameBoard.ValidateGameBoard();
        }

        public void PlayMove(Player player, int oldX, int oldY, int x, int y)
        {
            _gameBoard[oldX, oldY].SetPlayer(null);
            _gameBoard[x, y].SetPlayer(player);
        }

        internal int GetPlayerPositionX(Player player)
        {
            return _gameBoard.GetPlayerPositionX(player);
        }

        internal int GetPlayerPositionY(Player player)
        {
            return _gameBoard.GetPlayerPositionY(player);
        }
    }
}
