using System;
using System.Collections.Generic;
using System.Linq;
using RFItemPermissions.Models;
using Rocket.API;
using SDG.Unturned;
using Steamworks;

namespace RFItemPermissions.EventListeners
{
    internal static class PlayerEvent
    {
        internal static void OnPreCheckPermission(IRocketPlayer player, List<string> permissions, ref bool skipCheck,
            ref bool shouldAllow)
        {
            skipCheck = false;
            if (player is ConsolePlayer)
                return;

            if (!ulong.TryParse(player.Id, out var steamId) || steamId == 0)
                return;

            var nPlayer = PlayerTool.getPlayer(new CSteamID(steamId));
            if (nPlayer == null)
                return;

            // player is online
            foreach (var permission in permissions)
            {
                var isBreak = false;
                foreach (var itemPermission in Plugin.Conf.ItemPermissions.Where(itemPermission =>
                             itemPermission.Permissions.Contains(permission)))
                {
                    if (nPlayer.inventory.has(itemPermission.Id) == null)
                        continue;

                    // player has ItemPermission id and permissions has ItemPermission permission
                    skipCheck = true;
                    shouldAllow = true;
                    isBreak = true;
                    break;
                }
                if (isBreak)
                    break;
            }
        }
    }
}