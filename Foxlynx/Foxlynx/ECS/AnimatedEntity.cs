using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Foxlynx
{
    public class AnimatedEntity : Entity
    {

        public int FrameCounter;
        public int SwitchFrame;
        public int DefaultFrame;
        public Vector2 CurrentFrame;
        public Vector2 AmountOfFrames;
        public bool IsMoving;

        public int FrameWidth
        {
            get
            {
                return image.Width / (int)AmountOfFrames.X;
            }
        }

        public int FrameHeight
        {
            get
            {
                return image.Height / (int)AmountOfFrames.Y;
            }
        }

        public AnimatedEntity(Image image) : base(image)
        {
            AmountOfFrames = new Vector2(image.Column, image.Row);
            CurrentFrame = Vector2.Zero;
            SwitchFrame = 100;
        }

        public override void Update(GameTime gameTime)
        {
            DoAnimation(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(image.Texture, new Rectangle(
                    (int)X - image.offsetX, 
                    (int)Y - image.offsetY,
                    Width, Height),
                    new Rectangle((int)CurrentFrame.X * FrameWidth, (int)CurrentFrame.Y * FrameHeight, FrameWidth, FrameHeight),
                    Color.White);
            }
        }

        public override void SetImage(Image image)
        {
            base.SetImage(image);
            AmountOfFrames.X = image.Column;
            AmountOfFrames.Y = image.Row;
            SetSize(FrameWidth * World.PIXEL_SIZE, FrameHeight * World.PIXEL_SIZE);
        }

        public void DoAnimation(GameTime gameTime)
        {
            if (IsMoving)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;

                    if (CurrentFrame.X * FrameWidth >= image.Width)
                        CurrentFrame.X = 0;
                }
            }
            else
                CurrentFrame.X = DefaultFrame;
        }

    }
}
