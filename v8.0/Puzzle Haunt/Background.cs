using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Puzzle_Haunt
{
    internal class Background : Entity
    {
        #region Fields

        private static bool _resetLocation;

        private static float _playerX;

        private bool _initiateTransitionRight;

        private bool _initiateTransitionLeft;

        private static bool _spawnToLeft;

        private static bool _spawnToRight;

        private static int _mapExited;

        private int _mapCount = 1;

        private int _newMapCount = 1;

        private int _mapCountLimit;

        private string _mapName = "corridor_0";

        private string _newMapName;

        private Input input;

        private float _timer;

        private bool _backgroundChangeDelay;

        private static Vector2 _playerLocation;

        #endregion Fields

        #region Properties

        public static float PlayerX
        { get => _playerX; set { _playerX = value; } }
        public static bool ResetLocation
        { get => _resetLocation; set { _resetLocation = value; } }

        public static bool SpawnToLeft
        { get => _spawnToLeft; set { _spawnToLeft = value; } }
        public static bool SpawnToRight
        { get => _spawnToRight; set { _spawnToRight = value; } }
        public static int MapExited
        { get => _mapExited; set { _mapExited = value; } }

        public int MapCount { get => _mapCount; }

        #endregion Properties

        public Background()
        {
            _playerLocation.X = 200;
        }

        public Background(Texture2D texture)
        {
            _texture = texture;
        }

        public void NextBackground()
        {
            if (_initiateTransitionLeft == true && _mapCount >= 1 && !_backgroundChangeDelay)
            {
                _mapCount--;
                _texture = _textureDictionary[_mapName + _mapCount];

                //_initiateTransitionLeft = false;
            }

            if (_initiateTransitionRight == true && _mapCount <= _mapCountLimit && !_backgroundChangeDelay)
            {
                _mapCount++;
                _texture = _textureDictionary[_mapName + _mapCount];

                //_initiateTransitionRight = false;
            }
        }

        public void MapCountChange()
        {
            if (_mapName == "corridor_0") _mapCountLimit = 5;
            else if (_mapName == "attic_0") _mapCountLimit = 5;
            else if (_mapName == "chapel_0") _mapCountLimit = 2;
            else if (_mapName == "laundry_0") _mapCountLimit = 3;
            else if (_mapName == "patientRoom_0") _mapCountLimit = 3;
        }

        public void ChangeRoom(float pTimer)
        {
            MapCountChange();

            _backgroundChangeDelay = false;
            KeyboardState keyboardState = Keyboard.GetState();
            if (pTimer >= 1)
            {
                if (_mapName == "corridor_0" && keyboardState.IsKeyDown(Keys.E))
                {
                    EnterAttic(keyboardState);
                    EnterChapel();
                    //EnterCorridor(keyboardState);
                    EnterLaundry();
                    EnterPatientRoom();
                }
                else if (_mapName != "corridor_0" && keyboardState.IsKeyDown(Keys.E))
                {
                    EnterCorridor(keyboardState);
                }
                _mapExited = 0;
            }

            //if (_mapName == "corridor_0") _mapExited = 0;

            //System.Diagnostics.Debug.WriteLine(pTimer);
        }

        //door locations 1st door: 20-370; 2nd door: 650-1000; 3rd door: 1250-1589

        public void EnterLaundry()
        {
            if (_playerLocation.X > 650 && _playerLocation.X < 1000 && _mapCount == 2)
            {
                _mapCount = 1;
                _spawnToLeft = true;
                _mapName = "laundry_0";
                _texture = _textureDictionary[_mapName + 1];
                _backgroundChangeDelay = true;
            }
        }

        public void EnterAttic(KeyboardState keyboardState)
        {
            if (_playerLocation.X > 0 && _playerLocation.X < 400 && _mapCount == 1)
            {
                _mapCount = 1;
                _spawnToLeft = true;
                _mapName = "attic_0";
                _mapCount = 1;
                _texture = _textureDictionary[_mapName + _mapCount];
                _backgroundChangeDelay = true;
            }
            else if (_playerLocation.X > 1300 && _mapCount == 5)
            {
                _mapCount = 1;
                _spawnToRight = true;
                _mapName = "attic_0";
                _mapCount = 5;
                _texture = _textureDictionary[_mapName + _mapCount];
                _backgroundChangeDelay = true;
            }
        }

        public void EnterChapel()
        {
            if (_playerLocation.X > 20 && _playerLocation.X < 370 && _mapCount == 3)
            {
                _mapCount = 1;
                _spawnToLeft = true;
                _mapName = "chapel_0";
                _texture = _textureDictionary[_mapName + 1];
                _backgroundChangeDelay = true;
            }
        }

        public void EnterPatientRoom()
        {
            if (_playerLocation.X > 1250 && _playerLocation.X < 1584 && _mapCount == 4)
            {
                _mapCount = 1;
                _spawnToLeft = true;
                _mapName = "patientRoom_0";
                _texture = _textureDictionary[_mapName + 1];
                _backgroundChangeDelay = true;
            }
        }

        public void EnterCorridor(KeyboardState keyboardState)
        {
            if (_mapName != "corridor_0" && keyboardState.IsKeyDown(Keys.E) && _playerLocation.X < 5)
            {
                if (_mapName == "laundry_0") { _newMapCount = 2; _mapExited = 2; }
                else if (_mapName == "chapel_0") { _newMapCount = 3; _mapExited = 3; }
                else if (_mapName == "patientRoom_0") { _newMapCount = 4; _mapExited = 4; }
                else if (_mapName == "attic_0" && _mapCount == 1) { _spawnToLeft = true; _newMapCount = 1; }

                _mapName = "corridor_0";
                _texture = _textureDictionary[_mapName + _newMapCount];
                _mapCount = _newMapCount;
                _backgroundChangeDelay = true;
            }
            else if (_mapName != "corridor_0" && keyboardState.IsKeyDown(Keys.E) && _playerLocation.X > 1500 && _mapName == "attic_0" && _mapCount == 5)
            {
                _spawnToRight = true; _newMapCount = 5;

                _mapName = "corridor_0";
                _texture = _textureDictionary[_mapName + _newMapCount];
                _mapCount = _newMapCount;
                _backgroundChangeDelay = true;
            }
        }

        public override void Update(GameTime pGameTime)
        {
            //_playerX = Player.PlayerX;
            _initiateTransitionRight = Player.InitiateTransitionRight;
            _initiateTransitionLeft = Player.InitiateTransitionLeft;
            Player.MapCount = _mapCount;
            Player.MapName = _mapName;

            if (_playerLocation.X < 100 || _playerLocation.X > 1500)
            {
                _spawnToLeft = false;
                _spawnToRight = false;
            }

            Player.SpawnToLeft = _spawnToLeft;
            Player.SpawnToRight = _spawnToRight;

            Player.MapExited = _mapExited;

            Player.NewPlayerLocation = _playerLocation;
            _playerLocation = Player.PlayerLocation;

            Player.BackgroundChangeDelay = _backgroundChangeDelay;

            Player.MapCountLimit = _mapCountLimit;

            Player.NewMapCount = _newMapCount;

            ChangeRoom(_timer);
            NextBackground();

            if (!_backgroundChangeDelay) _timer += (float)pGameTime.ElapsedGameTime.TotalSeconds;
            else _timer = 0f;

            //System.Diagnostics.Debug.WriteLine(_mapCount);

            base.Update(pGameTime);
        }
    }
}