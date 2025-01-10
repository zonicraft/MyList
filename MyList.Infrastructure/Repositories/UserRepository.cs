using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyListDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<User> FindByIdAsync (int id)
        {
            return await _dbContext.Users
                .Include(x => x.FilmeUsers)
                .Include (p => p.SerieUsers)
                .SingleOrDefaultAsync(x => x.Id == id);
        }


        public Task<User> FindByUsername(string username)
        {
            return _dbContext.Users
                .SingleOrDefaultAsync(x => x.Username == username);
        }

        public Task<User> FindByUsernameAndPassword(string username, string password)
        {
            return _dbContext.Users
               .Include(x => x.FilmeUsers)
               .SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public override async Task<User> FindOrCreateAsync(User e)
        {
            var f = await FindByUsername(e.Username);

            if (f == null)
            {
                f = Create(e);
                await _dbContext.SaveChangesAsync();
            }
            return f;
        }

        public override async Task<User> UpsertAsync(User e)
        {
            User f = null;
            User existing = await FindByUsername(e.Username);


            if (existing == null)
            {
                if (e.Id == 0)
                {
                    f = Create(e);
                }
                else
                {
                    f = Update(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                f = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return f;
        }
    }
}
