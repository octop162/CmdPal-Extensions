// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using Community.PowerToys.Run.Plugin.VisualStudio.Core.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using VisualStudioExtension.Pages;

namespace VisualStudioExtension
{
    internal partial class CommandsProvider : CommandProvider
    {
        private readonly SettingsManager _settingsManager;
        private readonly VisualStudioService _visualStudioService;
        private readonly ICommandItem[] _commands;
        private readonly string _lightIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/VisualStudio.light.png");
        private readonly string _darkIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/VisualStudio.dark.png");

        public CommandsProvider(SettingsManager settingsManager, VisualStudioService visualStudioService)
        {
            _settingsManager = settingsManager;
            _visualStudioService = visualStudioService;
            Settings = _settingsManager.Settings;
            DisplayName = "Name".GetLocalized();
            Icon = new(new(_lightIcon), new(_darkIcon));

            _commands =
            [
                new CommandItem(new SearchPage(_settingsManager, _visualStudioService))
                {
                    Subtitle = "Description".GetLocalized(),
                }
            ];
        }

        public override ICommandItem[] TopLevelCommands() => _commands;
    }
}
