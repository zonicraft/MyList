using MyList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList.Domain.Services
{
    public class SerieService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public SerieService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Serie> AddSerieAsync(string CategoriaNome, Serie Serie, string Data_inicio, string Realizador,string Numero_temporadas, string Produtora, string Pg, string Sinopse)
        {
            var categoria = new Categoria(CategoriaNome);
            var categoriaUpdated = await _unitOfWork.CategoriaRepository.FindOrCreateAsync(categoria);
            await _unitOfWork.SaveAsync();

            Serie.Categoria = categoriaUpdated;
            var serieUpdated = await _unitOfWork.SerieRepository.UpsertAsync(Serie);
            await _unitOfWork.SaveAsync();

            return serieUpdated;
        }

        public async Task<bool> AddSerieToSerieListAsync(int serieListId, int userId, string categoryName, string productName)
        {
            bool res = false;

            var category = new Categoria(categoryName);
            var categoryUpdated = await _unitOfWork.CategoriaRepository
                .FindOrCreateAsync(category);
            await _unitOfWork.SaveAsync();

            var product = new Serie(productName, categoryUpdated.Id);
            var productUpdated = await _unitOfWork.SerieRepository
                .FindOrCreateAsync(product);
            await _unitOfWork.SaveAsync();

            var user = await _unitOfWork.UserRepository
                .FindByIdAsync(userId);

            var pu = user.AddSerie(productUpdated);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            var shoppingList = await _unitOfWork.SerieListRepository
                .FindByIdAsync(serieListId);
            var existingProduct = shoppingList.AddSerie(pu.Id);

            _unitOfWork.SerieListRepository.Update(shoppingList);
            await _unitOfWork.SaveAsync();

            res = true;

            return res;
        }

    }
}
