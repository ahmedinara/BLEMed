using BSMediator.Core.BioTimeEntites;
using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreations;
using BSMediator.Core.Shared;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Data.Repositories.User
{
    public class UserDbRepository : BSMediator.Core.Repositories.Users.IUserRepository
    {
        private readonly BioTimeContext _context;

        public UserDbRepository(BioTimeContext bioTimeContext)
        {
            _context = bioTimeContext;
        }

        #region Add

        #endregion

        #region Update

        #endregion

        #region Get
        public async Task<EmplyeeList> GetUsers(string emp_code, string first_name, string last_name, string department, string app_status, string token, string pageNo)
        {
            try
            {
                var result = _context.PersonnelEmployees.AsQueryable();
                if (!string.IsNullOrEmpty(emp_code))
                    result = result.Where(r => r.CardNo.Contains(emp_code));
                if (!string.IsNullOrEmpty(first_name))
                    result = result.Where(r => r.FirstName.Contains(first_name));
                if (!string.IsNullOrEmpty(last_name))
                    result = result.Where(r => r.LastName.Contains(last_name));

                PagedList<PersonnelEmployee> PagedResult = await PagedList<PersonnelEmployee>.CreateAsync(result, int.Parse(pageNo), 10);
                EmplyeeList emplyeeList = new EmplyeeList
                {
                    count = PagedResult.TotalCount,
                    data = PagedResult.Select(p => new Datum
                    {
                        id = p.Id,
                        emp_code = p.EmpCode,
                        first_name = p.FirstName,
                        last_name = p.LastName,
                        nickname = p.Nickname,
                        card_no = p.CardNo,
                        mobile = p.Mobile,
                        email = p.Email
                    }).ToList()
                };

                return emplyeeList;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }
        
        }
        public async Task<Datum> GetUserById(int personId, string token)
        {
            var result = await _context.PersonnelEmployees.FindAsync(personId);
            return new Datum
            {
                id = result.Id,
                emp_code = result.EmpCode,
                first_name = result.FirstName,
                last_name = result.LastName,
                nickname = result.Nickname,
                card_no = result.CardNo,
                mobile = result.Mobile,
                email = result.Email,
            };
        }
        #endregion

        #region Update
        #endregion

        #region Commit

        #endregion
    }
}
