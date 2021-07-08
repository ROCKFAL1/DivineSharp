using Divine.Game;
using Divine.Update;
using System;

namespace RockRubick
{
    internal sealed class CooldownManager
    {

        public CooldownManager()
        {
            UpdateManager.CreateIngameUpdate(250, CooldownUpdater);
        }

        public static void Dispose()
        {
            UpdateManager.DestroyIngameUpdate(CooldownUpdater);
        }

        private static void CooldownUpdater()
        {
            var main = General.localHero.Spellbook.Spell4;
            if (main.Cooldown != 0 || main.ChargeRestoreTimeRemaining > 4)
            {
                if (!Dictionaries.RubickSpellCD.ContainsKey(main.Id))
                {
                    if (main.Charges == 0)
                    {
                        Dictionaries.RubickSpellCD.Add(main.Id, GameManager.GameTime + main.Cooldown);
                    }
                    else
                    {
                        if ((int)Math.Ceiling(main.Charges * 0.3) >= main.CurrentCharges)
                        {
                            Dictionaries.RubickSpellCD.Add(main.Id, GameManager.GameTime + ((int)Math.Floor(main.Charges * 0.3)
                                                                                            - main.CurrentCharges) * main.ChargeRestoreTime + main.ChargeRestoreTimeRemaining);
                        }
                    }
                }
            }
            else
            {
                if (Dictionaries.RubickSpellCD.ContainsKey(main.Id))
                {
                    if (main.Charges == 0)
                    {
                        Dictionaries.RubickSpellCD.Remove(main.Id);
                    }
                    else if (main.Charges > (int)Math.Ceiling(main.Charges * 0.3))
                    {
                        Dictionaries.RubickSpellCD.Remove(main.Id);
                    }

                }
            }
            if (Dictionaries.RubickSpellCD == null || Dictionaries.RubickSpellCD.Count == 0)
            {
                return;
            }
            try
            {
                foreach (var v in Dictionaries.RubickSpellCD)
                {
                    if (GameManager.GameTime > v.Value)
                    {
                        Dictionaries.RubickSpellCD.Remove(v.Key);
                    }
                }
            }
            catch
            {
                //TODO
            }
        }
    }
}
