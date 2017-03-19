using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public partial class Form1 : Form
    {
        private PictureBox[][] _gameBoardPictureBoxes;
        private PictureBox[] _playerPictureBoxes;
        public Form1()
        {
            InitializeComponent();
            InitializeGameBoardPictureBoxes();
        }

        private void InitializeGameBoardPictureBoxes()
        {
            int boardSizeX = 80;
            int boardSizeY = 45;
            Point startingPoint = new Point(0, 0);
            _gameBoardPictureBoxes = new PictureBox[boardSizeX][];
            for (int i = 0; i < _gameBoardPictureBoxes.Length; i++)
            {
                _gameBoardPictureBoxes[i] = new PictureBox[boardSizeY];

                for (int j = 0; j < _gameBoardPictureBoxes[i].Length; j++)
                {
                    _gameBoardPictureBoxes[i][j] = new PictureBox();
                    Controls.Add(_gameBoardPictureBoxes[i][j]);
                    _gameBoardPictureBoxes[i][j].Location = new Point(startingPoint.X + (i * 20), startingPoint.Y + (j * 20));
                    _gameBoardPictureBoxes[i][j].Visible = true;
                    _gameBoardPictureBoxes[i][j].Size = new Size(20, 20);
                }
            }
        }

        private void InitializePlayerPictureBoxes(Player[] players)
        {
            _playerPictureBoxes = new PictureBox[players.Length];
            for (int i = 0; i < _playerPictureBoxes.Length; i++)
            {
                _playerPictureBoxes[i] = new PictureBox();
                Controls.Add(_playerPictureBoxes[i]);
                _playerPictureBoxes[i].Visible = true;
                _playerPictureBoxes[i].BackColor = Color.Transparent;
                _playerPictureBoxes[i].Image = Properties.Resources.car;
                _playerPictureBoxes[i].Size = new Size(16, 16);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            const int numberOfPlayers = 3;
            IGenerator generator;
            if (radioButton1.Checked)
                generator = new StaticGenerator();
            else if (radioButton2.Checked)
                generator = new RandomGenerator(new DummyLogger());//new TextLogger());
            else
                generator = null;
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player();
            }
            InitializePlayerPictureBoxes(players);
            ChaseGame chaseGame = new ChaseGame(_gameBoardPictureBoxes.Length, _gameBoardPictureBoxes[0].Length, players, generator);
            
            DrawChaseGame(chaseGame, players);
            Text = chaseGame.ValidateGameBoard() ? "Valid" : "Invalid";
        }

        private void DrawChaseGame(ChaseGame chaseGame, Player[] players)
        {
            IGameBoard gameBoard = chaseGame.GetGameBoard();
            for (int x = 0; x < gameBoard.LengthX; x++)
            {
                for (int y = 0; y < gameBoard.LengthY; y++)
                {
                    if (gameBoard[x, y].StateChanged)
                    {
                        gameBoard[x, y].StateChanged = false;
                        if (gameBoard.FieldIsType(x, y, GameFieldType.Grass))
                        {
                            _gameBoardPictureBoxes[x][y].Image = Properties.Resources.grass;
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
                            _gameBoardPictureBoxes[x][y].Image = image;
                        }
                    }
                }
            }

            Point startingPoint = new Point(0, 0);
            for (int i = 0; i < players.Length; i++)
            {
                int x = chaseGame.GetPlayerPositionX(players[i]);
                int y = chaseGame.GetPlayerPositionY(players[i]);
                _playerPictureBoxes[i].Location = new Point(startingPoint.X + (x * 20), startingPoint.Y + (y * 20));
                _playerPictureBoxes[i].BringToFront();
            }
        }
    }
}
