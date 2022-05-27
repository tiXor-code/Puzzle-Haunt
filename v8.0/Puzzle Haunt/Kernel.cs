using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Puzzle_Haunt
{
    /// <summary>
    /// This is the main class for the game.
    /// </summary>
    public class Kernel : Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _scrnHeight;
        private int _scrnWidth;

        // DECLARE an IEntityManager for managing entities, call it _entityMgr:
        private IEntityManager _entityMgr;

        // DECLARE collection of entities, call it _entities:
        private IDictionary<String, IEntity> _entities;



        private static bool _placeTorch1;
        private static bool _placeTorch2;
        private static bool _placeTorch3;
        private static bool _placeTorch4;

        private static bool _pickNote1;
        private static bool _pickNote2;
        private static bool _pickNote3;
        private static bool _pickNote4;

        private static bool _resetGame;

        private State _currentState;

        private State _nextState;

        private static bool _drawGame;

        #endregion Fields

        #region Properties

        public int ScreenWidth { get => _scrnWidth; }
        public int ScreenHeight { get => _scrnHeight; }

        public IDictionary<String, IEntity> Entities { get => _entities; }

        public IEntityManager EntityMgr { get => _entityMgr; }

        public static bool PlaceTorch1
        { get => _placeTorch1; set { _placeTorch1 = value; } }
        public static bool PlaceTorch2
        { get => _placeTorch2; set { _placeTorch2 = value; } }
        public static bool PlaceTorch3
        { get => _placeTorch3; set { _placeTorch3 = value; } }
        public static bool PlaceTorch4
        { get => _placeTorch4; set { _placeTorch4 = value; } }

        public static bool PickNote1
        { get => _pickNote1; set { _pickNote1 = value; } }
        public static bool PickNote2
        { get => _pickNote2; set { _pickNote2 = value; } }
        public static bool PickNote3
        { get => _pickNote3; set { _pickNote3 = value; } }
        public static bool PickNote4
        { get => _pickNote4; set { _pickNote4 = value; } }

        public static bool ResetGame
        { get => _resetGame; set { _resetGame = value; } }

        public static bool DrawGame
        { get => _drawGame; set { _drawGame = value; } }

        #endregion Properties

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Kernel()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

            // SET screen height to 900:
            _graphics.PreferredBackBufferHeight = 1080;
            // SET screen width to 1600:
            _graphics.PreferredBackBufferWidth = 1920;

            _graphics.IsFullScreen = true;

            // CREATE _entityMgr:
            _entityMgr = new EntityManager();

            // CREATE entities collection:
            _entities = new Dictionary<String, IEntity>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // GET ScreenHeight:
            _scrnHeight = GraphicsDevice.Viewport.Height;
            _scrnWidth = GraphicsDevice.Viewport.Width;

            _entities.Add("background", _entityMgr.Create<Background>("background"));
            _entities["background"].UName = "background";

            _entities["background"].Texture = Content.Load<Texture2D>("Assets/Backgrounds/Corridor/Corridor_1");
            _entities["background"].Locn = new Vector2(0, 0);

            for (int i = 1; i <= 4; i++)
            {
                _entities.Add("torch" + i, _entityMgr.Create<Torch>("torch" + i));
                _entities["torch" + i].UName = "torch" + i;
            }

            for (int i = 1; i <= 4; i++)
            {
                _entities.Add("noteOnScreen" + i, _entityMgr.Create<Notes>("noteOnScreen" + i));
                _entities["noteOnScreen" + i].UName = "noteOnScreen" + i;
                _entities["noteOnScreen" + i].Texture = Content.Load<Texture2D>("Assets/Notes/NotesOnScreen/noteOnScreen_" + i);
            }

            for (int i = 1; i <= 4; i++)
            {
                _entities.Add("noteOnFloor" + i, _entityMgr.Create<Notes>("noteOnFloor" + i));
                _entities["noteOnFloor" + i].UName = "noteOnFloor" + i;
                _entities["noteOnFloor" + i].Texture = Content.Load<Texture2D>("Assets/Notes/NotesOnFloor/noteOnFloor_" + i);
            }

            // CREATE player:
            _entities.Add("player", _entityMgr.Create<Player>("player"));
            _entities["player"].UName = "player";

            // CREATE ghost:
            _entities.Add("ghost", _entityMgr.Create<Ghost>("ghost"));
            _entities["ghost"].UName = "ghost";

            //_entities["ghost"].Locn = new Vector2(2000, 300);

            // --- INITIALISE player ---
            // SET its player index to One
            (_entities["player"] as Player).PlayerIndex = PlayerIndex.One;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // LOAD NotesOnScreen

            for (int i = 1; i <= 4; i++) _entities["noteOnScreen" + i].LoadTexture("noteOnScreen_" + i, Content.Load<Texture2D>("Assets/Notes/NotesOnScreen/noteOnScreen_" + i));

            for (int i = 1; i <= 4; i++) _entities["noteOnFloor" + i].LoadTexture("noteOnFloor_" + i, Content.Load<Texture2D>("Assets/Notes/NotesOnFloor/noteOnFloor_" + i));

            // LOAD player animations
            for (int i = 1; i <= 9; i++) _entities["player"].LoadTexture("walkRight_" + i, Content.Load<Texture2D>("Assets/Characters/Player/moveRight/moveRight_" + i));

            for (int i = 1; i <= 9; i++) _entities["player"].LoadTexture("walkLeft_" + i, Content.Load<Texture2D>("Assets/Characters/Player/moveLeft/moveLeft_" + i));

            for (int i = 1; i <= 9; i++) _entities["player"].LoadTexture("idleRight_" + i, Content.Load<Texture2D>("Assets/Characters/Player/idleRight/idleRight_" + i));

            for (int i = 1; i <= 9; i++) _entities["player"].LoadTexture("idleLeft_" + i, Content.Load<Texture2D>("Assets/Characters/Player/idleLeft/idleLeft_" + i));

            for (int i = 1; i <= 5; i++) _entities["player"].LoadTexture("interact_" + i, Content.Load<Texture2D>("Assets/Characters/Player/interact/interact_" + i));

            // LOAD ghost animation
            for (int i = 1; i <= 7; i++) _entities["ghost"].LoadTexture("ghost_" + i, Content.Load<Texture2D>("Assets/Characters/Ghost/ghost_" + i));

            // LOAD Torches
            for (int j = 1; j <= 4; j++)
            {
                for (int i = 1; i <= 3; i++) _entities["torch" + j].LoadTexture("torch_" + i, Content.Load<Texture2D>("Assets/UI/Torch/torch_" + i));
            }

            // LOAD Maps
            for (int i = 1; i <= 5; i++) _entities["background"].LoadTexture("corridor_0" + i, Content.Load<Texture2D>("Assets/Backgrounds/Corridor/Corridor_" + i));

            for (int i = 1; i <= 5; i++) _entities["background"].LoadTexture("attic_0" + i, Content.Load<Texture2D>("Assets/Backgrounds/Attic/Attic_" + i));

            for (int i = 1; i <= 2; i++) _entities["background"].LoadTexture("chapel_0" + i, Content.Load<Texture2D>("Assets/Backgrounds/Chapel/Chapel_" + i));

            for (int i = 1; i <= 3; i++) _entities["background"].LoadTexture("laundry_0" + i, Content.Load<Texture2D>("Assets/Backgrounds/Laundry/Laundry_" + i));

            for (int i = 1; i <= 3; i++) _entities["background"].LoadTexture("patientRoom_0" + i, Content.Load<Texture2D>("Assets/Backgrounds/PatientRoom/PatientRoom_" + i));

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            if (_drawGame)
            {
                // DRAW entities:
                //foreach (Entity entity in _entities.Values)
                //{
                //    entity.Garbage(entity);
                //}
            }

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // GET _scrnHeight & _scrnWidth
            _scrnHeight = GraphicsDevice.Viewport.Height;
            _scrnWidth = GraphicsDevice.Viewport.Width;

            // INITIALIZE the player as Player.One:
            _entities["player"].Direction = Input.GetKeyboardInputDirection(PlayerIndex.One);

            // UPDATE the player:
            _entities["player"].Update(gameTime);

            // UPDATE the ghost:
            _entities["ghost"].Update(gameTime);

            // UPDATE the background:
            _entities["background"].Update(gameTime);

            for (int i = 1; i <= 4; i++)
            {
                _entities["torch" + i].Update(gameTime);
                _entities["noteOnFloor" + i].Update(gameTime);
                _entities["noteOnScreen" + i].Update(gameTime);
            }

            #region Torch Logic

            Vector2 torch1Location = new Vector2(1225, 300);
            Vector2 torch2Location = new Vector2(601, 300);

            if (_placeTorch1 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Corridor/Corridor_1")) _entities["torch1"].Locn = torch2Location;
            else _entities["torch1"].Locn = new Vector2(2000, 0);
            if (_placeTorch2 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Corridor/Corridor_2")) _entities["torch2"].Locn = torch1Location;
            else _entities["torch2"].Locn = new Vector2(2000, 0);
            if (_placeTorch3 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Corridor/Corridor_3")) _entities["torch3"].Locn = torch2Location;
            else _entities["torch3"].Locn = new Vector2(2000, 0);
            if (_placeTorch4 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Corridor/Corridor_4")) _entities["torch4"].Locn = torch1Location;
            else _entities["torch4"].Locn = new Vector2(2000, 0);

            #endregion Torch Logic

            #region NotesOnFloor Logic

            Vector2 noteOnFloor1Location = new Vector2(900, 810);
            Vector2 noteOnFloor2Location = new Vector2(1550, 810);
            Vector2 noteOnFloor3Location = new Vector2(1550, 828);
            Vector2 noteOnFloor4Location = new Vector2(1550, 820);

            Vector2 noteOnScreenLocation = new Vector2(600, 400);

            if (_entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Attic/attic_3")) _entities["noteOnFloor1"].Locn = noteOnFloor1Location;
            else _entities["noteOnFloor1"].Locn = new Vector2(2000, 0);
            if (_pickNote1 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Attic/attic_3")) _entities["noteOnScreen1"].Locn = noteOnScreenLocation;
            else _entities["noteOnScreen1"].Locn = new Vector2(2000, 0);

            if (_entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Laundry/laundry_3")) _entities["noteOnFloor2"].Locn = noteOnFloor2Location;
            else _entities["noteOnFloor2"].Locn = new Vector2(2000, 0);
            if (_pickNote2 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Laundry/laundry_3")) _entities["noteOnScreen2"].Locn = noteOnScreenLocation;
            else _entities["noteOnScreen2"].Locn = new Vector2(2000, 0);

            if (_entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Chapel/chapel_2")) _entities["noteOnFloor3"].Locn = noteOnFloor3Location;
            else _entities["noteOnFloor3"].Locn = new Vector2(2000, 0);
            if (_pickNote3 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/Chapel/chapel_2")) _entities["noteOnScreen3"].Locn = noteOnScreenLocation;
            else _entities["noteOnScreen3"].Locn = new Vector2(2000, 0);

            if (_entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/PatientRoom/patientRoom_3")) _entities["noteOnFloor4"].Locn = noteOnFloor4Location;
            else _entities["noteOnFloor4"].Locn = new Vector2(2000, 0);
            if (_pickNote4 && _entities["background"].Texture == Content.Load<Texture2D>("Assets/Backgrounds/PatientRoom/patientRoom_3")) _entities["noteOnScreen4"].Locn = noteOnScreenLocation;
            else _entities["noteOnScreen4"].Locn = new Vector2(2000, 0);

            #endregion NotesOnFloor Logic

            // STATE LOGIC:
            if (_nextState != null)
            {
                _currentState = _nextState;

                _drawGame = !_drawGame;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _currentState.Draw(gameTime, _spriteBatch);

            // false in main menu, true in game, false in endgame
            if (_nextState != null || _drawGame)
            {
                // DRAW entities:
                foreach (IEntity entity in _entities.Values)
                {
                    entity.Draw(_spriteBatch);
                }
            }

            //foreach (var sprite in _sprites)
            //    sprite.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}