using Microsoft.EntityFrameworkCore;
using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Infrastructure.Repositories
{
    public abstract class Repository<T>: IRepository<T> where T : Entity
    {
        protected readonly MyListDbContext _dbContext;

        public Repository(MyListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T e)
        {
            T entity = _dbContext.Set<T>().Add(e).Entity;
            return entity;
        }

        public T Delete(T e)

        {
            T entity = _dbContext.Set<T>().Remove(e).Entity;
            return entity;
        }

        public virtual async Task<List<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public T Update(T e)
        {
            _dbContext.Entry(e).State = EntityState.Modified;
            T entity = _dbContext.Set<T>().Update(e).Entity;
            return entity;
        }


        public abstract Task<T> FindOrCreateAsync(T e);



        public abstract Task<T> UpsertAsync(T e);

    }
}

