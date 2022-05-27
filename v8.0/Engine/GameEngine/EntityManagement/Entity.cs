using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Most of this class is referenced to Marc Price
    /// </summary>
    public abstract class Entity : IEntity, IDisposable
    {
        #region Fields

        // Set scale

        protected Dictionary<string, Texture2D> _textureDictionary = new Dictionary<string, Texture2D>();

        protected AnimationManager _animationManager;

        protected String _uname;

        // Key:
        protected string _key;

        // Texture:
        protected Texture2D _texture;

        // Location:
        protected Vector2 _locn = new Vector2(0, 0);

        // Direction:
        protected Vector2 _direction = new Vector2(0, 0);

        // Speed:
        protected float _speed = 1;

        private bool disposedValue;

        //protected Dictionary<Entity, string>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// UName: the unique name for this entity
        /// </summary>
        public String UName
        { get { return _uname; } set { _uname = value; } }

        /// <summary>
        /// Texture: public interface to get/set the underlying Texture 2D
        /// </summary>
        public Texture2D Texture
        { get { return _texture; } set { _texture = value; } }

        /// <summary>
        /// Locn: public interface to get the underlying location as a Vector2
        /// </summary>
        public Vector2 Locn
        {
            get { return _locn; }
            set
            {
                _locn = value;

                if (_animationManager != null)
                    _animationManager.Locn = _locn;
            }
        }

        public AnimationManager AnimationMgr { get; set; }

        /// <summary>
        /// Velocity of this entity as Vector2.
        /// </summary>
        public Vector2 Velocity
        { get { return _speed * _direction; } }

        /// <summary>
        /// insert comments
        /// </summary>
        public Vector2 Direction
        { get { return _direction; } set { _direction = value; } }

        /// <summary>
        /// insert comments
        /// </summary>
        public float Speed
        { get { return _speed; } }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Assign a texture to this entity.
        /// </summary>
        /// <param name="texture"></param>
        /*public void LoadTexture(Texture2D texture)
        {
            _texture = texture;
        }*/

        public void LoadTexture(string Key, Texture2D texture)
        {
            _textureDictionary.Add(Key, texture);
        }

        public void LoadAnimation(string Key, Texture2D texture, int frameCount)
        {
            for (int i = 0; i <= frameCount; i++)
            {
                Key += i;
                _textureDictionary.Add(Key, texture);

                Key = Key.Remove(Key.Length - 1, 1);
            }
        }

        /// <summary>
        /// Update the entities state - should be called during each simulation update.
        /// </summary>
        /// <param name="pGameTime">The time since the previous update.</param>

        public virtual void Boundaries(Vector2 locn)
        {
            _locn = locn;
            if (_locn.X <= 0)
                _locn.X = 0;
            else if (_locn.X >= 1920 - Texture.Width)
                _locn.X = 1920 - Texture.Width;
            else if (_locn.Y <= 0)
                _locn.Y = 0;
            else if (_locn.Y >= 1080 - Texture.Height)
                _locn.Y = 1080 - Texture.Height;
            //else throw new Exception("Entity out of boundaries");
        }

        public virtual void Update(GameTime pGameTime)
        {
            _locn += _speed * _direction;
            Boundaries(Locn);
            //AnimationMechanic();
        }

        /// <summary>
        /// Draw this entity in the scene.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use for drawing.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, _locn, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("There is no default texture initialized for the last added Entity!");
        }

        public void Garbage(Entity entity)
        {
            entity = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Entity()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods
    }
}