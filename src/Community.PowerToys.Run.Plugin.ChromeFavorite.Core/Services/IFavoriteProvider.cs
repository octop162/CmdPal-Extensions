// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Models;

namespace Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services
{
    public interface IFavoriteProvider
    {
        FavoriteItem Root { get; }
    }
}
