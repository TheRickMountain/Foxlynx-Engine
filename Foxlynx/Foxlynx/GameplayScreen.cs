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
        private Map map;

        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Map> mapLoader = new XmlManager<Map>();
            map = mapLoader.Load("Load/Map/Map1.xml");
            map.LoadContent(content);

            entities = new List<Entity>();
            colliders = new List<ColliderComponent>();

            camera = new Camera();

            player = new Player();
            player.SetPosition(16, 28);
            entities.Add(player);

            Entity wolf = new Entity(content.Load<Texture2D>("wolf"));
            wolf.SetPosition(192 + 19, 128 + 16);
            wolf.SetSize(32, 32);
            entities.Add(wolf);

            Entity entity = new Entity(content.Load<Texture2D>("tree"));
            entity.SetPosition(128 + 16, 128 + 16);
            entity.SetSize(96, 194);
            entity.SetOffset(0, -80);
            entity.AddComponent(new ColliderComponent(32, 32));
            colliders.Add(entity.GetComponent<ColliderComponent>());
            entities.Add(entity);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Entity entity in entities)
                entity.Update(gameTime);

            foreach (ColliderComponent collider in colliders) {
                collider.CheckCollision(player.GetComponent<ColliderComponent>(), 1.0f);
            }

            map.Update(gameTime);

            camera.Update(player);

            entities.Sort();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);

            map.Draw(spriteBatch);

            foreach (Entity entity in entities)
                entity.Draw(spriteBatch);

            spriteBatch.End();
        }

    }
}
