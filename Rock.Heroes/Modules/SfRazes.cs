using Divine;
using Divine.Entity;
using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Heroes.Components;
using Divine.Extensions;
using Divine.Game;
using Divine.Menu.Items;
using Divine.Numerics;
using Divine.Order;
using Divine.Order.EventArgs;
using Divine.Order.Orders.Components;
using Divine.Particle;
using Divine.Particle.Components;
using Divine.Renderer;
using System;

namespace RockHeroes.Modules
{
    internal class SfRazes
    {
        private readonly MenuSwitcher DrawRazes;
        private readonly MenuSwitcher Razes2Mouse;
        private readonly MenuSlider ColorSwitcher;
        private readonly Color[] Colours = { Color.White, Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Pink, Color.Purple };

        public SfRazes(Context context)
        {
            var SfRazesMenu = context.rootMenu.CreateMenu("SF helper").SetHeroImage(HeroId.npc_dota_hero_nevermore);
            Razes2Mouse = SfRazesMenu.CreateSwitcher("Shadowrazes to mouse direction");
            DrawRazes = SfRazesMenu.CreateSwitcher("Draw Shadowrazes");
            ColorSwitcher = SfRazesMenu.CreateSlider("Colors", 0, 0, 6);
            if (EntityManager.LocalHero.HeroId != HeroId.npc_dota_hero_nevermore)
            {
                return;
            }

            OrderManager.OrderAdding += OnUnitOrder;
            DrawRazes.ValueChanged += DrawRazes_ValueChanged;
        }

        private void DrawRazes_ValueChanged(MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                RendererManager.Draw += OnDraw;
            }
            else
            {
                RendererManager.Draw -= OnDraw;
                for (int i = 0; i < 3; i++)
                    ParticleManager.RemoveParticle($"DrawRaze_{i}");
                return;
            }

        }


        private static Vector2 FromPolarCoordinates(float radial, float polar)
        {
            return new Vector2((float)Math.Cos(polar) * radial, (float)Math.Sin(polar) * radial);
        }

        internal void Dispose()
        {
            //TODO
        }

        public static Vector3 InFront(Unit unit, float distance)
        {
            var alpha = unit.RotationRad;
            var vector2FromPolarAngle = FromPolarCoordinates(1f, alpha);

            var v = unit.Position + (vector2FromPolarAngle.ToVector3() * distance);
            return new Vector3(v.X, v.Y, 0);
        }

        private void OnDraw()
        {
            Color Choosed = Colours[ColorSwitcher.Value];
            var localHero = EntityManager.LocalHero;
            if (localHero.Name != "npc_dota_hero_nevermore")
            {
                return;
            }
            var razes = new[] { 200, 450, 700 };
            for (int i = 0; i < 3; i++)
            {
                var inFront = InFront(localHero, razes[i]);

                ParticleManager.CreateOrUpdateParticle(
                    $"DrawRaze_{i}",
                    "materials/ensage_ui/particles/alert_range.vpcf",
                    localHero,
                    ParticleAttachment.AbsOrigin,
                    new ControlPoint(0, inFront),
                    new ControlPoint(1, Choosed),
                    new ControlPoint(2, 250, 255, 7));
            }
        }

        private void OnUnitOrder(OrderAddingEventArgs e)
        {
            if (!Razes2Mouse.Value) { return; }
            var localHero = EntityManager.LocalHero;
            Ability order = e.Order.Ability;
            if (localHero.Name != "npc_dota_hero_nevermore" || e.IsCustom) { return; }
            if (e.Order.Type == OrderType.Cast && (order.Id == AbilityId.nevermore_shadowraze1
                                                   || order.Id == AbilityId.nevermore_shadowraze2
                                                   || order.Id == AbilityId.nevermore_shadowraze3))
            {
                localHero.MoveToDirection(GameManager.MousePosition);
                order.Cast();
                e.Process = true;
            }
        }
    }
}
