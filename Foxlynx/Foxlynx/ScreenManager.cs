using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Foxlynx.Components;

namespace Foxlynx
{
    public class ScreenManager
    {

        private static ScreenManager instance;
        public Vector2 Dimension { get; private set; }
        public ContentManager Content { get; private set; }
        XmlManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen, newScreen;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }

        public ScreenManager()
        {
            Dimension = new Vector2(1280, 720);
            currentScreen = new SplashScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.GetType();
            currentScreen = xmlGameScreenManager.Load("Load/SplashScreen.xml");
        }

        public void ChangeScreen(string name)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("Foxlynx." + name));
            currentScreen.UnloadContent();
            currentScreen = newScreen;
            xmlGameScreenManager.Type = currentScreen.GetType();
            if(File.Exists(currentScreen.XmlPath))
                currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
            currentScreen.LoadContent();
        }

        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

    }
}
