﻿using Divine;
using System.Collections.Generic;

namespace RockRubick
{
    internal sealed class Dictionaries
    {
        public static Dictionary<AbilityId, float> RubickSpellCD = new Dictionary<AbilityId, float> { };
        public static Dictionary<Hero, AbilityId> LastSpell = new Dictionary<Hero, AbilityId> { };
        public static Dictionary<(Hero, AbilityId), float> Removed = new Dictionary<(Hero, AbilityId), float> { };
        public static List<AbilityId> ignore = new List<AbilityId> {
            AbilityId.lone_druid_true_form,
            AbilityId.pudge_rot,
            AbilityId.witch_doctor_voodoo_restoration
        };

        public static readonly Dictionary<AbilityId, int> SpellList = new Dictionary<AbilityId, int>
        {
            #region spells
            {AbilityId.nevermore_shadowraze3, 3},
            {AbilityId.nevermore_shadowraze2 , 3},
            {AbilityId.nevermore_shadowraze1, 3},
            {AbilityId.lion_finger_of_death, 4 },
            {AbilityId.lion_impale, 3},
            {AbilityId.enigma_black_hole, 5},
            {AbilityId.earthshaker_echo_slam, 5},// нужна спецальная логика! UPD: Добавлено!
            {AbilityId.enigma_midnight_pulse, 3},
            {AbilityId.abaddon_aphotic_shield, 1},
            {AbilityId.abaddon_borrowed_time, 2},
            {AbilityId.alchemist_unstable_concoction, 2},
            {AbilityId.ancient_apparition_ice_blast, 4},
            {AbilityId.ancient_apparition_cold_feet, 2},
            {AbilityId.antimage_blink, 1},
            {AbilityId.arc_warden_flux, 1},
            {AbilityId.arc_warden_magnetic_field, 2},
            {AbilityId.arc_warden_tempest_double, 4},
            {AbilityId.bane_brain_sap, 3 },
            {AbilityId.bane_fiends_grip, 5},
            {AbilityId.batrider_flaming_lasso, 4},
            {AbilityId.batrider_firefly, 2},
            {AbilityId.beastmaster_wild_axes, 2},
            {AbilityId.beastmaster_primal_roar, 5},
            {AbilityId.bloodseeker_bloodrage, 1},
            {AbilityId.bloodseeker_rupture, 4},
            {AbilityId.bounty_hunter_track, 1},
            {AbilityId.bounty_hunter_shuriken_toss, 2},
            {AbilityId.brewmaster_primal_split, 4},
            {AbilityId.broodmother_spin_web, 1},
            {AbilityId.centaur_hoof_stomp, 2},
            {AbilityId.centaur_stampede, 3},
            {AbilityId.chaos_knight_chaos_bolt, 3},
            {AbilityId.chen_hand_of_god, 2},
            {AbilityId.rattletrap_hookshot, 4},
            {AbilityId.rattletrap_power_cogs, 2},
            {AbilityId.crystal_maiden_frostbite, 3},
            {AbilityId.crystal_maiden_freezing_field, 5},
            {AbilityId.dark_seer_wall_of_replica, 4},
            {AbilityId.dark_seer_ion_shell, 2},
            {AbilityId.dark_seer_surge, 2},
            {AbilityId.dark_willow_cursed_crown, 3},
            {AbilityId.dark_willow_bedlam, 5},
            {AbilityId.dark_willow_terrorize, 2},
            {AbilityId.dark_willow_shadow_realm, 2 },
            {AbilityId.dark_willow_bramble_maze, 2},
            {AbilityId.dazzle_poison_touch, 2},
            {AbilityId.dazzle_shallow_grave, 1},
            {AbilityId.death_prophet_exorcism, 5},
            {AbilityId.death_prophet_silence, 3},
            {AbilityId.disruptor_static_storm, 5},
            {AbilityId.disruptor_glimpse, 1},
            {AbilityId.disruptor_thunder_strike, 2},
            {AbilityId.doom_bringer_doom, 5},
            {AbilityId.drow_ranger_wave_of_silence, 2},
            {AbilityId.earthshaker_fissure, 2},
            {AbilityId.elder_titan_earth_splitter, 5},
            {AbilityId.ember_spirit_sleight_of_fist, 1},
            {AbilityId.ember_spirit_fire_remnant, 3},
            {AbilityId.enchantress_natures_attendants, 1},
            {AbilityId.enigma_malefice, 2},
            {AbilityId.faceless_void_time_walk, 1},
            {AbilityId.faceless_void_chronosphere, 2},
            {AbilityId.grimstroke_soul_chain, 5},
            {AbilityId.grimstroke_ink_creature, 3},
            {AbilityId.gyrocopter_rocket_barrage, 3},
            {AbilityId.gyrocopter_homing_missile, 3},
            {AbilityId.gyrocopter_call_down, 4},
            {AbilityId.hoodwink_sharpshooter, 5},
            {AbilityId.hoodwink_bushwhack, 3},
            {AbilityId.huskar_inner_fire, 1},
            {AbilityId.invoker_chaos_meteor, 4},
            {AbilityId.invoker_sun_strike, 3},
            {AbilityId.invoker_deafening_blast, 3},
            {AbilityId.invoker_emp, 3},
            {AbilityId.invoker_tornado, 2},
            {AbilityId.jakiro_macropyre, 4},
            {AbilityId.jakiro_ice_path, 1},
            {AbilityId.keeper_of_the_light_illuminate, 3},
            {AbilityId.kunkka_torrent, 2},
            {AbilityId.kunkka_ghostship, 4},
            {AbilityId.legion_commander_press_the_attack, 1},
            {AbilityId.legion_commander_overwhelming_odds, 2},
            {AbilityId.leshrac_pulse_nova, 4},
            {AbilityId.leshrac_split_earth, 3},
            {AbilityId.leshrac_diabolic_edict, 3},
            {AbilityId.lich_chain_frost, 5},
            {AbilityId.lich_ice_spire, 4},
            {AbilityId.lich_sinister_gaze, 3},
            {AbilityId.lina_dragon_slave, 3},
            {AbilityId.lina_laguna_blade, 4},
            {AbilityId.lina_light_strike_array, 3},
            {AbilityId.lion_voodoo, 3},
            {AbilityId.lone_druid_spirit_bear, 3},
            {AbilityId.luna_eclipse, 5},
            {AbilityId.luna_lucent_beam, 3},
            {AbilityId.lycan_shapeshift, 2},
            {AbilityId.magnataur_reverse_polarity, 5},
            {AbilityId.magnataur_empower, 2},
            {AbilityId.mars_arena_of_blood, 4},
            {AbilityId.mars_spear, 3},
            {AbilityId.medusa_stone_gaze, 3},
            {AbilityId.mirana_starfall, 3},
            {AbilityId.monkey_king_tree_dance, 3},
            {AbilityId.monkey_king_wukongs_command, 3},
            {AbilityId.morphling_morph, 2},
            {AbilityId.naga_siren_song_of_the_siren, 4},
            {AbilityId.furion_wrath_of_nature, 3},
            {AbilityId.furion_teleportation, 1},
            {AbilityId.necrolyte_reapers_scythe, 5},
            {AbilityId.necrolyte_death_pulse, 2},
            {AbilityId.nyx_assassin_vendetta, 5},
            {AbilityId.nyx_assassin_impale, 3},
            {AbilityId.ogre_magi_fireblast, 2},
            {AbilityId.omniknight_guardian_angel, 4},
            {AbilityId.omniknight_purification, 3},
            {AbilityId.oracle_false_promise, 3},
            {AbilityId.obsidian_destroyer_sanity_eclipse, 4},
            {AbilityId.obsidian_destroyer_astral_imprisonment, 2},
            {AbilityId.pangolier_rollup, 4},
            {AbilityId.phantom_lancer_spirit_lance, 2},
            {AbilityId.phoenix_supernova, 5},
            {AbilityId.puck_dream_coil, 5},
            {AbilityId.puck_waning_rift, 3},
            {AbilityId.puck_illusory_orb, 2},
            {AbilityId.pudge_meat_hook, 3},
            {AbilityId.pudge_dismember, 3},
            {AbilityId.pugna_life_drain, 4},
            {AbilityId.pugna_nether_blast, 3},
            {AbilityId.queenofpain_sonic_wave, 5},
            {AbilityId.queenofpain_blink, 1},
            {AbilityId.queenofpain_scream_of_pain, 3},
            {AbilityId.queenofpain_shadow_strike, 3},
            {AbilityId.razor_eye_of_the_storm, 4},
            {AbilityId.razor_static_link, 3},
            {AbilityId.sandking_burrowstrike, 3},
            {AbilityId.sandking_epicenter, 5},
            {AbilityId.shadow_demon_demonic_purge, 4},
            {AbilityId.shadow_demon_soul_catcher, 2},
            {AbilityId.nevermore_requiem, 5 },
            {AbilityId.shadow_shaman_ether_shock, 3},
            {AbilityId.shadow_shaman_shackles, 3},
            {AbilityId.shadow_shaman_voodoo, 3},
            {AbilityId.shadow_shaman_mass_serpent_ward, 5 },
            {AbilityId.silencer_global_silence, 4},
            {AbilityId.silencer_last_word, 3},
            {AbilityId.skywrath_mage_arcane_bolt, 3},
            {AbilityId.skywrath_mage_concussive_shot, 2},
            {AbilityId.skywrath_mage_ancient_seal, 3},
            {AbilityId.skywrath_mage_mystic_flare, 4},
            {AbilityId.slark_shadow_dance, 1 },
            {AbilityId.slark_pounce, 1},
            {AbilityId.snapfire_scatterblast, 3},
            {AbilityId.snapfire_firesnap_cookie, 3},
            {AbilityId.snapfire_mortimer_kisses, 5},
            {AbilityId.sniper_assassinate, 4},
            {AbilityId.sniper_shrapnel, 3},
            {AbilityId.spectre_spectral_dagger, 2},
            {AbilityId.spirit_breaker_charge_of_darkness, 3},
            {AbilityId.storm_spirit_electric_vortex, 3},
            {AbilityId.sven_storm_bolt, 3},
            {AbilityId.techies_remote_mines, 4},
            {AbilityId.templar_assassin_psionic_trap, 3},
            {AbilityId.terrorblade_sunder, 4},
            {AbilityId.tidehunter_ravage, 5},
            {AbilityId.shredder_chakram, 4},
            {AbilityId.shredder_chakram_2, 4},
            {AbilityId.tinker_rearm, 3},
            {AbilityId.tinker_laser, 3},
            {AbilityId.tinker_heat_seeking_missile, 3},
            {AbilityId.tiny_avalanche, 3},
            {AbilityId.tiny_toss, 3},
            {AbilityId.treant_overgrowth, 5},
            {AbilityId.treant_natures_grasp, 3},
            {AbilityId.tusk_ice_shards, 2},
            {AbilityId.abyssal_underlord_firestorm, 3},
            {AbilityId.undying_tombstone, 4},
            {AbilityId.vengefulspirit_magic_missile, 3},
            {AbilityId.vengefulspirit_nether_swap, 4},
            {AbilityId.venomancer_poison_nova, 5},
            {AbilityId.venomancer_venomous_gale, 3},
            {AbilityId.viper_viper_strike, 4},
            {AbilityId.viper_nethertoxin, 3},
            {AbilityId.void_spirit_astral_step, 4},
            {AbilityId.void_spirit_dissimilate, 3},
            {AbilityId.void_spirit_aether_remnant, 3},
            {AbilityId.void_spirit_resonant_pulse, 2},
            {AbilityId.warlock_fatal_bonds, 3},
            {AbilityId.warlock_rain_of_chaos, 4},
            {AbilityId.weaver_time_lapse, 4},
            {AbilityId.windrunner_powershot, 3},
            {AbilityId.windrunner_shackleshot, 3},
            {AbilityId.windrunner_focusfire, 3},
            {AbilityId.winter_wyvern_winters_curse, 4},
            {AbilityId.winter_wyvern_splinter_blast, 3},
            {AbilityId.winter_wyvern_cold_embrace, 1},
            {AbilityId.witch_doctor_death_ward, 5},
            {AbilityId.greevil_maledict, 4},
            {AbilityId.witch_doctor_paralyzing_cask, 3},
            {AbilityId.skeleton_king_hellfire_blast, 3},
            {AbilityId.zuus_arc_lightning, 3},
            {AbilityId.zuus_lightning_bolt, 3},
            {AbilityId.zuus_thundergods_wrath, 5},
            {AbilityId.juggernaut_blade_fury, 3},
            {AbilityId.juggernaut_healing_ward, 2},
            {AbilityId.juggernaut_omni_slash, 3},
            {AbilityId.doom_bringer_devour, 3},
            {AbilityId.weaver_the_swarm, 2},
            {AbilityId.undying_flesh_golem, 2},
            {AbilityId.undying_soul_rip, 3},
            {AbilityId.oracle_purifying_flames, 3}
           #endregion
        };
    }
}
