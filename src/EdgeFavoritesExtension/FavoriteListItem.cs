// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Linq;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Models;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core.Services;
using EdgeFavoritesExtension.Commands;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EdgeFavoritesExtension
{
    internal partial class FavoriteListItem : ListItem
    {
        public FavoriteListItem(FavoriteItem favorite, EdgeManager edgeManager, SettingsManager settingsManager)
            : base(new NoOpCommand())
        {
            if (favorite.Type == FavoriteType.Folder)
            {
                Command = new NoOpCommand();
                Title = favorite.Name;
                Subtitle = settingsManager.SearchMode == SearchMode.Tree
                    ? string.Format(CultureInfo.CurrentCulture, "FolderResult_Profile_Subtitle".GetLocalized(), favorite.Path, favorite.Profile.Name)
                    : string.Format(CultureInfo.CurrentCulture, "FolderResult_Subtitle".GetLocalized(), favorite.Path);
                Icon = new IconInfo("\uE8B7");
                TextToSuggest = $"{favorite.Path}/";
                MoreCommands = GetMoreCommands(favorite, edgeManager, settingsManager);
            }
            else if (favorite.Type == FavoriteType.Url)
            {
                Command = new OpenEdgeCommand(edgeManager, favorite, false, false);
                Title = favorite.Name;
                Subtitle = settingsManager.SearchMode == SearchMode.Tree
                    ? string.Format(CultureInfo.CurrentCulture, "FavoriteResult_Profile_Subtitle".GetLocalized(), favorite.Path, favorite.Profile.Name)
                    : string.Format(CultureInfo.CurrentCulture, "FavoriteResult_Subtitle".GetLocalized(), favorite.Path);
                Icon = new IconInfo("\uE734");
                TextToSuggest = favorite.Path;
                MoreCommands = GetMoreCommands(favorite, edgeManager, settingsManager);
            }
            else
            {
                throw new ArgumentException("Invalid favorite item", nameof(favorite));
            }
        }

        private static IContextItem[] GetMoreCommands(FavoriteItem favorite, EdgeManager edgeManager, SettingsManager settingsManager)
        {
            if (favorite.Type == FavoriteType.Folder)
            {
                var favorites = favorite.Children.Where(c => c.Type == FavoriteType.Url).ToArray();
                if (favorites.Length > 0)
                {
                    return
                    [
                        new CommandContextItem(new OpenEdgeCommand(edgeManager, favorites, false, true)),
                        new CommandContextItem(new OpenEdgeCommand(edgeManager, favorites, true, false)),
                    ];
                }
            }
            else if (favorite.Type == FavoriteType.Url)
            {
                return
                [
                    new CommandContextItem(new CopyTextCommand(favorite.Url!)),
                    new CommandContextItem(new OpenEdgeCommand(edgeManager, favorite, false, true)),
                    new CommandContextItem(new OpenEdgeCommand(edgeManager, favorite, true, false)),
                ];
            }

            return [];
        }
    }
}
