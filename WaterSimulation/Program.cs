using System;

namespace WaterSimulation
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new WaterSimulator())
                game.Run();
        }
    }
}
