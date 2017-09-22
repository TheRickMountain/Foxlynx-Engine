using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Foxlynx.Components;

namespace Foxlynx
{
    public class Player : AnimatedEntity
    {

        private int speed = 50;

        public Player() : base(ScreenManager.Instance.Content.Load<Texture2D>("character"), 4, 6)
        {
            SetSize(Texture.Width / 6, Texture.Height / 4);
            SetOffset(0, -8);
            AddComponent(new ColliderComponent(10, 6, 0.0f));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            doMovement(gameTime);

            doAnimation(gameTime);
        }

        private void doMovement(GameTime gameTime)
        {
            int upKey = InputManager.Instance.KeyDown(Keys.W) ? 1 : 0;
            int downKey = InputManager.Instance.KeyDown(Keys.S) ? 1 : 0;
            int leftKey = InputManager.Instance.KeyDown(Keys.A) ? 1 : 0;
            int rightKey = InputManager.Instance.KeyDown(Keys.D) ? 1 : 0;

            int xaxis = rightKey - leftKey;
            int yaxis = downKey - upKey;

            float direction = (float)MathUtils.Direction(0, 0, xaxis, yaxis);
            float length = 0;

            if (xaxis != 0 || yaxis != 0)
            {
                length = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                IsMoving = true;
            }
            else
                IsMoving = false;

            VelocityX = (float)MathUtils.getLengthdirX(length, direction);
            VelocityY = (float)MathUtils.getLengthdirY(length, direction);

            X += VelocityX;
            Y += VelocityY;

            if (yaxis < 0)
                CurrentFrame.Y = 3;
            else if (yaxis > 0)
                CurrentFrame.Y = 0;

            if (xaxis < 0)
                CurrentFrame.Y = 1;
            else if (xaxis > 0)
                CurrentFrame.Y = 2;
        }

        private void doAnimation(GameTime gameTime)
        {
            if (IsMoving)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;

                    if (CurrentFrame.X * FrameWidth >= Texture.Width)
                        CurrentFrame.X = 0;
                }
            }
            else
                CurrentFrame.X = 0;
        }

    }
}
