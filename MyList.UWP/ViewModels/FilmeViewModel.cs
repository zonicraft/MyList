using MyList.Domain.Models;
using MyList.Domain.Services;
using MyList.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList.UWP.ViewModels
{
    public class FilmeViewModel :BindableBase
    {
        public FilmeViewModel()
        {
            Filme = new Filme();
            Filmes = new ObservableCollection<Filme>();
            FilmeService = new FilmeService(new UnitOfWork());  //implementar FilmeService
            MyListFilmes = new ObservableCollection<MyListFilme>();
            TitleText = "Filmes";

            Loading = true;

        }


        public ObservableCollection<Filme> Filmes { get; set; }

        public FilmeService FilmeService { get; set; }//implementar FilmeService

        public Domain.Models.FilmeList FilmeList { get; set; }

        public ObservableCollection<MyListFilme> MyListFilmes { get; set; }

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

        private string _filmeNome;

        public string FilmeNome
        {
            get { return _filmeNome; }
            set
            {
                Set(ref _filmeNome, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _filmeAno;

        public string FilmeAno
        {
            get { return _filmeAno; }
            set
            {
                Set(ref _filmeAno, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _filmeRealizador;

        public string FilmeRealizador
        {
            get { return _filmeRealizador; }
            set
            {
                Set(ref _filmeRealizador, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }



        private string _filmeProdutora;

        public string FilmeProdutora
        {
            get { return _filmeProdutora; }
            set
            {
                Set(ref _filmeProdutora, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }





        private string _filmePg;

        public string FilmePg
        {
            get { return _filmePg; }
            set
            {
                Set(ref _filmePg, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }




        private string _filmeSinopse;

        public string FilmeSinopse
        {
            get { return _filmeSinopse; }
            set
            {
                Set(ref _filmeSinopse, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }


        private byte[] _thumb;

        public byte[] Thumb
        {
            get { return _thumb; }
            set { Set(ref _thumb, value); }
        }

        public bool Valid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(CategoriaNome)
                    && !string.IsNullOrWhiteSpace(FilmeNome);
                 
            }
        }



        public bool Invalid
        {
            get
            {
                return !Valid;
            }
        }

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { Set(ref _loading, value); }
        }

        private Filme _filme;


        public Filme Filme
        {
            get { return _filme; }
            set
            {
                _filme = value;
                CategoriaNome = _filme.Categoria?.Nome;
                FilmeAno = Filme?.Ano;
                FilmeNome = Filme?.Titulo;
                FilmeRealizador = Filme?.Realizador;
                FilmeProdutora = Filme?.Produtora;
                FilmePg = Filme?.Pg_rating;
                FilmeSinopse = Filme?.Sinopse;
                Thumb = _filme?.Cartaz;

            }

        }

        private string _titleText;

        public string TitleText
        {
            get { return _titleText; }
            set
            {
                Set(ref _titleText, value);
            }
        }


        internal async Task<Filme> AddProductAsync()
        {
            Filme.Titulo = FilmeNome;
            Filme.Cartaz = Thumb;
            Filme.Ano = FilmeAno;
            Filme.Realizador = FilmeRealizador;
            Filme.Produtora = FilmeProdutora;
            Filme.Pg_rating = FilmePg;
            Filme.Sinopse = FilmeSinopse;


            return await FilmeService.AddFilmeAsync(CategoriaNome, Filme, FilmeAno, FilmeRealizador, FilmeProdutora, FilmePg, FilmeSinopse);
        }


        internal async void DeleteProductAsync(Filme f)
        {
            using (var uow = new UnitOfWork())
            {
                uow.FilmeRepository.Delete(f);
                Filmes.Remove(f);
                await uow.SaveAsync();
            }
        }



        internal async Task<ObservableCollection<Categoria>> LoadCategoriesByNameStartWithAsync(string text)
        {
            ObservableCollection<Categoria> res;
            using (var uow = new UnitOfWork())
            {
                var list = await uow.CategoriaRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Categoria>(list);
            }

            return res;
        }

        //novo metodo para encontrar filmes
        internal async Task<ObservableCollection<Filme>> LoadFilmesByNameStartWithAsync(string text)
        {
            ObservableCollection<Filme> res;
            using (var uow = new UnitOfWork())
            {
                var list2 = await uow.FilmeRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Filme>(list2);
            }

            return res;
        }



        public async Task LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                //Loading = true;

                var list = await uow.FilmeRepository.FindAllAsync();

                Filmes.Clear();
                foreach (var l in list)
                {
                    Filmes.Add(l);
                }

                Loading = false;
            }
        }

        public async void LoadAllByFilmeListAsync()
        {
            if (FilmeList.Id != 0)
            {
                using (var uow = new UnitOfWork())
                {
                    var userId = App.UserViewModel.LoggedUser.Id;
                    var list = await uow.FilmeRepository
                        .FindAllByUserAndFilmeListIdAsync(userId, FilmeList.Id);

                    MyListFilmes.Clear();
                    foreach (var item in list)
                    {
                        MyListFilmes.Add(item);
                    }

                }

                TitleText = $"Filmes da lista {FilmeList.Nome}";
            }
        }

        private string _countFilmeList;

        public string CountFilmeList
        {
            get { return _countFilmeList; }
            set
            {
                Set(ref _countFilmeList, value);
            }
        }

        public async void countNumFilmeLists()
        {
            

            if (FilmeList.Id != 0)
            {
                using (var uow = new UnitOfWork())
                {
                    var userId = App.UserViewModel.LoggedUser.Id;
                    var list = await uow.FilmeRepository
                        .FindAllByUserAndFilmeListIdAsync(userId, FilmeList.Id);

                    MyListFilmes.Clear();
                    foreach (var item in MyListFilmes)
                    {
                        _countFilmeList += 1;
                    }

                }
                
            }
        }






        internal async void DeleteAsync(MyListFilme p)
        {
            using (var uow = new UnitOfWork())
            {
                uow.MyListFilmeRepository.Delete(p);
                MyListFilmes.Remove(p);
                await uow.SaveAsync();
            }
        }

        internal async Task AddFilmeToFilmeListAsync()
        {
            await FilmeService.AddFilmeToFilmeListAsync(FilmeList.Id, App.UserViewModel.LoggedUser.Id, CategoriaNome, FilmeNome);
        }

        internal async Task<ObservableCollection<Filme>> LoadProductsByCategoryStartWithAsync(string text)
        {
            ObservableCollection<Filme> res = null;

            using (var uow = new UnitOfWork())
            {
                var list = new List<Filme>();
                int categoryId = 0;
                var cat = await uow.CategoriaRepository.FindByNameAsync(CategoriaNome);

                categoryId = cat?.Id ?? -1; //se o resultado for nulo o categoryId é igual a '-1' 

                list = await uow.FilmeRepository.FindAllByCategoryStartWithAsync(categoryId, text);

                res = new ObservableCollection<Filme>(list);

            }

            return res;
        }
    }
}
