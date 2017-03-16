using System;
using System.Drawing;
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

        private void button1_Click(object sender, EventArgs e)
        {
            const int numberOfPlayers = 5;
            IGenerator generator;
            if (radioButton1.Checked)
                generator = new StaticGenerator();
            else if (radioButton2.Checked)
                generator = new RandomGenerator(new TextLogger());
            else
                generator = null;
            ChaseGame chaseGame = new ChaseGame(_pictureBoxes, numberOfPlayers, generator);
            Text = chaseGame.ValidateGameBoard() ? "Valid" : "Invalid";
        }
    }
}
