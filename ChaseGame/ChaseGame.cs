using System.Linq;
using System.Windows.Forms;
using Bromano.Validation;

namespace ChaseGameNamespace
{
    public class ChaseGame
    {
        private Player[] _players;
        private readonly GameBoard _gameBoard;
       
        public ChaseGame(PictureBox[][] pictureBoxes, int numberOfPlayers)
		{
			ValidateInput(pictureBoxes, numberOfPlayers);

			_players = new Player[numberOfPlayers];
			_gameBoard = new GameBoard(pictureBoxes);
		}

		private void ValidateInput(PictureBox[][] pictureBoxes, int numberOfPlayers)
		{
			Validate.IsTrue(numberOfPlayers > 0);
			Validate.NotNull(pictureBoxes);
			Validate.IsTrue(pictureBoxes.Length > 0);
			Validate.IsTrue(pictureBoxes.All(row => row?.Length > 0));
		}

		public bool CheckGameBoard()
        {
            return _gameBoard.ValidateGameBoard();
        }
    }
}
