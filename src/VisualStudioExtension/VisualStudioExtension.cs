// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Community.PowerToys.Run.Plugin.VisualStudio.Core;
using Community.PowerToys.Run.Plugin.VisualStudio.Core.Services;
using Microsoft.CmdPal.Extensions;

namespace VisualStudioExtension;

[ComVisible(true)]
[Guid("b6f2e125-fa86-4e65-b787-5f98b672bff3")]
[ComDefaultInterface(typeof(IExtension))]
public sealed partial class VisualStudioExtension : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;
    private readonly ILogger _logger;
    private readonly VisualStudioService _visualStudioService;

    private readonly VisualStudioExtensionCommandsProvider _provider;

    public VisualStudioExtension(ManualResetEvent extensionDisposedEvent)
    {
        _extensionDisposedEvent = extensionDisposedEvent;

        _logger = new Logger();
        _visualStudioService = new VisualStudioService(_logger);
        _visualStudioService.InitInstances([]); // TODO implement settings
        _provider = new VisualStudioExtensionCommandsProvider(_visualStudioService);
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose() => _extensionDisposedEvent.Set();
}
