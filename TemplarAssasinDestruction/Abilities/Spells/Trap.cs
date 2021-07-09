using Divine.Entity;
using Divine.Extensions;
using Divine.Entity.Entities.Abilities.Spells;
using Divine.Entity.Entities.Units.Heroes;
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

            var trapsAroundTarget = Extensions.GetAllTrapsInRange(target.Position, 400);


            if (trapsAroundTarget == null || trapsAroundTarget.Count() == 0)
            {
                return false;
            }

            var trapForDestroy = trapsAroundTarget.Where(x => target.InFront(100).Distance2D(x.Position) > 400).FirstOrDefault();

            if (trapForDestroy == null)
            {
                return false; ;
            }

            trapForDestroy.Spellbook.Spell1.Cast();
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
