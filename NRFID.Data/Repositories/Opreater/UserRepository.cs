using BSMediator.Core.Repositories.Opreation;
using BSMediator.Core.Repositories.Opreations;
using Microsoft.EntityFrameworkCore;
using NFC.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BSMediator.Data.Repositories.Opreation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext appContext)
        {
            _context = appContext;
        }

        #region Add
        public async Task<BSMediator.Core.Entities.User> AddUser(BSMediator.Core.Entities.User user)
        {
            var entity = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<BSMediator.Core.Entities.User>> GetUsers(string fristName, string lastName, bool? isActive, string email, string phone)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(fristName))
                users.Where(x => x.FristName.Contains(fristName));
            if (!string.IsNullOrEmpty(lastName))
                users.Where(x => x.LastName.Contains(lastName));
            if (isActive != null)
                users.Where(x => x.IsActive == isActive);
            if (!string.IsNullOrEmpty(email))
                users.Where(x => x.Email.Contains(email));
            if (!string.IsNullOrEmpty(phone))
                users.Where(x => x.MobileNo.Contains(phone));

            return await users.ToListAsync();
        }
        public async Task<IEnumerable<BSMediator.Core.Entities.User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<BSMediator.Core.Entities.User> GetActiveUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email && e.IsActive == true);
        }
        public async Task<BSMediator.Core.Entities.User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<bool> CheckUserById(int id)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Id == id);
        }
        #endregion

        #region Update
        public async Task<BSMediator.Core.Entities.User> UpdateUser(BSMediator.Core.Entities.User user)
        {
            try
            {

                var entity = _context.Users.Update(user);
                await Commit();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Commit
        private async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion


    }
}
