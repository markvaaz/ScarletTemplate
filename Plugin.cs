using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using ScarletCore.Commanding;
using ScarletCore.Data;
using ScarletCore.Events;
using ScarletCore.Localization;
using ScarletCore.Systems;

namespace ScarletTemplate;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("markvaaz.ScarletCore")]
public class Plugin : BasePlugin {
  static Harmony _harmony;
  public static Harmony Harmony => _harmony;
  public static Plugin Instance { get; private set; }
  public static ManualLogSource LogInstance { get; private set; }
  public static Settings Settings { get; private set; }
  public static Database Database { get; private set; }

  public override void Load() {
    Instance = this;
    LogInstance = Log;

    Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");

    _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    _harmony.PatchAll(Assembly.GetExecutingAssembly());

    Settings = new Settings(MyPluginInfo.PLUGIN_GUID, Instance);
    Database = new Database(MyPluginInfo.PLUGIN_GUID);
    GameSystems.OnInitialize(Initialize);
    CommandHandler.RegisterAll();
  }

  private void Initialize() {

  }

  public override bool Unload() {
    _harmony?.UnpatchSelf();
    ActionScheduler.UnregisterAssembly();
    EventManager.UnregisterAssembly();
    CommandHandler.UnregisterAssembly();
    return true;
  }

  // Example command group using the new ScarletCore command system
  [CommandGroup("example", Language.English, adminOnly: false)]
  [CommandGroupAlias("exemplo", Language.Portuguese)]
  public static class ExampleCommands {

    [Command("hello", Language.English, description: "Say hello")]
    [CommandAlias("ola", Language.Portuguese, description: "Dizer olá")]
    public static void HelloCommand(CommandContext ctx) {
      ctx.ReplySuccess($"Hello {ctx.Sender.Name}!");
    }

    [Command("info", Language.English, description: "Get player info")]
    [CommandAlias("informacao", Language.Portuguese, description: "Obter informações do jogador")]
    public static void InfoCommand(CommandContext ctx, PlayerData player) {
      ctx.ReplySuccess($"Player: {player.Name}, Level: {player.Level}");
    }
  }
}
