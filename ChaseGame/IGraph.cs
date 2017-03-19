using System.Collections.Generic;

namespace DataStructures.Graphs
{
    public interface IGraph<TVertex>
    {
        int VertexCount { get; }

        IEnumerable<TVertex> GetVertices();

        bool ContainsVertex(TVertex vertex);

        IEnumerable<TVertex> GetNeighbours(TVertex vertex);
    }
}