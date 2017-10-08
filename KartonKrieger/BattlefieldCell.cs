using System.Collections.Generic;

namespace KartonKrieger
{
    public class BattlefieldCell
    {
        public CellGround Ground = null;

        public Character Character = null;

        public Dictionary<CardinalDirection, BattlefieldCell> AdjacentCells = new Dictionary<CardinalDirection, BattlefieldCell>();

        public bool Walkable
        {
            get
            {
                return Ground.AggregateState == AggregateState.Solid;
            }
        }

        public void LinkAdjacentCell(BattlefieldCell cell, CardinalDirection direction)
        {
            if (cell == null) return;
            AdjacentCells[direction] = cell;
        }
    }
}
