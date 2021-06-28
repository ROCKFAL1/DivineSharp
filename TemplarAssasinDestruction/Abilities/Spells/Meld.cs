using Divine;
using Divine.SDK.Extensions;

namespace TemplarAssasinDestruction.Abilities.Spells
{
    class Meld
    {
        public Spell BaseMeld { get; set; }

        private Hero LocalHero;
        private Context Context;

        public Meld(Spell spell, Context context)
        {
            BaseMeld = spell;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {


            if (!SpellHelper.CanBeCasted(BaseMeld))
            {
                return false;
            }
            
            var target = TargetManager.CurrentTarget;

            if (target.IsAttackImmune())
            {
                return false;
            }

            if (LocalHero.Distance2D(target) > Extensions.GetAttackRange())
            {
                return false;
            }

            BaseMeld.Cast();
            return true;

        }
    }
}
