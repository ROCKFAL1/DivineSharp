using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace RockRubick
{
    internal sealed class SpellStealHelper
    {

        public static Dictionary<Hero, AbilityId> OrderLastSpell(Dictionary<Hero, AbilityId> lastspell, Dictionary<AbilityId, int> dictionary) //Сортировка словаря последних использованых способностей по убыванию приоритета
        {
            Dictionary<Hero, AbilityId> orderedDictionary = new Dictionary<Hero, AbilityId> { };
            Dictionary<Hero, (AbilityId, int)> HeroAbilityInt = new Dictionary<Hero, (AbilityId, int)> { };

            foreach (var element in lastspell)
            {
                HeroAbilityInt.Add(element.Key, (element.Value, dictionary.Where(x => x.Key == element.Value).FirstOrDefault().Value));
            }
            var HAI = HeroAbilityInt.OrderByDescending(x => x.Value.Item2);
            foreach (var element in HAI)
            {
                orderedDictionary.Add(element.Key, element.Value.Item1);
            }
            return orderedDictionary;
        }
    }
}
