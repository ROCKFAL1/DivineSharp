using Divine;
using System;

namespace TemplarAssasinDestruction.Abilities.Spells
{
    class SpellHelper : IDisposable
    {
        private static Context Context;

        private static Hero LocalHero;

        public SpellHelper(Context context)
        {
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public void Dispose()
        {
            //TODO
        }

        public static bool CanBeCasted(Spell spell)
        {
            if (spell.Level == 0 || spell.Cooldown != 0)
            {
                return false;
            }

            if (LocalHero.Mana < spell.ManaCost)
            {
                return false;
            }

            var unitState = LocalHero.UnitState;

            if ((unitState & UnitState.Hexed) == UnitState.Hexed
                || (unitState & UnitState.Stunned) == UnitState.Stunned
                || (unitState & UnitState.Silenced) == UnitState.Silenced)
            {
                return false;
            }

            return true;
        }

       
    }
}
