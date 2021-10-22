using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterSimulation
{
    static class WaterHandler
    {
        public static void MoveWater(List<Tile> elements)
        {
            var waterBlocks = OrderElementsByHeightDESC(GetWaterFromElements(elements));

            foreach(var water in waterBlocks)
            {
                var colisionPosition = water.GetBelowPosition();
                var colider = CheckForColision(colisionPosition, elements);
                var floorColider = HandleColision(colider, water, colisionPosition, elements, false);

                //Voda se nemůže rozlévat do stran pokud nemá pevné podloží (buď pevný blok nebo vodu na maximální kapacitě)
                var waterIsOnFloor = WaterIsOnFloor(floorColider);

                colisionPosition = water.GetTopPosition();
                colider = CheckForColision(colisionPosition, elements);
                HandleColision(colider, water, colisionPosition, elements, waterIsOnFloor);

                colisionPosition = water.GetLeftHandPosition();
                colider = CheckForColision(colisionPosition, elements);
                HandleColision(colider, water, colisionPosition, elements, waterIsOnFloor);

                colisionPosition = water.GetRightHandPosition();
                colider = CheckForColision(colisionPosition, elements);
                HandleColision(colider, water, colisionPosition, elements, waterIsOnFloor);
            }

            var markedForDeletion = CheckForEmptyWaterBlocks(waterBlocks);
            elements.RemoveAll(x => markedForDeletion.Contains(x));
        }

        private static List<Water> GetWaterFromElements(List<Tile> elements)
        {
            return elements.Where(x => x is Water).Select(x => x as Water).ToList();            
        }

        private static List<Water> OrderElementsByHeightDESC(List<Water> waterBlocks)
        {
            return waterBlocks.OrderByDescending(x => x.Y).ToList();
        }

        private static Tile HandleColision(Tile colider, Water water, Point colisionPosition, List<Tile> elements, bool waterIsOnFloor)
        {
            //Při kontrole horních bloků se dějí bloky kódu, které se jindy nedějí
            bool checkingUpperPosition = water.Position.Y > colisionPosition.Y;
            bool checkingBelowPosition = water.Position.Y < colisionPosition.Y;            

            if (colider == null)
            {
                //Voda se může roztéct na prázdné místo pokud je na pevné podlaze, nebo pokud pod ní nic není
                if (!checkingUpperPosition && (waterIsOnFloor || checkingBelowPosition))
                {
                    var newWater = new Water(colisionPosition.X, colisionPosition.Y, water.GiveWater());
                    elements.Add(newWater);
                }

                if (checkingUpperPosition)
                {
                    water.HasWaterAboveIt = false;
                }
            }

            if (colider is Water)
            {
                //Voda se může roztéct na prázdné místo pokud je na pevné podlaze, nebo pokud pod ní nic není
                if (!checkingUpperPosition && (waterIsOnFloor || checkingBelowPosition))
                {
                    var waterColider = colider as Water;
                    waterColider.GetWater(water, checkingBelowPosition);
                }

                if (checkingUpperPosition)
                {
                    water.HasWaterAboveIt = true;
                }
            }

            if (colider is Block)
            {
                if (checkingUpperPosition)
                {
                    water.HasWaterAboveIt = false;
                }
            }


            return colider;
        }

        private static bool WaterIsOnFloor(Tile colider)
        {
            if (colider is Block)
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

        private static Tile CheckForColision(Point colisionPosition, List<Tile> elements)
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

        private static List<Tile> CheckForEmptyWaterBlocks(List<Water> waterBlocks)
        {
            List<Tile> markedForDeletion = new List<Tile>();
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
}
