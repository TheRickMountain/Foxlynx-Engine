using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Foxlynx.Components;

namespace Foxlynx
{
    public class GameplayScreen : GameScreen
    {

        private List<Entity> entities;

        private List<ColliderComponent> colliders;

        private Camera camera;
        private Player player;

        private SpriteFont font;

        public override void LoadContent()
        {
            base.LoadContent();

            entities = new List<Entity>();
            colliders = new List<ColliderComponent>();

            camera = new Camera();

            font = content.Load<SpriteFont>("Title");

            /*** Player ***/
            player = new Player(content.Load<Texture2D>("character"));
            player.SetPosition(25, 25);
            player.SetSize(32, 32);
            player.AddComponent(new ColliderComponent());
            entities.Add(player);

            /*** Wall ***/
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++)
                {
                    Entity entity = new Entity(content.Load<Texture2D>("stone"));
                    entity.SetPosition(i * 32, j * 32);
                    entity.SetSize(32, 32);
                    entities.Add(entity);
                }
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(InputManager.Instance.GetX() + " " + InputManager.Instance.GetY());

            foreach (Entity entity in entities)
                entity.Update(gameTime);

            foreach (ColliderComponent collider in colliders) {
                collider.CheckCollision(player.GetComponent<ColliderComponent>(), 1.0f);
            }

            camera.Update(player);

            entities.Sort();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);

            foreach (Entity entity in entities)
                entity.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Title", new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Hello, World", new Vector2(font.MeasureString("Title").X,
                 font.MeasureString("Title").Y), Color.Red);

            spriteBatch.End();
        }

    }
}
