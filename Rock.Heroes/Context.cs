using Divine.Menu;
using Divine.Menu.Items;
using RockHeroes.Modules;
using RockHeroes.Modules.EarthSpirit;

namespace RockHeroes
{
    internal class Context
    {
        public readonly RootMenu rootMenu;
        public readonly EarthSpirit EarthSpirit;
        public readonly SfRazes sfRazes;

        public Context()
        {
            rootMenu = MenuManager.CreateRootMenu("Rock.Heroes");
            EarthSpirit = new EarthSpirit(this);
            sfRazes = new SfRazes(this);
        }

        public void Dispose()
        {
            EarthSpirit.Dispose();
            sfRazes.Dispose();
        }

    }
}
