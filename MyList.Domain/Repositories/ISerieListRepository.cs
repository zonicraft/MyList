using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyList.Domain.Models;
using MyList.Domain.SeedWork;

namespace MyList.Domain.Repositories
{
    public interface ISerieListRepository : IRepository<SerieList>
    {
        Task<List<SerieList>> FindAllByUserIdAsync(int userId);

        Task<SerieList> FindByNameAndUserIdAsync(string name, int userId);
    }
}
