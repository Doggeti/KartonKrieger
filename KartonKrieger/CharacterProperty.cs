namespace KartonKrieger
{
    public abstract class CharacterProperty
    {
        public int CurrentValue;
        public int BaseMaxValue;
        public int LevelMultiplier;

        public int GetMaxValueAtLevel(int level)
        {
            return BaseMaxValue + LevelMultiplier * level;
        }
    }
}
