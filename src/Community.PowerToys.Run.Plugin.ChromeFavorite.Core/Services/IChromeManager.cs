// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Models;

namespace Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services
{
    public interface IChromeManager
    {
        string UserDataPath { get; }

        bool ChannelDetected { get; }

        void Initialize(Channel channel);

        bool Open(FavoriteItem favorite, bool inPrivate, bool newWindow);

        bool Open(FavoriteItem[] favorites, bool inPrivate, bool newWindow);
    }
}
