using System.Collections.Generic;

namespace KartonKrieger
{
    public class BattlefieldCell
    {
        CellGround Ground = null;

        public Character Character = null;

        Dictionary<CardinalDirection, BattlefieldCell> AdjacentCells = new Dictionary<CardinalDirection, BattlefieldCell>();

        public void LinkAdjacentCell(BattlefieldCell cell, CardinalDirection direction)
        {
            if (cell == null) return;
            AdjacentCells[direction] = cell;
        }
    }
}
