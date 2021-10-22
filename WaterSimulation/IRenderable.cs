using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    interface IRenderable
    {
        public void Draw(SpriteBatch spritebatch);
    }
}
