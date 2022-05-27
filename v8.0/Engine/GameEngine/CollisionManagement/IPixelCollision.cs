using Microsoft.Xna.Framework;

namespace Engine
{
    public interface IPixelCollision
    {
        /// <summary>
        /// Method for Pixel Collision
        /// </summary>
        /// <param name="rectangleA"></param>
        /// <param name="dataA"></param>
        /// <param name="rectangleB"></param>
        /// <param name="dataB"></param>
        /// <returns></returns>
        bool IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB);
    }
}