// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Community.PowerToys.Run.Plugin.VisualStudio.Core.Services;
using Microsoft.CmdPal.Extensions;
using Microsoft.CmdPal.Extensions.Helpers;
using VisualStudioExtension.Pages;

namespace VisualStudioExtension;

public partial class VisualStudioExtensionCommandsProvider : CommandProvider
{
    private readonly VisualStudioService _visualStudioService;

    public VisualStudioExtensionCommandsProvider(VisualStudioService visualStudioService)
    {
        _visualStudioService = visualStudioService;

        _commands =
        [
            new CommandItem(new VisualStudioExtensionPage(_visualStudioService))
            {
                Subtitle = "Search Visual Studio recents",
            }
        ];
    }

    private readonly ICommandItem[] _commands;

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }
}
