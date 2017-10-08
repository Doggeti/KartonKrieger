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

            foreach (var attack in FindAttacks())
            {
                if (attack.Buildup > 0) attack.Buildup--;
                if (attack.ActiveCooldown > 0) attack.ActiveCooldown--;
            }
        }

        public void UpdateInitiative()
        {
            while (Field.Characters.Select(c => c.Initiative).Max() < 100)
            {
                foreach (Character character in Field.Characters)
                {
                    if (character.Alive)
                    {
                        character.Initiative += character.InitiativeGain;
                    }
                    else
                    {
                        character.Initiative = 0;
                    }
                } 
            }
        }

        public Character ChooseActiveCharacter()
        {
            var aliveCharacters = Field.Characters.Where(c => c.Alive);

            float max = aliveCharacters.Select(c => c.Initiative).Max();

            var maxCharacters = aliveCharacters.Where(c => c.Initiative == max);

            return maxCharacters.FirstOrDefault();
        }

        public int GetMoveCosts(int x, int y)
        {
            return 1;
        }

        private bool CanEnterCell(Character character, BattlefieldCell cell)
        {
            if (cell.Ground.AggregateState == AggregateState.Solid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TryMoveNorth()
        {
            ActiveCharacter.Facing = CardinalDirection.North;

            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (y == 0)
            {
                return;
            }

            int targetX = x;
            int targetY = y - 1;

            BattlefieldCell targetCell = Field.Cells[targetX, targetY];

            if (targetCell.Character != null)
            {
                return;
            }

            if (!CanEnterCell(ActiveCharacter, targetCell))
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            targetCell.Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.North;
        }

        public void TryMoveSouth()
        {
            ActiveCharacter.Facing = CardinalDirection.South;

            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (y == Field.Cells.GetLength(1) - 1)
            {
                return;
            }

            int targetX = x;
            int targetY = y + 1;

            BattlefieldCell targetCell = Field.Cells[targetX, targetY];

            if (targetCell.Character != null)
            {
                return;
            }

            if (!CanEnterCell(ActiveCharacter, targetCell))
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            targetCell.Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.South;
        }

        public void TryMoveWest()
        {
            ActiveCharacter.Facing = CardinalDirection.West;

            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (x == 0)
            {
                return;
            }

            int targetX = x - 1;
            int targetY = y;

            BattlefieldCell targetCell = Field.Cells[targetX, targetY];

            if (targetCell.Character != null)
            {
                return;
            }

            if (!CanEnterCell(ActiveCharacter, targetCell))
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            targetCell.Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.West;
        }

        public void TryMoveEast()
        {
            ActiveCharacter.Facing = CardinalDirection.East;

            var coordinates = Field.FindCharacter(ActiveCharacter);

            int x = coordinates.Item1;
            int y = coordinates.Item2;

            if (x == Field.Cells.GetLength(0) - 1)
            {
                return;
            }

            int targetX = x + 1;
            int targetY = y;

            BattlefieldCell targetCell = Field.Cells[targetX, targetY];

            if (targetCell.Character != null)
            {
                return;
            }

            if (!CanEnterCell(ActiveCharacter, targetCell))
            {
                return;
            }

            int moveCosts = GetMoveCosts(targetX, targetY);

            if (moveCosts > ActiveCharacter.ActionPoints)
            {
                return;
            }

            ActiveCharacter.ActionPoints -= moveCosts;

            targetCell.Character = ActiveCharacter;
            Field.Cells[x, y].Character = null;

            ActiveCharacter.Facing = CardinalDirection.East;
        }

        public List<Attack> FindAttacks()
        {
            var weapons = ActiveCharacter.Inventory.OfType<Weapon>();
            var attacks = weapons.SelectMany(w => w.Attacks);
            return attacks.ToList();
        }

        public Character FindTargetForAttack()
        {
            if (ActiveCharacter?.SelectedAttack == null)
            {
                return null;
            }

            var start = Field.FindCharacter(ActiveCharacter);
            BattlefieldCell currentCell = Field.Cells[start.Item1, start.Item2];

            int minRange = ActiveCharacter.SelectedAttack.MinRange;
            int maxRange = ActiveCharacter.SelectedAttack.MaxRange;

            for (int step = 1; step <= maxRange; step++)
            {
                if (currentCell.AdjacentCells[ActiveCharacter.Facing] == null)
                {
                    return null;
                }
                else
                {
                    currentCell = currentCell.AdjacentCells[ActiveCharacter.Facing];
                }

                if (step < minRange)
                {
                    continue;
                }

                if (currentCell.Character != null && ActiveCharacter.Faction != currentCell.Character.Faction)
                {
                    return currentCell.Character;
                }
            }

            return null;
        }
    }
}
