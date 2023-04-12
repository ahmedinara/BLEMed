using BSMediator.Core.ResourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Models
{
    public class EmplyeeList
    {
            public int count { get; set; }
            public string next { get; set; }
            public string previous { get; set; }
            public string msg { get; set; }
            public int code { get; set; }
            public List<Datum> data { get; set; }

    }
    public class Datum
    {
        [Display(Name = "EmployeeId", ResourceType = typeof(Messages))]
        public int id { get; set; }
        [Display(Name = "EmployeeCode", ResourceType = typeof(Messages))]
        public string emp_code { get; set; }
        [Display(Name = "FirstName", ResourceType = typeof(Messages))]
        public string first_name { get; set; }
        [Display(Name = "LastName", ResourceType = typeof(Messages))]
        public string last_name { get; set; }
        [Display(Name = "NickName", ResourceType = typeof(Messages))]
        public string nickname { get; set; }
        public string device_password { get; set; }
        [Display(Name = "CardNo", ResourceType = typeof(Messages))]
        public string card_no { get; set; }
        public Department department { get; set; }
        public string position { get; set; }
        public string hire_date { get; set; }
        public string gender { get; set; }
        public object birthday { get; set; }
        public int verify_mode { get; set; }
        public string emp_type { get; set; }
        public string contact_tel { get; set; }
        public string office_tel { get; set; }
        [Display(Name = "Mobile", ResourceType = typeof(Messages))]
        public string mobile { get; set; }
        public string national { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string postcode { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Messages))]
        public string email { get; set; }
        public string enroll_sn { get; set; }
        public string ssn { get; set; }
        public string religion { get; set; }
        public bool enable_att { get; set; }
        public bool enable_overtime { get; set; }
        public bool enable_holiday { get; set; }
        public int dev_privilege { get; set; }
        public string self_password { get; set; }
        public object[] flow_role { get; set; }
        public Area[] area { get; set; }
        public int app_status { get; set; }
        public int app_role { get; set; }
        public string update_time { get; set; }
        public string fingerprint { get; set; }
        public string face { get; set; }
        public string palm { get; set; }
        public string vl_face { get; set; }
    }

    public class Department
    {
        public int id { get; set; }
        public string dept_code { get; set; }
        [Display(Name = "department", ResourceType = typeof(Messages))]
        public string dept_name { get; set; }
    }

    public class Area
    {
        public int id { get; set; }
        public string area_code { get; set; }
        public string area_name { get; set; }
    }
}
