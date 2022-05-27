using Microsoft.Xna.Framework;

namespace Engine
{
    /// <summary>
    /// Reference to Marc Price
    /// </summary>
    internal interface IEntityInternal : IEntity
    {
        void Spawn(Vector2 position);

        void Terminate();
    }
}