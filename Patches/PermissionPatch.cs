using System.Collections.Generic;
using HarmonyLib;
using Rocket.API;
using Rocket.Core.Permissions;

namespace RFItemPermissions.Patches
{
    [HarmonyPatch]
    internal static class PermissionPatch
    {
        internal delegate void PreCheckPermission(IRocketPlayer player, List<string> permissions, ref bool skipCheck, ref bool shouldAllow);

        internal static event PreCheckPermission OnPreCheckPermission;
        
        [HarmonyPatch(typeof(RocketPermissionsManager), "HasPermission")]
        [HarmonyPrefix]
        internal static bool PreHasPermission(ref bool __result, IRocketPlayer player, List<string> permissions)
        {
            var skipCheck = false;
            OnPreCheckPermission?.Invoke(player, permissions, ref skipCheck, ref __result);
            return !skipCheck;
        }
    }
}