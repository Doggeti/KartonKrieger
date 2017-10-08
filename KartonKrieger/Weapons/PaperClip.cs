using System;

namespace KartonKrieger.Weapons
{
    public class PaperClip : Weapon
    {
        public PaperClip()
        {
            Name = "Paper Clip";

            Attack attack = new Attack();
            attack.Name = "Stab";
            attack.Costs = 1;
            attack.MinRange = 1;
            attack.MaxRange = 1;
            attack.Buildup = 1;
            attack.Cooldown = 1;
            attack.Style = AttackStyle.Melee;
            attack.DamageTypes.Add(DamageType.Pierce, new Tuple<int, int, int>(1, 1, 1));
            Attacks.Add(attack);

            attack = new Attack();
            attack.Name = "Strike";
            attack.Costs = 1;
            attack.MinRange = 1;
            attack.MaxRange = 1;
            attack.Buildup = 1;
            attack.Cooldown = 2;
            attack.Style = AttackStyle.Melee;
            attack.DamageTypes.Add(DamageType.Blunt, new Tuple<int, int, int>(2, 3, 1));
            Attacks.Add(attack);

            attack = new Attack();
            attack.Name = "Throw";
            attack.Costs = 1;
            attack.MinRange = 2;
            attack.MaxRange = 5;
            attack.Buildup = 1;
            attack.Cooldown = 3;
            attack.Style = AttackStyle.Ranged;
            attack.DamageTypes.Add(DamageType.Pierce, new Tuple<int, int, int>(3, 4, 1));
            Attacks.Add(attack);

            InitCooldowns();
        }
    }
}
