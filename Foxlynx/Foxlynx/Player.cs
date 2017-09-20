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

        private int speed = 100;

        private bool isMoving = false;

        public Player() : base(ScreenManager.Instance.Content.Load<Texture2D>("character"), 4, 6)
        {
            SetSize(Texture.Width / 6, Texture.Height / 4);
            SetOffset(0, -12);
            AddComponent(new ColliderComponent(16, 8));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int xaxis = 0;
            int yaxis = 0;

            if (InputManager.Instance.KeyDown(Keys.W))
            {
                yaxis = -1;
                CurrentFrame.Y = 3;
            }
            else if(InputManager.Instance.KeyDown(Keys.S))
            {
                yaxis = 1;
                CurrentFrame.Y = 0;
            }

            if (InputManager.Instance.KeyDown(Keys.A))
            {
                xaxis = -1;
                CurrentFrame.Y = 1;
            }
            else if (InputManager.Instance.KeyDown(Keys.D))
            {
                xaxis = 1;
                CurrentFrame.Y = 2;
            }

            float direction = (float)MathUtils.Direction(0, 0, xaxis, yaxis);
            float length = 0;

            if (xaxis != 0 || yaxis != 0)
            {
                length = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                isMoving = true;
            }
            else
                isMoving = false;

            VelocityX = (float)MathUtils.getLengthdirX(length, direction);
            VelocityY = (float)MathUtils.getLengthdirY(length, direction);

            X += (int)VelocityX;
            Y += (int)VelocityY;

            if (isMoving)
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
