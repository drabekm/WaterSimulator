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

        public Vector2 Position { get; set; }

        public BlockType blockType { get; set; }

        public static Texture2D Sprite { get; set; }        

        public Block(int x, int y)
        {
            X = x;
            Y = y;

            Position = new Vector2(x, y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException("Musíš si udělat vlastní lol");
        }

    }
}
