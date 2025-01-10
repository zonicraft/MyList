using MyList.Domain.Models;
using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<Review> FindByFilmeIDAsync(int filmeid);
    }
}
