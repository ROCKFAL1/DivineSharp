using Divine.Entity;
using Divine.Extensions;
using Divine.Numerics;
using Divine.Entity.Entities;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace TemplarAssasinDestruction
{
    class Extensions
    {
        public static float GetAttackRange()
        {
            Hero localHero = EntityManager.LocalHero;
            float attackRange = localHero.AttackRange;
            if (localHero.Inventory.MainItems.Any(x => x.Id == AbilityId.item_dragon_lance || x.Id == AbilityId.item_hurricane_pike))
            {
                attackRange += 140;
            }

            if (localHero.Spellbook.Talents.Any( x => x.Id == AbilityId.special_bonus_unique_templar_assassin_8 && x.Level == 1))
            {
                attackRange += 100;
            }

            var psiBlades = localHero.Spellbook.GetSpellById(AbilityId.templar_assassin_psi_blades);
            attackRange += psiBlades.Level == 0 ? 0 : 80 + (psiBlades.Level - 1) * 50;
            return attackRange;
        }

        public static Entity NearestTrapToPos(Vector3 position)
        {
            return GetAllTrapsInRange(position, 9999).FirstOrDefault();
        }

        public static IEnumerable<Unit> GetAllTrapsInRange(Vector3 position, float range)
        {
            var traps = EntityManager.GetEntities<Unit>()
                .Where(x => x.Distance2D(position) < range
                && x.Name == "npc_dota_templar_assassin_psionic_trap"
                && x.IsAlive)
                .OrderBy(x => x.Distance2D(position));

            return traps;
        }
    }
}
