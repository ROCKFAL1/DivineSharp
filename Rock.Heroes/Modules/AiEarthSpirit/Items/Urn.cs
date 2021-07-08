using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Helpers;
using Divine.Menu.Items;
using System.Linq;

namespace RockHeroes.Modules.EarthSpirit
{
    internal class Urn
    {
        public Urn(Hero hero, Hero target, MenuItemToggler hasItem, ref Sleeper sleeper)
        {
            var item = AbilityId.item_urn_of_shadows;

            if (hasItem.GetValue(item) && ItemsHelper.FindItem(hero, item) && hero.Position.Distance2D(target.Position) <= 500 && hero.Inventory.MainItems.Where(x => x.Id == item).FirstOrDefault().Cooldown == 0)
            {
                ItemsHelper.CastItemEnemy(hero, target, item);
                sleeper.Sleep(100);
            }
        }
    }
}
