// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Diagnostics;
using Community.PowerToys.Run.Plugin.VisualStudio.Core.Models;
using Microsoft.CmdPal.Extensions;
using Microsoft.CmdPal.Extensions.Helpers;

namespace VisualStudioExtension.Commands
{
    internal partial class OpenVisualStudioCommand : InvokableCommand
    {
        private readonly bool _elevated;
        private readonly string _fileName;
        private readonly string _arguments;

        internal OpenVisualStudioCommand(CodeContainer codeContainer, bool elevated)
        {
            _elevated = elevated;
            _fileName = codeContainer.Instance.InstancePath;
            _arguments = $"\"{codeContainer.FullPath}\"";

            Icon = new(_elevated ? "\uE7EF" : "\uE737");
            Name = _elevated ? "Open as administrator" : "Open";
        }

        public override ICommandResult Invoke()
        {
            using var process = new Process();
            process.StartInfo.FileName = _fileName;
            process.StartInfo.Arguments = _arguments;
            process.StartInfo.UseShellExecute = true;

            if (_elevated)
            {
                process.StartInfo.Verb = "runas";
            }

            try
            {
                process.Start();
            }
            catch (Win32Exception)
            {
            }

            return CommandResult.Hide();
        }
    }
}
