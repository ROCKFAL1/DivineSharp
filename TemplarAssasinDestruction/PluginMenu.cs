using Divine;
using Divine.Menu;
using Divine.Menu.Items;
using System.Collections.Generic;
using System.Windows.Input;

namespace TemplarAssasinDestruction
{
    internal class PluginMenu
    {
        public MenuSwitcher PluginStatus { get; set; }
        public MenuHoldKey ComboKey { get; set; }
        public MenuItemToggler ComboItems { get; set; }
        public Menu HarassMenu { get; set; }
        public MenuHoldKey HarassKey { get; set; }
        public MenuSelector HarassMode { get; set; }

        private Menu RootMenu;

        private Dictionary<AbilityId, bool> Items = new Dictionary<AbilityId, bool>
        {
            { AbilityId.item_blink, true },
            { AbilityId.item_swift_blink, true },
            { AbilityId.item_overwhelming_blink, true },
            { AbilityId.item_arcane_blink, true },
            { AbilityId.item_black_king_bar, true },
            { AbilityId.item_sheepstick, true },
            { AbilityId.item_manta, true },
            { AbilityId.item_nullifier, true },
            { AbilityId.item_orchid, true },
            { AbilityId.item_bloodthorn, true }
        };


        public PluginMenu()
        {
            RootMenu = MenuManager.CreateRootMenu("TADestruction")
                .SetHeroTexture(HeroId.npc_dota_hero_templar_assassin)
                .SetTooltip("V1.2 BETA");

            PluginStatus = RootMenu.CreateSwitcher("On/Off");
            ComboKey = RootMenu.CreateHoldKey("Combo Key", Key.None);
            ComboItems = RootMenu.CreateItemToggler("Items", Items);
            HarassMenu = RootMenu.CreateMenu("Harass Settings");
            HarassMode = HarassMenu.CreateSelector("Harass Mode", new string[2] { "Closest Harass Position", " To Mouse" });
            HarassKey = HarassMenu.CreateHoldKey("Harass Key", Key.None);

        }
    }
}