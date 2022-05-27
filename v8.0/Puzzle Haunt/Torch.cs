using Engine;
using Microsoft.Xna.Framework;

namespace Puzzle_Haunt
{
    public class Torch : Entity
    {
        #region Fields

        private float _timer;

        private float _frameSpeed;

        private float _currentFrame = 1;

        #endregion Fields



        #region Methods

        public Torch()
        {
            _frameSpeed = 0.1f;
        }

        public void Animation()
        {
            if (_timer > _frameSpeed)
            {
                _timer = 0f;

                _currentFrame++;

                if (_currentFrame >= 4)
                    _currentFrame = 1;
            }
            else
            {
                _texture = _textureDictionary["torch_" + _currentFrame];
            }
        }

        public override void Update(GameTime pGameTime)
        {
            _timer += (float)pGameTime.ElapsedGameTime.TotalSeconds;

            Animation();

            //_locn.X = 100;

            base.Update(pGameTime);
        }

        #endregion Methods
    }
}