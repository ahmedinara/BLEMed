using System;
using System.Collections.Generic;

#nullable disable

namespace BSMediator.Core.BioTimeEntites
{
    public partial class AuthUser
    {
        

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmpPin { get; set; }
        public string Email { get; set; }
        public string TelePhone { get; set; }
        public int? AuthTimeDept { get; set; }
        public int? LoginId { get; set; }
        public int? LoginType { get; set; }
        public int? LoginCount { get; set; }
        public bool IsStaff { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperuser { get; set; }
        public bool IsPublic { get; set; }
        public bool CanManageAllDept { get; set; }
        public int? DelFlag { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LoginIp { get; set; }
        public string SessionKey { get; set; }
        public DateTime? UpdateTime { get; set; }

     
    }
}
