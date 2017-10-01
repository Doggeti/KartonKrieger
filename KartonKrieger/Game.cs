using System;
using System.Collections.Generic;
using System.Linq;

namespace KartonKrieger
{
    class Game
    {
        public Random Randomizer = new Random();
        public Battlefield Field = new Battlefield(10, 10);
        public Character ActiveCharacter;

        public void StartRound()
        {
            UpdateInitiative();

            ActiveCharacter = ChooseActiveCharacter();

            ActiveCharacter.ActionPoints = ActiveCharacter.ActionPointsStart;
        }

        public void UpdateInitiative()
        {
            while (Field.Characters.Select(c => c.Initiative).Max() < 100)
            {
                foreach (Character character in Field.Characters)
                {
                    character.Initiative += character.InitiativeGain;
                } 
            }
        }

        public Character ChooseActiveCharacter()
        {
            float max = Field.Characters.Select(c => c.Initiative).Max();

            var maxCharacters = Field.Characters.Where(c => c.Initiative == max);

            return maxCharacters.FirstOrDefault();
        }

        public int GetMoveCosts(int x, int y)
        {
            return 1;
        }

        public void TryMoveNorth()
        {
            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (y == 0)
            {
                return;
            }

            int targetX = x;
            int targetY = y - 1;

            if (Field.Cells[targetX, targetY].Character != null)
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            Field.Cells[targetX, targetY].Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.North;
        }

        public void TryMoveSouth()
        {
            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (y == Field.Cells.GetLength(1) - 1)
            {
                return;
            }

            int targetX = x;
            int targetY = y + 1;

            if (Field.Cells[targetX, targetY].Character != null)
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            Field.Cells[targetX, targetY].Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.South;
        }

        public void TryMoveWest()
        {
            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (x == 0)
            {
                return;
            }

            int targetX = x - 1;
            int targetY = y;

            if (Field.Cells[targetX, targetY].Character != null)
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            Field.Cells[x - 1, y].Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.West;
        }

        public void TryMoveEast()
        {
            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (x == Field.Cells.GetLength(0) - 1)
            {
                return;
            }

            int targetX = x + 1;
            int targetY = y;

            if (Field.Cells[targetX, targetY].Character != null)
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            Field.Cells[x + 1, y].Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.East;
        }

        public List<Attack> FindAttacks()
        {
            var weapons = ActiveCharacter.Inventory.OfType<Weapon>();
            var attacks = weapons.SelectMany(w => w.Attacks);
            return attacks.ToList();
        }
    }
}
