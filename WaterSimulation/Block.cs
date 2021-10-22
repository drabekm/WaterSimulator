using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    internal class Block : Tile
    {

        public new static Texture2D Sprite { get; set; }

        public Block(int x, int y) : base(x, y)
        {
            tilekType = BlockType.Tile;
            base.Size = 32;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Rectangle(X, Y, Size, Size), Color.White);
        }
    }
}
