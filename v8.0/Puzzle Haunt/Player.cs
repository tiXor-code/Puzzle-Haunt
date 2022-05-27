using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Puzzle_Haunt
{
    public class Player : Entity, ICollidable
    {
        #region Fields

        // DECLARE a PlayerIndex to store this Paddle's PlayerIndex, call it _playerIndex:
        private PlayerIndex _playerIndex;

        // DECLARE a boolean to trigger the background transition, call it initiateTransitionLeft/Right:
        private static bool _initiateTransitionLeft = false;

        private static bool _initiateTransitionRight = false;

        private static bool _spawnToLeft = false;
        private static bool _spawnToRight = false;

        private static Vector2 _playerLocation;
        private static Vector2 _newPlayerLocation;

        private float _playerY;

        private bool _resetLocation;

        private AnimationManager walkLeft;

        private AnimationManager walkRight;

        private AnimationManager _animationManager;

        private Sprite _sprite;

        private Animation _animation;

        protected Dictionary<string, Animation> _animations;

        private float _timer;

        private float _timerDelayTorchAnimation;

        private float _timerDelayNote;

        private float _frameSpeed;

        private float _currentFrame = 1;

        private float _currentFrameIdle = 1;

        private static int _mapCount;

        private static int _newMapCount;

        private static int _mapCountLimit;

        private bool _turnLeft = false;

        private bool _turnRight = true;

        private static int _mapExited;

        private static bool _backgroundChangeDelay;

        private bool _torchAnimationDelay;

        private bool _noteDelay;

        private bool isTorchAnimationRunning = false;

        private float _currentFrameTorchAnimation = 1;

        private static string _mapName;

        private bool _placeTorch1;
        private bool _placeTorch2;
        private bool _placeTorch3;
        private bool _placeTorch4;

        private bool _pickNote1;
        private bool _pickNote2;
        private bool _pickNote3;
        private bool _pickNote4;

        private bool _endGame;

        private bool _resetGame;

        #endregion Fields

        #region Properties

        public PlayerIndex PlayerIndex
        { get => _playerIndex; set { _playerIndex = value; } }

        /// <summary>
        /// Hitbox for this entity.
        /// </summary>
        public Rectangle HitBox => new Rectangle((int)_locn.X, (int)_locn.Y, _texture.Width, _texture.Height);

        public Vector2 ScreenXEdges { get; set; }
        public Vector2 ScreenYEdges { get; set; }

        public static Vector2 PlayerLocation { get => _playerLocation; }
        public static Vector2 NewPlayerLocation
        { set { _newPlayerLocation = value; } }

        public static bool InitiateTransitionRight
        { get => _initiateTransitionRight; set { _initiateTransitionRight = value; } }
        public static bool InitiateTransitionLeft
        { get => _initiateTransitionLeft; set { _initiateTransitionLeft = value; } }
        public static bool SpawnToLeft
        { get => _spawnToLeft; set { _spawnToLeft = value; } }
        public static bool SpawnToRight
        { get => _spawnToRight; set { _spawnToRight = value; } }

        public static int MapCount
        { get => _mapCount; set { _mapCount = value; } }
        public static int MapCountLimit
        { get => _mapCountLimit; set { _mapCountLimit = value; } }
        public static int NewMapCount
        { get => _newMapCount; set { _newMapCount = value; } }
        public static int MapExited
        { get => _mapExited; set { _mapExited = value; } }
        public static bool BackgroundChangeDelay
        { get => _backgroundChangeDelay; set { _backgroundChangeDelay = value; } }
        public static string MapName
        { get => _mapName; set { _mapName = value; } }

        private Background background;

        #endregion Properties

        #region Methods
        public Player()
        {
            _locn.X = 200;
            _locn.Y = 320;

            _speed = 20;

            _frameSpeed = 0.1f;

            _mapCount = 1;
        }

        public void BackgroundTransition()
        {
            if (_locn.X <= 2 && _mapCount > 1 && !_backgroundChangeDelay)
            {
                _initiateTransitionLeft = true;
                _locn.X = 1585;
            }
            else _initiateTransitionLeft = false;

            if (_locn.X >= 1586 && _mapCount < _mapCountLimit && !_backgroundChangeDelay)
            {
                _initiateTransitionRight = true;
                _locn.X = 3;
            }
            else _initiateTransitionRight = false;
            SpawnLocation();
        }

        public void SpawnLocation()
        {
            if (_spawnToLeft)
            {
                _locn.X = 10;
            }
            if (_spawnToRight)
            {
                _locn.X = 1580;
                _direction.X = -1;
            }
        }

        public void EnterCorridor()
        {
            if (_mapExited == 2)
            { _locn.X = 825; }
            else if (_mapExited == 3) _locn.X = 175;
            else if (_mapExited == 4) _locn.X = 1420;
        }

        public void IdleAnimation()
        {
            if (Velocity.X == 0)
            {
                if (_timer > _frameSpeed)
                {
                    _timer = 0f;

                    _currentFrameIdle++;

                    if (_currentFrameIdle > 9)
                        _currentFrameIdle = 1;
                }
                else
                {
                    if (_turnRight) _texture = _textureDictionary["idleRight_" + _currentFrameIdle];
                    else if (_turnLeft) _texture = _textureDictionary["idleLeft_" + _currentFrameIdle];
                    else
                    {
                        _turnLeft = false;
                        _turnRight = false;
                    }
                }
            }
        }

        public void MoveAnimation()
        {
            if (_direction.X == 1)
            {
                if (_timer > _frameSpeed)
                {
                    _timer = 0f;

                    _currentFrame++;

                    if (_currentFrame >= 10)
                        _currentFrame = 1;
                }
                else
                {
                    _turnLeft = false;
                    _texture = _textureDictionary["walkRight_" + _currentFrame];
                    _turnRight = true;
                }
            }
            else if (_direction.X == -1)
            {
                if (_timer > _frameSpeed)
                {
                    _timer = 0f;

                    _currentFrame++;

                    if (_currentFrame >= 10)
                        _currentFrame = 1;
                }
                else
                {
                    _turnRight = false;
                    _texture = _textureDictionary["walkLeft_" + _currentFrame];
                    _turnLeft = true;
                }
            }
        }

        public bool TorchAnimation(GameTime pGameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // IF _timer is higher than the
            if (_mapName == "corridor_0")
            {
                if (keyboardState.IsKeyDown(Keys.F) || isTorchAnimationRunning)
                {
                    if (_timer > _frameSpeed)
                    {
                        _timer = 0f;

                        _currentFrameTorchAnimation++;

                        if (_currentFrameTorchAnimation > 5)
                        {
                            _currentFrameTorchAnimation = 1;
                            _torchAnimationDelay = true;
                            isTorchAnimationRunning = false;
                        }
                    }
                    else if (!_torchAnimationDelay)
                    {
                        _timerDelayTorchAnimation += (float)pGameTime.ElapsedGameTime.TotalSeconds;

                        isTorchAnimationRunning = true;
                        _texture = _textureDictionary["interact_" + _currentFrameTorchAnimation];
                        _speed = 0;
                        //Debug.WriteLine(_currentFrameTorchAnimation);
                    }

                    if (!isTorchAnimationRunning) _speed = 20;
                }
                else isTorchAnimationRunning = false;
            }

            return isTorchAnimationRunning;
        }

        public void AnimationLogic(GameTime pGameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if ((_direction.X == -1 || _direction.X == 1) && !TorchAnimation(pGameTime)) MoveAnimation();
            else if (!_torchAnimationDelay && Velocity.X == 0 && keyboardState.IsKeyDown(Keys.F) || TorchAnimation(pGameTime))
            { TorchLogic(pGameTime); }
            else IdleAnimation();
        }

        public void TorchLogic(GameTime pGameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (!_torchAnimationDelay && Velocity.X == 0 && keyboardState.IsKeyDown(Keys.F) || TorchAnimation(pGameTime))
            {
                if (_locn.X > 400 && _locn.X < 600 && _mapName == "corridor_0" && _mapCount == 1 && _placeTorch1 == false)
                {
                    _locn.X = 530;
                    TorchAnimation(pGameTime);
                    _placeTorch1 = true;
                }
                if (_locn.X > 1000 && _locn.X < 1310 && _mapName == "corridor_0" && _mapCount == 2 && _placeTorch2 == false)
                {
                    _locn.X = 1155;
                    TorchAnimation(pGameTime);
                    _placeTorch2 = true;
                }
                //else _placeTorch2 = false;
                if (_locn.X > 400 && _locn.X < 600 && _mapName == "corridor_0" && _mapCount == 3 && _placeTorch3 == false)
                {
                    _locn.X = 530;
                    TorchAnimation(pGameTime);
                    _placeTorch3 = true;
                }
                if (_locn.X > 1000 && _locn.X < 1310 && _mapName == "corridor_0" && _mapCount == 4 && _placeTorch4 == false)
                {
                    _locn.X = 1155;
                    TorchAnimation(pGameTime);
                    _placeTorch4 = true;
                }
            }
        }

        public void ResetGame()
        {
            if (_resetGame)
            {
                _placeTorch1 = false;
                _placeTorch2 = false;
                _placeTorch3 = false;
                _placeTorch4 = false;
                _resetGame = false;
            }

            // IF any torches are placed, start checking the order:
            if (_placeTorch1 || _placeTorch2 || _placeTorch3 || _placeTorch4)
            {
                // IF the second torch is placed first, progress to the next stage:
                if (_placeTorch2)
                {
                    // IF any more torches are placed, start checking the order:
                    if (_placeTorch1 || _placeTorch3 || _placeTorch4)
                    {
                        // IF the third torch is placed second, progress to the next stage:
                        if (_placeTorch3)
                        {
                            // IF any more torches are placed, start checking the order:
                            if (_placeTorch1 || _placeTorch4)
                            {
                                // IF the first torch is placed third, progress to the next stage:
                                if (_placeTorch1)
                                {
                                    // IF the forth torch is placed forth, trigger end game:
                                    if (_placeTorch4) _endGame = true;
                                }
                                // ELSE IF any but the first torch are placed third, trigger reset game:
                                else _resetGame = true;
                            }
                        }
                        // ELSE IF any but the third torch are placed second, trigger reset game:
                        else _resetGame = true;
                    }
                }
                // ELSE IF any but the second torch are placed first, trigger reset game:
                else _resetGame = true;
            }
        }

        public void ReadNote()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (Velocity.X == 0 && keyboardState.IsKeyDown(Keys.F) && !_noteDelay)
            {
                if (_locn.X > 700 && _locn.X < 1100 && _mapName == "attic_0" && _mapCount == 3 && !_pickNote1)
                {
                    //_locn.X = 530;
                    //TorchAnimation(pGameTime);
                    _pickNote1 = true;
                    _noteDelay = true;
                }
                else if (_pickNote1 == true && keyboardState.IsKeyDown(Keys.F) && !_noteDelay)
                {
                    _pickNote1 = false;
                    _noteDelay = true;
                }
                if (_locn.X > 1350 && _locn.X < 1750 && _mapName == "laundry_0" && _mapCount == 3 && !_noteDelay && !_pickNote2)
                {
                    //_locn.X = 1155;
                    //TorchAnimation(pGameTime);
                    _pickNote2 = true;
                    _noteDelay = true;
                }
                else if (_pickNote2 == true && keyboardState.IsKeyDown(Keys.F) && !_noteDelay)
                {
                    _pickNote2 = false;
                    _noteDelay = true;
                }
                if (_locn.X > 1350 && _locn.X < 1750 && _mapName == "chapel_0" && _mapCount == 2 && !_noteDelay && !_pickNote3)
                {
                    //_locn.X = 530;
                    //TorchAnimation(pGameTime);
                    _pickNote3 = true;
                    _noteDelay = true;
                }
                else if (_pickNote3 == true && keyboardState.IsKeyDown(Keys.F) && !_noteDelay)
                {
                    _pickNote3 = false;
                    _noteDelay = true;
                }
                if (_locn.X > 1350 && _locn.X < 1750 && _mapName == "patientRoom_0" && _mapCount == 3 && !_noteDelay && !_pickNote4)
                {
                    //_locn.X = 1155;
                    //TorchAnimation(pGameTime);
                    _pickNote4 = true;
                    _noteDelay = true;
                }
                else if (_pickNote4 == true && keyboardState.IsKeyDown(Keys.F) && !_noteDelay)
                {
                    _pickNote4 = false;
                    _noteDelay = true;
                }
            }

            if (_pickNote1 || _pickNote2 || _pickNote3 || _pickNote4)
            {
                _speed = 0;
            }
            else _speed = 20;
        }

        public override void Update(GameTime pGameTime)
        {
            _resetLocation = Background.ResetLocation;

            Kernel.PlaceTorch1 = _placeTorch1;
            Kernel.PlaceTorch2 = _placeTorch2;
            Kernel.PlaceTorch3 = _placeTorch3;
            Kernel.PlaceTorch4 = _placeTorch4;

            Kernel.PickNote1 = _pickNote1;
            Kernel.PickNote2 = _pickNote2;
            Kernel.PickNote3 = _pickNote3;
            Kernel.PickNote4 = _pickNote4;

            Ghost.ResetGame = _resetGame;
            Kernel.ResetGame = _resetGame;

            GameState.EndGame = _endGame;

            _playerLocation.X = _locn.X;

            BackgroundTransition();
            EnterCorridor();

            TorchLogic(pGameTime);

            AnimationLogic(pGameTime);

            _timer += (float)pGameTime.ElapsedGameTime.TotalSeconds;

            ResetGame();
            ReadNote();

            // IF the torchAnimation delay is true, start counting
            if (_torchAnimationDelay && _timerDelayTorchAnimation < 1) _timerDelayTorchAnimation += (float)pGameTime.ElapsedGameTime.TotalSeconds;
            else if (_timerDelayTorchAnimation >= 1) _torchAnimationDelay = false;
            else if (_timerDelayTorchAnimation >= 1 || _torchAnimationDelay == false) _timerDelayTorchAnimation = 0f;

            if (_noteDelay && _timerDelayNote < 1) _timerDelayNote += (float)pGameTime.ElapsedGameTime.TotalSeconds;
            else if (_timerDelayNote >= 1) { _noteDelay = false; _timerDelayNote = 0f; }

            //Debug.WriteLine(_locn.X);

            base.Update(pGameTime);
        }
        #endregion
    }
}