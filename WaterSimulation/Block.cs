using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    abstract class Block : IColidable, IRenderable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }

        public Point Position { get; set; }
        public Rectangle Rectangle { get; set; }

        public BlockType tileType { get; set; }

        public static Texture2D Sprite { get; set; }        

        public Block(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;

            Position = new Point(x, y);
            Rectangle = new Rectangle(Position, new Point(Size, Size)); 
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException("Musíš si udělat vlastní lol");
        }

    }
}
