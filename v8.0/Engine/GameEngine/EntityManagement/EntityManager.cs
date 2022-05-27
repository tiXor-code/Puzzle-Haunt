using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Most of this class is referenced to Marc Price
    /// </summary>
    public class EntityManager : IEntityManager
    {
        #region Fields

        private IDictionary<String, IEntity> _entities = new Dictionary<String, IEntity>();

        #endregion Fields



        #region Methods

        /// <summary>
        /// Create a new gameplay entity of type T. T must implement IEntity.
        /// </summary>
        /// <typeparam name="T">The concrete entity typename</typeparam>
        /// <param name="uname">A unique string-based name for the new entity</param>
        /// <returns></returns>
        public IEntity Create<T>(String uname) where T : IEntity, new()
        {
            // CREATE new entity and store it in _entities:
            _entities.Add(uname, new T());

            // RETURN a reference to the new entity;
            return _entities[uname];
        }

        #endregion Methods
    }
}