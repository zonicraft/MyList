using MyList.Domain;
using MyList.Domain.Repositories;
using MyList.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyListDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new MyListDbContext();
            _dbContext.Database.EnsureCreated();
        }

        public IFilmeRepository FilmeRepository => new FilmeRepository(_dbContext);

        public ICategoriaRepository CategoriaRepository => new CategoriaRepository(_dbContext);

        public IReviewRepository ReviewRepository => new ReviewRepository(_dbContext);

        public ISerieRepository SerieRepository => new SerieRepository(_dbContext);

        public IUserRepository UserRepository => new UserRepository(_dbContext);

        public IFilmeUserRepository FilmeUserRepository => new FilmeUserRepository(_dbContext);

        public IFilmeListRepository FilmeListRepository => new FilmeListRepository(_dbContext);

        public ISerieUserRepository SerieUserRepository => new SerieUserRepository(_dbContext);

        public ISerieListRepository SerieListRepository => new SerieListRepository(_dbContext);

        public IMyListFilmeRepository MyListFilmeRepository => new MyListFilmeRepository(_dbContext);

        public IMyListSerieRepository MyListSerieRepository => new MyListSerieRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
