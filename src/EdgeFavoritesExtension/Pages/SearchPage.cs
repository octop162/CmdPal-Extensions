// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Models;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EdgeFavoritesExtension.Pages
{
    internal sealed partial class SearchPage : DynamicListPage
    {
        private readonly EdgeManager _edgeManager;
        private readonly FavoriteQuery _favoriteQuery;
        private readonly SettingsManager _settingsManager;

        public SearchPage(EdgeManager edgeManager, FavoriteQuery favoriteQuery, SettingsManager settingsManager)
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
            if (!_edgeManager.ChannelDetected)
            {
                return [];
            }

            if (_settingsManager.SearchMode == SearchMode.Tree)
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
                return Search().OrderBy(r => r.Title).ToArray();
            }
        }

        private IEnumerable<FavoriteListItem> Search()
        {
            var emptyQuery = string.IsNullOrWhiteSpace(SearchText);

            foreach (var f in _favoriteQuery.GetAll().Where(f => !f.IsEmptySpecialFolder))
            {
                if (f.Type == FavoriteType.Folder && _settingsManager.SearchMode == SearchMode.FlatFavorites)
                {
                    continue;
                }

                var score = StringMatcher.FuzzySearch(SearchText, f.Name);

                if (emptyQuery || score.Score > 0)
                {
                    yield return new FavoriteListItem(f, _edgeManager, _settingsManager);
                }
            }
        }
    }
}
