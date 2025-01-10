using MyList.Domain.Models;
using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.Repositories
{
    public interface ISerieRepository : IRepository<Serie>
    {
        Task<Serie> FindByNameAsync(string nome);
        Task<List<Serie>> FindAllByNameStartWithAsync(string name);

        Task<List<MyListSerie>> FindAllByUserAndSerieListIdAsync(
           int userId, int shoppingListId);
        Task<List<Serie>> FindAllByCategoryStartWithAsync(int categoryId, string text);
    }
}
