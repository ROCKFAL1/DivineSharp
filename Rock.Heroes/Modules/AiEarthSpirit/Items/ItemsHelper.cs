using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Heroes;
using System;
using System.Linq;

namespace RockHeroes.Modules.EarthSpirit
{
    internal class ItemsHelper
    {

        private static bool ManaCheck(float manaCost, float manaPool)
        {
            if (manaPool - manaCost > 0)
                return true;
            return false;
        }

        public static bool FindItem(Hero hero, AbilityId abilityId)
        {
            var Item = hero.Inventory.MainItems.FirstOrDefault(x => x.Id == abilityId);

            bool BoolItem;

            if (Item != null)
            {
                BoolItem = true;
            }
            else
            {
                BoolItem = false;
            }

            return BoolItem;
        }

        public static void CastItem(Hero hero, AbilityId abilityId)
        {
            var Item = hero.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);
            if (Item != null/* && Item.Level > 0*/ && Item.Cooldown == 0 /*&& ManaCheck(Item.ManaCost, hero.Mana)*/)
            {
                Item.Cast();
            }
        }
        public static void CastItemEnemy(Unit unit, Unit target, AbilityId abilityId)
        {
            try
            {
                var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);
                if (Item != null && Item.Level > 0 && Item.Cooldown == 0 && ManaCheck(Item.ManaCost, unit.Mana))
                {
                    Item.Cast(target);
                }
            }
            catch (Exception)
            {

                //nothing
            }
        }

        public static void CastItemTarget(Unit unit, Unit target, AbilityId abilityId)
        {
            try
            {
                var Item = unit.Inventory.Items.FirstOrDefault(x => x.Id == abilityId);
                if (Item != null && Item.Level > 0 && Item.Cooldown == 0 && ManaCheck(Item.ManaCost, unit.Mana))
                {
                    Item.Cast(target.Position);
                }
            }
            catch (Exception)
            {

                //nothing
            }
        }
    }
}
