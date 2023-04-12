using System;
using BSMediator.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BSMediator.Core.BioTimeEntites
{
    public partial class BioTimeContext : DbContext
    {
        public BioTimeContext()
        {
        }

        public BioTimeContext(DbContextOptions<BioTimeContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=" + Config.DbServerName + ";Initial Catalog=" + Config.DataBaseName + ";User ID=" + Config.DbUserName + ";Password=" + Config.DbPassword + "");
            }
        }


        public virtual DbSet<AuthUser> AuthUsers { get; set; }
     
        public virtual DbSet<PersonnelEmployee> PersonnelEmployees { get; set; }
      

    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.ToTable("auth_user");

                entity.HasIndex(e => e.Username, "UQ__auth_use__F3DBC5727161B19E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthTimeDept).HasColumnName("auth_time_dept");

                entity.Property(e => e.CanManageAllDept).HasColumnName("can_manage_all_dept");

                entity.Property(e => e.DateJoined)
                    .HasColumnType("datetime")
                    .HasColumnName("date_joined");

                entity.Property(e => e.DelFlag).HasColumnName("del_flag");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(254)
                    .HasColumnName("email");

                entity.Property(e => e.EmpPin)
                    .HasMaxLength(30)
                    .HasColumnName("emp_pin");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsPublic).HasColumnName("is_public");

                entity.Property(e => e.IsStaff).HasColumnName("is_staff");

                entity.Property(e => e.IsSuperuser).HasColumnName("is_superuser");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("last_name");

                entity.Property(e => e.LoginCount).HasColumnName("login_count");

                entity.Property(e => e.LoginId).HasColumnName("login_id");

                entity.Property(e => e.LoginIp)
                    .HasMaxLength(32)
                    .HasColumnName("login_ip");

                entity.Property(e => e.LoginType).HasColumnName("login_type");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.SessionKey)
                    .HasMaxLength(32)
                    .HasColumnName("session_key");

                entity.Property(e => e.TelePhone)
                    .HasMaxLength(30)
                    .HasColumnName("tele_phone");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<PersonnelEmployee>(entity =>
            {
                entity.ToTable("personnel_employee");

                entity.HasIndex(e => e.EmpCode, "UQ__personne__B1056ABCF89C5978")
                    .IsUnique();

                entity.HasIndex(e => e.DepartmentId, "personnel_employee_department_id_068bbd08");

                entity.HasIndex(e => e.PositionId, "personnel_employee_position_id_c9321343");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccGroup)
                    .HasMaxLength(5)
                    .HasColumnName("acc_group");

                entity.Property(e => e.AccTimezone)
                    .HasMaxLength(20)
                    .HasColumnName("acc_timezone");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.AppRole).HasColumnName("app_role");

                entity.Property(e => e.AppStatus).HasColumnName("app_status");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.CardNo)
                    .HasMaxLength(20)
                    .HasColumnName("card_no");

                entity.Property(e => e.ChangePwdTag)
                    .HasMaxLength(32)
                    .HasColumnName("change_pwd_tag");

                entity.Property(e => e.ChangeTime)
                    .HasColumnType("datetime")
                    .HasColumnName("change_time");

                entity.Property(e => e.ChangeUser)
                    .HasMaxLength(150)
                    .HasColumnName("change_user");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .HasColumnName("city");

                entity.Property(e => e.ContactTel)
                    .HasMaxLength(20)
                    .HasColumnName("contact_tel");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(150)
                    .HasColumnName("create_user");

                entity.Property(e => e.DelTag).HasColumnName("del_tag");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DevPrivilege).HasColumnName("dev_privilege");

                entity.Property(e => e.DevicePassword)
                    .HasMaxLength(20)
                    .HasColumnName("device_password");

                entity.Property(e => e.DriverLicenseAutomobile)
                    .HasMaxLength(30)
                    .HasColumnName("driver_license_automobile");

                entity.Property(e => e.DriverLicenseMotorcycle)
                    .HasMaxLength(30)
                    .HasColumnName("driver_license_motorcycle");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.EmpCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("emp_code");

                entity.Property(e => e.EmpType).HasColumnName("emp_type");

                entity.Property(e => e.EnableAtt).HasColumnName("enable_att");

                entity.Property(e => e.EnableHoliday).HasColumnName("enable_holiday");

                entity.Property(e => e.EnableOvertime).HasColumnName("enable_overtime");

                entity.Property(e => e.EnablePayroll).HasColumnName("enable_payroll");

                entity.Property(e => e.EnableWhatsapp).HasColumnName("enable_whatsapp");

                entity.Property(e => e.EnrollSn)
                    .HasMaxLength(20)
                    .HasColumnName("enroll_sn");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .HasColumnName("gender");

                entity.Property(e => e.HireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("hire_date");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.IsValid).HasColumnName("is_valid");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.LoginIp)
                    .HasMaxLength(32)
                    .HasColumnName("login_ip");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .HasColumnName("mobile");

                entity.Property(e => e.National)
                    .HasMaxLength(50)
                    .HasColumnName("national");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(50)
                    .HasColumnName("nickname");

                entity.Property(e => e.OfficeTel)
                    .HasMaxLength(20)
                    .HasColumnName("office_tel");

                entity.Property(e => e.Passport)
                    .HasMaxLength(30)
                    .HasColumnName("passport");

                entity.Property(e => e.Photo)
                    .HasMaxLength(200)
                    .HasColumnName("photo");

                entity.Property(e => e.PositionId).HasColumnName("position_id");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(10)
                    .HasColumnName("postcode");

                entity.Property(e => e.Religion)
                    .HasMaxLength(20)
                    .HasColumnName("religion");

                entity.Property(e => e.Reserved).HasColumnName("reserved");

                entity.Property(e => e.SelfPassword)
                    .HasMaxLength(128)
                    .HasColumnName("self_password");

                entity.Property(e => e.SessionKey)
                    .HasMaxLength(32)
                    .HasColumnName("session_key");

                entity.Property(e => e.Ssn)
                    .HasMaxLength(20)
                    .HasColumnName("ssn");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemperatureAlert).HasColumnName("temperature_alert");

                entity.Property(e => e.TemperatureAlertScope).HasColumnName("temperature_alert_scope");

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");

                entity.Property(e => e.ValidEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("valid_end_date");

                entity.Property(e => e.ValidStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("valid_start_date");

                entity.Property(e => e.VerifyMode).HasColumnName("verify_mode");

                entity.Property(e => e.WhatsappException).HasColumnName("whatsapp_exception");

                entity.Property(e => e.WhatsappPunch).HasColumnName("whatsapp_punch");

              
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
