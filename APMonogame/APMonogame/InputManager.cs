using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace APMonogame
{
    public class InputManager
    {
        #region Variables & Properties
        KeyboardState prevKeyState, keyState;
        public KeyboardState PrevKeyState
        {
            get { return prevKeyState; }
            set { prevKeyState = value; }
        }
        public KeyboardState KeyState
        {
            get { return keyState; }
            set { keyState = value; }
        }
        #endregion
        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
        }
        #region Check for Key presses
        public bool KeyPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;           
        }
        //Verschillende buttons assignen voor zelfde functie
        public bool KeyPressed(params Keys[] keys)
        {
            foreach(Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(Keys key)
        {
            if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                return true;
            return false;
        }

        //Verschillende buttons assignen voor zelfde functie
        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
                return true;
            return false;
        }
        //Verschillende buttons assignen voor zelfde functie
        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
        #endregion

    }
}
