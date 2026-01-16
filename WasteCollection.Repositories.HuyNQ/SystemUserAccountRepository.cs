using Microsoft.EntityFrameworkCore;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Repositories.HuyNQ.Base;
using WasteCollection.Repositories.HuyNQ.DBContext;

namespace WasteCollection.Repositories.HuyNQ;

public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
{
    public SystemUserAccountRepository() { }

    public SystemUserAccountRepository(WasteCollectionDbContext context) => _context = context;

    public async Task<SystemUserAccount?> GetUserAsync(string userName, string password)
    {
        return await _context.SystemUserAccounts
            .FirstOrDefaultAsync(u =>
                u.Email == userName &&
                u.Password == password);

        //return await _context.SystemUserAccounts
        //    .FirstOrDefaultAsync(u =>
        //        u.UserName == userName &&
        //        u.Password == password);

        //return await _context.SystemUserAccounts
        //    .FirstOrDefaultAsync(u =>
        //        u.Phone == userName &&
        //        u.Password == password);

        //return await _context.SystemUserAccounts
        //    .FirstOrDefaultAsync(u =>
        //        u.EmployeeCode == userName &&
        //        u.Password == password);
    }
}
