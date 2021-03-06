using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimulation
{
    internal class Water : Block
    {        
        public new static Texture2D Sprite { get; set; }

        private const int maxWaterAmount = 100;
        public const float transferSpeed = 1f;
        public const int WaterSize = 16;

        public float WaterAmount { get; set; }
        public bool HasWaterAboveIt { get; set; }

        public Water(int x, int y) : base(x, y, WaterSize)
        {
            this.WaterAmount = 100;

            tileType = BlockType.Water;
        }

        public Water(int x, int y, float waterAmount) : base(x, y, WaterSize)
        {
            tileType = BlockType.Water;

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
            double scale = 1;
            if (!HasWaterAboveIt)
            {
                scale = WaterAmount / maxWaterAmount;
            }
            int height = (int)(Size * scale);

            spritebacth.Draw(Sprite, new Rectangle(X, Y + (Size - height), Size, height), Color.White);
        }

        public bool CanAcceptWater()
        {
            return this.WaterAmount < maxWaterAmount;
        }

        public void GetWater(Water water, bool waterIsFlowingDown)
        {

            if (!waterIsFlowingDown)
            {
                //Ve vodě kam se vlévá musí být méně vody než ze které se vylévá
                if (this.WaterAmount < water.WaterAmount && (water.WaterAmount > 5))
                {
                    this.WaterAmount = Math.Min(WaterAmount + water.GiveWater(), 100);
                }
            }
            else
            {
                //Podmínka z kódu nahoře neplatí, pokud voda teče dolů
                this.WaterAmount = Math.Min(WaterAmount + water.GiveWater(), 100);                
            }
        }

        public float GiveWater()
        {
            this.WaterAmount -= transferSpeed;
            return transferSpeed;
        }

        public float GiveAllWater()
        {
            var tempWaterAmount = this.WaterAmount;
            this.WaterAmount = 0;
            return tempWaterAmount;
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
