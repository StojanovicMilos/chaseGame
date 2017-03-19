using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public partial class Form1 : Form
    {
        private PictureBox[][] _pictureBoxes;
        public Form1()
        {
            InitializeComponent();
            InitializePictureBoxes();
        }

        private void InitializePictureBoxes()
        {
            int boardSizeX = 80;
            int boardSizeY = 45;
            Point startingPoint = new Point(0, 0);
            _pictureBoxes = new PictureBox[boardSizeX][];
            for (int i = 0; i < _pictureBoxes.Length; i++)
            {
                _pictureBoxes[i] = new PictureBox[boardSizeY];

                for (int j = 0; j < _pictureBoxes[i].Length; j++)
                {
                    _pictureBoxes[i][j] = new PictureBox();
                    Controls.Add(_pictureBoxes[i][j]);
                    _pictureBoxes[i][j].Location = new Point(startingPoint.X + (i * 20), startingPoint.Y + (j * 20));
                    _pictureBoxes[i][j].BackColor = Color.Black;
                    _pictureBoxes[i][j].Visible = true;
                    _pictureBoxes[i][j].Size = new Size(20, 20);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            const int numberOfPlayers = 5;
            IGenerator generator;
            if (radioButton1.Checked)
                generator = new StaticGenerator();
            else if (radioButton2.Checked)
                generator = new RandomGenerator(new TextLogger());
            else
                generator = null;
            ChaseGame chaseGame = new ChaseGame(_pictureBoxes.Length, _pictureBoxes[0].Length, numberOfPlayers, generator);
            DrawChaseGame(chaseGame);
            Text = chaseGame.ValidateGameBoard() ? "Valid" : "Invalid";
        }

        private void DrawChaseGame(ChaseGame chaseGame)
        {
            IGameBoard gameBoard = chaseGame.GetGameBoard();
            for (int x = 0; x < gameBoard.LengthX; x++)
            {
                for (int y = 0; y < gameBoard.LengthY; y++)
                {
                    if (gameBoard.FieldIsType(x, y, GameFieldType.Grass))
                    {
                        _pictureBoxes[x][y].Image = Properties.Resources.grass;
                    }
                    else
                    {
                        StringBuilder imagePath = new StringBuilder("..\\..\\Resources\\road");
                        if (gameBoard.TopNeighbourIsType(x, y, GameFieldType.Road))
                        {
                            imagePath.Append("top");
                        }
                        if (gameBoard.RightNeighbourIsType(x, y, GameFieldType.Road))
                        {
                            imagePath.Append("right");
                        }
                        if (gameBoard.BottomNeighbourIsType(x, y, GameFieldType.Road))
                        {
                            imagePath.Append("bottom");
                        }
                        if (gameBoard.LeftNeighbourIsType(x, y, GameFieldType.Road))
                        {
                            imagePath.Append("left");
                        }
                        imagePath.Append(".bmp");
                        Image image = new Bitmap(imagePath.ToString());
                        _pictureBoxes[x][y].Image = image;
                    }
                }
            }
        }
    }
}
