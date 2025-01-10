using MyList.Domain.Models;
using MyList.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList.UWP.ViewModels
{
    public class CategoriaViewModel : BindableBase
    {
        public ObservableCollection<Categoria> Categorias { get; set; }

        private Categoria _categoria;
        public Categoria Categoria
        {
            get { return _categoria; }
            set
            {
                _categoria = value;
                CategoriaNome = _categoria?.Nome;

            }
        }

        private string _categoriaNome;
        public string CategoriaNome
        {
            get { return _categoriaNome; }
            set
            {
                Set(ref _categoriaNome, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));

            }
        }



        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(CategoriaNome);//verdadeiro caso o nome da categoria seja nulo ou espaço em branco, falso caso contrario
                return res;
            }
        }

        public bool Invalid
        {
            get
            {
                return !Valid;
            }
        }

        public CategoriaViewModel()
        {
            Categoria = new Categoria();
            Categorias = new ObservableCollection<Categoria>();
        }

        internal async Task<Categoria> UpsertAsync()
        {
            Categoria res = null;
            using (var uow = new UnitOfWork())
            {

                Categoria.Nome = CategoriaNome;
                res = await uow.CategoriaRepository.UpsertAsync(Categoria);
                await uow.SaveAsync();//salvar na base de dados
            }
            return res;
        }

        internal async void DeleteAsync(Categoria e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.CategoriaRepository.Delete(e);
                Categorias.Remove(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.CategoriaRepository.FindAllAsync();

                Categorias.Clear();
                foreach (var item in list)
                {
                    Categorias.Add(item);
                }

            }
        }
    }
}
