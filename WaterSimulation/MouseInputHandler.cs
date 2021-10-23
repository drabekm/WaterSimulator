using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterSimulation
{
    static class MouseInputHandler
    {
        public static void HandleClick(MouseState mouseState, List<Block> elements)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                var tiles = elements.Where(x => x.tileType == BlockType.Tile);

                Block tileToBeDeleted = null;
                foreach(var tile in tiles)
                {
                    var tileRectangle = new Rectangle(tile.Position, new Point(tile.Size, tile.Size));
                    if (tileRectangle.Contains(mouseState.Position))
                    {
                        tileToBeDeleted = tile;
                        break;
                    }
                }

                if (tileToBeDeleted != null)
                {
                    elements.Remove(tileToBeDeleted);
                }
            }
        }
    }
}
