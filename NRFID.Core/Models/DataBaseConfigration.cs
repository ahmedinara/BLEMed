using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class DataBaseConfigration
    {
        public DataBaseConfigration()
        {
                
        }
        public DataBaseConfigration(string ServerName,string DataBaseName,string UserName ,string Password)
        {
            this.ServerName = ServerName;
            this.DataBaseName = DataBaseName;
            this.UserName = UserName;
            this.Password = Password;
        }
        public string ServerName { get; set; }
        public string DataBaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
