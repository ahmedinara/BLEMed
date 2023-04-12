using BSMediator.Core.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Entities
{
    public class UserAction
    {
        public int Id { get; set; }
        [Display(Name = "IsAuthrized", ResourceType = typeof(Messages))]

        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int ActionId { get; set; }
        public Action Action { get; set; }

    }
}
