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

        private AnimatedEntity shadow;

        public Player() : base(ScreenManager.Instance.Content.Load<Texture2D>("character"), 4, 6)
        {
            SetSize(Texture.Width / 6, Texture.Height / 4);
            SetOffset(0, -8);
            AddComponent(new ColliderComponent(16, 6));

            shadow = new AnimatedEntity(ScreenManager.Instance.Content.Load<Texture2D>("shadow"), 1, 6);
            shadow.SetSize(shadow.Texture.Width / 6, shadow.Texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            shadow.SetPosition(X, Y - 8);
            if(isMoving)
            {
                shadow.FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(shadow.FrameCounter >= shadow.SwitchFrame)
                {
                    shadow.FrameCounter = 0;
                    shadow.CurrentFrame.X++;

                    if (shadow.CurrentFrame.X * shadow.FrameWidth >= shadow.Texture.Width)
                        shadow.CurrentFrame.X = 0;
                }
            }
            else
            {
                shadow.CurrentFrame.X = 0;
            }

            doMovement(gameTime);

            doAnimation(gameTime);
        }

        private void doMovement(GameTime gameTime)
        {
            int xaxis = 0;
            int yaxis = 0;

            if (InputManager.Instance.KeyDown(Keys.W))
            {
                yaxis = -1;
                CurrentFrame.Y = 3;
            }
            else if (InputManager.Instance.KeyDown(Keys.S))
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
        }

        private void doAnimation(GameTime gameTime)
        {
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            shadow.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

    }
}
