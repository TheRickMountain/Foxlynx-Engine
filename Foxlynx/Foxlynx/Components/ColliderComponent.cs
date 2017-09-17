using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Foxlynx.Components
{
    public class ColliderComponent : Component
    {

        private MyRect Body;

        public override void Initialize()
        {
            Body = Parent.Body;
        }

        public bool CheckCollision(ColliderComponent other, float push)
        {
            Vector2 otherPosition = other.getPosition();
            Vector2 otherHalfSize = other.getHalfSize();
            Vector2 thisPosition = getPosition();
            Vector2 thisHalfSize = getHalfSize();

            float deltaX = otherPosition.X - thisPosition.X;
            float deltaY = otherPosition.Y - thisPosition.Y;

            float intersectX = Math.Abs(deltaX) - (otherHalfSize.X + thisHalfSize.X);
            float intersectY = Math.Abs(deltaY) - (otherHalfSize.Y + thisHalfSize.Y);

            if (intersectX < 0.0f && intersectY < 0.0f)
            {
                push = Math.Min(Math.Max(push, 0.0f), 1.0f);

                if (intersectX > intersectY)
                {
                    if (deltaX > 0.0f)
                    {
                        move(intersectX * (1.0f - push), 0.0f);
                        other.move(-intersectX * push, 0.0f);
                    }
                    else
                    {
                        move(-intersectX * (1.0f - push), 0.0f);
                        other.move(intersectX * push, 0.0f);
                    }
                }
                else
                {
                    if (deltaY > 0.0f)
                    {
                        move(0.0f, intersectY * (1.0f - push));
                        other.move(0.0f, -intersectY * push);
                    }
                    else
                    {
                        move(0.0f, -intersectY * (1.0f - push));
                        other.move(0.0f, intersectY * push);
                    }
                }

                return true;
            }

            return false;
        }

        private void move(float dx, float dy)
        {
            Body.X += (int)dx;
            Body.Y += (int)dy;
        }

        private Vector2 getPosition()
        {
            return new Vector2(Body.X + Body.Width / 2, Body.Y + Body.Height / 2);
        }

        private Vector2 getHalfSize()
        {
            return new Vector2(Body.Width / 2.0f, Body.Height / 2.0f);
        }

    }
}
