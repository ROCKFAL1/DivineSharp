using Divine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplarAssasinDestruction.Abilities.Items
{
    class Nullifier
    {
        public Item BaseNullfier { get; set; }

        private Context Context;
        private Hero LocalHero;

        public Nullifier(Item item, Context context)
        {
            BaseNullfier = item;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!ItemHelper.CanBeCasted(BaseNullfier))
            {
                return false;
            }

            Hero target = TargetManager.CurrentTarget;

            var unitState = target.UnitState;

            if ((unitState & UnitState.Stunned) == UnitState.Stunned
                || (unitState & UnitState.Hexed) == UnitState.Hexed)
            {
                return false;
            }

            BaseNullfier.Cast(target);
            return true;
        }
    }
}
