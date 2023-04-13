using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace TalkMoreThanOnce
{
    public class TalkMoreThanOnce : Mod
    {
        private TalkMoreThanOnceConfig Config;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // when button pressed,              call OnButtonPressed function(below)
            //helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            
            // Add mod config option to menu
            Config = helper.ReadConfig<TalkMoreThanOnceConfig>();
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        }

        
        // private void OnUpdate() {
        //     if (!Context.IsWorldReady)
        //         return;

        //     foreach (Friendship friendship in Game1.player.friendshipData.Values) {
        //         friendship.talkedToToday = false;
        //     }
        // }

        /* Add Mod Config to Menu */
        private void OnGameLaunched(object sender, GameLaunchedEventArgs e) {
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new TalkMoreThanOnceConfig(),
                save: () => this.Helper.WriteConfig(this.Config)
            );

            configMenu.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Talk More Than Once",
                tooltip: null
            );

            configMenu.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Enabled",
                tooltip: () => "Gives player the ability to speak to NPCs an infinite number of times",
                getValue: () => this.Config.TalkMoreThanOnceEnabled,
                setValue: value => this.Config.TalkMoreThanOnceEnabled = value
            );
        }

        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }
    }
}