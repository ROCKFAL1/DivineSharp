using Divine.Entity;
using Divine.Extensions;
using Divine.Entity.Entities.Abilities.Items;
using Divine.Entity.Entities.Units.Heroes;

namespace TemplarAssasinDestruction.Abilities.Items
{
    class BlackKingBar
    {
        public Item BaseBkb { get; set; }

        private Context Context;
        private Hero LocalHero;

        public BlackKingBar(Item item, Context context)
        {
            BaseBkb = item;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!ItemHelper.CanBeCasted(BaseBkb))
            {
                return false;
            }

            if (LocalHero.Distance2D(TargetManager.CurrentTarget) > Extensions.GetAttackRange())
            {
                return false;
            }

            BaseBkb.Cast();
            return true;
        }

    }
}
