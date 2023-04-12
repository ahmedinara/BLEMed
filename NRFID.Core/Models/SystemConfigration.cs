using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class SystemConfigration
    {
        public SystemConfigration()
        {

        }
        public SystemConfigration(string SystemType,string ConnectionType)
        {
            this.ConnectionType = ConnectionType;
            this.SystemType = SystemType;    
        }
        public string ConnectionType { get; set; }
        public string SystemType { get; set; }
    }
    public class SystemConfigrationWithDitales
    {
        public string ConnectionType { get; set; }
        public string SystemType { get; set; }
        public List<SystemConfigrationDitales> dt { get; set; }

    }
    public class SystemConfigrationDitales
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
