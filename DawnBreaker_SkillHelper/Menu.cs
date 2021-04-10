using Divine.Menu;
using Divine.Menu.Items;

namespace DawnBreaker_SkillHelper
{
    class Menu
    {
        public RootMenu RootMenu { get; set; }

        public MenuSwitcher StarbreakerHelper { get; set; }
        public MenuSwitcher HammerHelper { get; set; }

        public Menu(Context Context)
        {
            RootMenu = MenuManager.CreateRootMenu("DawnBreaker Helper")
                                  .SetHeroTexture(Context.Dawnbreaker.LocalHero.HeroId);

            StarbreakerHelper = RootMenu.CreateSwitcher("Starbreaker Helper")
                                        .SetAbilityTexture(Context.Dawnbreaker.Starbreaker.Id)
                                        .SetTooltip("Tries to hit as many heroes as possible");

            HammerHelper = RootMenu.CreateSwitcher("Hammer Helper")
                                   .SetAbilityTexture(Context.Dawnbreaker.Hammer.Id)
                                   .SetTooltip("Using hammer on the hero will lead to maximum rapprochement");

        }

        public void Dispose()
        {
            RootMenu.Remove("DawnBreaker Helper");
            RootMenu.Remove("Starbreaker Helper");
            RootMenu.Remove("Hammer Helper");
        }

    }
}
