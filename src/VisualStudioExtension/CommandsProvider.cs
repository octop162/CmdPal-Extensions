// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        public CommandsProvider(SettingsManager settingsManager, VisualStudioService visualStudioService)
        {
            _settingsManager = settingsManager;
            _visualStudioService = visualStudioService;

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
