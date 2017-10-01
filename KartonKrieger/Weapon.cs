using System.Collections.Generic;

namespace KartonKrieger
{
    public class Weapon : Equipment
    {
        public string Name;

        public List<Attack> Attacks = new List<Attack>();
    }
}
