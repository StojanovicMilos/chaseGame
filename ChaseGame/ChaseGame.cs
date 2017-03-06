using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public class ChaseGame
    {
        private Player[] players;
        private GameBoard gameBoard;
       
        public ChaseGame(PictureBox[,] pictureBoxes, int numberOfPlayers)
        {
            if ((numberOfPlayers <= 0) || (pictureBoxes.GetLength(0) <= 0) || (pictureBoxes.GetLength(1) <= 0) || (pictureBoxes == null))
                throw new ArgumentException();

            players = new Player[numberOfPlayers];
            gameBoard = new GameBoard(pictureBoxes);
        }

        public bool CheckGameBoard()
        {
            return gameBoard.checkGameBoard();
        }
    }
}
