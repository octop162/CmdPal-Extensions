﻿// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Models;
using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ChromeFavoritesExtension.Commands
{
    internal partial class OpenChromeCommand : InvokableCommand
    {
        private readonly ChromeManager _edgeManager;
        private readonly FavoriteItem[] _favorites;
        private readonly bool _inPrivate;
        private readonly bool _newWindow;

        public OpenChromeCommand(ChromeManager edgeManager, FavoriteItem favorite, bool inPrivate, bool newWindow)
        {
            _edgeManager = edgeManager;
            _favorites = [favorite];
            _inPrivate = inPrivate;
            _newWindow = newWindow;

            if (inPrivate)
            {
                Name = "Command_OpenPrivate".GetLocalized();
                Icon = new("\uE727");
            }
            else if (newWindow)
            {
                Name = "Command_OpenWindow".GetLocalized();
                Icon = new("\uE8A7");
            }
            else
            {
                Name = "Command_Open".GetLocalized();
                Icon = new("\uE737");
            }
        }

        public OpenChromeCommand(ChromeManager edgeManager, FavoriteItem[] favorites, bool inPrivate, bool newWindow)
        {
            _edgeManager = edgeManager;
            _favorites = favorites;
            _inPrivate = inPrivate;
            _newWindow = newWindow;

            if (inPrivate)
            {
                Name = string.Format(CultureInfo.CurrentCulture, "Command_OpenAllPrivate".GetLocalized(), favorites.Length);
                Icon = new("\uE727");
            }
            else if (newWindow)
            {
                Name = string.Format(CultureInfo.CurrentCulture, "Command_OpenAllWindow".GetLocalized(), favorites.Length);
                Icon = new("\uE8A7");
            }
            else
            {
                Name = string.Format(CultureInfo.CurrentCulture, "Command_OpenAll".GetLocalized(), favorites.Length);
                Icon = new("\uE737");
            }
        }

        public override CommandResult Invoke()
        {
            if (_favorites.Length == 1)
            {
                _edgeManager.Open(_favorites[0], _inPrivate, _newWindow);
            }
            else
            {
                _edgeManager.Open(_favorites, _inPrivate, _newWindow);
            }

            return CommandResult.Dismiss();
        }
    }
}
