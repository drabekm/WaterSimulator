using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    abstract class Tile : IColidable, IRenderable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }

        public Point Position { get; set; }

        public BlockType tilekType { get; set; }

        public static Texture2D Sprite { get; set; }        

        public Tile(int x, int y)
        {
            X = x;
            Y = y;

            Position = new Point(x, y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException("Musíš si udělat vlastní lol");
        }

    }
}
