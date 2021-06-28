using Divine;
using Divine.SDK.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
