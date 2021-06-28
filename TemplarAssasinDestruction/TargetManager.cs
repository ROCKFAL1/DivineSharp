using Divine;
using Divine.SDK.Extensions;
using System;
using System.Linq;

namespace TemplarAssasinDestruction
{
    class TargetManager : IDisposable
    {
        static public Hero CurrentTarget { get; set; }

        private Context Context;

        public TargetManager(Context context)
        {
            Context = context;

            Context.PluginMenu.ComboKey.ValueChanged += ComboKey_ValueChanged;
        }

        private void ComboKey_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
        {
            if (e.Value)
            {
                UpdateManager.CreateIngameUpdate(100, TargetUpdater);
            }
            else
            {
                UpdateManager.DestroyIngameUpdate(TargetUpdater);
            }
        }

        private Hero GetNearestToMouse()
        {
            Hero target = EntityManager.GetEntities<Hero>()
                .Where(x => x.Distance2D(GameManager.MousePosition) < 600
                        && x.IsAlive
                        && x.IsEnemy(Context.TemplarAssasin.LocalHero)
                        && x.IsVisible)
                .OrderBy(x => x.Distance2D(Context.TemplarAssasin.LocalHero))
                .FirstOrDefault();
            return target;
        }

        private void TargetUpdater()
        {
            CurrentTarget = GetNearestToMouse();
            //Console.WriteLine("Target: " + CurrentTarget);
        }

        public void Dispose()
        {
            Context.PluginMenu.ComboKey.ValueChanged -= ComboKey_ValueChanged;
            UpdateManager.DestroyIngameUpdate(TargetUpdater);
            //todo
        }
    }
}
