using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Foxlynx
{
    public class Camera
    {

        Vector2 position;
        Matrix viewMatrix;

        int pixelScale = 2;

        public Camera()
        {
        }

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public int ScreenWidth
        {
            get { return (int)ScreenManager.Instance.Dimension.X; }
        }

        public int ScreenHeight
        {
            get { return (int)ScreenManager.Instance.Dimension.Y; }
        }

        public void Update(Player player)
        {
            position.X = (player.X + player.Width / 2) - (ScreenWidth / 2) / pixelScale;
            position.Y = (player.Y + player.Height / 2) - (ScreenHeight / 2) / pixelScale;

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0)) * Matrix.CreateScale(pixelScale);
        }
    }
}
