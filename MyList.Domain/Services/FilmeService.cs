using MyList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.Services
{
    public class FilmeService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public FilmeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Filme> AddFilmeAsync(string CategoriaNome, Filme Filme, string Ano, string Realizador, string Produtora, string Pg, string Sinopse)
        {
            // Gerir as categorias
            var categoria = new Categoria(CategoriaNome);
            var categoriaUpdated = await _unitOfWork.CategoriaRepository.FindOrCreateAsync(categoria);
            await _unitOfWork.SaveAsync();


            // Gerir os  filmes
            Filme.Categoria = categoriaUpdated;
            var filmeUpdated = await _unitOfWork.FilmeRepository.UpsertAsync(Filme);
            await _unitOfWork.SaveAsync();


            return filmeUpdated;
        }

        public async Task<bool> AddFilmeToFilmeListAsync(int filmeListId, int userId, string categoryName, string productName)
        {
            bool res = false;

            var category = new Categoria(categoryName);
            var categoryUpdated = await _unitOfWork.CategoriaRepository
                .FindOrCreateAsync(category);
            await _unitOfWork.SaveAsync();

            var product = new Filme(productName, categoryUpdated.Id);
            var productUpdated = await _unitOfWork.FilmeRepository
                .FindOrCreateAsync(product);
            await _unitOfWork.SaveAsync();

            var user = await _unitOfWork.UserRepository
                .FindByIdAsync(userId);

            var pu = user.AddFilme(productUpdated);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            var shoppingList = await _unitOfWork.FilmeListRepository
                .FindByIdAsync(filmeListId);
            var existingProduct = shoppingList.AddFilme(pu.Id);

            _unitOfWork.FilmeListRepository.Update(shoppingList);
            await _unitOfWork.SaveAsync();

            res = true;

            return res;
        }
    }
}
