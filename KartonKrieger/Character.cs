using System.Collections.Generic;
using System.Linq;

namespace KartonKrieger
{
    public class Character
    {
        public int Level;
        public string Name;
        public string Faction;

        public float Initiative;
        public float InitiativeGain;

        public int ActionPoints;
        public int ActionPointsStart;

        public CardinalDirection Facing;

        List<CharacterProperty> Properties = new List<CharacterProperty>();
        public List<Equipment> Inventory = new List<Equipment>();

        public Attack SelectedAttack;

        public void AddProperty<T>() where T : CharacterProperty, new()
        {
            if (Properties.OfType<T>().Count() > 0)
            {
                return;
            }

            Properties.Add(new T());
        }

        public CharacterProperty GetProperty<T>() where T : CharacterProperty
        {
            return Properties.OfType<T>().SingleOrDefault();
        }

        public float GetPropertyValue<T>() where T : CharacterProperty
        {
            CharacterProperty property = Properties.OfType<T>().SingleOrDefault();
            return property?.CurrentValue ?? 0;
        }

        public void SetPropertyValue<T>(int newValue) where T : CharacterProperty
        {
            CharacterProperty property = Properties.OfType<T>().SingleOrDefault();
            property.CurrentValue = newValue;
        }

        public float GetPropertyMax<T>() where T : CharacterProperty
        {
            CharacterProperty property = Properties.OfType<T>().SingleOrDefault();
            return property?.GetMaxValueAtLevel(Level) ?? 0;
        }

        public bool Alive
        {
            get
            {
                var live = GetProperty<Live>();

                if (live == null)
                {
                    return false;
                }
                else
                {
                    return live.CurrentValue > 0;
                }
            }
        }
    }
}
