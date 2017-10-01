using System;
using System.Collections.Generic;

namespace KartonKrieger
{
    public class Attack
    {
        public string Name;
        public int Costs;

        public AttackStyle Style;
        public List<Tuple<DamageType, int, int>> DamageTypes = new List<Tuple<DamageType, int, int>>();
    }
}
