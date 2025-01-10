using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;

namespace MyList.Infrastructure.Repositories
{
    public class SerieListRepository : Repository<SerieList>, ISerieListRepository
    {
        public SerieListRepository(MyListDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<SerieList> FindByIdAsync(int id)
        {
            return await _dbContext.SerieLists
                .Include(sl => sl.MyListSeries)
                .SingleOrDefaultAsync(sl => sl.Id == id);
        }

        public Task<List<SerieList>> FindAllByUserIdAsync(int userId)
        {
            return _dbContext.SerieLists
              .Include(sl => sl.MyListSeries)
              .Where(x => x.UserId == userId)
              .ToListAsync();
        }

        public Task<SerieList> FindByNameAndUserIdAsync(string name, int userId)
        {
            return _dbContext.SerieLists
               .SingleOrDefaultAsync(x => x.Nome == name && x.UserId == userId);
        }

        public async override Task<SerieList> FindOrCreateAsync(SerieList e)
        {
            var s = await FindByNameAndUserIdAsync(e.Nome, e.UserId);

            if (s == null)
            {
                s = Create(e);
                await _dbContext.SaveChangesAsync();
            }
            return s;
        }

        public async override Task<SerieList> UpsertAsync(SerieList e)
        {
            SerieList f = null;
            SerieList existing = await FindByNameAndUserIdAsync(e.Nome, e.UserId);

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