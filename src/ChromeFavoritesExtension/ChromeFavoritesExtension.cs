// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services;
using Microsoft.CommandPalette.Extensions;

namespace ChromeFavoritesExtension
{
    [ComVisible(true)]
#if DEBUG
    [Guid("2857c99a-97a3-460a-b81b-2e84da103dc9")]
#else
    [Guid("e5363d08-aa2b-4af8-aa0a-8a9dfc45e491")]
#endif
    [ComDefaultInterface(typeof(IExtension))]
    public sealed partial class ChromeFavoritesExtension : IExtension, IDisposable
    {
        private readonly ManualResetEvent _extensionDisposedEvent;
        private readonly SettingsManager _settingsManager;
        private readonly ILogger _logger;
        private readonly ChromeManager _edgeManager;
        private readonly ProfileManager _profileManager;
        private readonly FavoriteQuery _favoriteQuery;

        private readonly CommandsProvider _provider;

        public ChromeFavoritesExtension(ManualResetEvent extensionDisposedEvent)
        {
            _extensionDisposedEvent = extensionDisposedEvent;

            _settingsManager = new SettingsManager();
            _logger = new Logger();
            _edgeManager = new ChromeManager(_logger);
            _profileManager = new ProfileManager(_edgeManager);
            _favoriteQuery = new FavoriteQuery(_profileManager);
            _provider = new CommandsProvider(_settingsManager, _edgeManager, _favoriteQuery, _profileManager);
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
}
