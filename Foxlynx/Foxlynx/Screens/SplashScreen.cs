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
    public class SplashScreen : GameScreen
    {

        Texture2D image;
        public string Path;
        private float alpha = 1.0f;
        private bool transition = false;

        public override void LoadContent()
        {
            base.LoadContent();
            image = content.Load<Texture2D>(Path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Enter) || InputManager.Instance.LeftButtonPressed())
            {
                //transition = true;
            //}

            //if(transition)
            //{
               // alpha -= (float)gameTime.ElapsedGameTime.TotalSeconds;

               // if (alpha <= 0)
                //{
                    ScreenManager.Instance.ChangeScreen("GameplayScreen");
                //}
            } 
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(image, new Vector2(ScreenManager.Instance.Dimension.X / 2 - image.Width / 2,
                ScreenManager.Instance.Dimension.Y / 2 - image.Height / 2), Color.White * alpha);

            spriteBatch.End();
        }

    }
}
