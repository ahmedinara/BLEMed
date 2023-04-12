using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Entities
{
   public class UserAccessLog
    {
        public int Id { get; set; }
        public string EncriptedCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UserId { get; set; }
    }
}
