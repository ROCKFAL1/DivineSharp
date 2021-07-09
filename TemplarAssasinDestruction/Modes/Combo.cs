using System;
using System.Linq;

using Divine.Entity;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Game;
using Divine.Helpers;
using Divine.Orbwalker;
using Divine.Update;

namespace TemplarAssasinDestruction.Modes
{
    class Combo : IDisposable
    {
        private Context Context;

        private Hero LocalHero;
        private TemplarAssasin TA;

        private Sleeper OrbWalkerSleeper = new Sleeper();
        private Sleeper ComboSleeper = new Sleeper();

        private Sleeper ProjSleeper = new Sleeper();


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
            if (!LocalHero.IsAlive  || TA.PsionicProj.BasePsionicProj.IsChanneling || ProjSleeper.Sleeping)
            {
                return;
            }

            var target = TargetManager.CurrentTarget;

            if (target == null)
            {
                return;
            }

            var enemyNearTarget = EntityManager.GetEntities<Hero>().Where(x => x != target
                    && x.IsEnemy(LocalHero)
                    && x.IsAlive
                    && x.IsVisible
                    && x.Distance2D(target) < Extensions.GetAttackRange() * 2)
                        .OrderBy(x => x.Distance2D(target))
                        .FirstOrDefault();

            if (!OrbWalkerSleeper.Sleeping)
            {
                if (enemyNearTarget != null)
                {
                    OrbwalkerManager.OrbwalkTo(target, target.Position.Extend(enemyNearTarget.Position, -Extensions.GetAttackRange() / 2));
                    OrbWalkerSleeper.Sleep(100);
                    return;
                }

                if (!target.IsMoving)
                {
                    LocalHero.Attack(target);
                    OrbWalkerSleeper.Sleep(200);
                    return;
                }
                else
                {
                    OrbwalkerManager.OrbwalkTo(target, GameManager.MousePosition);
                    OrbWalkerSleeper.Sleep(200);
                    return;
                }
            }

            if (ComboSleeper.Sleeping)
            {
                return;
            }

            if (TA.PsionicProj.Execute())
            {
                ProjSleeper.Sleep(500);
                ComboSleeper.Sleep(300);
                OrbWalkerSleeper.Sleep(300);
                return;
            }

            if (TA.PsionicTrap.Execute())
            {
                ComboSleeper.Sleep(200);
                return;
            }

       
            if (TA.Trap.Execute())
            {
                ComboSleeper.Sleep(150);
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

            if (TA.Hex != null && TA.Hex.Execute())
            {
                ComboSleeper.Sleep(300);
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
                ComboSleeper.Sleep(750);
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
