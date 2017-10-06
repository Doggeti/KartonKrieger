namespace KartonKrieger
{
    public abstract class CharacterProperty
    {
        public float CurrentValue;
        public float BaseMaxValue;
        public float LevelMultiplier;

        public float GetMaxValueAtLevel(int level)
        {
            return BaseMaxValue + LevelMultiplier * level;
        }
    }
}
