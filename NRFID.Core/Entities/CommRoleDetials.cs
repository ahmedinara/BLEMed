using BSMediator.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Entities
{
    public class CommRoleDetials
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CommRoleId { get; set; }
        public CommRole commRole { get; set; }
        [ForeignKey("ApplicationSetting")]
        public int TypeId { get; set; }
        public ApplicationSetting ApplicationSetting { get; set; }
    }
}
