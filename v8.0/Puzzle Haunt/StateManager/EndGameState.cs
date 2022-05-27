using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Puzzle_Haunt
{
    public class EndGameState : State
    {
        private List<Entity> _components;

        public EndGameState(Kernel game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var endGameTexture = _content.Load<Texture2D>("Assets/UI/Menu/buttonNewGame");
            var endGameBackgroundTexture = _content.Load<Texture2D>("Assets/Backgrounds/EndGameBackground/backgroundEndGame");

            var buttonFont = _content.Load<SpriteFont>("Assets/UI/Menu/Font");

            var endGameButton = new Button(endGameTexture, buttonFont)
            {
                Position = new Vector2(960, 540),
                Text = "Well Played!",
            };

            var endGameBackground = new MenuBackground(endGameBackgroundTexture)
            {
                //Position = new Vector2(960, 540),
            };

            endGameButton.Click += EndGameButton_Click;

            _components = new List<Entity>()
      {
        endGameBackground,
        endGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(spriteBatch);
        }

        private void EndGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}