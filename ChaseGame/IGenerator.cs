using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public interface IGenerator
    {
        IGameBoard GenerateGameBoard(PictureBox[][] pictureBoxes);
    }
}