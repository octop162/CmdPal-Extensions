// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EdgeFavoritesExtension.Pages
{
    internal sealed partial class TreePage : DynamicListPage
    {
        private readonly EdgeManager _edgeManager;
        private readonly FavoriteQuery _favoriteQuery;
        private readonly SettingsManager _settingsManager;

        public TreePage(EdgeManager edgeManager, FavoriteQuery favoriteQuery, SettingsManager settingsManager)
        {
            _edgeManager = edgeManager;
            _favoriteQuery = favoriteQuery;
            _settingsManager = settingsManager;

            Name = "Name".GetLocalized();
            Icon = new("\uE728");
        }

        public override void UpdateSearchText(string oldSearch, string newSearch) => RaiseItemsChanged(0);

        public override IListItem[] GetItems()
        {
            if (_edgeManager.ChannelDetected)
            {
                return _favoriteQuery
                    .Search(SearchText)
                    .OrderBy(f => f.Type)
                    .ThenBy(f => f.Name)
                    .Where(f => !f.IsEmptySpecialFolder)
                    .Select(f => new FavoriteListItem(f, _edgeManager, _settingsManager))
                    .ToArray();
            }
            else
            {
                return [];
            }
        }
    }
}
