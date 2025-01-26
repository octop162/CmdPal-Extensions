// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Community.PowerToys.Run.Plugin.VisualStudio.Core.Models;
using Microsoft.CmdPal.Extensions;
using Microsoft.CmdPal.Extensions.Helpers;
using VisualStudioExtension.Commands;
using VisualStudioExtension.Pages;

namespace VisualStudioExtension
{
    internal partial class CodeContainerListItem : ListItem
    {
        private readonly SettingsManager _settingsManager;

        public CodeContainerListItem(CodeContainer codeContainer, SettingsManager settingsManager)
            : base(new OpenVisualStudioCommand(codeContainer, false))
        {
            _settingsManager = settingsManager;

            Title = codeContainer.Name;
            Icon = new IconInfo(codeContainer.Instance.InstancePath);
            Subtitle = string.Format("Result_Subtitle".GetLocalized(), codeContainer.Instance.DisplayName, codeContainer.FullPath);
            MoreCommands =
            [
                new CommandContextItem(new OpenVisualStudioCommand(codeContainer, true)),
                new CommandContextItem(new CopyTextCommand(codeContainer.FullPath)),
                new CommandContextItem(new OpenFolderCommand(codeContainer)),
                new CommandContextItem(new SettingsPage(_settingsManager)),
            ];
        }
    }
}
