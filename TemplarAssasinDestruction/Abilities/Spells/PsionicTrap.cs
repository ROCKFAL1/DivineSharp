using Divine.Entity;
using Divine.Extensions;
using Divine.Entity.Entities.Abilities.Spells;
using Divine.Entity.Entities.Units.Heroes;

namespace TemplarAssasinDestruction.Abilities.Spells
{
    class PsionicTrap
    {
        public Spell BasePsionicTrap { get; set; }

        private Hero LocalHero;
        private Context Context;

        public PsionicTrap(Spell spell, Context context)
        {
            BasePsionicTrap = spell;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {

            if (!SpellHelper.CanBeCasted(BasePsionicTrap))
            {
                return false;
            }

            var target = TargetManager.CurrentTarget;

            var predictedPos = target.IsMoving ? target.InFront(target.MovementSpeed * 1.75f) : target.Position;

            var InPredictionTrap = Extensions.NearestTrapToPos(predictedPos);

            if (Context.TemplarAssasin.PsionicProj.IsReadyToCombo())
            {
                if (InPredictionTrap == null || predictedPos.Distance2D(InPredictionTrap.Position) > 400)
                {
                    BasePsionicTrap.Cast(predictedPos);
                    return true;
                }
            }

            
            if (Context.TemplarAssasin.Trap.IsReadyToCombo())
            {
                var nearestToHeroTrap = Extensions.NearestTrapToPos(LocalHero.Position);
                if (nearestToHeroTrap == null || target.Distance2D(nearestToHeroTrap) > 400)
                {
                    BasePsionicTrap.Cast(predictedPos);
                    return true;
                }
            }

            return false;
        }


    }
}
