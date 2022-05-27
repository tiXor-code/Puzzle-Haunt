using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Manages the collection of entities that are in the Scene.
    /// For now this is pretty simple, but it will get more complex when it's put to full use.
    /// Reference to Marc Price for this class
    /// </summary>
    internal class SceneGraph : ISceneGraph
    {
        #region Fields

        // DECLARE an IDictionary to store the collection of entities, call it _scene:
        private IDictionary<String, IEntity> _scene;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Initialise the scene graph, passing a reference to the collection for the scene.
        /// </summary>
        /// <param name="scene">The collection starting all entities on the scene</param>
        public void Initialise(IDictionary<String, IEntity> scene)
        {
            // ASSIGN scene:
            _scene = scene;
        }

        /// <summary>
        /// Place the given entity onto the Scene.
        /// </summary>
        /// <param name="entity">the entity to be spawned</param>
        /// <param name="position">the entity's _position</param>
        public void Spawn(IEntity entity, Vector2 position)
        {
            // ASSIGN _position to entity:
            (entity as IEntityInternal).Spawn(position);

            // ADD entity to _scene using entity's UName as key:
            _scene.Add(entity.UName, entity);
        }

        /// <summary>
        /// Retrieve a reference to the specified entity.
        /// </summary>
        /// <param name="uname">UName of the required entity</param>
        /// <returns></returns>
        public IEntity GetEntity(String uname)
        {
            return _scene[uname];
        }

        /// <summary>
        /// Update the scene graph - must be called on each simulation update cycle
        /// </summary>
        /// <param name="gameTime">The game timer</param>
        public void Update(GameTime gameTime)
        {
            // ITERATE through the scenegraph, updating entities:
            foreach (IEntity entity in _scene.Values)
            {
                entity.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw all entities in the game.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // BEGIN sprite batch:
            spriteBatch.Begin();

            // DRAW entities:
            foreach (IEntity entity in _scene.Values)
            {
                entity.Draw(spriteBatch);
            }

            // END sprite batch:
            spriteBatch.End();
        }
    }

    #endregion Methods
}