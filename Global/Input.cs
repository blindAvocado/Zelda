using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    public class Input
    {
        private KeyboardState oldState;
        private KeyboardState currentState;

        public Input(KeyboardState oldState, KeyboardState state)
        {
            this.oldState = oldState;
            this.currentState = state;
        }

        public bool IsKeyDown(Keys key)
        {
            return this.currentState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return this.currentState.IsKeyUp(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return this.oldState.IsKeyUp(key) && this.currentState.IsKeyDown(key);
        }
        
        public bool IsKeyReleased(Keys key)
        {
            return this.oldState.IsKeyDown(key) && this.currentState.IsKeyUp(key);
        }
    }
}
