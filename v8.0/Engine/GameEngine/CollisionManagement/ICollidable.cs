using Microsoft.Xna.Framework;

namespace Engine
{
    /// <summary>
    /// Entities MUST implement this interface to be able to collide.
    /// Reference to Marc Price for this Interface.
    /// </summary>
    public interface ICollidable
    {
        /// <summary>
        /// A reference to a AABB hitbox as a Monogame Rectangle.
        /// </summary>
        Rectangle HitBox { get; }
    }
}