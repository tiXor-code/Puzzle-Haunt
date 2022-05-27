using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// Most of this class is referenced to Marc Price
    /// </summary>
    public class CollisionManager : ICollisionManager
    {
        #region Fields

        // DECLARE a reference to an IReadOnlyDictionary<String, IEntity> to store reference to the scene:
        private IReadOnlyDictionary<String, IEntity> _scene;

        #endregion Fields



        #region Public Methods

        /// <summary>
        /// Initialise the collision manager.
        /// </summary>
        /// <param name="scene">A read-only reference to the scene</param>
        // Cannot manage to make this work because it return an error everytime : CS0051
        // If I make it public, the error is created and I am unable to fix it
        public void Initialise(IReadOnlyDictionary<String, IEntity> scene)
        {
            // ASSIGN _scene:
            _scene = scene;
        }

        /// <summary>
        /// Update the collision manager - must be caller prior to each scene update.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // CREATE a list to store all collidables in _scene, call it collidables...
            IList<ICollidable> collidables = new List<ICollidable>();

            // ITERATE through _scene and store all collidable entities into collidables:
            foreach (IEntity entity in _scene.Values)
            {
                // TEST if entities[i] is a collidable object:
                if (entity is ICollidable)
                {
                    collidables.Add(entity as ICollidable);
                }
            }

            // ITERATE through collidables, checking pairs for collision:
            for (int i = 0; i < collidables.Count() - 1; i++)
            {
                for (int j = i + 1; j < collidables.Count(); j++)
                {
                    CheckAndRespond(collidables[i], collidables[j]);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Check for collision between a pair of collidables and respond if true.
        /// </summary>
        /// <param name="a">first collidable in pair</param>
        /// <param name="b">second collidable in pair</param>
        private void CheckAndRespond(ICollidable a, ICollidable b)
        {
            // CHECK for collision between a and b:
            if (a.HitBox.Intersects(b.HitBox))
            {
                // RESPOND to collision where needed:
                if (a is ICollisionResponder)
                {
                    (a as ICollisionResponder).OnCollide(b);
                }
                if (b is ICollisionResponder)
                {
                    (b as ICollisionResponder).OnCollide(a);
                }
            }
        }

        #endregion Private Methods
    }
}