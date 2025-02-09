// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Services;
using EdgeFavoritesExtension.Pages;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EdgeFavoritesExtension
{
    internal partial class CommandsProvider : CommandProvider
    {
        private readonly SettingsManager _settingsManager;
        private readonly EdgeManager _edgeManager;
        private readonly FavoriteQuery _favoriteQuery;

        public CommandsProvider(SettingsManager settingsManager, EdgeManager edgeManager, FavoriteQuery favoriteQuery)
        {
            _settingsManager = settingsManager;
            _edgeManager = edgeManager;
            _favoriteQuery = favoriteQuery;

            _commands =
            [
                _settingsManager.SearchTree
                    ? new CommandItem(new TreePage(_edgeManager, _favoriteQuery, _settingsManager))
                    : new CommandItem(new SearchPage(_edgeManager, _favoriteQuery, _settingsManager))
            ];
        }

        private readonly ICommandItem[] _commands;

        public override ICommandItem[] TopLevelCommands()
        {
            return _commands;
        }
    }
}
