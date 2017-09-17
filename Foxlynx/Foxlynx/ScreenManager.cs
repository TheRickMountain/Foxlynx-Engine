using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Foxlynx.Components;

namespace Foxlynx
{
    class ScreenManager
    {

        private static ScreenManager instance;
        public Vector2 Dimension { get; private set; }

        private List<Entity> entities;

        private List<ColliderComponent> colliders;

        private Player player;

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
        }

        public void LoadContent(ContentManager content)
        {
            entities = new List<Entity>();
            colliders = new List<ColliderComponent>();

            /*** Player ***/
            player = new Player(content.Load<Texture2D>("character"));
            player.SetPosition(25, 25);
            player.SetSize(32, 32);
            player.AddComponent(new ColliderComponent());

            //colliders.Add(player.GetComponent<ColliderComponent>());
            entities.Add(player);

            /*** Wall ***/
            Entity entity = new Entity(content.Load<Texture2D>("stone"));
            entity.SetPosition(250, 250);
            entity.SetSize(32, 128);
            entity.AddComponent(new ColliderComponent());

            colliders.Add(entity.GetComponent<ColliderComponent>());
            entities.Add(entity);
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();

            foreach (Entity entity in entities)
                entity.Update(gameTime);

            foreach (ColliderComponent collider in colliders)
                collider.CheckCollision(player.GetComponent<ColliderComponent>(), 1.0f);

            entities.Sort();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise);

            foreach (Entity entity in entities)
                entity.Draw(spriteBatch);

            spriteBatch.End();
        }

    }
}
