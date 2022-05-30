using System.Collections.Generic;
using System.Xml.Serialization;

namespace RFItemPermissions.Models
{
    public class ItemPermission
    {
        [XmlAttribute]
        public ushort Id { get; set; }
        [XmlArrayItem("Permission")]
        public List<string> Permissions { get; set; }
        public ItemPermission()
        {
            
        }

        public override bool Equals(object obj)
        {
            if (obj is not ItemPermission other)
                return false;
            
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}