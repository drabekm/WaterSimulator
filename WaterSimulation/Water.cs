using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    internal class Water : Block
    {
        public float WaterAmount { get; set; }
        public new static Texture2D Sprite { get; set; }

        private const int maxWaterAmount = 100;
        public const float transferSpeed = 1f;

        public Water(int x, int y) : base(x, y)
        {
            this.WaterAmount = 100;
            base.Size = 32;

            blockType = BlockType.Water;
        }

        public Water(int x, int y, float waterAmount) : base(x, y)
        {
            blockType = BlockType.Water;
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

        public Vector2 GetTopPosition()
        {
            return new Vector2(X, Y - Size);
        }

        public Vector2 GetBelowPosition()
        {
            return new Vector2(X, Y + Size);
        }

        public Vector2 GetRightHandPosition()
        {
            return new Vector2(X + Size, Y);
        }

        public Vector2 GetLeftHandPosition()
        {
            return new Vector2(X - Size, Y);
        }
    }
}
