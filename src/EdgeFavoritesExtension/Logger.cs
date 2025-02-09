// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using Community.PowerToys.Run.Plugin.EdgeFavorite.Core;

namespace EdgeFavoritesExtension
{
    internal class Logger : ILogger
    {
        // TODO implement basic logging
        public void LogDebug(string message, Type fullClassName, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
        }

        public void LogError(string message, Type fullClassName, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
        }

        public void LogError(Exception exception, string message, Type fullClassName, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
        }

        public void LogInformation(string message, Type fullClassName, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
        }

        public void LogWarning(string message, Type fullClassName, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
        }
    }
}
