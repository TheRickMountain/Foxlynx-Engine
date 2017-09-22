using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace Foxlynx
{
    public class InputManager
    {

        private static InputManager instance;

        KeyboardState currentKeyState, prevKeyState;
        MouseState currentMouseState, prevMouseState;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }

        public void Update()
        {
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public int GetX()
        {
            int tmp = currentMouseState.X;
            if (tmp >= ScreenManager.Instance.Dimension.X)
                return (int)ScreenManager.Instance.Dimension.X;
            else if (tmp <= 0)
                return 0;

            return tmp;
        }

        public int GetY()
        {
            int tmp = currentMouseState.Y;
            if (tmp >= ScreenManager.Instance.Dimension.Y)
                return (int)ScreenManager.Instance.Dimension.Y;
            else if (tmp <= 0)
                return 0;

            return tmp;
        }

        public bool LeftButtonPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed &&
                prevMouseState.LeftButton == ButtonState.Released;
        }

        public bool LeftButtonReleased()
        {
            return currentMouseState.LeftButton == ButtonState.Released &&
                prevMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool LeftButtonDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool RightButtonPressed()
        {
            return currentMouseState.RightButton == ButtonState.Pressed &&
                prevMouseState.RightButton == ButtonState.Released;
        }

        public bool RightButtonReleased()
        {
            return currentMouseState.RightButton == ButtonState.Released &&
                prevMouseState.RightButton == ButtonState.Pressed;
        }

        public bool RightButtonDown()
        {
            return currentMouseState.RightButton == ButtonState.Pressed;
        }

        public bool MiddleButtonPressed()
        {
            return currentMouseState.MiddleButton == ButtonState.Pressed &&
                prevMouseState.MiddleButton == ButtonState.Released;
        }

        public bool MiddleButtonReleased()
        {
            return currentMouseState.MiddleButton == ButtonState.Released &&
                prevMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool MiddleButtonDown()
        {
            return currentMouseState.MiddleButton == ButtonState.Pressed;
        }

    }
}
