using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Foxlynx.Components;
using Foxlynx.Pathfinding;

namespace Foxlynx.ECS
{
    class Cat : AnimatedEntity
    {

        private Tile currTile;
        private Tile nextTile;
        private Tile destTile;

        private PathAStar pathAStar;

        private float movementPerc;
        private float speed = 2f;

        public Cat() : base(ScreenManager.Instance.Content.Load<Texture2D>("cat"), 4, 3)
        {
            SetSize(Texture.Width / 3, Texture.Height / 4);
            SetOffset(0, -8);
            SwitchFrame = 150;
            AddComponent(new ColliderComponent(16, 16, 0.0f));
        }

        public override void Initialize()
        {
            currTile = destTile = nextTile = World.Instance.level.GetTile((int)(X / World.TILE_DIMENSION), (int)(Y / World.TILE_DIMENSION));
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);

            DoAnimation(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            if (currTile.Equals(destTile))
            {
                pathAStar = null;
                return;
            }

            if(nextTile.Equals(currTile))
            {
                nextTile = pathAStar.NextTile;
                Console.WriteLine(nextTile.X + " " + nextTile.Y);
            }
  
            float distToTravel = MathUtils.Distance(currTile.X, currTile.Y, nextTile.X, nextTile.Y);

            float distThisFrame = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            float percThisFrame = distThisFrame / distToTravel;

            movementPerc += percThisFrame;

            if(movementPerc >= 1)
            {
                currTile = nextTile;
                movementPerc = 0;
            }

            X = MathUtils.Lerp(currTile.Position.X, nextTile.Position.X, movementPerc) + World.TILE_DIMENSION / 2;
            Y = MathUtils.Lerp(currTile.Position.Y, nextTile.Position.Y, movementPerc) + World.TILE_DIMENSION / 2;
        }

        public void SetDestTile(Tile tile)
        {
            if(tile.MovementCost == 1.0f)
            {
                currTile = nextTile = World.Instance.level.GetTile((int)(X / World.TILE_DIMENSION), (int)(Y / World.TILE_DIMENSION));
                pathAStar = new PathAStar(currTile, tile);
                if (pathAStar.Length != -1)
                {
                    destTile = tile;
                }
            }     
        }


        private void DoAnimation(GameTime gameTime)
        {
            if (IsMoving)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;

                    if (CurrentFrame.X * FrameWidth >= Texture.Width)
                        CurrentFrame.X = 0;
                }
            }
            else
                CurrentFrame.X = 1;
        }

    }
}
