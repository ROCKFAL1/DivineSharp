using Divine.Entity;
using Divine.Extensions;
using Divine.Entity.Entities.Abilities.Spells;
using Divine.Entity.Entities.Units.Heroes;

namespace TemplarAssasinDestruction.Abilities.Spells
{
    class Refraction
    {
        public Spell BaseRefraction { get; set; }

        private Hero LocalHero;
        private Context Context;

        public Refraction(Spell spell, Context context)
        {
            BaseRefraction = spell;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!SpellHelper.CanBeCasted(BaseRefraction))
            {
                return false;
            }

            var target = TargetManager.CurrentTarget;

            if (LocalHero.Distance2D(target) > Extensions.GetAttackRange())
            {
                return false;
            }

            BaseRefraction.Cast();
            LocalHero.Attack(target);
            return true;

        }
    }
}
