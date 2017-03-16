using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
	public class GameField
    {
		private readonly PictureBox _pictureBox;

		private readonly Dictionary<GameFieldType, Color> _colors = new Dictionary<GameFieldType, Color>
		{
			{ GameFieldType.Road, Color.Silver },
			{ GameFieldType.Grass, Color.Green }
		};

	    public GameFieldType Type { get; }

        public void SetPictureBoxImage(Image image)
        {
            _pictureBox.Image = image;
        }

        public GameField(PictureBox pictureBox, GameFieldType type)
        {
	        _pictureBox = pictureBox;
	        Type = type;
        }

		public void Draw()
		{
			_pictureBox.BackColor = _colors[Type];
		}
    }
}
