using System;

namespace Engine
{
    public interface IEntityManager
    {
        /// <summary>
        /// Create a new gameplay entity of type T. T must implement IEntity.
        /// </summary>
        /// <typeparam name="T">The concrete entity typename</typeparam>
        /// <param name="uname">A unique string-based name for the new entity</param>
        /// <returns></returns>
        IEntity Create<T>(String uname) where T : IEntity, new();
    }
}