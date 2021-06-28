using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using Divine.SDK.Orbwalker;
using System;

namespace TemplarAssasinDestruction.Modes
{
    class Combo : IDisposable
    {
        private Context Context;

        private Hero LocalHero;
        private TemplarAssasin TA;

        private Sleeper OrbWalkerSleeper = new Sleeper();
        private Sleeper ComboSleeper = new Sleeper();


        public Combo(Context context)
        {
            Context = context;
            TA = Context.TemplarAssasin;
            LocalHero = TA.LocalHero;
            Context.PluginMenu.ComboKey.ValueChanged += ComboKey_ValueChanged;
        }

        public void Dispose()
        {
            Context.PluginMenu.ComboKey.ValueChanged -= ComboKey_ValueChanged;
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }

        private void ComboKey_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.IngameUpdate += UpdateManager_IngameUpdate;
            }
            else
            {
                UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
            }
        }

        private void UpdateManager_IngameUpdate()
        {
            if (!LocalHero.IsAlive )
            {
                return;
            }

            var target = TargetManager.CurrentTarget;

            if (target == null)
            {
                return;
            }

            if (!OrbWalkerSleeper.Sleeping)
            {
                if (!target.IsMoving)
                {
                    LocalHero.Attack(target);
                    OrbWalkerSleeper.Sleep(250);
                    return;
                }
                else
                {
                    OrbwalkerManager.OrbwalkTo(target, GameManager.MousePosition);
                    OrbWalkerSleeper.Sleep(250);
                    return;
                }
            }

            if (ComboSleeper.Sleeping)
            {
                return;
            }

            if (TA.PsionicTrap.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }

            if (TA.PsionicProj.Execute())
            {

                ComboSleeper.Sleep(1500);
                return;
            }

            if (TA.Trap.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }

            if (TA.Hex != null && TA.Hex.Execute())
            {
                ComboSleeper.Sleep(250);
                return;
            }

            if (TA.Refraction.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }


            if (TA.Blink != null && TA.Blink.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }

            

            if (TA.BlackKingBar != null && TA.BlackKingBar.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }

            if (TA.Nullifier != null && TA.Nullifier.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }

            if (TA.Orchid != null && TA.Orchid.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }

            if (TA.Meld.Execute())
            {
                LocalHero.Attack(target);
                OrbWalkerSleeper.Sleep(750);
                ComboSleeper.Sleep(300);
                return;
            }

            if (TA.Manta != null && TA.Manta.Execute())
            {
                ComboSleeper.Sleep(150);
                return;
            }

        }
    }
}
