using System;

namespace Puzzle_Haunt
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Kernel())
                game.Run();
        }
    }
}