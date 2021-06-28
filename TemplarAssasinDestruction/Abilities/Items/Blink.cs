using Divine;
using Divine.SDK.Extensions;

namespace TemplarAssasinDestruction.Abilities.Items
{
    class Blink
    {
        public Item BaseBlink { get; set; }

        private Context Context;
        private Hero LocalHero;

        public Blink(Item item, Context context)
        {
            BaseBlink = item;
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public bool Execute()
        {
            if (!ItemHelper.CanBeCasted(BaseBlink))
            {
                return false;
            }

            Hero target = TargetManager.CurrentTarget;

            var attackRange = Extensions.GetAttackRange();

            float distance = attackRange / 3;

            if (LocalHero.Distance2D(target) < attackRange + distance)
            {
                if (BaseBlink.Id == AbilityId.item_swift_blink)
                {
                    BaseBlink.Cast(target.InFront(150));
                    return true;
                }

                return false;
            }

            var blinkPoint = target.InFront(distance);

            if (blinkPoint.Distance2D(LocalHero.Position) > 1100)
            {
                return false;
            }

            

            BaseBlink.Cast(blinkPoint);
            return true;
        }
    }
}
