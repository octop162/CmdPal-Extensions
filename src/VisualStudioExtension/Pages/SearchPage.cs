// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Community.PowerToys.Run.Plugin.VisualStudio.Core.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace VisualStudioExtension.Pages
{
    internal sealed partial class SearchPage : DynamicListPage
    {
        private readonly VisualStudioService _visualStudioService;
        private readonly SettingsManager _settingsManager;

        public SearchPage(SettingsManager settingsManager, VisualStudioService visualStudioService)
        {
            _settingsManager = settingsManager;
            _visualStudioService = visualStudioService;

            Name = "Name".GetLocalized();

            var lightIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/VisualStudio.light.png");
            var darkIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/VisualStudio.dark.png");
            Icon = new(new(lightIcon), new(darkIcon));
        }

        public override void UpdateSearchText(string oldSearch, string newSearch) => RaiseItemsChanged(0);

        public override IListItem[] GetItems() => Search().OrderBy(r => r.Title).ToArray();

        private IEnumerable<CodeContainerListItem> Search()
        {
            var emptyQuery = string.IsNullOrWhiteSpace(SearchText);

            foreach (var r in _visualStudioService.GetResults(_settingsManager.ShowPrerelease))
            {
                var score = StringMatcher.FuzzySearch(SearchText, r.Name);

                if (emptyQuery || score.Score > 0)
                {
                    yield return new CodeContainerListItem(r, _settingsManager);
                }
            }
        }
    }
}
