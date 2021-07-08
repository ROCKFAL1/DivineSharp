using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Helpers;
using Divine.Menu.Items;
using System.Linq;

namespace RockHeroes.Modules.EarthSpirit
{
    internal class Shivas_guard
    {
        public Shivas_guard(Hero hero, Unit target, MenuItemToggler hasItem, ref Sleeper sleeper)
        {
            var item = AbilityId.item_shivas_guard;

            if (hasItem.GetValue(item) && ItemsHelper.FindItem(hero, item) && hero.Position.Distance2D(target.Position) <= 700 && hero.Inventory.MainItems.Where(x => x.Id == item).FirstOrDefault().Cooldown == 0)
            {
                ItemsHelper.CastItem(hero, item);
                sleeper.Sleep(100);
            }
        }
    }
}
