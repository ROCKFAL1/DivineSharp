﻿using Divine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplarAssasinDestruction.Abilities.Items
{
    class Orchid
    {
        public Item BaseOrchid { get; set; }

        private Context Context;
        private Hero LocalHero;

        public Orchid(Item item, Context context)
        {
            BaseOrchid = item;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!ItemHelper.CanBeCasted(BaseOrchid))
            {
                return false;
            }

            Hero target = TargetManager.CurrentTarget;

            var unitState = target.UnitState;

            if ((unitState & UnitState.Stunned) == UnitState.Stunned
                || (unitState & UnitState.Hexed) == UnitState.Hexed
                || (unitState & UnitState.Silenced) == UnitState.Silenced)
            {
                return false;
            }

            BaseOrchid.Cast(target);
            return true;
        }
    }
}
