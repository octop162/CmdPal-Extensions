// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Community.PowerToys.Run.Plugin.VisualStudio.Core.Services;
using Microsoft.CmdPal.Extensions;
using Microsoft.CmdPal.Extensions.Helpers;
using VisualStudioExtension.Pages;

namespace VisualStudioExtension
{
    internal partial class CommandsProvider : CommandProvider
    {
        private readonly SettingsManager _settingsManager;
        private readonly VisualStudioService _visualStudioService;

        public CommandsProvider(SettingsManager settingsManager, VisualStudioService visualStudioService)
        {
            _settingsManager = settingsManager;
            _visualStudioService = visualStudioService;

            _commands =
            [
                new CommandItem(new VisualStudioPage(_settingsManager, _visualStudioService))
                {
                    Subtitle = "Description".GetLocalized(),
                }
            ];
        }

        private readonly ICommandItem[] _commands;

        public override ICommandItem[] TopLevelCommands()
        {
            return _commands;
        }
    }
}
