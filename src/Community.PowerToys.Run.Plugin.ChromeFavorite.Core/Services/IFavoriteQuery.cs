// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Models;

namespace Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services
{
    public interface IFavoriteQuery
    {
        IEnumerable<FavoriteItem> GetAll();

        IEnumerable<FavoriteItem> Search(string query);
    }
}
