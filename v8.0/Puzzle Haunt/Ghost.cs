using Engine;
using Microsoft.Xna.Framework;

namespace Puzzle_Haunt
{
    public class Ghost : Entity
    {
        #region Fields

        private float _timer;

        private float _ghostTimer;

        private float _frameSpeed;

        private float _currentFrame = 30;
        private float _animationCurrentFrame = 1;

        private static bool _resetGame;

        private bool _animationStarted;

        #endregion Fields

        #region Properties

        public static bool ResetGame
        { get => _resetGame; set { _resetGame = value; } }

        #endregion Properties

        #region Methods

        public Ghost()
        {
            _frameSpeed = 0.001f;

            _ghostTimer = 1f;

            _locn.X = 2000;
            _locn.Y = 300;
        }

        public void Animation()
        {
            if (_timer > _frameSpeed)
            {
                _timer = 0f;

                _animationCurrentFrame++;

                if (_animationCurrentFrame >= 8)
                    _animationCurrentFrame = 1;
            }
            else
            {
                _texture = _textureDictionary["ghost_" + _animationCurrentFrame];
            }
        }

        public override void Boundaries(Vector2 locn)
        {
        }

        public void Reset()
        {
            // if resetgame Active
            if (_resetGame || _animationStarted)
            {
                // start off the screen in the right
                if (!_animationStarted)
                {
                    _locn.X = 2000;
                    _animationStarted = true;
                }
                else
                {
                    _locn.X -= 2 * _currentFrame;
                    if (_locn.X < -100) _animationStarted = false;
                }
            }
            else
            {
                _locn.X = 2000;
                _locn.Y = 300;
            }
        }

        public override void Update(GameTime pGameTime)
        {
            _timer += (float)pGameTime.ElapsedGameTime.TotalSeconds;

            Animation();

            Reset();

            _ghostTimer *= (float)pGameTime.ElapsedGameTime.TotalSeconds;

            //_locn.X = 100;

            base.Update(pGameTime);
        }

        #endregion Methods
    }
}