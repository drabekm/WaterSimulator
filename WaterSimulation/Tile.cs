using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    internal class Tile : Block
    {
        public new static Texture2D Sprite { get; set; }

        public const int TileSize = 32;

        public Tile(int x, int y) : base(x, y, TileSize)
        {
            tileType = BlockType.Tile;
            base.Size = TileSize;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Rectangle(X, Y, TileSize, TileSize), Color.White);
        }
    }
}
