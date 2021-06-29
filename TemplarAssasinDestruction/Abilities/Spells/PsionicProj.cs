using Divine;
using Divine.SDK.Extensions;
using System.Linq;

namespace TemplarAssasinDestruction.Abilities.Spells
{
    class PsionicProj
    {
        public Spell BasePsionicProj { get; set; }

        private Hero LocalHero;
        private Context Context;

        public PsionicProj(Spell spell, Context context)
        {
            BasePsionicProj = spell;
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

            var predictedPos = target.IsMoving ? target.InFront(target.MovementSpeed * 1.75f) : target.Position;

            var InPredictionTrap = Extensions.NearestTrapToPos(predictedPos)?.Distance2D(predictedPos) < 400;

            if (!InPredictionTrap)
            {
                var baseTrap = Context.TemplarAssasin.PsionicTrap.BasePsionicTrap;

                if (SpellHelper.CanBeCasted(baseTrap))
                {
                    baseTrap.Cast(predictedPos);
                    return true;
                }
            }

            BasePsionicProj.Cast(predictedPos);
            return true;
        }

        public bool IsReadyToCombo()
        {
            if (!LocalHero.HasAghanimsScepter())
            {
                return false;
            }

            if (!SpellHelper.CanBeCasted(BasePsionicProj))
            {
                return false;
            }

            if (EntityManager.GetEntities<Hero>().Any(x => x.Distance2D(LocalHero) < 500
                                                        && x.IsAlive
                                                        && x.IsVisible
                                                        && x.IsEnemy(LocalHero)))
            {
                return false;
            }

            if (LocalHero.Distance2D(TargetManager.CurrentTarget) < 1100)
            {
                return false;
            }

            return true;
        }

    }
}
