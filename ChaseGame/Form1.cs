using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public partial class Form1 : Form
    {
        private PictureBox[,] pictureBoxes;
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
            pictureBoxes = new PictureBox[boardSizeX, boardSizeY];
            for (int i = 0; i < pictureBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                {
                    pictureBoxes[i, j] = new PictureBox();
                    this.Controls.Add(pictureBoxes[i, j]);
                    pictureBoxes[i, j].Location = new Point(startingPoint.X + (i * 20), startingPoint.Y + (j * 20));
                    pictureBoxes[i, j].BackColor = Color.Black;
                    pictureBoxes[i, j].Visible = true;
                    pictureBoxes[i, j].Size = new Size(20, 20);
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numberOfPlayers = 5;
            ChaseGame chaseGame = new ChaseGame(pictureBoxes, numberOfPlayers);
        }
    }
}
