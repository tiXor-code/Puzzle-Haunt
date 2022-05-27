using Microsoft.Xna.Framework;

namespace Engine
{
    /// <summary>
    /// This is referenced as Marc Price's code
    /// </summary>
    public interface ISceneManager
    {
        /// <summary>
        /// Place the given entity onto the Scene.
        /// </summary>
        /// <param name="entity">the entity to be spawned</param>
        /// <param name="position">the entity's _position</param>
        void Spawn(IEntity entity, Vector2 position);

        void Remove(); // does nothing at the moment
    }
}