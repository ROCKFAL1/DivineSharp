using Divine;
using Divine.SDK.Extensions;
using Divine.SDK.Prediction;
using System.Linq;

namespace DawnBreaker_SkillHelper
{
    internal class HammerHelper
    {
        private Context Context { get; set; }

        public HammerHelper(Context Context)
        {
            this.Context = Context;
            Context.Menu.HammerHelper.ValueChanged += HammerHelper_ValueChanged;
        }

        private void HammerHelper_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
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
            Context.Menu.HammerHelper.ValueChanged -= HammerHelper_ValueChanged;
        }

        private float HammerCastRange()
        {
            if (Context.Dawnbreaker.LocalHero.Spellbook.Talents.Any(x => x.Id == AbilityId.special_bonus_unique_dawnbreaker_celestial_hammer_cast_range && x.Level == 1))
            {
                return 2400;
            }
            return 1000 + 100 * (Context.Dawnbreaker.Hammer.Level - 1);
        }

        private void OrderManager_OrderAdding(OrderAddingEventArgs e)
        {
            if (e.Order.Ability != Context.Dawnbreaker.Hammer)
            {
                return;
            }

            var Target = EntityManager.GetEntities<Hero>().Where(x => x.Position.Distance2D(GameManager.MousePosition) < 200
                                                                      && x.IsAlive
                                                                      && x.IsEnemy(Context.Dawnbreaker.LocalHero)
                                                                      && !x.IsIllusion).OrderBy(y => y.Position.Distance2D(GameManager.MousePosition));

            if (Target == null || Target.Count() == 0)
            {
                return;
            }

            var input = new PredictionInput
            {
                Owner = Context.Dawnbreaker.LocalHero,
                Speed = 1200,
                Delay = 0.6f,
                PredictionSkillshotType = PredictionSkillshotType.SkillshotLine
            };

            var range = HammerCastRange();

            input = input.WithTarget(Target.FirstOrDefault());

            var predictPos = PredictionManager.GetPrediction(input).CastPosition;
            var pos = Context.Dawnbreaker.LocalHero.Distance2D(predictPos);
            var predict = Context.Dawnbreaker.LocalHero.Position.Extend(predictPos,  pos * 1.75f > range ? range : pos * 1.75f);

            Context.Dawnbreaker.Hammer.Cast(predict, false, true);

            UpdateManager.BeginInvoke((int)(Context.Dawnbreaker.LocalHero.Position.Distance2D(predict) > 800 ? Context.Dawnbreaker.LocalHero.Position.Distance2D(predict) : 800) , () =>
            {
                Context.Dawnbreaker.Converge.Cast();
            });
        }
    }
}
