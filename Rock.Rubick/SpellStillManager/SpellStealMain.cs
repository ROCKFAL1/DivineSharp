using Divine.Extensions;
using Divine.Update;

namespace RockRubick
{
    internal sealed class SpellStealMain
    {
        public SpellStealMain()
        {
            new Dictionaries();
            new LastSpellManager();
            new CooldownManager();
            //new ConsoleWriter();


            UpdateManager.CreateIngameUpdate(25, IngameUpdate);
        }
        public static void Dispose()
        {
            LastSpellManager.Dispose();
            CooldownManager.Dispose();

            UpdateManager.DestroyIngameUpdate(IngameUpdate);
        }

        private static void IngameUpdate()
        {
            if (General.localHero.HasAghanimsScepter())
            {
                new AghanimMode();
            }
            else
            {
                new NonAghanimMode();
            }
        }
    }
}

