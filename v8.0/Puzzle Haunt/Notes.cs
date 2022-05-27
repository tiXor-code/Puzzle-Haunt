using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzle_Haunt
{
    internal class Notes : Entity
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

        #region Methods

        public Notes()
        {
        }

        public override void Boundaries(Vector2 locn)
        {
        }

        public override void Update(GameTime pGameTime)
        {
            base.Update(pGameTime);
        }

        #endregion Methods
    }
}