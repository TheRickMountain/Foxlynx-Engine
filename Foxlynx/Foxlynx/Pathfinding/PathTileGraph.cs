using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxlynx.Pathfinding
{
    public class PathTileGraph
    {

        public Dictionary<Tile, Node<Tile>> nodes;

        public PathTileGraph(Level level)
        {
            nodes = new Dictionary<Tile, Node<Tile>>();

            for(int x = 0; x < level.Width; x++)
            {
                for(int y = 0; y < level.Height; y++)
                {
                    Tile t = level.GetTile(x, y);

                    Node<Tile> n = new Node<Tile>();
                    n.data = t;
                    nodes.Add(t, n);
                }
            }
        }

    }
}
