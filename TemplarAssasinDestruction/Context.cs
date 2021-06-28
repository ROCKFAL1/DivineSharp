using TemplarAssasinDestruction.Abilities.Items;
using TemplarAssasinDestruction.Abilities.Spells;
using TemplarAssasinDestruction.Modes;

namespace TemplarAssasinDestruction
{
    class Context
    {
        public TemplarAssasin TemplarAssasin { get; set; }
        public PluginMenu PluginMenu { get; set; }
        public TargetManager TargetManager { get; set; }
        public SpellHelper SpellHelper { get; set; }
        public ItemHelper ItemHelper { get; set; }
        public Harass Harass { get; set; }
        public Combo Combo { get; set; }

        public Context()
        {
            PluginMenu = new PluginMenu();

            PluginMenu.PluginStatus.ValueChanged += PluginStatus_ValueChanged;
        }

        private void PluginStatus_ValueChanged(Divine.Menu.Items.MenuSwitcher switcher, Divine.Menu.EventArgs.SwitcherEventArgs e)
        {
            if (e.Value)
            {
                TemplarAssasin = new TemplarAssasin(this);
                TargetManager = new TargetManager(this);
                SpellHelper = new SpellHelper(this);
                ItemHelper = new ItemHelper(this);
                Harass = new Harass(this);
                Combo = new Combo(this);
            }
            else
            {
                TemplarAssasin.Dispose();
                TargetManager.Dispose();
                Harass.Dispose();
                Combo.Dispose();
                SpellHelper.Dispose();
                ItemHelper.Dispose();
            }
        }
    }
}
