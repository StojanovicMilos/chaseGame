namespace ChaseGameNamespace
{
    public interface IGenerator
    {
        IGameBoard GenerateGameBoard(int boardSizeX, int boardSizeY, Player[] players);
    }
}