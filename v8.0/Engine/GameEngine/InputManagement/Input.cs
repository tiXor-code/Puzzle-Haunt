using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public class Input
    {
        public Input()
        {
        }

        public Keys Left { get; set; }
        public Keys Right { get; set; }

        public Keys E { get; set; }

        public static Vector2 GetKeyboardInputDirection(PlayerIndex x)
        {
            //DECLARE a KeyboardState, call it keyboardstate:
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 direction = new Vector2();

            /*
			if ((keyboardState.IsKeyDown(Keys.W)) && (x==PlayerIndex.One))
			{
				direction.Y = -1;
            }
            else if ((keyboardState.IsKeyDown(Keys.S)) && (x == PlayerIndex.One))
            {
				direction.Y = 1;
            }*/
            if ((keyboardState.IsKeyDown(Keys.A)) && (x == PlayerIndex.One))
            {
                direction.X = -1;
            }
            else if ((keyboardState.IsKeyDown(Keys.D)) && (x == PlayerIndex.One))
            {
                direction.X = 1;
            }

            if (keyboardState.IsKeyDown(Keys.E))
            {
            }
            /*
            if ((keyboardState.IsKeyDown(Keys.Up)) && (x == PlayerIndex.Two))
            {
                direction.Y = -1;
            }
            else if ((keyboardState.IsKeyDown(Keys.Down)) && (x == PlayerIndex.Two))
            {
                direction.Y = 1;
            }*/

            return direction;
        }
    }
}