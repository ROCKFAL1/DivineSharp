using Divine;
using Divine.SDK.Extensions;
using System;

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

            var predictedPos = target.IsMoving ? target.InFront(target.MovementSpeed * 1.5f) : target.Position;

            var InPredictionTrap = Extensions.NearestTrapToPos(predictedPos);

            if (Context.TemplarAssasin.PsionicProj.IsReadyToCombo())
            {
                

                if (InPredictionTrap == null || predictedPos.Distance2D(InPredictionTrap.Position) > 500)
                {
                    BasePsionicTrap.Cast(predictedPos);
                    return true;
                }
            }

            
            if (Context.TemplarAssasin.Trap.IsReadyToCombo())
            {
                var nearestToHeroTrap = Extensions.NearestTrapToPos(LocalHero.Position);
                if (nearestToHeroTrap == null || LocalHero.Distance2D(nearestToHeroTrap) > LocalHero.Distance2D(predictedPos))
                {
                    BasePsionicTrap.Cast(predictedPos);
                    return true;
                }
            }

            return false;
        }


    }
}
