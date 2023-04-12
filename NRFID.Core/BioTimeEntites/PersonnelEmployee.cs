using System;
using System.Collections.Generic;

#nullable disable

namespace BSMediator.Core.BioTimeEntites
{
    public partial class PersonnelEmployee
    {
        

        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ChangeTime { get; set; }
        public string ChangeUser { get; set; }
        public short Status { get; set; }
        public string EmpCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Passport { get; set; }
        public string DriverLicenseAutomobile { get; set; }
        public string DriverLicenseMotorcycle { get; set; }
        public string Photo { get; set; }
        public string SelfPassword { get; set; }
        public string DevicePassword { get; set; }
        public int? DevPrivilege { get; set; }
        #region cardNo
        public string CardNo { get; set; }
        #endregion
        public string AccGroup { get; set; }
        public string AccTimezone { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string OfficeTel { get; set; }
        public string ContactTel { get; set; }
        public string Mobile { get; set; }
        public string National { get; set; }
        public string Religion { get; set; }
        public string Title { get; set; }
        public string EnrollSn { get; set; }
        public string Ssn { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? HireDate { get; set; }
        public int? VerifyMode { get; set; }
        public string City { get; set; }
        public bool IsAdmin { get; set; }
        public int? EmpType { get; set; }
        public bool EnableAtt { get; set; }
        public bool EnablePayroll { get; set; }
        public bool EnableOvertime { get; set; }
        public bool EnableHoliday { get; set; }
        public bool Deleted { get; set; }
        public int? Reserved { get; set; }
        public int? DelTag { get; set; }
        public short? AppStatus { get; set; }
        public short? AppRole { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
        public short? EnableWhatsapp { get; set; }
        public short? WhatsappException { get; set; }
        public short? WhatsappPunch { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public bool IsValid { get; set; }
        public DateTime? ValidEndDate { get; set; }
        public DateTime? ValidStartDate { get; set; }
        public short? TemperatureAlert { get; set; }
        public short? TemperatureAlertScope { get; set; }
        public string ChangePwdTag { get; set; }
        public string LoginIp { get; set; }
        public string SessionKey { get; set; }

     
    }
}
