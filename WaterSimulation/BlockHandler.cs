using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterSimulation
{
    class BlockHandler
    {
        public bool Paused { get; set; }

        public List<Block> Elements { get; set; }

        private BlockInputType _blockInputType { get; set; }
        private bool MouseStillDownAfterTileCreated = false;
        public BlockHandler(List<Block> elements)
        {
            this.Elements = elements;
        }


        public bool HandleInput(MouseState mouseState, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.P))
            {
                this.Paused = !this.Paused;
            }
            if (keyboardState.IsKeyDown(Keys.D1))
            {
                _blockInputType = BlockInputType.Erase;
            }
            if (keyboardState.IsKeyDown(Keys.D2))
            {
                _blockInputType = BlockInputType.Tile;
            }
            if (keyboardState.IsKeyDown(Keys.D3))
            {
                _blockInputType = BlockInputType.Water;
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!MouseStillDownAfterTileCreated)
                {
                    MouseStillDownAfterTileCreated = true;
                    switch (_blockInputType)
                    {
                        case BlockInputType.Erase:
                            List<Block> tilesToDelete = new List<Block>();
                            foreach (var element in Elements.Where(x => x.tileType == BlockType.Tile))
                            {
                                if (element.Rectangle.Contains(mouseState.Position))
                                {
                                    tilesToDelete.Add(element);
                                }
                            }
                            Elements.RemoveAll(x => tilesToDelete.Contains(x));
                            break;

                        case BlockInputType.Tile:
                            foreach (var element in Elements.Where(x => x.tileType == BlockType.Tile))
                            {
                                if (element.Rectangle.Contains(mouseState.Position))
                                {
                                    break;
                                }
                            }

                            var newTileX = (mouseState.Position.X / Tile.TileSize) * Tile.TileSize;
                            var newTileY = (mouseState.Position.Y / Tile.TileSize) * Tile.TileSize;
                            Elements.Add(new Tile(newTileX, newTileY));
                            break;

                        case BlockInputType.Water:
                            foreach (var element in Elements.Where(x => x.tileType == BlockType.Tile))
                            {
                                if (element.Rectangle.Contains(mouseState.Position))
                                {
                                    break;
                                }
                            }

                            var newTileXA = (mouseState.Position.X / Water.WaterSize) * Water.WaterSize;
                            var newTileYA = (mouseState.Position.Y / Water.WaterSize) * Water.WaterSize;
                            Elements.Add(new Water(newTileXA, newTileYA));
                            return true;
                    }
                }

            }
            else
            {
                MouseStillDownAfterTileCreated = false;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var element in Elements)
            {
                element.Draw(spriteBatch);
            }
        }

        public void MoveWater()
        {
            if (Paused)
            {
                return;
            }

            //var waterBlocks = OrderElementsByHeightDESC(GetWaterFromElements(Elements));
            var waterBlocks = OrderElementsByHeightASC(GetWaterFromElements(Elements));

            foreach (var water in waterBlocks)
            {
                var colisionPosition = water.GetBelowPosition();
                var colider = CheckForColision(colisionPosition, Elements);
                var floorColider = HandleColision(colider, water, colisionPosition, Elements, false);

                //Voda se nemůže rozlévat do stran pokud nemá pevné podloží (buď pevný blok nebo vodu na maximální kapacitě)
                var waterIsOnFloor = WaterIsOnFloor(floorColider);

                colisionPosition = water.GetTopPosition();
                colider = CheckForColision(colisionPosition, Elements);
                HandleColision(colider, water, colisionPosition, Elements, waterIsOnFloor);

                colisionPosition = water.GetLeftHandPosition();
                colider = CheckForColision(colisionPosition, Elements);
                HandleColision(colider, water, colisionPosition, Elements, waterIsOnFloor);

                colisionPosition = water.GetRightHandPosition();
                colider = CheckForColision(colisionPosition, Elements);
                HandleColision(colider, water, colisionPosition, Elements, waterIsOnFloor);
            }

            var markedForDeletion = CheckForEmptyWaterBlocks(waterBlocks); 
            Elements.RemoveAll(x => markedForDeletion.Contains(x));

            var totalWaterAmount = GetWaterFromElements(Elements).Sum(x => x.WaterAmount);
            if (GetWaterFromElements(Elements).Count() > 0 && totalWaterAmount != 100)
            {
                int asdasd = 23;
            }
        }

        private bool IsWrongWaterAmount(List<Water> waterBlocks)
        {
            return waterBlocks.Sum(x => x.WaterAmount) != 100;
        }

        private List<Water> GetWaterFromElements(List<Block> elements)
        {
            return elements.Where(x => x is Water).Select(x => x as Water).ToList();            
        }

        private List<Water> OrderElementsByHeightDESC(List<Water> waterBlocks)
        {
            return waterBlocks.OrderByDescending(x => x.Y).ToList();
        }

        private List<Water> OrderElementsByHeightASC(List<Water> waterBlocks)
        {
            return waterBlocks.OrderBy(x => x.Y).ToList();
        }

        private Block HandleColision(Block colider, Water water, Point colisionPosition, List<Block> elements, bool waterIsOnFloor)
        {
            //Při kontrole horních bloků se dějí bloky kódu, které se jindy nedějí
            bool checkingUpperPosition = water.Position.Y > colisionPosition.Y;
            bool checkingBelowPosition = water.Position.Y < colisionPosition.Y;            

            if (colider == null)
            {
                //Voda se může roztéct na prázdné místo pokud je na pevné podlaze, nebo pokud pod ní nic není
                if (!checkingUpperPosition && (waterIsOnFloor || checkingBelowPosition))
                {
                    if (water.WaterAmount > 5)
                    {
                        var newWater = new Water(colisionPosition.X, colisionPosition.Y, water.GiveWater());
                        elements.Add(newWater);
                    }
                    
                }

                if (checkingUpperPosition)
                {
                    water.HasWaterAboveIt = false;
                }
            }

            if (colider is Water)
            {
                var waterColider = colider as Water;

                //Voda se může roztéct na prázdné místo pokud je na pevné podlaze, nebo pokud pod ní nic není
                if (!checkingUpperPosition && waterColider.CanAcceptWater() && (waterIsOnFloor || checkingBelowPosition))
                {                    
                    waterColider.GetWater(water, checkingBelowPosition);
                }

                if (checkingUpperPosition)
                {
                    water.HasWaterAboveIt = true;
                }
            }

            if (colider is Tile)
            {
                if (checkingUpperPosition)
                {
                    water.HasWaterAboveIt = false;
                }
            }


            return colider;
        }

        private bool WaterIsOnFloor(Block colider)
        {
            if (colider is Tile)
            {
                return true;
            }

            var waterColider = colider as Water;
            if (waterColider != null)
            {
                return waterColider.WaterAmount == 100;
            }

            return false;
        }

        private Block CheckForColision(Point colisionPosition, List<Block> elements)
        {            
            foreach(var element in elements)
            {
                var elementRecangle = new Rectangle(element.Position,new Point(element.Size, element.Size));
                if (elementRecangle.Contains(colisionPosition))
                {
                    return element;
                }
            }

            return null;
        }

        private List<Block> CheckForEmptyWaterBlocks(List<Water> waterBlocks)
        {
            List<Block> markedForDeletion = new List<Block>();
            foreach(var water in waterBlocks)
            {
                if (water.LostTooMuchWater())
                {
                    markedForDeletion.Add(water);
                }
            }

            return markedForDeletion;
        }
    }

    enum BlockInputType
    {
        Erase,
        Water,
        Tile,
    }
}
