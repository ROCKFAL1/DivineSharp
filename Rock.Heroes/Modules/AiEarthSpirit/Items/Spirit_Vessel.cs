using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Helpers;
using Divine.Menu.Items;
using System.Linq;

namespace RockHeroes.Modules.EarthSpirit
{
    internal class Spirit_Vessel
    {
        public Spirit_Vessel(Hero hero, Hero target, MenuItemToggler hasItem, ref Sleeper sleeper)
        {
            var item = AbilityId.item_spirit_vessel;

            if (hasItem.GetValue(item) && ItemsHelper.FindItem(hero, item) && hero.Position.Distance2D(target.Position) <= 500 && hero.Inventory.MainItems.Where(x => x.Id == item).FirstOrDefault().Cooldown == 0)
            {
                ItemsHelper.CastItemEnemy(hero, target, item);
                sleeper.Sleep(100);
            }
        }
    }
}
