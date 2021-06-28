using Divine;
using Divine.SDK.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplarAssasinDestruction.Abilities.Items
{
    class Hex
    {
        public Item BaseHex { get; set; }

        private Context Context;
        private Hero LocalHero;

        public Hex(Item item, Context context)
        {
            BaseHex = item;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!ItemHelper.CanBeCasted(BaseHex))
            {
                return false;
            }

            Hero target = TargetManager.CurrentTarget;

            var unitState = target.UnitState;

            if ( (unitState & UnitState.Hexed) == UnitState.Hexed
                || (unitState & UnitState.Stunned) == UnitState.Stunned)
            {
                return false;
            }

            BaseHex.Cast(target);
            return true;
        }
    }
}
