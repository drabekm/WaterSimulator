using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    internal class Water : Tile
    {
        public float WaterAmount { get; set; }
        public new static Texture2D Sprite { get; set; }

        private const int maxWaterAmount = 100;
        public const float transferSpeed = 1f;

        public Water(int x, int y) : base(x, y)
        {
            this.WaterAmount = 100;
            base.Size = 32;

            tilekType = BlockType.Water;
        }

        public Water(int x, int y, float waterAmount) : base(x, y)
        {
            tilekType = BlockType.Water;
            base.Size = 32;

            if (waterAmount > 100)
            {
                this.WaterAmount = 100;
            }
            else
            {
                this.WaterAmount = waterAmount;
            }
        }

        public override void Draw(SpriteBatch spritebacth)
        {
            double scale = WaterAmount / maxWaterAmount;
            int height = (int)(Size * scale);

            spritebacth.Draw(Sprite, new Rectangle(X, Y + (Size - height), Size, height), Color.White);
        }

        public void GetWater(float givenWater)
        {            
            this.WaterAmount = Math.Min(WaterAmount + givenWater, 100);
        }

        public float GiveWater()
        {
            this.WaterAmount -= transferSpeed;
            return transferSpeed;
        }

        public bool LostTooMuchWater()
        {
            return this.WaterAmount <= 0;
        }

        public Point GetTopPosition()
        {
            return new Point(X, Y - Size);
        }

        public Point GetBelowPosition()
        {
            return new Point(X, Y + Size);
        }

        public Point GetRightHandPosition()
        {
            return new Point(X + Size, Y);
        }

        public Point GetLeftHandPosition()
        {
            return new Point(X - Size, Y);
        }
    }
}
