# Windows Command Palette Extensions

This monorepo is the home for my Windows Command Palette extensions.

| Extension | Description |
| --- | --- |
| Chrome Favorites | Search Microsoft Chrome favorites. Based on the existing [PowerToys Run  Chrome Favorite plugin](https://github.com/davidegiacometti/PowerToys-Run-ChromeFavorite). |
| Visual Studio | Search Visual Studio recents. Based on the existing [PowerToys Run Visual Studio plugin](https://github.com/davidegiacometti/PowerToys-Run-VisualStudio). |

## Installation

### Command Palette

You can install the extensions directly from Command Palette.

### WinGet

You can install the extensions via [WinGet](https://learn.microsoft.com/windows/package-manager/winget/) running the following commands from the command line / PowerShell:

```
winget install davidegiacometti.ChromeFavoritesForCmdPal
winget install davidegiacometti.VisualStudioForCmdPal
```

### Manual

You can install the extensions manually using the MSIX available in the [GitHub releases](https://github.com/davidegiacometti/CmdPal-Extensions/releases).

## Contributing

- **New Extensions:** I’m not accepting PRs for new extensions. If you have a new idea, feel free to create your own project!
- **Fixes & Improvements:** I do accept PRs for bug fixes and improvements to existing code. However, to avoid the risk of your PR being rejected, please ensure that you file an issue and receive feedback before starting any work.
