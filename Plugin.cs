using HarmonyLib;
using RFItemPermissions.EventListeners;
using RFItemPermissions.Patches;
using Rocket.Core.Plugins;
using Logger = Rocket.Core.Logging.Logger;

namespace RFItemPermissions
{
    public class Plugin : RocketPlugin<Configuration>
    {
        private static int Major = 1;
        private static int Minor = 0;
        private static int Patch = 0;
        
        public static Plugin Inst;
        public static Configuration Conf;

        private Harmony _harmony { get; set; }

        protected override void Load()
        {
            Inst = this;
            Conf = Configuration.Instance;
            if (Conf.Enabled)
            {
                _harmony = new Harmony("RFItemPermissions.Patches");
                _harmony.PatchAll();

                PermissionPatch.OnPreCheckPermission += PlayerEvent.OnPreCheckPermission;
            }
            else
                Logger.LogWarning($"[{Name}] Plugin: DISABLED");

            Logger.LogWarning($"[{Name}] Plugin loaded successfully!");
            Logger.LogWarning($"[{Name}] {Name} v{Major}.{Minor}.{Patch}");
            Logger.LogWarning($"[{Name}] Made with 'rice' by RiceField Plugins!");
        }
        protected override void Unload()
        {
            if (Conf.Enabled)
            {
                _harmony.UnpatchAll(_harmony.Id);
                
                PermissionPatch.OnPreCheckPermission -= PlayerEvent.OnPreCheckPermission;
            }
            
            Conf = null;
            Inst = null;

            Logger.LogWarning($"[{Name}] Plugin unloaded successfully!");
        }
        // public override TranslationList DefaultTranslations => new TranslationList
        // {
        //     {"GRANTED_PERMISSION", "{0} granted permission of '{1}' because they has {2} inside inventory."},
        // };
    }
}