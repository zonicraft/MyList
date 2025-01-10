using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure.Repositories
{
    public class FilmeUserRepository : Repository<FilmeUser>, IFilmeUserRepository
    {
        public FilmeUserRepository(MyListDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<FilmeUser> FindOrCreateAsync(FilmeUser e)
        {
            var f = await _dbContext.FilmeUsers.
                SingleOrDefaultAsync(x => x.FilmeId == e.FilmeId && x.UserId == e.UserId);

            if (f == null)
            {
                f = Create(e);
                await _dbContext.SaveChangesAsync();
            }
            return f;
        }

        public override async Task<FilmeUser> UpsertAsync(FilmeUser e)
        {
            FilmeUser filme = null;
            FilmeUser existing = await _dbContext.FilmeUsers.
                SingleOrDefaultAsync(x => x.FilmeId == e.FilmeId && x.UserId == e.UserId);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    filme = Create(e);
                }
                else
                {
                    filme = Update(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                filme = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return filme;
        }
    }
}
