using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class CommRoleParent
    {
        public List<ConnectionOption> ConnectionOptions { get; set; }
        public List<CommRoleModel> CommRoleModels { get; set; }

    }
    public class ConnectionOption
    {
        public string Name { get; set; }
    }
    public class CommRoleModel
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public List<CommRoleDetialsModel> commRoleDetials { get; set; }
    }
    public class CommRoleDetialsModel
    {

        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CommRoleId { get; set; }
        public string TypeValue { get; set; }
        public int TypeId { get; set; }

    }
}
