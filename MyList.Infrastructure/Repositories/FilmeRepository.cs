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
    public class FilmeRepository: Repository<Filme>, IFilmeRepository
    {
        public FilmeRepository(MyListDbContext dbContext) : base(dbContext)
        {

        }



        public Task<Filme> FindByNameAsync(string titulo)
        {
            return _dbContext.Filmes.SingleOrDefaultAsync(p => p.Titulo == titulo);
        }

        public override async Task<Filme> FindOrCreateAsync(Filme f)
        {
            var product = await FindByNameAsync(f.Titulo);

            if (product == null)
            {
                product = Create(f);
                await _dbContext.SaveChangesAsync();
            }

            return product;

        }

        public override async Task<Filme> UpsertAsync(Filme f)
        {
            Filme filme = null;
            Filme existing = await FindByNameAsync(f.Titulo);

            if (existing == null)
            {
                if (f.Id == 0)
                {
                    filme = Create(f);
                }
                else
                {
                    filme = Update(f);
                }
            }
            else if (existing.Id == f.Id)
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
                filme = Update(f);
            }
            else
            {
                _dbContext.Entry(existing).State = EntityState.Detached;
            }

            await _dbContext.SaveChangesAsync();

            return filme;

        }

        #region Métodos Extra

        public override async Task<List<Filme>> FindAllAsync()
        {
            return await _dbContext.Filmes
                .Include(p => p.Categoria)
                .OrderBy(p => p.Titulo)
                .ToListAsync();

        }



        public Task<List<Filme>> FindAllByCategoryStartWithAsync(int categoriaId, string name)
        {
            return _dbContext.Filmes
                .Where(p => p.Titulo.StartsWith(name) && (categoriaId == 0 || p.CategoriaID == categoriaId))
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }

        #endregion

        public Task<List<Filme>> FindAllByNameStartWithAsync(string name)
        {
            return _dbContext.Filmes
                .Where(c => c.Titulo.StartsWith(name))
                .OrderBy(c => c.Titulo)
                .ToListAsync();

        }

        public Task<List<MyListFilme>> FindAllByUserAndFilmeListIdAsync(int userId, int filmeListId)
        {
            return _dbContext.MyListFilmes
               .Include(slp => slp.FilmeUser.Filme)
               .Where(slp => slp.FilmeListId == filmeListId)
               .ToListAsync();
        }
    }
}

