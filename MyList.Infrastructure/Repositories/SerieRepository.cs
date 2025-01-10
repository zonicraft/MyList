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
    public class SerieRepository : Repository<Serie>, ISerieRepository
    {

        public SerieRepository (MyListDbContext dbContext) : base(dbContext)//feito
        {

        }


        public Task<Serie> FindByNameAsync(string nome) //feito
        {
            return _dbContext.Series.SingleOrDefaultAsync(p => p.Titulo == nome);
        }

        public override async Task<Serie> FindOrCreateAsync(Serie e) //Feito
        {
            var serie = await FindByNameAsync(e.Titulo);

            if (serie == null)
            {
                serie = Create(e);
                await _dbContext.SaveChangesAsync();
            }

            return serie;
        }


        public override async Task<Serie> UpsertAsync(Serie e)//feito
        {
            Serie serie = null;
            Serie existing = await FindByNameAsync(e.Titulo);

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


        #region Métodos Extra

        public override async  Task<List<Serie>> FindAllAsync()//feito
        {
            return await _dbContext.Series
                .Include(p => p.Categoria)
                .OrderBy(p => p.Titulo)
                .ToListAsync();

        }



        public Task<List<Serie>> FindAllByCategoryStartWithAsync(int categoriaId, string name)//feito
        {
            return _dbContext.Series
                .Where(p => p.Titulo.StartsWith(name) && (categoriaId == 0 || p.CategoriaID == categoriaId))
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }

 
        #endregion




        public Task<List<Serie>> FindAllByNameStartWithAsync(string name)//Feito
        {
            return _dbContext.Series
              .Where(c => c.Titulo.StartsWith(name))
              .OrderBy(c => c.Titulo)
              .ToListAsync();
        }


        public Task<List<MyListSerie>> FindAllByUserAndSerieListIdAsync(int userId, int serieListId)
        {
            return _dbContext.MyListSeries
               .Include(slp => slp.SerieUser.Serie)
               .Where(slp => slp.SerieListId == serieListId)
               .ToListAsync();
        }

        
    }
}
