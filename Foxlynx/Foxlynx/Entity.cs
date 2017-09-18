using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Foxlynx
{
    public class Entity : IComparable<Entity>
    {
        public Texture2D Texture;
        public MyRect Body;
        public float xVelocity;
        public float yVelocity;

        private List<Component> components;

        public Entity(Texture2D texture)
        {
            Texture = texture;
            Body = new MyRect();

            components = new List<Component>();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(Body.X, Body.Y, Body.Width, Body.Height), Color.White);
        }

        public Vector2 GetPosition()
        {
            return new Vector2(Body.X, Body.Y);
        }

        public void SetPosition(int x, int y)
        {
            Body.X = x;
            Body.Y = y;
        }

        public Vector2 GetSize()
        {
            return new Vector2(Body.Width, Body.Height);
        }

        public void SetSize(int width, int height)
        {
            Body.Width = width;
            Body.Height = height;
        }

        public T GetComponent<T> () where T : Component
        {
            for(int i = 0; i < components.Count; i++)
            {
                var component = components[i];
                if (component is T)
                    return component as T;
            }

            return null;
        }

        public void AddComponent(Component component)
        {
            component.Parent = this;
            component.Initialize();
            components.Add(component);
        }

        public int CompareTo(Entity other)
        {
            if (other.Body.Y + other.Body.Height > Body.Y + Body.Height)
                return -1;
            else if (other.Body.Y + other.Body.Height < Body.Y + Body.Height)
                return 1;
            else
                return 0;
        }
    }
}
