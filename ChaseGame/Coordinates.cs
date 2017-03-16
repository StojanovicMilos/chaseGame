namespace ChaseGameNamespace
{
	public class Coordinates
	{
		public int X { get; }

		public int Y { get; }

		public Coordinates(int x, int y)
		{
			Y = y;
			X = x;
		}

		public override bool Equals(object obj)
		{
			Coordinates item = obj as Coordinates;

			return (X == item?.X) && (Y == item?.Y);
		}

		public override int GetHashCode()
		{
			int hash = 13;
			hash = (hash * 7) + X.GetHashCode();
			hash = (hash * 7) + Y.GetHashCode();

			return hash;
		}

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }
    }
}
