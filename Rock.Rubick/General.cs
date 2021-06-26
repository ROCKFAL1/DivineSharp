using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Helpers;

namespace RockRubick
{
    internal sealed class General
    {
        public static Hero localHero;
        public static Sleeper sleeper = new Sleeper();

        public General()
        {
            localHero = EntityManager.LocalHero;
        }
    }
}
