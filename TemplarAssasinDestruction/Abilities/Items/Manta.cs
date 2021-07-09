using Divine.Entity;
using Divine.Extensions;
using Divine.Entity.Entities.Abilities.Items;
using Divine.Entity.Entities.Units.Heroes;

namespace TemplarAssasinDestruction.Abilities.Items
{
    class Manta
    {
        public Item BaseManta { get; set; }

        private Context Context;
        private Hero LocalHero;

        public Manta(Item item, Context context)
        {
            BaseManta = item;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!ItemHelper.CanBeCasted(BaseManta))
            {
                return false;
            }

            if (LocalHero.Distance2D(TargetManager.CurrentTarget) > Extensions.GetAttackRange())
            {
                return false;
            }

            BaseManta.Cast();
            return true;
        }
    }
}
