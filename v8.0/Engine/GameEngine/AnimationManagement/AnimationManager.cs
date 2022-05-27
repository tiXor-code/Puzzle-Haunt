using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// Inspired from: https://www.youtube.com/watch?v=OLsiWxgONeM
    /// </summary>
    public class AnimationManager : Entity
    {
        #region Fields

        private Animation _animation;

        private float _timer;

        #endregion Fields

        #region Properties

        public Vector2 Position { get; set; }

        #endregion Properties

        #region Methods

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_animation.Texture,
                             Position,
                             new Microsoft.Xna.Framework.Rectangle(_animation.CurrentFrame * _animation.FrameWidth,
                                                                   0,
                                                                   _animation.FrameWidth,
                                                                   _animation.FrameHeight),
                             Microsoft.Xna.Framework.Color.White);
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public Texture2D CurrentFrame()
        {
            Texture2D currentFrame = _animation.Texture;
            return currentFrame;
        }

        public void WalkLeft()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        #endregion Methods
    }
}