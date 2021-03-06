﻿using System;
using System.Collections.Generic;

namespace KartonKrieger
{
    public class Attack
    {
        public string Name;
        public int Costs;

        public int MinRange;
        public int MaxRange;

        public int Buildup;
        public int Cooldown;
        public int ActiveCooldown;

        public AttackStyle Style;
        public Dictionary<DamageType, Tuple<int, int, int>> DamageTypes = new Dictionary<DamageType, Tuple<int, int, int>>();
    }
}
