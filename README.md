# ScarletTemplate

A modular .NET template for creating V Rising mods with optional dependencies.

## 🚀 Quick Start

### Installation

Install the template from NuGet:

```bash
dotnet new install ScarletTemplate
```

### Usage

Create a new V Rising mod project:

```bash
dotnet new scarlettemplate -n MyMod
```

## 📦 What's Included

### Dependencies
- **ScarletCore**: Core framework for V Rising mods with integrated command system
- **BepInEx**: Mod framework for Unity games
- **VRising.Unhollowed.Client**: V Rising game bindings


## 📁 Generated Project Structure

```
MyMod/
├── MyMod.csproj           # Project file with conditional dependencies
├── MyMod.sln              # Solution file
├── Plugin.cs              # Main plugin class with conditional code
├── MyPluginInfo.cs        # Auto-generated plugin metadata
├── nuget.config           # NuGet sources (BepInEx, Samboy Feed)
└── LICENSE                # MIT License
```

## 💡 Examples

### Basic Plugin
```csharp
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("markvaaz.ScarletCore")]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded!");
    }
}
```

### Commands with ScarletCore
```csharp
using ScarletCore.Commanding;

[CommandGroup("mymod", Language.English, adminOnly: false)]
[CommandGroupAlias("meuplugin", Language.Portuguese)]
public static class MyCommands
{
    [Command("hello", Language.English, description: "Say hello")]
    [CommandAlias("ola", Language.Portuguese, description: "Dizer olá")]
    public static void HelloCommand(CommandContext ctx)
    {
        ctx.ReplySuccess($"Hello {ctx.User.CharacterName}!");
    }

    [Command("info", Language.English, description: "Get player info")]
    [CommandAlias("informacao", Language.Portuguese, description: "Obter informações")]
    public static void InfoCommand(CommandContext ctx, PlayerData player)
    {
        ctx.ReplySuccess($"Player: {player.Name}, Level: {player.Level}");
    }
}
```

## 🔧 Development

### Prerequisites
- .NET 6.0 SDK or later
- V Rising Dedicated Server (for testing)

### Building
```bash
dotnet build
```

### Installation Path
The template automatically copies built mods to:
```
C:\Program Files (x86)\Steam\steamapps\common\VRisingDedicatedServer\BepInEx\plugins
```

## 📋 Requirements

- .NET 6.0+
- V Rising Dedicated Server
- BepInEx 6.0+

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 🔗 Links

- [V Rising](https://store.steampowered.com/app/1604030/V_Rising/)
- [BepInEx](https://github.com/BepInEx/BepInEx)
- [ScarletCore](https://github.com/markvaaz/ScarletCore)
