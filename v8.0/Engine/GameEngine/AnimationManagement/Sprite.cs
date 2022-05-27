using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// Inspired from: https://www.youtube.com/watch?v=OLsiWxgONeM
    /// </summary>
    public class Sprite : Entity
    {
        #region Fields

        //protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        //protected Texture2D _texture;

        #endregion Fields

        #region Properties

        public Input Input;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        //public Texture2D Texture { get { return _texture; } }

        //public float Speed = 1f;

        public Vector2 Velocity;

        #endregion Properties

        #region Methods

        //public virtual void Draw(SpriteBatch spriteBatch)
        //{
        //    if (_texture != null)
        //        spriteBatch.Draw(_texture, Position, Color.White);
        //    else if (_animationManager != null)
        //        _animationManager.Draw(spriteBatch);
        //    else throw new Exception("This ain't right...!");
        //}

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
                _animationManager.Play(_animations["WalkRight"]);
            else
            if (Velocity.X < 0)
                _animationManager.Play(_animations["WalkLeft"]);
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            SetAnimations();

            _animationManager.Update(gameTime);

            Locn += Velocity;
            Velocity = Vector2.Zero;
        }

        #endregion Methods
    }
}