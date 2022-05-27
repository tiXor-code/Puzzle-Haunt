using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzle_Haunt
{
    /// <summary>
    /// CLASS inspired from https://www.youtube.com/watch?v=76Mz7ClJLoE&t=4s&ab_channel=Oyyou
    /// </summary>
    public class GameState : State
    {
        #region Fields

        private static bool _endGame;

        #endregion Fields

        #region Properties

        public static bool EndGame
        { get => _endGame; set { _endGame = value; } }

        #endregion Properties

        #region Methods

        public GameState(Kernel game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
        }

        public void EndGameState()
        {
            if (_endGame == true)
            {
                //Kernel.DrawGame = !_endGame;
                _game.ChangeState(new EndGameState(_game, _graphicsDevice, _content));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            EndGameState();
        }

        #endregion Methods
    }
}