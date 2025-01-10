using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure.Repositories
{
    public class FilmeListRepository : Repository<FilmeList>, IFilmeListRepository
    {
        public FilmeListRepository(MyListDbContext dbContext) : base(dbContext)
        {
        }
        public async override Task<FilmeList> FindByIdAsync(int id)
        {
            return await _dbContext.FilmeLists
                .Include(sl => sl.MyListFilmes)
                .SingleOrDefaultAsync(sl => sl.Id == id);
        }

        public Task<List<FilmeList>> FindAllByUserIdAsync(int userId)
        {
            return _dbContext.FilmeLists
               .Include(sl => sl.MyListFilmes)
               .Where(x => x.UserId == userId)
               .ToListAsync();
        }

        public Task<FilmeList> FindByNameAndUserIdAsync(string name, int userId)
        {
            return _dbContext.FilmeLists
                .SingleOrDefaultAsync(x => x.Nome == name && x.UserId == userId);
        }

        public async override Task<FilmeList> FindOrCreateAsync(FilmeList e)
        {
            var f = await FindByNameAndUserIdAsync(e.Nome, e.UserId);

            if (f == null)
            {
                f = Create(e);
                await _dbContext.SaveChangesAsync();
            }
            return f;
        }

        public async override Task<FilmeList> UpsertAsync(FilmeList e)
        {
            FilmeList f = null;
            FilmeList existing = await FindByNameAndUserIdAsync(e.Nome, e.UserId);

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
