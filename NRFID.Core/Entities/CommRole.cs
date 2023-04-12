using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Entities
{
    public class CommRole
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public ICollection<CommRoleDetials> commRoleDetials { get; set; }


    }
}
