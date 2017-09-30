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
        public Image image;
        public float X, Y;
        public int Width, Height;
        public float VelocityX, VelocityY;
        public bool IsVisible;

        private List<Component> components;

        public Entity(Image image)
        {
            this.image = image;
            IsVisible = true;
            components = new List<Component>();
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                spriteBatch.Draw(image.Texture, new Rectangle(
                    (int)X - image.offsetX, 
                    (int)Y - image.offsetY, 
                    Width, Height),
                Color.White);
            } 
        }

        public virtual void SetImage(Image image)
        {
            this.image = image;
        }

        public void SetPosition(float x, float y)
        {
            X = x;
            Y = y;
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
            if (other.Y + other.Height / 2 + other.image.offsetY > Y + Height / 2 + image.offsetY)
                return -1;
            else if (other.Y + other.Height / 2 + other.image.offsetY < Y + Height / 2 + image.offsetY)
                return 1;
            else
                return 0;
        }
    }
}
