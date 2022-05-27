using Microsoft.Xna.Framework;

namespace Engine
{
    public class SceneManager : ISceneManager
    {
        #region Fields

        private SceneGraph _sceneGraph;

        #endregion Fields



        #region Public Methods

        /// <summary>
        /// Initialise the scene manager.
        /// </summary>
        /// <param name="collisionMgr">A reference to the collision manager.</param>
        // Cannot manage to make this work because it return an error everytime : CS0051
        // If I make it public, the error is created and I am unable to fix it
        public void Spawn(IEntity entity, Vector2 Locn)
        {
            entity.Locn = Locn;
        }

        public void Remove()
        {
            // add functionality in here
        }

        #endregion Public Methods
    }
}