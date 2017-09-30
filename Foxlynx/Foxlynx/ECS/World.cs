using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Foxlynx.Components;
using Foxlynx.Pathfinding;

namespace Foxlynx
{
    public class World
    {

        private static World instance;

        public const int SPRITE_DIMENSION = 16;
        public const int PIXEL_SIZE = 3;
        public const int TOTAL_DIMENSION = SPRITE_DIMENSION * PIXEL_SIZE;

        public Camera camera;
        public Level level;
        public PathTileGraph tileGraph;

        private List<Entity> entities;
        private List<ColliderComponent> colliders;

        public static World Instance
        {
            get
            {
                if (instance == null)
                    instance = new World();

                return instance;
            }
        }


        public void LoadContent(ContentManager content)
        {
            entities = new List<Entity>();
            colliders = new List<ColliderComponent>();

            XmlManager<Level> levelLoader = new XmlManager<Level>();
            level = levelLoader.Load("Load/Map/Level1.xml");
            level.LoadContent(content);

            tileGraph = new PathTileGraph(level);

            entities = new List<Entity>();
            colliders = new List<ColliderComponent>();

            camera = new Camera();
        }

        public void UnloadContent()
        {
            level.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity entity in entities)
                entity.Update(gameTime);

            foreach (Entity entity in entities)
                foreach (ColliderComponent collider in colliders)
                    if (!entity.Equals(collider.Parent))
                        collider.CheckCollision(entity.GetComponent<ColliderComponent>());

            level.Update(gameTime);

            camera.Update(GameplayScreen.Player);

            entities.Sort();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);

            level.Draw(spriteBatch);

            foreach (Entity entity in entities)
                entity.Draw(spriteBatch);

            spriteBatch.End();
        }


        public void AddEntity(Entity entity)
        {
            entity.Initialize();
            if (entity.HasComponent<ColliderComponent>())
            {
                if (!entity.Equals(GameplayScreen.Player))
                    colliders.Add(entity.GetComponent<ColliderComponent>());
            }

            entities.Add(entity);
        }


    }
}
