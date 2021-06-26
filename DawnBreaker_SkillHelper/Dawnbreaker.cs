using Divine;
using Divine.Entity;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Abilities.Spells;
using Divine.Entity.Entities.Units.Components;
using Divine.Entity.Entities.Units.Heroes;

namespace DawnBreaker_SkillHelper
{
    class Dawnbreaker
    {
        public Hero LocalHero { get; set; }
        public Spell Starbreaker { get; set; }
        public Spell Hammer { get; set; }
        public Spell Converge { get; set; }

        public Dawnbreaker()
        {
            LocalHero = EntityManager.LocalHero;

            Spellbook spellbook = LocalHero.Spellbook;
            Starbreaker = spellbook.Spell1;
            Hammer = spellbook.GetSpellById(AbilityId.dawnbreaker_celestial_hammer); //TODO
            Converge = spellbook.GetSpellById(AbilityId.dawnbreaker_converge); //TODO
        }

        public void Dispose()
        {
            LocalHero = null;
            Starbreaker = null;
            Hammer = null;
            Converge = null;
        }

    }
}
