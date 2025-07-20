﻿// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ChromeFavoritesExtension.Pages
{
    internal sealed partial class TreePage : DynamicListPage
    {
        private readonly ChromeManager _edgeManager;
        private readonly FavoriteQuery _favoriteQuery;
        private readonly SettingsManager _settingsManager;
        private readonly ProfileManager _profileManger;

        public TreePage(ChromeManager edgeManager, FavoriteQuery favoriteQuery, SettingsManager settingsManager, ProfileManager profileManger)
        {
            _edgeManager = edgeManager;
            _favoriteQuery = favoriteQuery;
            _settingsManager = settingsManager;
            _profileManger = profileManger;

            Name = "Name".GetLocalized();
#if DEBUG
            Name += " (Dev)";
#endif
            Icon = Consts.Icon;
        }

        public override void UpdateSearchText(string oldSearch, string newSearch) => RaiseItemsChanged(0);

        public override IListItem[] GetItems()
        {
            if (!_edgeManager.ChannelDetected)
            {
                return [];
            }

            return _favoriteQuery
                .Search(SearchText)
                .OrderBy(f => f.Type)
                .ThenBy(f => f.Name)
                .Where(f => !f.IsEmptySpecialFolder)
                .Select(f => new FavoriteListItem(f, _edgeManager, _settingsManager, _profileManger))
                .ToArray();
        }
    }
}
