using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Puzzle_Haunt
{
    /// <summary>
    /// CLASS inspired from https://www.youtube.com/watch?v=76Mz7ClJLoE&t=4s&ab_channel=Oyyou
    /// </summary>
    public class MenuState : State
    {
        private List<Entity> _components;

        public MenuState(Kernel game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonNewGameTexture = _content.Load<Texture2D>("Assets/UI/Menu/buttonNewGame");
            var buttonOptionsTexture = _content.Load<Texture2D>("Assets/UI/Menu/buttonOptions");
            var buttonQuitTexture = _content.Load<Texture2D>("Assets/UI/Menu/buttonQuit");

            var menuBackgroundTexture = _content.Load<Texture2D>("Assets/UI/Menu/background");

            var buttonFont = _content.Load<SpriteFont>("Assets/UI/Menu/Font");

            //var menuBackground;

            var menuBackground = new MenuBackground(menuBackgroundTexture);

            var newGameButton = new Button(buttonNewGameTexture, buttonFont)
            {
                Position = new Vector2(100, 125),
                //Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var optionsButton = new Button(buttonOptionsTexture, buttonFont)
            {
                Position = new Vector2(200, 450),
                //Text = "Options",
            };

            optionsButton.Click += OptionsGameButton_Click;

            var quitGameButton = new Button(buttonQuitTexture, buttonFont)
            {
                Position = new Vector2(149, 752),
                //Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Entity>()
      {
        menuBackground,
        optionsButton,
        quitGameButton,
        newGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(spriteBatch);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void OptionsGameButton_Click(object sender, EventArgs e)
        {
        }

        private void ControlsGameButton_Click(object sender, EventArgs e)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}