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
    public class Layer
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
        public TileMap Tile;

        [XmlElement("Path")]
        public string Path;

        List<Tile> tiles;

        Texture2D texture;

        public Layer()
        {
            tiles = new List<Tile>();
        }

        public void LoadContent(ContentManager content, Vector2 tileDimension)
        {
            texture = content.Load<Texture2D>("atlas");

            Vector2 position = -tileDimension;

            foreach (string row in Tile.Row)
            {
                string[] split = row.Split(']');
                position.X = -tileDimension.X;
                position.Y += tileDimension.Y;
                foreach(string s in split)
                {
                    if(s != String.Empty)
                    {
                        position.X += tileDimension.X;
                        tiles.Add(new Tile());

                        string str = s.Replace("[", String.Empty);
                        int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                        int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                        tiles[tiles.Count - 1].LoadContent(
                            position, new Rectangle(
                                value1 * (int)tileDimension.X,
                                value2 * (int)tileDimension.Y, 
                                (int) tileDimension.X, 
                                (int) tileDimension.Y));
                    }
                }
            }
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

    }
}
