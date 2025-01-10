using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
        T Create(T e);

        T Update(T e);

        Task<T> UpsertAsync(T e);//insere e atualiza

        Task<T> FindOrCreateAsync(T e); //procura e caso n exista, cria

        T Delete(T e);

        Task<T> FindByIdAsync(int id); //procura através do id

        Task<List<T>> FindAllAsync(); //devolve todos os produtos da tabela


    }
}
