// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Linq;
using Community.PowerToys.Run.Plugin.VisualStudio.Core.Services;
using Microsoft.CmdPal.Extensions;
using Microsoft.CmdPal.Extensions.Helpers;

namespace VisualStudioExtension.Pages;

internal sealed partial class VisualStudioExtensionPage : ListPage
{
    private readonly VisualStudioService _visualStudioService;
    private readonly SettingsManager _settingsManager;

    public VisualStudioExtensionPage(SettingsManager settingsManager, VisualStudioService visualStudioService)
    {
        _settingsManager = settingsManager;
        _visualStudioService = visualStudioService;

        Name = "Name".GetLocalized();

        var lightIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/VisualStudio.light.png");
        var darkIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/VisualStudio.dark.png");
        Icon = new(new(lightIcon), new(darkIcon));
    }

    public override IListItem[] GetItems()
    {
        return _visualStudioService
            .GetResults(_settingsManager.ShowPrerelease)
            .Select(r => new CodeContainerListItem(r, _settingsManager))
            .ToArray();
    }
}
