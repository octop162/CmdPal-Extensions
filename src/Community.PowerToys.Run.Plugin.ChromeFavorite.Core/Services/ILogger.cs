// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Community.PowerToys.Run.Plugin.ChromeFavorite.Core.Services
{
    public interface ILogger
    {
        void LogError(string message, Type type);

        void LogError(Exception exception, string message, Type type);

        void LogWarning(string message, Type type);
    }
}
