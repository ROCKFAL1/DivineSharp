using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Helpers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplarAssasinDestruction.Modes
{
    class Harass : IDisposable
    {
        private Context Context;
        private Hero LocalHero;

        private Sleeper HarassSleeper = new Sleeper();

        public Harass(Context context)
        {
            Context = context;

            LocalHero = Context.TemplarAssasin.LocalHero;
            Context.PluginMenu.HarassKey.ValueChanged += HarassKey_ValueChanged;
        }

        public void Dispose()
        {
            Context.PluginMenu.HarassKey.ValueChanged -= HarassKey_ValueChanged;
            UpdateManager.IngameUpdate -= UpdateManager_IngameUpdate;
        }

        private void HarassKey_ValueChanged(Divine.Menu.Items.MenuHoldKey holdKey, Divine.Menu.EventArgs.HoldKeyEventArgs e)
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

        private bool IsDamagableCreep(Unit creep)
        {
            if (creep.IsEnemy(LocalHero))
            {
                return true;
            }
            if (creep.IsAlly(LocalHero) && creep.HealthPercent() <= 0.5)
            {
                return true;
            }
            return false;
        }



        private void UpdateManager_IngameUpdate()
        {
            if (HarassSleeper.Sleeping)
            {
                return;
            }
            var entities = EntityManager.GetEntities<Entity>();

            var nearestDamagableCreeps = entities
                .OfType<Creep>()
                .Where(x => x.Distance2D(LocalHero) < 500
                        && x.IsSpawned
                        && x.IsValid
                        && x.IsVisible
                        && x.IsAlive
                        && IsDamagableCreep(x));

            var nearestEnemy = entities
                .OfType<Hero>()
                .Where(x => x.Distance2D(LocalHero) < Extensions.GetAttackRange() * 3 
                                    && x.IsVisible
                                    && x.IsEnemy(LocalHero)
                                    && x.IsAlive)
                .OrderBy(x => x.Distance2D(LocalHero))
                .FirstOrDefault();

            var mousePos = GameManager.MousePosition;

            if (nearestDamagableCreeps == null || nearestDamagableCreeps.Count() == 0 || nearestEnemy == null)
            {
                HarassSleeper.Sleep(50);
                LocalHero.Move(mousePos);
                return;
            }

            var attackRange = Extensions.GetAttackRange();

            Dictionary<Vector3, Unit> harassPoints = new Dictionary<Vector3, Unit>();

            foreach (var creep in nearestDamagableCreeps)
            {
                harassPoints.Add(creep.Position.Extend(nearestEnemy.Position, -(Extensions.GetAttackRange() / 2)), creep);             
            }

            

            var nearestHarassPoint = harassPoints
                .Where(x => x.Key.Distance2D(nearestEnemy.Position) < Extensions.GetAttackRange() * 2)
                .OrderBy(x => x.Key.Distance2D(Context.PluginMenu.HarassMode.Value == "Closest Harass Position"
                                                                                        ? LocalHero.Position
                                                                                        : mousePos))
                .FirstOrDefault();

            if (nearestHarassPoint.Key == null || nearestHarassPoint.Value == null)
            {
                HarassSleeper.Sleep(50);
                LocalHero.Move(mousePos);
                return;
            }

            if (LocalHero.Distance2D(nearestHarassPoint.Key) > 50)
            {
                LocalHero.Move(nearestHarassPoint.Key);
                HarassSleeper.Sleep(100);
                return;
            }
            else
            {
                LocalHero.Attack(nearestHarassPoint.Value);
                HarassSleeper.Sleep(100);
                return;
            }

        }
    }
}
