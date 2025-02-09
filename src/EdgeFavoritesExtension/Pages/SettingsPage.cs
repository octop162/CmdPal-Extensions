// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.CmdPal.Extensions;
using Microsoft.CmdPal.Extensions.Helpers;

namespace EdgeFavoritesExtension.Pages
{
    internal partial class SettingsPage : FormPage
    {
        private readonly SettingsManager _settingsManager;
        private readonly Settings _settings;

        public override IForm[] Forms()
        {
            var s = _settings.ToForms();
            return s;
        }

        public SettingsPage(SettingsManager settingsManager)
        {
            Name = "Settings".GetLocalized();
            Icon = new("\uE713");

            _settingsManager = settingsManager;
            _settings = _settingsManager.Settings;

            _settings.SettingsChanged += SettingsChanged;
        }

        private void SettingsChanged(object sender, Settings args) => _settingsManager.SaveSettings();
    }
}
