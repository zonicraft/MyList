using MyList.Domain.Models;
using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.Repositories
{
    public interface IFilmeListRepository : IRepository<FilmeList>
    {
        Task<List<FilmeList>> FindAllByUserIdAsync(int userId);

        Task<FilmeList> FindByNameAndUserIdAsync(string name, int userId);
    }
}
