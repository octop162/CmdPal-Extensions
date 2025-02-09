// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EdgeFavoritesExtension
{
    [ComVisible(true)]
    [Guid("e5363d08-aa2b-4af8-aa0a-8a9dfc45e491")]
    [ComDefaultInterface(typeof(IExtension))]
    public sealed partial class EdgeFavoritesExtension : IExtension, IDisposable
    {
        private readonly ManualResetEvent _extensionDisposedEvent;
        private readonly SettingsManager _settingsManager;
        private readonly Settings _settings;
        private readonly ILogger _logger;
        private readonly EdgeManager _edgeManager;
        private readonly ProfileManager _profileManager;
        private readonly FavoriteQuery _favoriteQuery;

        private readonly CommandsProvider _provider;

        public EdgeFavoritesExtension(ManualResetEvent extensionDisposedEvent)
        {
            _extensionDisposedEvent = extensionDisposedEvent;

            _settingsManager = new SettingsManager();
            _logger = new Logger();
            _settingsManager = new SettingsManager();
            _logger = new Logger();
            _edgeManager = new EdgeManager(_logger);
            _profileManager = new ProfileManager(_logger, _edgeManager);
            _favoriteQuery = new FavoriteQuery(_profileManager);
            _provider = new CommandsProvider(_settingsManager, _edgeManager, _favoriteQuery);

            _settings = _settingsManager.Settings;
            _settings.SettingsChanged += SettingsChanged;

            Initialize();
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

        private void SettingsChanged(object sender, Settings args) => Initialize();

        private void Initialize()
        {
            _edgeManager.Initialize(_settingsManager.Channel);
            _profileManager.ReloadProfiles(_settingsManager.ExcludedProfiles);
        }
    }
}
