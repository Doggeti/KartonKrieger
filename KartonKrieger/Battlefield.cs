using System;
using System.Collections.Generic;

namespace KartonKrieger
{
    public class Battlefield
    {
        public BattlefieldCell[,] Cells;

        public List<Character> Characters = new List<Character>();

        public Battlefield(int lengthInCells, int widthInCells)
        {
            InitCells(lengthInCells, widthInCells);
            LinkCells();
        }

        public void InitCells(int lengthInCells, int widthInCells)
        {
            Random rnd = new Random();

            Cells = new BattlefieldCell[lengthInCells, widthInCells];

            for (int l = 0; l < Cells.GetLength(0); l++)
            {
                for (int w = 0; w < Cells.GetLength(1); w++)
                {
                    BattlefieldCell cell = new BattlefieldCell();

                    if (rnd.Next(10) < 1)
                    {
                        cell.Ground = new CellGround { AggregateState = AggregateState.Liquid };
                    }
                    else
                    {
                        cell.Ground = new CellGround { AggregateState = AggregateState.Solid };
                    }

                    Cells[l, w] = cell;
                }
            }
        }

        public void LinkCells()
        {
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    BattlefieldCell current = Cells[x, y];

                    if (x > 0)
                    {
                        BattlefieldCell west = Cells[x - 1, y];
                        current.LinkAdjacentCell(west, CardinalDirection.West);
                        west.LinkAdjacentCell(current, CardinalDirection.East);
                    }

                    if (x < Cells.GetLength(0) - 1)
                    {
                        BattlefieldCell east = Cells[x + 1, y];
                        current.LinkAdjacentCell(east, CardinalDirection.East);
                        east.LinkAdjacentCell(current, CardinalDirection.West);
                    }

                    if (y > 0)
                    {
                        BattlefieldCell north = Cells[x, y - 1];
                        current.LinkAdjacentCell(north, CardinalDirection.North);
                        north.LinkAdjacentCell(current, CardinalDirection.South);
                    }

                    if (y < Cells.GetLength(1) - 1)
                    {
                        BattlefieldCell south = Cells[x, y + 1];
                        current.LinkAdjacentCell(south, CardinalDirection.South);
                        south.LinkAdjacentCell(current, CardinalDirection.North);
                    }
                }
            }
        }

        public CardinalDirection GetOppsiteDirection(CardinalDirection direction)
        {
            switch (direction)
            {
                case CardinalDirection.North: return CardinalDirection.South;
                case CardinalDirection.South: return CardinalDirection.North;
                case CardinalDirection.East: return CardinalDirection.West;
                case CardinalDirection.West: return CardinalDirection.East;
                default: throw new Exception("Impossible CardinalDirection");
            }
        }

        public Tuple<int, int> FindCharacter(Character character)
        {
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    if (Cells[x, y].Character == character)
                    {
                        return new Tuple<int, int>(x, y);
                    }
                }
            }

            throw new Exception("Character not found");
        }
    }
}
