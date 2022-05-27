using System.Collections.Generic;

namespace Engine
{
    public interface ICollisionManager
    {
        //void CheckWallCollision(int ScreenWidth, int ScreenHeight);

        //bool CheckCollision(Paddle paddle, Ball ball);

        // Cannot manage to make this work because it return an error everytime : CS0051
        void Initialise(IReadOnlyDictionary<string, IEntity> scene);
    }
}