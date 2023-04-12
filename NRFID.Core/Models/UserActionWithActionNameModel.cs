using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class UserActionWithActionNameModel
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public int ActionId { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
