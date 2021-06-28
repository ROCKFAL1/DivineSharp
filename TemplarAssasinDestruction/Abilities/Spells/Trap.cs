using Divine;
using Divine.SDK.Extensions;

namespace TemplarAssasinDestruction.Abilities.Spells
{
    class Trap
    {
        public Spell BaseTrap { get; set; }

        private Hero LocalHero;
        private Context Context;

        public Trap(Spell spell, Context context)
        {
            BaseTrap = spell;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!IsReadyToCombo())
            {
                return false;
            }

            var target = TargetManager.CurrentTarget;

            if ((target.UnitState & UnitState.Hexed) == UnitState.Hexed 
                || (target.UnitState & UnitState.Stunned) == UnitState.Stunned)
            {
                return false;
            }

            var TrapOnPredict = Extensions.NearestTrapToPos(LocalHero.Position);

            if (TrapOnPredict == null)
            {
                return false;
            }

            if (TrapOnPredict.Distance2D(target) > 400)
            {
                return false;
            }

            if (TrapOnPredict != Extensions.NearestTrapToPos(LocalHero.Position))
            {
                return false;
            }

            if (target.InFront(100).Distance2D(TrapOnPredict.Position) < 400)
            {
                return false;
            }

            BaseTrap.Cast();
            return true;

        }

        public bool IsReadyToCombo()
        {
            if (!SpellHelper.CanBeCasted(BaseTrap))
            {
                return false;
            }
            return true;
        }
    }
}
