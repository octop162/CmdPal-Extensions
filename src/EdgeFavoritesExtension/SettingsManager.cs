// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Linq;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Models;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EdgeFavoritesExtension
{
    internal class SettingsManager : JsonSettingsManager
    {
        private readonly ToggleSetting _searchTree = new(
            nameof(SearchTree),
            "Search as tree",
            "Navigate the folder tree when searching (requires Command Palette restart).",
            false);

        private readonly TextSetting _excludedProfiles = new(
           nameof(ExcludedProfiles),
           "Excluded profiles",
           "Prevents favorites from the specified profiles to be loaded. Add one profile per line.",
           string.Empty);

        private readonly ChoiceSetSetting _channel = new(
            nameof(Channel),
            "Channel",
            "Select the channel to use.",
            [
                new("Stable", "Stable"),
                new("Beta", "Beta"),
                new("Dev", "Dev"),
                new("Canary", "Canary"),
            ]);

        public bool SearchTree => _searchTree.Value;

        public string[] ExcludedProfiles => _excludedProfiles.Value?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray() ?? [];

        public Channel Channel => _channel.Value != null && Enum.TryParse(_channel.Value.ToString(), out Channel channel) ? channel : Channel.Stable;

        public SettingsManager()
        {
            FilePath = SettingsJsonPath();

            Settings.Add(_searchTree);
            Settings.Add(_excludedProfiles);
            Settings.Add(_channel);

            LoadSettings();
        }

        private static string SettingsJsonPath()
        {
            var directory = Utilities.BaseSettingsPath("EdgeFavoritesExtension");
            Directory.CreateDirectory(directory);
            return Path.Combine(directory, "settings.json");
        }
    }
}
