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
    public class Image
    {

        private Texture2D texture;
        public int offsetX, offsetY;
        private int width, height;

        private int column, row;

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }

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

        public int Column
        {
            get
            {
                return column;
            }
        }

        public int Row
        {
            get
            {
                return row;
            }
        }

        public Image(string path, int column, int row)
        {
            texture = ScreenManager.Instance.Content.Load<Texture2D>(path);
            width = texture.Width;
            height = texture.Height;
            this.column = column;
            this.row = row;
        }

        public Image SetOffset(int x, int y)
        {
            offsetX = x;
            offsetY = y;
            return this;
        }

    }
}
