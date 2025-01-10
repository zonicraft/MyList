using MyList.Domain.Models;
using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUsernameAndPassword(string username, string password);

        Task<User> FindByUsername(string username);
    }
}
