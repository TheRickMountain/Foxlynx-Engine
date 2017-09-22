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
        public Vector2 CurrentFrame;
        public Vector2 AmountOfFrames;
        public bool IsMoving;

        public int FrameWidth
        {
            get
            {
                return Texture.Width / (int)AmountOfFrames.X;
            }
        }

        public int FrameHeight
        {
            get
            {
                return Texture.Height / (int)AmountOfFrames.Y;
            }
        }

        public AnimatedEntity(Texture2D texture, int rows, int columns) : base(texture)
        {
            AmountOfFrames = new Vector2(columns, rows);
            CurrentFrame = new Vector2(0, 0);
            SwitchFrame = 100;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)X - Width / 2 + OffsetX, (int)Y - Height / 2 + OffsetY, Width, Height),
                    new Rectangle((int)CurrentFrame.X * FrameWidth, (int)CurrentFrame.Y * FrameHeight, FrameWidth, FrameHeight),
                    Color.White);
            }
        }

    }
}
