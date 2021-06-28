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

            var predictedPos = target.IsMoving ? target.InFront(target.MovementSpeed) : target.Position;

            var TrapInPredict = Extensions.NearestTrapToPos(predictedPos);

            if (TrapInPredict == null)
            {
                return false;
            }

            if (TrapInPredict.Distance2D(predictedPos) > 500)
            {
                return false;
            }

            if (TrapInPredict != Extensions.NearestTrapToPos(LocalHero.Position))
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
