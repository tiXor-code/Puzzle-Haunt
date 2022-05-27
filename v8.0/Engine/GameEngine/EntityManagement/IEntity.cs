using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public interface IEntity
    {
        /// <summary>
        /// Getter/Setter for the Unique Name
        /// </summary>
        string UName { get; set; }

        //Texture2D LoadTexture { get; set; }

        /// <summary>
        /// Getter/Setter for the Texture
        /// </summary>
        Texture2D Texture { get; set; }

        AnimationManager AnimationMgr { get; set; }

        /// <summary>
        /// Getter/Setter for the Location
        /// </summary>
        Vector2 Locn { get; set; }

        /// <summary>
        /// Getter/Setter for the Velocity
        /// </summary>
        Vector2 Velocity { get; }

        /// <summary>
        /// insert comments
        /// </summary>
        Vector2 Direction { get; set; }

        /// <summary>
        /// insert comments
        /// </summary>
        float Speed { get; }

        /// <summary>
        /// insert comments
        /// </summary>
        void Draw(SpriteBatch spriteBatch);

        //Texture2D Texture { get; }

        /// <summary>
        /// insert comments
        /// </summary>
        void LoadTexture(string Key, Texture2D texture);

        /// <summary>
        ///
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="texture"></param>
        /// <param name="frameCount"></param>
        void LoadAnimation(string Key, Texture2D texture, int frameCount);

        void Garbage(Entity entity);

        /// <summary>
        ///  Exposes prototype for Update() functionality:
        /// </summary>
        /// <param name="pGameTime"></param>
        void Update(GameTime pGameTime);
    }
}