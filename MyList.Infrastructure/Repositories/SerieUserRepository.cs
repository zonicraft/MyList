using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;

namespace MyList.Infrastructure.Repositories
{
    public class SerieUserRepository : Repository<SerieUser>, ISerieUserRepository
    {
        public SerieUserRepository(MyListDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<SerieUser> FindOrCreateAsync(SerieUser e)
        {
            var f = await _dbContext.SerieUsers.
               SingleOrDefaultAsync(x => x.SerieId == e.SerieId && x.UserId == e.UserId);

            if (f == null)
            {
                f = Create(e);
                await _dbContext.SaveChangesAsync();
            }
            return f;
        }

        public override async Task<SerieUser> UpsertAsync(SerieUser e)
        {
            SerieUser serie = null;
            SerieUser existing = await _dbContext.SerieUsers.
                SingleOrDefaultAsync(x => x.SerieId == e.SerieId && x.UserId == e.UserId);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    serie = Create(e);
                }
                else
                {
                    serie = Update(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                serie = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return serie;
        }
    }
}
