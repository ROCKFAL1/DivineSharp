using Divine;
using Divine.Menu.Items;
using Divine.SDK.Extensions;
using SharpDX;
using System;
using System.Linq;
using Divine.Extensions;

namespace Utilities.Modules
{
    internal sealed class Creepblocker
    {
        private readonly Unit hero;

        private readonly UpdateHandler updateHandler;

        private readonly MenuHoldKey holdKey;
        private readonly MenuSlider sensitivity;
        private readonly MenuSwitcher skipranged;

        public Creepblocker(Context context)
        {
            hero = EntityManager.LocalHero;

            var CreepblockMenu = context.rootMenu.CreateMenu("Creepblock");
            holdKey = CreepblockMenu.CreateHoldKey("Creep block key");
            sensitivity = CreepblockMenu.CreateSlider("Block sensitivity", 550, 500, 700);
            skipranged = CreepblockMenu.CreateSwitcher("Block ranged", false);


            updateHandler = UpdateManager.CreateIngameUpdate(50, false, OnUpdate);
            holdKey.ValueChanged += (sender, e) =>
            {
                updateHandler.IsEnabled = e.Value;
            };
        }

        internal static void Dispose()
        {
            //TODO
        }

        private void OnUpdate()
        {
            if (!hero.IsAlive || GameManager.IsPaused)
            {
                return;
            }

            var creeps = EntityManager.GetEntities<Creep>().Where(
                    x => x.IsValid && x.IsSpawned && x.IsAlive && x.Team == hero.Team && x.Distance2D(hero) < 500)
                .ToList();

            if (!creeps.Any())
            {
                return;
            }

            var creepsMoveDirection = creeps.Aggregate(new Vector3(), (sum, creep) => sum + creep.InFront(1000))
                                      / creeps.Count;

            var tower = EntityManager.GetEntities<Tower>().FirstOrDefault(
                x => x.IsValid && x.IsAlive && x.Distance2D(hero) < 500 && (x.Name == "npc_dota_badguys_tower2_mid" || x.Name == "npc_dota_goodguys_tower2_mid"));

            if (tower?.Distance2D(hero) < 200)
            {
                // dont block near retarded dire mid t2 tower
                hero.Move(creepsMoveDirection);
                return;
            }

            foreach (var creep in creeps.OrderByDescending(x => x.IsMoving)
                .ThenBy(x => x.Distance2D(creepsMoveDirection)))
            {
                if (!skipranged && creep.IsRanged)
                {
                    continue;
                }

                var creepDistance = creep.Distance2D(creepsMoveDirection) + 50;
                var heroDistance = hero.Distance2D(creepsMoveDirection);
                var creepAngle = creep.FindRotationAngle(hero.Position);

                if (creepDistance < heroDistance && creepAngle > 2 || creepAngle > 2.5)
                {
                    continue;
                }

                var moveDistance = sensitivity.Value / hero.MovementSpeed * 100;
                var movePosition = creep.InFront(Math.Max(moveDistance, moveDistance * creepAngle));

                if (movePosition.Distance(creepsMoveDirection) > heroDistance)
                {
                    continue;
                }

                if (creepAngle < 0.3)
                {
                    if (hero.MovementSpeed - creep.MovementSpeed > 50
                        && creeps.Select(x => x.FindRotationAngle(hero.Position)).Average() < 0.4)
                    {
                        hero.Stop();
                        return;
                    }

                    continue;
                }

                hero.Move(movePosition);
                return;
            }

            hero.Stop();
        }
    }
}
