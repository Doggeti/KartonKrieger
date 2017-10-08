using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace KartonKrieger
{
    public partial class Form1 : Form
    {
        static int BOARD_SIZE = 10;

        Game Game = new Game();
        Attack[] DisplayedAttacks = new Attack[3];

        public RichTextBox[,] Cells = new RichTextBox[BOARD_SIZE, BOARD_SIZE];

        public Form1()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1: Attack1.PerformClick(); break;
                case Keys.D2: Attack2.PerformClick(); break;
                case Keys.D3: Attack3.PerformClick(); break;
                case Keys.Up: GoNorth.PerformClick(); break;
                case Keys.Down: GoSouth.PerformClick(); break;
                case Keys.Left: GoWest.PerformClick(); break;
                case Keys.Right: GoEast.PerformClick(); break;
                case Keys.A: Attack.PerformClick(); break;
                case Keys.E: EndRound.PerformClick(); break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Width = 1200;
            Height = 800;

            CreateCells();

            RandomizeCells();

            Game.StartRound();

            DrawControls();

            DrawCells();
        }

        private void CreateCells()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    int size = 60;

                    RichTextBox rtb = new RichTextBox();
                    rtb.Width = size;
                    rtb.Height = size;
                    rtb.TabStop = false;
                    rtb.SelectionAlignment = HorizontalAlignment.Center;

                    Controls.Add(rtb);

                    rtb.Left = x * rtb.Width + 200;
                    rtb.Top = y * rtb.Height + size;

                    Cells[x, y] = rtb;
                }
            }
        }

        private void RandomizeCells()
        {
            int x = Game.Randomizer.Next(0, 10);
            int y = Game.Randomizer.Next(0, 10);

            Character character;

            character = AddCharacterToField(x, y, "Player", (CardinalDirection)Game.Randomizer.Next(0, 4), "Friendly");
            character.Inventory.Add(new Weapons.PaperClip());

            int enemyCount = 2;

            while (enemyCount > 0)
            {
                x = Game.Randomizer.Next(0, 10);
                y = Game.Randomizer.Next(0, 10);

                if (Game.Field.Cells[x, y].Character != null)
                {
                    continue;
                }

                if (Game.Field.Cells[x,y].Ground.AggregateState == AggregateState.Liquid)
                {
                    continue;
                }

                character = AddCharacterToField(x, y, $"Enemy{enemyCount}", (CardinalDirection)Game.Randomizer.Next(0, 4), "Enemy");
                character.Inventory.Add(new Weapons.PaperClip());

                enemyCount--;
            }
        }

        private Character AddCharacterToField(int x, int y, string name, CardinalDirection facing, string faction)
        {
            Character character = new Character();
            character.Level = 1;
            character.Name = name;
            character.InitiativeGain = Game.Randomizer.Next(20, 81);
            character.ActionPointsStart = 1;
            character.Facing = facing;
            character.Faction = faction;

            character.AddProperty<Live>();
            CharacterProperty live = character.GetProperty<Live>();
            live.BaseMaxValue = 9;
            live.LevelMultiplier = 1;
            live.CurrentValue = live.GetMaxValueAtLevel(character.Level);

            Game.Field.Cells[x, y].Character = character;
            Game.Field.Characters.Add(character);

            return character;
        }

        private void DrawControls()
        {
            ActiveCharacter.Text = Game.ActiveCharacter?.Name ?? string.Empty;
            ActionPoints.Text = $"{Game.ActiveCharacter?.ActionPoints ?? 0}";

            int number = 0;

            List<Attack> attacks = Game.FindAttacks();

            foreach (var attack in attacks)
            {
                DisplayedAttacks[number] = attack;

                number++;

                Button attackButton = Controls.Find($"Attack{number}", false).OfType<Button>().FirstOrDefault();

                if (attackButton == null)
                {
                    break;
                }

                if (Game.ActiveCharacter.SelectedAttack != null && attack == Game.ActiveCharacter.SelectedAttack)
                {
                    attackButton.Font = new System.Drawing.Font(attackButton.Font, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    attackButton.Font = new System.Drawing.Font(attackButton.Font, System.Drawing.FontStyle.Regular);
                }

                attackButton.Text = attack.Name;

                if (attack.Buildup > 0)
                {
                    attackButton.Text += $" [{attack.Buildup}]";
                    attackButton.Enabled = false;
                }
                else if (attack.ActiveCooldown > 0)
                {
                    attackButton.Text += $" [{attack.ActiveCooldown}]";
                    attackButton.Enabled = false;
                }
                else
                {
                    attackButton.Enabled = true;
                }

                foreach (var damageTypeValues in attack.DamageTypes)
                {
                    DamageType type = damageTypeValues.Key;
                    var values = damageTypeValues.Value;

                    attackButton.Text += Environment.NewLine + $"{type} ({values.Item1}-{values.Item1 + values.Item2 + values.Item3})";
                }
            }

            while (number < 3)
            {
                DisplayedAttacks[number] = null;

                number++;

                Button attackButton = Controls.Find($"Attack{number}", false).OfType<Button>().FirstOrDefault();

                attackButton.Text = string.Empty;
            }
        }

        private void DrawCells()
        {
            int activeX = 0;
            int activeY = 0;

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    RichTextBox rtb = Cells[x, y];
                    BattlefieldCell cell = Game.Field.Cells[x, y];
                    Character character = cell.Character;

                    switch (cell.Ground.AggregateState)
                    {
                        case AggregateState.Solid: rtb.BackColor = System.Drawing.Color.LightGreen; break;
                        case AggregateState.Liquid: rtb.BackColor = System.Drawing.Color.LightBlue; break;
                    }

                    if (character == null)
                    {
                        rtb.Text = string.Empty;
                        rtb.Font = new System.Drawing.Font(rtb.Font, System.Drawing.FontStyle.Regular);
                    }
                    else
                    {
                        rtb.Text = character.Name;

                        if (character.Alive)
                        {
                            rtb.Text += Environment.NewLine + $"{character.GetPropertyValue<Live>()} / {character.GetPropertyMax<Live>()}";
                            rtb.Text += Environment.NewLine + $"{character.Initiative} ({character.InitiativeGain})";

                            switch (cell.Character.Facing)
                            {
                                case CardinalDirection.North:
                                    rtb.Text += Environment.NewLine + "↑";
                                    break;
                                case CardinalDirection.South:
                                    rtb.Text += Environment.NewLine + "↓";
                                    break;
                                case CardinalDirection.East:
                                    rtb.Text += Environment.NewLine + "→";
                                    break;
                                case CardinalDirection.West:
                                    rtb.Text += Environment.NewLine + "←";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            rtb.Text += Environment.NewLine + "✝";
                        }

                        if (cell.Character == Game.ActiveCharacter)
                        {
                            activeX = x;
                            activeY = y;

                            rtb.Font = new System.Drawing.Font(rtb.Font, System.Drawing.FontStyle.Bold);
                        }
                        else
                        {
                            rtb.Font = new System.Drawing.Font(rtb.Font, System.Drawing.FontStyle.Regular);
                        }
                    }
                }
            }

            if (activeX > 0) DrawMoveCosts(activeX - 1, activeY);
            if (activeX < BOARD_SIZE - 1) DrawMoveCosts(activeX + 1, activeY);
            if (activeY > 0) DrawMoveCosts(activeX, activeY - 1);
            if (activeY < BOARD_SIZE - 1) DrawMoveCosts(activeX, activeY + 1);
        }

        private void DrawMoveCosts(int x, int y)
        {
            RichTextBox rtb = Cells[x, y];

            if (Game.Field.Cells[x, y].Character == null)
            {
                rtb.Text = $"{Game.GetMoveCosts(x, y)}";
            }
        }

        private void EndRound_Click(object sender, EventArgs e)
        {
            if (Game.ActiveCharacter.ActionPoints > 0)
            {
                var r = MessageBox.Show("End turn?", "Action points remaining", MessageBoxButtons.OKCancel);

                if (r == DialogResult.Cancel)
                {
                    return;
                }
            }

            Game.ActiveCharacter.Initiative = 0;

            Game.StartRound();

            DrawControls();
            DrawCells();
        }

        private void GoNorth_Click(object sender, EventArgs e)
        {
            Game.TryMoveNorth();

            DrawControls();
            DrawCells();
        }

        private void GoSouth_Click(object sender, EventArgs e)
        {
            Game.TryMoveSouth();

            DrawControls();
            DrawCells();
        }

        private void GoWest_Click(object sender, EventArgs e)
        {
            Game.TryMoveWest();

            DrawControls();
            DrawCells();
        }

        private void GoEast_Click(object sender, EventArgs e)
        {
            Game.TryMoveEast();

            DrawControls();
            DrawCells();
        }

        private void Attack1_Click(object sender, EventArgs e)
        {
            if (Game.ActiveCharacter != null)
            {
                Game.ActiveCharacter.SelectedAttack = DisplayedAttacks[0];
            }

            DrawControls();
        }

        private void Attack2_Click(object sender, EventArgs e)
        {
            if (Game.ActiveCharacter != null)
            {
                Game.ActiveCharacter.SelectedAttack = DisplayedAttacks[1];
            }

            DrawControls();
        }

        private void Attack3_Click(object sender, EventArgs e)
        {
            if (Game.ActiveCharacter != null)
            {
                Game.ActiveCharacter.SelectedAttack = DisplayedAttacks[2];
            }

            DrawControls();
        }

        private void Attack_Click(object sender, EventArgs e)
        {
            Attack attack = Game.ActiveCharacter?.SelectedAttack;

            if (attack == null)
            {
                return;
            }

            if (attack.Costs > Game.ActiveCharacter.ActionPoints)
            {
                return;
            }

            Character target = Game.FindTargetForAttack();

            if (target != null)
            {
                var live = target.GetProperty<Live>();

                float precision = Game.Randomizer.Next(0, 101);

                foreach (var damageTypeValues in attack.DamageTypes)
                {
                    DamageType type = damageTypeValues.Key;
                    var values = damageTypeValues.Value;

                    float damage = values.Item1;

                    damage += values.Item2 * precision / 100.0f;

                    live.CurrentValue -= (float)Math.Round(damage);
                }

                Game.ActiveCharacter.ActionPoints -= Game.ActiveCharacter.SelectedAttack.Costs;

                attack.ActiveCooldown = attack.Cooldown;
            }

            DrawControls();
            DrawCells();
        }

        private void TurnLeft_Click(object sender, EventArgs e)
        {
            if (Game.ActiveCharacter == null)
            {
                return;
            }

            switch (Game.ActiveCharacter.Facing)
            {
                case CardinalDirection.North: Game.ActiveCharacter.Facing = CardinalDirection.West; break;
                case CardinalDirection.South: Game.ActiveCharacter.Facing = CardinalDirection.East; break;
                case CardinalDirection.East: Game.ActiveCharacter.Facing = CardinalDirection.North; break;
                case CardinalDirection.West: Game.ActiveCharacter.Facing = CardinalDirection.South; break;
                default: break;
            }

            DrawCells();
        }

        private void TurnRight_Click(object sender, EventArgs e)
        {
            if (Game.ActiveCharacter == null)
            {
                return;
            }

            switch (Game.ActiveCharacter.Facing)
            {
                case CardinalDirection.North: Game.ActiveCharacter.Facing = CardinalDirection.East; break;
                case CardinalDirection.South: Game.ActiveCharacter.Facing = CardinalDirection.West; break;
                case CardinalDirection.East: Game.ActiveCharacter.Facing = CardinalDirection.South; break;
                case CardinalDirection.West: Game.ActiveCharacter.Facing = CardinalDirection.North; break;
                default: break;
            }

            DrawCells();
        }
    }
}
