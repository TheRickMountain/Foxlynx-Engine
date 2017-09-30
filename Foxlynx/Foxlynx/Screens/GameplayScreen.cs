using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Foxlynx.Components;

using Foxlynx.ECS;

namespace Foxlynx
{
    public class GameplayScreen : GameScreen
    {
        
        
        public static Player Player;

        private World world;

        private Cat cat;

        public override void LoadContent()
        {
            base.LoadContent();

            world = World.Instance;
            world.LoadContent(content);

            Player = new Player();
            Tile tile = world.level.GetTile(1, 1);
            Player.SetPosition(tile.Position.X + World.TOTAL_DIMENSION / 2, tile.Position.Y + World.TOTAL_DIMENSION / 2);
            World.Instance.AddEntity(Player);

            cat = new Cat();
            tile = world.level.GetTile(5, 0);
            cat.SetPosition(tile.Position.X + World.TOTAL_DIMENSION / 2, tile.Position.Y + World.TOTAL_DIMENSION / 2);
            World.Instance.AddEntity(cat);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            world.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            world.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.G))
            {
                cat.SetDestTile(world.level.GetTile(10, 10));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            world.Draw(spriteBatch);
        }

    }
}
