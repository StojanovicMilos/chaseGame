namespace DataStructures.Graphs
{
    public interface IWeightedGraph<TVertex> : IGraph<TVertex>
    {
        double GetEdgeWeight(TVertex source, TVertex target);
    }
}