using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Xml.Serialization;

namespace Foxlynx
{
    public class Level
    {

        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;

            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap tileMap;

        [XmlElement("Path")]
        public string Path;
        
        [XmlIgnore]
        public List<Tile> tiles;

        private Texture2D texture;


        private int width;
        private int height;

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public Level()
        {
            tiles = new List<Tile>();
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("atlas");

            Vector2 position = -new Vector2(World.TILE_DIMENSION, World.TILE_DIMENSION);

            foreach (string row in tileMap.Row)
            {
                string[] split = row.Split(']');
                position.X = -World.TILE_DIMENSION;
                position.Y += World.TILE_DIMENSION;
                foreach(string s in split)
                {
                    if(s != String.Empty)
                    {
                        position.X += World.TILE_DIMENSION;
                        tiles.Add(new Tile());

                        string str = s.Replace("[", String.Empty);
                        int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                        int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                        tiles[tiles.Count - 1].LoadContent(
                            position, new Rectangle(
                                value1 * World.TILE_DIMENSION,
                                value2 * World.TILE_DIMENSION,
                                World.TILE_DIMENSION,
                                World.TILE_DIMENSION));
                    }
                }
            }


            width = (int)(position.X / World.TILE_DIMENSION) + 1;
            height = (int)(position.Y / World.TILE_DIMENSION) + 1;
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in tiles)
            {
                spriteBatch.Draw(texture, tile.Position, tile.SourceRect, Color.White);
            }
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x + width * y];
        }

    }
}
