using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Service;

namespace DawnBreaker_SkillHelper
{
    public class Bootstrap : Bootstrapper
    {
        private Context Context { get; set; }

        protected override void OnActivate()
        {
            if (EntityManager.LocalHero.HeroId != HeroId.npc_dota_hero_dawnbreaker)
            {
                return;
            }

            Context = new Context();
        }

        protected override void OnDeactivate()
        {
            Context.Dispose();
        }

    }
}
