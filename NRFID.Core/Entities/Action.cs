using BSMediator.Core.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Entities
{
    public class Action
    {
        public int Id { get; set; }
        [Display(Name = "ActionName", ResourceType = typeof(Messages))]

        public string ActionName { get; set; }

        public ICollection<UserAction> UserActions { get; set; }

    }
}
