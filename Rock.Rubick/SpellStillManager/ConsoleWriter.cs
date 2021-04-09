using Divine;
using System;

namespace RockRubick
{
    internal sealed class ConsoleWriter
    {
        public ConsoleWriter()
        {
            UpdateManager.CreateIngameUpdate(1000, Cw);
        }

        private void Cw()
        {

            foreach (var v in Dictionaries.LastSpell)
            {
                Console.WriteLine($"LAST USED SPELL: {v.Key} | {v.Value}");
            }

            foreach (var v in Dictionaries.RubickSpellCD)
            {
                Console.WriteLine($"SPELL ON CD: {v.Key} | {v.Value}");
            }

            foreach (var v in Dictionaries.Removed)
            {
                Console.WriteLine($"SPELL IN REMOVED: {v.Key} | {v.Value}");
            }

            Console.WriteLine("________________________________________________");
        }
    }
}
