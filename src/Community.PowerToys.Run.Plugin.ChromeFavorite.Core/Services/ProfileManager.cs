// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Models;

namespace Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services
{
    public sealed partial class ProfileManager : IProfileManager, IDisposable
    {
        private readonly ChromeManager _chromeManager;
        private readonly List<IFavoriteProvider> _favoriteProviders = new();
        private bool _disposed;

        public ReadOnlyCollection<IFavoriteProvider> FavoriteProviders => _favoriteProviders.AsReadOnly();

        public ProfileManager(ChromeManager chromeManager)
        {
            _chromeManager = chromeManager;
        }

        public void ReloadProfiles(IEnumerable<string> excluded)
        {
            var userDataPath = _chromeManager.UserDataPath;

            if (!Directory.Exists(userDataPath))
            {
                return;
            }

            if (_favoriteProviders.Count > 0)
            {
                DisposeFavoriteProviders();
                _favoriteProviders.Clear();
            }

            foreach (var path in Directory.GetFiles(userDataPath, "Bookmarks", new EnumerationOptions { RecurseSubdirectories = true, MaxRecursionDepth = 2 }))
            {
                var directory = Directory.GetParent(path);

                if (directory == null)
                {
                    continue;
                }

                // Guest profile doesn't allow favorites
                if (directory.Name.Equals("Guest Profile", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var name = GetProfileName(directory.FullName) ?? directory.Name;

                if (excluded.Any(e => e.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                var profile = new ProfileInfo(name, directory.Name);
                _favoriteProviders.Add(new FavoriteProvider(path, profile));
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            DisposeFavoriteProviders();
            _disposed = true;
        }

        private string? GetProfileName(string directoryPath)
        {
            try
            {
                var preferencesPath = Path.Combine(directoryPath, "Preferences");
                if (!File.Exists(preferencesPath))
                {
                    return null;
                }

                using var fs = new FileStream(preferencesPath, FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);
                string json = sr.ReadToEnd();
                var parsed = JsonDocument.Parse(json);
                parsed.RootElement.TryGetProperty("profile", out var profileElement);
                profileElement.TryGetProperty("name", out var nameElement);
                if (nameElement.ValueKind != JsonValueKind.String)
                {
                    return null;
                }

                var name = nameElement.GetString();
                if (string.IsNullOrWhiteSpace(name))
                {
                    return null;
                }

                return name;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void DisposeFavoriteProviders()
        {
            foreach (var provider in _favoriteProviders)
            {
                if (provider is IDisposable disposableProvider)
                {
                    disposableProvider.Dispose();
                }
            }
        }
    }
}
