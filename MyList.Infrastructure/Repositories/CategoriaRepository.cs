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
    public class CategoriaRepository : Repository<Categoria> , ICategoriaRepository
    {
        public CategoriaRepository(MyListDbContext dbContext) : base(dbContext)
        {

        }



        public Task<Categoria> FindByNameAsync(string nome)
        {
            return _dbContext.Categorias.SingleOrDefaultAsync(c => c.Nome == nome);
        }

        public override async Task<Categoria> FindOrCreateAsync(Categoria e)
        {
            var category = await FindByNameAsync(e.Nome);

            if (category == null)
            {
                category = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return category;

        }

        public override async Task<Categoria> UpsertAsync(Categoria e)
        {
            Categoria category = null;
            Categoria existing = await FindByNameAsync(e.Nome);

            if (existing == null)
            {
                if (e.Id == 0)
                {
                    category = Create(e);
                }
                else
                {
                    category = Update(e);
                }
            }
            else if (existing.Id == e.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                category = Update(e);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return category;

        }

        #region Métodos Extra

        public override async Task<List<Categoria>> FindAllAsync()
        {
            return await _dbContext.Categorias
                .Include(c => c.Filmes)
                .OrderBy(c => c.Nome)
                .ToListAsync();

        }

        public Task<List<Categoria>> FindAllByNameStartWithAsync(string name)
        {
            return _dbContext.Categorias
                .Where(c => c.Nome.StartsWith(name))
                .OrderBy(c => c.Nome)
                .ToListAsync();

        }

        #endregion
    }
}

