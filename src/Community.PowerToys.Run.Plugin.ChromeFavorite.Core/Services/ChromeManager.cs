// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Models;

namespace Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services
{
    public class ChromeManager : IChromeManager
    {
        private readonly ILogger? _logger;
        private readonly Dictionary<Channel, (string ExecutableName, string UserDataParentFolder)> _chromeInfo = new()
        {
            { Channel.Stable, ("chrome.exe", "Google\\Chrome") },
            { Channel.Beta, ("chrome.exe", "Google\\Chrome Beta") },
            { Channel.Dev, ("chrome.exe", "Google\\Chrome Dev") },
            { Channel.Canary, ("chrome.exe", "Google\\Chrome SxS") },
        };

        private string? _chromePath;
        private string? _userDataPath;

        public string UserDataPath => _userDataPath ?? string.Empty;

        public bool ChannelDetected => _chromePath != null && _userDataPath != null;

        public ChromeManager(ILogger? logger = null)
        {
            _logger = logger;
        }

        public void Initialize(Channel channel)
        {
            _chromePath = null;
            _userDataPath = null;

            var (executableName, userDataParentFolder) = _chromeInfo[channel];

            // Try common Chrome installation paths
            var possiblePaths = new[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google", "Chrome", "Application", executableName),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google", "Chrome", "Application", executableName),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "Application", executableName),
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    _chromePath = path;
                    _userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), userDataParentFolder, "User Data");
                    break;
                }
            }
        }

        public bool Open(FavoriteItem favorite, bool inPrivate, bool newWindow)
        {
            return OpenInternal(favorite.Profile, favorite.Url!, inPrivate, newWindow);
        }

        public bool Open(FavoriteItem[] favorites, bool inPrivate, bool newWindow)
        {
            if (favorites.Length == 0)
            {
                throw new InvalidOperationException("Favorites cannot be empty.");
            }

            // If there is no need to open in a new window, starting multiple processes is preferred to avoid long command line arguments
            if (newWindow)
            {
                return Open(favorites[0].Profile, string.Join(" ", favorites.Select(f => f.Url!)), inPrivate, newWindow);
            }
            else
            {
                var result = true;

                foreach (var favorite in favorites)
                {
                    if (!Open(favorite, inPrivate, false))
                    {
                        result = false;
                    }
                }

                return result;
            }
        }

        private bool Open(ProfileInfo profileInfo, string urls, bool inPrivate, bool newWindow)
        {
            return OpenInternal(profileInfo, urls, inPrivate, newWindow);
        }

        private bool OpenInternal(ProfileInfo profileInfo, string urls, bool inPrivate, bool newWindow)
        {
            if (_chromePath == null)
            {
                return false;
            }

            var args = $"\"{urls}\"";

            if (inPrivate)
            {
                args += " --incognito";
            }

            if (newWindow)
            {
                args += " --new-window";
            }

            args += $" --profile-directory=\"{profileInfo.Directory}\"";

            try
            {
                using var process = new Process();
                process.StartInfo.FileName = _chromePath;
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
