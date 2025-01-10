using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure.Repositories
{
    public class MyListSerieRepository : Repository<MyListSerie>, IMyListSerieRepository
    {
        public MyListSerieRepository(MyListDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<MyListSerie> FindOrCreateAsync(MyListSerie e)
        {
            var f = await _dbContext.MyListSeries
               .SingleOrDefaultAsync(x => x.SerieListId == e.SerieListId &&
               x.SerieUserId == e.SerieUserId);

            if (f == null)
            {
                f = Create(e);
            }
            return f;
        }

        public override Task<MyListSerie> UpsertAsync(MyListSerie e)
        {
            MyListSerie f;

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
