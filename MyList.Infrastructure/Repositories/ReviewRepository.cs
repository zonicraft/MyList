using Microsoft.EntityFrameworkCore;
using MyList.Domain.Models;
using MyList.Domain.Repositories;
using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure.Repositories
{
    public class ReviewRepository: Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MyListDbContext dbContext) : base(dbContext)
        {

        }


        
        public Task<Review> FindByFilmeIDAsync(int filmeid)
        {
            return _dbContext.Reviews.SingleOrDefaultAsync(p => p.FilmeID == filmeid);
        }

       public override async Task<Review> FindOrCreateAsync(Review r)
        {
            var review = await FindByFilmeIDAsync(r.FilmeID);

            if (review == null)
            {
                review = Create(r);
                await _dbContext.SaveChangesAsync();
            }

            return review;

        }

        public override async Task<Review> UpsertAsync(Review r)
        {
            Review review = null;
            Review existing = await FindByFilmeIDAsync(r.FilmeID);

            if (existing == null)
            {
                if (r.Id == 0)
                {
                    review = Create(r);
                }
                else
                {
                    review = Update(r);
                }
            }
            else if (existing.Id == r.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                review = Update(r);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return review;

        }

        #region Métodos Extra

        public override async Task<List<Review>> FindAllAsync()
        {
            return await _dbContext.Reviews
                .Include(p => p.Filme)
                .OrderBy(p => p.Titulo)
                .ToListAsync();

        }



        public Task<List<Review>> FindAllByFilmeIDStartWithAsync(int filmeId, string title)
        {
            return _dbContext.Reviews
                .Where(p => p.Titulo.StartsWith(title) && (filmeId == 0 || p.FilmeID == filmeId))
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }

        #endregion
    }
}
