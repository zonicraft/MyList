using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure.Repositories
{
    public class MyListFilmeRepository : Repository<MyListFilme>, IMyListFilmeRepository
    {
        public MyListFilmeRepository(MyListDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<MyListFilme> FindOrCreateAsync(MyListFilme e)
        {
            var f = await _dbContext.MyListFilmes
                .SingleOrDefaultAsync(x => x.FilmeListId == e.FilmeListId &&
                x.FilmeUserId == e.FilmeUserId);

            if (f == null)
            {
                f = Create(e);
            }
            return f;
        }

        public override Task<MyListFilme> UpsertAsync(MyListFilme e)
        {
            MyListFilme f;

            if (e.Id == 0)
            {
                f = Create(e);
            }
            else
            {
                f = Update(e);
            }
            return Task.FromResult(f);
        }
    }
}
