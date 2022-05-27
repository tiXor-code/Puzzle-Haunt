using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzle_Haunt
{
    internal class MenuBackground : Entity
    {
        #region Fields

        private Texture2D _texture;

        #endregion Fields

        #region Properties

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        #endregion Properties

        public MenuBackground(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            spriteBatch.Draw(_texture, Rectangle, colour);
        }
    }
}