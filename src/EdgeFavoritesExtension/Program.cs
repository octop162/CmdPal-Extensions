// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using Microsoft.CommandPalette.Extensions;
using Serilog;

namespace EdgeFavoritesExtension
{
    public class Program
    {
        [MTAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;

            if (args.Length > 0 && args[0] == "-RegisterProcessAsComServer")
            {
                using ExtensionServer server = new();
                var extensionDisposedEvent = new ManualResetEvent(false);
                var extensionInstance = new EdgeFavoritesExtension(extensionDisposedEvent);
                server.RegisterExtension(() => extensionInstance);
                extensionDisposedEvent.WaitOne();
            }
        }

        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            Log.Fatal(exception, "Unhandled exception");
            Log.CloseAndFlush();
        }
    }
}
