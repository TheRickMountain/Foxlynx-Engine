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
    class Player : Entity
    {

        private int speed = 100;

        public Player(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int upKey = InputManager.Instance.KeyDown(Keys.W) ? 1 : 0;
            int downKey = InputManager.Instance.KeyDown(Keys.S) ? 1 : 0;
            int leftKey = InputManager.Instance.KeyDown(Keys.A) ? 1 : 0;
            int rightKey = InputManager.Instance.KeyDown(Keys.D) ? 1 : 0;

            int xaxis = (rightKey - leftKey);
            int yaxis = (downKey - upKey);

            float direction = (float)MathUtils.Direction(0, 0, xaxis, yaxis);
            float length = 0;

            if (xaxis != 0 || yaxis != 0)
                length = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Velocity.X = (float)MathUtils.getLengthdirX(length, direction);
            Velocity.Y = (float)MathUtils.getLengthdirY(length, direction);

            Body.X += (int)Velocity.X;
            Body.Y += (int)Velocity.Y;
        }

    }
}
