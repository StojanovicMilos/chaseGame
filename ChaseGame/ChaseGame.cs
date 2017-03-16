using System.Linq;
using System.Windows.Forms;
using Bromano.Validation;

namespace ChaseGameNamespace
{
    public class ChaseGame
    {
        private Player[] _players;
        private readonly IGameBoard _gameBoard;

        public ChaseGame(PictureBox[][] pictureBoxes, int numberOfPlayers, IGenerator generator)
        {
            ValidateInput(pictureBoxes, numberOfPlayers);

            _players = new Player[numberOfPlayers];
            _gameBoard = generator.GenerateGameBoard(pictureBoxes);
            Draw();
        }

        private void Draw()
        {
            _gameBoard.Draw();
        }

        private void ValidateInput(PictureBox[][] pictureBoxes, int numberOfPlayers)
        {
            Validate.IsTrue(numberOfPlayers > 0);
            Validate.NotNull(pictureBoxes);
            Validate.IsTrue(pictureBoxes.Length > 0);
            Validate.IsTrue(pictureBoxes.All(row => row?.Length > 0));
        }

        public bool ValidateGameBoard()
        {
            return _gameBoard.ValidateGameBoard();
        }
    }
}
