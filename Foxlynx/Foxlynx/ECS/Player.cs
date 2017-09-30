using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Foxlynx.Components;

namespace Foxlynx
{
    public class Player : AnimatedEntity
    {

        private enum State
        {
            MOVE, ATTACK
        }

        private float speed = 150f;

        private Dictionary<int, Image> attack;

        private State state = State.MOVE;

        public Player() : base(new Image("player", 6, 4).SetOffset(0, -8 * World.PIXEL_SIZE))
        {
            SetSize((image.Width / 6) * World.PIXEL_SIZE, (image.Height / 4) * World.PIXEL_SIZE);
            AddComponent(new ColliderComponent(10 * World.PIXEL_SIZE, 6 * World.PIXEL_SIZE, 0.0f));

            attack = new Dictionary<int, Image>();
            attack.Add(0, new Image("player_attack_down", 7, 1));
            attack.Add(1, new Image("player_attack_left", 7, 1).SetOffset(-8 * World.PIXEL_SIZE, -8 * World.PIXEL_SIZE));
            attack.Add(2, new Image("player_attack_right", 7, 1).SetOffset(8 * World.PIXEL_SIZE, -8 * World.PIXEL_SIZE));
            attack.Add(3, new Image("player_attack_up", 7, 1).SetOffset(0, -16 * World.PIXEL_SIZE));
            attack.Add(4, image);
        }

        public override void Update(GameTime gameTime)
        {
            int attackKey = InputManager.Instance.LeftButtonPressed() ? 1 : 0;
            if(attackKey == 1)
            {
                state = State.ATTACK;
                Console.WriteLine(CurrentFrame.Y);
                SetImage(attack[(int)CurrentFrame.Y]);
                IsMoving = true;
                CurrentFrame.X = 0;
                CurrentFrame.Y = 0;
            }


            switch(state)
            {
                case State.MOVE:
                    Move(gameTime);
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }

            DoAnimation(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            int upKey = InputManager.Instance.KeyDown(Keys.W) ? 1 : 0;
            int downKey = InputManager.Instance.KeyDown(Keys.S) ? 1 : 0;
            int leftKey = InputManager.Instance.KeyDown(Keys.A) ? 1 : 0;
            int rightKey = InputManager.Instance.KeyDown(Keys.D) ? 1 : 0;

            int xaxis = rightKey - leftKey;
            int yaxis = downKey - upKey;

            float direction = (float)MathUtils.Direction(0, 0, xaxis, yaxis);
            float length = 0;

            if (xaxis != 0 || yaxis != 0)
            {
                length = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                IsMoving = true;
            }
            else
                IsMoving = false;

            VelocityX = (float)MathUtils.getLengthdirX(length, direction);
            VelocityY = (float)MathUtils.getLengthdirY(length, direction);

            X += VelocityX;
            Y += VelocityY;

            if (yaxis < 0)
                CurrentFrame.Y = 3;
            else if (yaxis > 0)
                CurrentFrame.Y = 0;

            if (xaxis < 0)
                CurrentFrame.Y = 1;
            else if (xaxis > 0)
                CurrentFrame.Y = 2;
        }

        private void Attack()
        {
            if(CurrentFrame.X == image.Column - 1)
            {
                IsMoving = false;
                state = State.MOVE;
                SetImage(attack[4]);
            }
        }

    }
}
