// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Community.PowerToys.Run.Plugin.VisualStudio.Core.Models;
using Microsoft.CmdPal.Extensions;
using Microsoft.CmdPal.Extensions.Helpers;

namespace VisualStudioExtension.Commands
{
    internal partial class OpenFolderCommand : InvokableCommand
    {
        private readonly string _fileName;

        internal OpenFolderCommand(CodeContainer codeContainer)
        {
            _fileName = Path.GetDirectoryName(codeContainer.FullPath) ?? string.Empty;

            Icon = new("\uE838");
            Name = "Command_OpenFolder".GetLocalized();
        }

        public override ICommandResult Invoke()
        {
            using var process = new Process();
            process.StartInfo.FileName = _fileName;
            process.StartInfo.UseShellExecute = true;

            try
            {
                process.Start();
            }
            catch (Win32Exception)
            {
            }

            return CommandResult.Dismiss();
        }
    }
}
