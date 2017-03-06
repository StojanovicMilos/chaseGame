using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
    public abstract class GameField
    {
        protected PictureBox pictureBox;
        public static readonly int fieldSize = 20;
        protected GameField(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }
        public abstract void Draw();
        protected abstract Color GetFieldColor();
    }

    public class RoadGameField : GameField
    {
        public RoadGameField(PictureBox pictureBox) : base(pictureBox) { }
        protected override Color GetFieldColor()
        {
            return Color.Silver;
        }
        public override void Draw()
        {
            pictureBox.BackColor = GetFieldColor();
        }
    }

    public class GrassGameField : GameField
    {
        public GrassGameField(PictureBox pictureBox) : base(pictureBox) { }
        protected override Color GetFieldColor()
        {
            return Color.Green;
        }
        public override void Draw()
        {
            pictureBox.BackColor = GetFieldColor();
        }

    }
}
