using MyList.Domain.Models;
using MyList.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MyListConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("::MyList App::");

            using var uow = new UnitOfWork();

            //Create Categories
            await uow.CategoriaRepository.UpsertAsync(
                new Categoria("Ficçaõ Científica"));
            await uow.CategoriaRepository.UpsertAsync(
                new Categoria("Romance"));
            await uow.CategoriaRepository.UpsertAsync(
                new Categoria("Ação"));

            //Get all Categories
            var lstCategories = await uow.CategoriaRepository.FindAllAsync();

            //Print Categories
            foreach (var item in lstCategories)
            {
                Console.WriteLine(" - " + item);
            }
        }
    }
}