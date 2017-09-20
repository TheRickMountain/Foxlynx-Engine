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
    public class Entity : IComparable<Entity>
    {
        public Texture2D Texture;
        public int X, Y;
        public int OffsetX, OffsetY;
        public int Width, Height;
        public float VelocityX, VelocityY;

        private List<Component> components;

        public Entity(Texture2D texture)
        {
            Texture = texture;
            components = new List<Component>();
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(X - Width / 2 + OffsetX, Y - Height / 2 + OffsetY, Width, Height), 
                Color.White);
        }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetOffset(int x, int y)
        {
            OffsetX = x;
            OffsetY = y;
        }

        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
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

        public bool HasComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                var component = components[i];
                if (component is T)
                    return true;
            }

            return false;
        }

        public int CompareTo(Entity other)
        {
            if (other.Y + other.Height / 2 + other.OffsetY > Y + Height / 2 + OffsetY)
                return -1;
            else if (other.Y + other.Height / 2 + other.OffsetY < Y + Height / 2 + OffsetY)
                return 1;
            else
                return 0;
        }
    }
}
