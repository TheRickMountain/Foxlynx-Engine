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

        Vector2 position;
        Rectangle sourceRect;

        public Vector2 Position
        {
            get { return position; }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect)
        {
            this.position = position;
            this.sourceRect = sourceRect;
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

    }
}
