using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Foxlynx
{
    public class Tile
    {

        private Vector2 position;
        private int x, y;
        private Rectangle sourceRect;
        private bool walkable;

        public Vector2 Position
        {
           get { return position; }
        }

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public float MovementCost
        {
            get
            {
                return walkable ? 1.0f : 0.0f;
            }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect)
        {
            this.position = position;
            x = (int)(position.X / World.TILE_DIMENSION);
            y = (int)(position.Y / World.TILE_DIMENSION);
            this.sourceRect = sourceRect;
            walkable = true;
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public List<Tile> GetNeighbours()
        {
            List<Tile> neighbours = new List<Tile>();

            Level level = World.Instance.level;

            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int checkX = x + i;
                    int checkY = y + j;

                    if (checkX >= 0 && checkX < level.Width && checkY >= 0 && checkY < level.Height)
                        neighbours.Add(level.GetTile(checkX, checkY));
                }
            }

            return neighbours;
        }

    }
}
