using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChaseGameNamespace
{
	public class GameField
    {

		private readonly Dictionary<GameFieldType, Color> _colors = new Dictionary<GameFieldType, Color>
		{
			{ GameFieldType.Road, Color.Silver },
			{ GameFieldType.Grass, Color.Green }
		};

	    public GameFieldType Type { get; }

        public GameField(GameFieldType type)
        {
	        Type = type;
        }
    }
}
