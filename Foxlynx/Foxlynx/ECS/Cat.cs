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

        public Cat() : base(new Image("cat", 3, 4).SetOffset(0, -8 * World.PIXEL_SIZE))
        {
            SetSize((image.Width / 3) * World.PIXEL_SIZE, (image.Height / 4) * World.PIXEL_SIZE);
            SwitchFrame = 150;
            DefaultFrame = 1;
            AddComponent(new ColliderComponent(16 * World.PIXEL_SIZE, 16 * World.PIXEL_SIZE, 0.0f));
        }

        public override void Initialize()
        {
            currTile = destTile = nextTile = World.Instance.level.GetTile((int)(X / World.TOTAL_DIMENSION), (int)(Y / World.TOTAL_DIMENSION));
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
                IsMoving = false;
                return;
            }

            if(nextTile.Equals(currTile))
                nextTile = pathAStar.NextTile;
  
            float distToTravel = MathUtils.Distance(currTile.X, currTile.Y, nextTile.X, nextTile.Y);

            float distThisFrame = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            float percThisFrame = distThisFrame / distToTravel;

            movementPerc += percThisFrame;

            int direction = (int)MathUtils.Direction(currTile.X, currTile.Y, nextTile.X, nextTile.Y);

            switch(direction)
            {
                case 315:
                case 0:
                case 45:
                    CurrentFrame.Y = 2;
                    break;
                case 90:
                    CurrentFrame.Y = 0;
                    break;
                case 135:
                case 180:
                case 225:
                    CurrentFrame.Y = 1;
                    break;
                case 270:
                    CurrentFrame.Y = 3;
                    break;
            }

            if (movementPerc >= 1)
            {
                currTile = nextTile;
                movementPerc = 0;
            }

            X = MathUtils.Lerp(currTile.Position.X, nextTile.Position.X, movementPerc) + World.TOTAL_DIMENSION / 2;
            Y = MathUtils.Lerp(currTile.Position.Y, nextTile.Position.Y, movementPerc) + World.TOTAL_DIMENSION / 2;

        }

        public void SetDestTile(Tile tile)
        {
            if(tile.MovementCost == 1.0f)
            {
                currTile = nextTile = World.Instance.level.GetTile((int)(X / World.TOTAL_DIMENSION), (int)(Y / World.TOTAL_DIMENSION));
                pathAStar = new PathAStar(currTile, tile);
                if (pathAStar.Length != -1)
                {
                    destTile = tile;
                    IsMoving = true;
                }
                else
                {
                    pathAStar = null;
                }
            }     
        }

    }
}
