using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Prediction;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DawnBreaker_SkillHelper
{
    internal class StarbreakerHelper
    {
        private Context Context { get; set; }
        private const float Distance = 500;

        public StarbreakerHelper(Context Context)
        {
            this.Context = Context;

            Context.Menu.StarbreakerHelper.ValueChanged += StarbreakerHelper_ValueChanged;
        }

        private void StarbreakerHelper_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                OrderManager.OrderAdding += OrderManager_OrderAdding;
            }
            else
            {
                OrderManager.OrderAdding -= OrderManager_OrderAdding;
            }
        }

        public void Dispose()
        {
            Context.Menu.StarbreakerHelper.ValueChanged -= StarbreakerHelper_ValueChanged;
        }

        private Unit FindMainTarget(Hero LocalHero, List<Hero> Heroes)
        {
            var heroPos = LocalHero.Position.ToVector2();
            var mousePos = GameManager.MousePosition.ToVector2();

            Vector2[] posOfCheck = { heroPos.Extend(mousePos, Distance / 2), heroPos.Extend(mousePos, Distance / 5) };

            foreach (var pos in posOfCheck)
            {
                var target = EntityManager.GetEntities<Hero>()
                                          .Where(x => Heroes.Contains(x)
                                                      && x.Position.Distance2D(pos) <= Distance)
                                          .OrderBy(y => y.Position.Distance2D(pos))
                                          .FirstOrDefault();

                if (target != null)
                {
                    return target;
                }
            }

            return default;
        }

        private bool IsRunningBackward(Vector3 EnemyPos, Vector3 PredictPoint)
        {
            var count = Math.Floor(EnemyPos.Distance2D(PredictPoint) / 100);
            for (int i = 0; i < count; i++)
            {
                var extendetPos = PredictPoint.Extend(EnemyPos, 100 * i);
                if (Context.Dawnbreaker.LocalHero.IsInRange(extendetPos, 100))
                {
                    return true;
                }
            }
            return false;

        }

        private void OrderManager_OrderAdding(OrderAddingEventArgs e)
        {
            
            if (e.Order.Ability != Context.Dawnbreaker.Starbreaker)
            {
                return;
            }

            var Targets = EntityManager.GetEntities<Hero>()
                .Where(x => x.Distance2D(Context.Dawnbreaker.LocalHero.Position) < Distance
                            && x.IsAlive
                            && x.IsEnemy(Context.Dawnbreaker.LocalHero)
                            && !x.IsIllusion).ToList();

            if (Targets == null || Targets.Count() == 0)
            {
                return;
            }

            var mainTarget = FindMainTarget(Context.Dawnbreaker.LocalHero, Targets);

            var input = new PredictionInput
            {
                Speed = 215 * 1.1f,
                Owner = Context.Dawnbreaker.LocalHero,
                AreaOfEffectHitMainTarget = true,
                AreaOfEffect = true,
                AreaOfEffectTargets = Targets,
                PredictionSkillshotType = PredictionSkillshotType.SkillshotCircle,
                Radius = 200,
                Delay = 0.5f,
                CollisionTypes = Divine.SDK.Prediction.Collision.CollisionTypes.None
            };

            input = input.WithTarget(mainTarget);

            var predict = PredictionManager.GetPrediction(input).CastPosition;

            if (IsRunningBackward(mainTarget.Position, predict))
            {
                Context.Dawnbreaker.Starbreaker.Cast(mainTarget.Position.Extend(predict, mainTarget.Position.Distance2D(predict) / 3), false, true);
                return;
            }

            Context.Dawnbreaker.Starbreaker.Cast(predict, false, true);

        }


    }
}
