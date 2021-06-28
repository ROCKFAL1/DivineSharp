using Divine;

namespace TemplarAssasinDestruction
{
    public class Bootstrap : Bootstrapper
    {
        protected override void OnActivate()
        {
            if (EntityManager.LocalHero.HeroId != HeroId.npc_dota_hero_templar_assassin)
            {
                return;
            }

            new Context();
        }
    }
}
