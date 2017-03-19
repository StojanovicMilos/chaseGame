namespace ChaseGameNamespace
{
    public class GameField
    {
        public bool StateChanged { get; set; }

        private Player _player;

        public Player GetPlayer()
        {
            return _player;
        }

        public void SetPlayer(Player value)
        {
            _player = value;
            StateChanged = true;
        }

        public GameFieldType Type { get; }

        public GameField(GameFieldType type)
        {
            _player = null;
            Type = type;
            StateChanged = true; //gamefield needs to be drawn after this
        }
    }
}
