using Divine.Entity;
using Divine.Entity.Entities.Abilities.Items;
using Divine.Entity.Entities.Units.Components;
using Divine.Entity.Entities.Units.Heroes;

using System;
using System.Linq;

namespace TemplarAssasinDestruction.Abilities.Items
{
    class ItemHelper : IDisposable
    {
        private static Context Context;

        private static Hero LocalHero;

        public ItemHelper(Context context)
        {
            Context = context;
            LocalHero = EntityManager.LocalHero;
        }

        public void Dispose()
        {
            //TODO
        }

        public static bool CanBeCasted(Item item)
        {

            if (item.Cooldown != 0 || !IsInMainSlot(item))
            {
                return false;
            }

            if (LocalHero.Mana < item.ManaCost)
            {
                return false;
            }

            if (Context.PluginMenu.ComboItems.GetValue(item.Id) == false)
            {
                return false;
            }

            var unitState = LocalHero.UnitState;

            if ( (unitState & UnitState.Hexed) == UnitState.Hexed
                || (unitState & UnitState.Stunned) == UnitState.Stunned)
            {
                return false;
            }
            return true;
        }

        private static bool IsInMainSlot(Item item)
        {
            return LocalHero.Inventory.MainItems.Any(x => x.Id == item.Id);
        }

    }
}
