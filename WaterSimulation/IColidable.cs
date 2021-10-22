using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    interface IColidable
    {
        int X { get; set; }
        int Y { get; set; }
        int Size { get; set; }
    }
}
