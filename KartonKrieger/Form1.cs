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

        public RichTextBox[,] Cells = new RichTextBox[BOARD_SIZE, BOARD_SIZE];

        public Form1()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up: GoNorth.PerformClick(); break;
                case Keys.Down: GoSouth.PerformClick(); break;
                case Keys.Left: GoWest.PerformClick(); break;
                case Keys.Right: GoEast.PerformClick(); break;
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
                    rtb.Enabled = false;

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
            Weapon weapon;
            Attack attack;

            character = AddCharacterToField(x, y, "Player", (CardinalDirection)Game.Randomizer.Next(0, 4));
            weapon = new Weapon();
            weapon.Name = "Paper Clip";
            attack = new Attack();
            attack.Name = "Stab";
            attack.Costs = 1;
            attack.Style = AttackStyle.Melee;
            attack.DamageTypes.Add(new Tuple<DamageType, int, int>(DamageType.Pierce, 1, 1));
            weapon.Attacks.Add(attack);
            attack = new Attack();
            attack.Name = "Strike";
            attack.Costs = 1;
            attack.Style = AttackStyle.Melee;
            attack.DamageTypes.Add(new Tuple<DamageType, int, int>(DamageType.Blunt, 2, 3));
            weapon.Attacks.Add(attack);
            character.Inventory.Add(weapon);

            int enemyCount = 2;

            while (enemyCount > 0)
            {
                x = Game.Randomizer.Next(0, 10);
                y = Game.Randomizer.Next(0, 10);

                if (Game.Field.Cells[x, y].Character != null)
                {
                    continue;
                }

                character = AddCharacterToField(x, y, $"Enemy{enemyCount}", (CardinalDirection)Game.Randomizer.Next(0, 4));
                weapon = new Weapon();
                weapon.Name = "Paper Clip";
                attack = new Attack();
                attack.Name = "Stab";
                attack.Costs = 1;
                attack.Style = AttackStyle.Melee;
                attack.DamageTypes.Add(new Tuple<DamageType, int, int>(DamageType.Pierce, 1, 1));
                weapon.Attacks.Add(attack);
                character.Inventory.Add(weapon);

                enemyCount--;
            }
        }

        private Character AddCharacterToField(int x, int y, string name, CardinalDirection facing)
        {
            Character character = new Character();
            character.Level = 1;
            character.Name = name;
            character.InitiativeGain = Game.Randomizer.Next(20, 81);
            character.ActionPointsStart = 1;
            character.Facing = facing;

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
                number++;

                Button attackButton = Controls.Find($"Attack{number}", false).OfType<Button>().FirstOrDefault();

                if (attackButton == null)
                {
                    break;
                }

                attackButton.Text = attack.Name;

                foreach (var damageType in attack.DamageTypes)
                {
                    attackButton.Text += Environment.NewLine + $"{damageType.Item1} ({damageType.Item2}-{damageType.Item3})";
                }
            }

            while (number < 3)
            {
                number++;

                Button attackButton = Controls.Find($"Attack{number}", false).OfType<Button>().FirstOrDefault();

                attackButton.Text = string.Empty;
            }
        }

        private void DrawCells()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    RichTextBox rtb = Cells[x, y];
                    BattlefieldCell cell = Game.Field.Cells[x, y];
                    Character character = cell.Character;

                    if (character == null)
                    {
                        rtb.Text = string.Empty;
                        rtb.Font = new System.Drawing.Font(rtb.Font, System.Drawing.FontStyle.Regular);
                    }
                    else
                    {
                        rtb.Text = character.Name;
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

                        if (cell.Character == Game.ActiveCharacter)
                        {
                            rtb.Font = new System.Drawing.Font(rtb.Font, System.Drawing.FontStyle.Bold);
                        }
                        else
                        {
                            rtb.Font = new System.Drawing.Font(rtb.Font, System.Drawing.FontStyle.Regular);
                        }
                    }
                }
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
    }
}
