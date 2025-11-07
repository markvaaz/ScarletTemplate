using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using ScarletCore.Data;
#if use_vcf
using VampireCommandFramework;
#endif
#if use_rcon_soft
using ScarletRCON.Shared;
#endif
#if use_rcon_hard
using ScarletRCON.CommandSystem;
#endif

namespace ScarletTemplate;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("markvaaz.ScarletCore")]
#if use_vcf
[BepInDependency("gg.deca.VampireCommandFramework")]
#endif
#if use_rcon_soft
[BepInDependency("markvaaz.ScarletRCON")] 
#endif
#if use_rcon_hard
[BepInDependency("markvaaz.ScarletRCON")]
#endif
public class Plugin : BasePlugin
{
  static Harmony _harmony;
  public static Harmony Harmony => _harmony;
  public static Plugin Instance { get; private set; }
  public static ManualLogSource LogInstance { get; private set; }
  public static Settings Settings { get; private set; }
  public static Database Database { get; private set; }

  public override void Load()
  {
    Instance = this;
    LogInstance = Log;

    Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");

    _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

    Settings = new Settings(MyPluginInfo.PLUGIN_GUID, Instance);
    Database = new Database(MyPluginInfo.PLUGIN_GUID);
    GameSystems.OnInitialize(Initialize);
#if use_vcf
    CommandRegistry.RegisterAll();
#endif
#if use_rcon_soft
    RconCommandRegistrar.RegisterAll();
#endif
#if use_rcon_hard
    CommandHandler.RegisterAll();
#endif
  }
  
  private void Initialize()
  {
   
  }

  public override bool Unload()
  {
    _harmony?.UnpatchSelf();
#if use_vcf
    CommandRegistry.UnregisterAssembly();
#endif
#if use_rcon_soft
    RconCommandRegistrar.UnregisterAssembly();
#endif
#if use_rcon_hard
    CommandHandler.UnregisterAssembly();
#endif
    ActionScheduler.UnregisterAssembly(Assembly.GetExecutingAssembly());
    EventManager.UnregisterAssembly(Assembly.GetExecutingAssembly());
    return true;
  }

#if use_vcf
/*
  [CommandGroup("groupname")]
  public class CommandGroup
  {
    [Command("commandname", "Description of the command")]
    public static void CommandName(ChatCommandContext context)
    {
      // Command implementation
      context.Reply("Command executed successfully!");
    }
  }
*/
#endif

#if use_rcon_soft || use_rcon_hard
/*
  [RconCommandCategory("categoryname")]
  public class RconCommandCategory
  {
    [RconCommand("commandname", "Description of the command")]
    public static string CommandName()
    {
      // Command implementation
      return "Command executed successfully!"
    }
  }
*/
#endif
}
