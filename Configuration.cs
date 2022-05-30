using System.Collections.Generic;
using RFItemPermissions.Models;
using Rocket.API;

namespace RFItemPermissions
{
    public class Configuration : IRocketPluginConfiguration
    {
        public bool Enabled;
        public HashSet<ItemPermission> ItemPermissions;

        public void LoadDefaults()
        {
            Enabled = true;
            ItemPermissions = new HashSet<ItemPermission>
            {
                new()
                {
                    Id = 488, Permissions = new List<string>
                    {
                        "broadcast", "i", "v"
                    }
                },
                new()
                {
                    Id = 363, Permissions = new List<string>
                    {
                        "broadcast", "i", "v"
                    }
                },
            };
        }
    }
}