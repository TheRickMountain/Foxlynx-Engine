﻿using System;
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
            if (InputManager.Instance.KeyPressed(Keys.Enter))
                ScreenManager.Instance.ChangeScreen("GameplayScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(image, new Vector2(ScreenManager.Instance.Dimension.X / 2 - image.Width / 2,
                ScreenManager.Instance.Dimension.Y / 2 - image.Height / 2), Color.White);

            spriteBatch.End();
        }

    }
}
