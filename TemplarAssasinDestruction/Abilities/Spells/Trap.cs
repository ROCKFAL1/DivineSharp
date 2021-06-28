using Divine;
using Divine.SDK.Extensions;
using System.Linq;

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

            var TrapsOnLocalHero = Extensions.GetAllTrapsInRange(LocalHero.Position, 450);

            if (TrapsOnLocalHero == null || TrapsOnLocalHero.Count() == 0)
            {
                return false;
            }

            if (TrapsOnLocalHero.All(x => x.Distance2D(target) > 450 || target.InFront(100).Distance2D(x.Position) < 450))
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
