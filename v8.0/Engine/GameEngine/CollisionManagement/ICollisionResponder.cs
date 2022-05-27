namespace Engine
{
    /// <summary>
    /// All entities that need to respond to collisions must implement this interface.
    /// Reference to Marc Price for this Interface.
    /// </summary>
    public interface ICollisionResponder : ICollidable
    {
        /// <summary>
        /// To be called when collision with another collidable has been detected.
        /// </summary>
        /// <param name="other">The other collidable in collision.</param>
        void OnCollide(ICollidable other);
    }
}