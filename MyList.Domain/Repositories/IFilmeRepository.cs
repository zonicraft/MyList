using MyList.Domain.Models;
using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.Repositories
{
    public interface IFilmeRepository : IRepository<Filme>
    {
        Task<Filme> FindByNameAsync(string nome);
        Task<List<Filme>> FindAllByNameStartWithAsync(string name);

        Task<List<MyListFilme>> FindAllByUserAndFilmeListIdAsync(
            int userId, int shoppingListId);
        Task<List<Filme>> FindAllByCategoryStartWithAsync(int categoryId, string text);
    }
}
