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
    public class SerieViewModel : BindableBase
    {
        public SerieViewModel()
        {
            Serie = new Serie();
            Series = new ObservableCollection<Serie>();
            SerieService = new SerieService(new UnitOfWork());  //implementar FilmeService
            MyListSeries = new ObservableCollection<MyListSerie>();
            TitleText = "Séries";

            Loading = true;

        }


        public ObservableCollection<Serie> Series { get; set; }

        public SerieService SerieService { get; set; }//implementar FilmeService

        public Domain.Models.SerieList SerieList { get; set; }

        public ObservableCollection<MyListSerie> MyListSeries { get; set; }

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

        private string _serieNome;

        public string SerieNome
        {
            get { return _serieNome; }
            set
            {
                Set(ref _serieNome, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _serieData_inicio;

        public string SerieData_inicio
        {
            get { return _serieData_inicio; }
            set
            {
                Set(ref _serieData_inicio, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        private string _serieRealizador;

        public string SerieRealizador
        {
            get { return _serieRealizador; }
            set
            {
                Set(ref _serieRealizador, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }



        private string _serieProdutora;

        public string SerieProdutora
        {
            get { return _serieProdutora; }
            set
            {
                Set(ref _serieProdutora, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }





        private string _seriePg;

        public string SeriePg
        {
            get { return _seriePg; }
            set
            {
                Set(ref _seriePg, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }


        private string _serieNumero_temporadas;

        public string SerieNumero_temporadas
        {
            get { return _serieNumero_temporadas; }
            set
            {
                Set(ref _serieNumero_temporadas, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }




        private string _serieSinopse;

        public string SerieSinopse
        {
            get { return _serieSinopse; }
            set
            {
                Set(ref _serieSinopse, value);
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
                    && !string.IsNullOrWhiteSpace(SerieNome);
                    
                    
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

        private Serie _serie;


        public Serie Serie
        {
            get { return _serie; }
            set
            {
                _serie = value;
                CategoriaNome = _serie.Categoria?.Nome;
                SerieData_inicio = Serie?.Data_inicio;
                SerieNome = Serie?.Titulo;
                SerieRealizador = Serie?.Realizador;
                SerieNumero_temporadas = Serie?.Num_temporadas;
                SerieProdutora = Serie?.Produtora;
                SeriePg = Serie?.Pg_rating;
                SerieSinopse = Serie?.Sinopse;
                Thumb = _serie?.Cartaz;

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


        internal async Task<Serie> AddProductAsync()
        {
            Serie.Titulo = SerieNome;
            Serie.Cartaz = Thumb;
            Serie.Data_inicio = SerieData_inicio;
            Serie.Realizador = SerieRealizador;
            Serie.Num_temporadas = SerieNumero_temporadas;
            Serie.Produtora = SerieProdutora;
            Serie.Pg_rating = SeriePg;
            Serie.Sinopse = SerieSinopse;


            return await SerieService.AddSerieAsync(CategoriaNome, Serie, SerieData_inicio, SerieRealizador, SerieNumero_temporadas, SerieProdutora, SeriePg, SerieSinopse);
        }


        internal async void DeleteProductAsync(Serie s)
        {
            using (var uow = new UnitOfWork())
            {
                uow.SerieRepository.Delete(s);
                Series.Remove(s);
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

        //novo metodo para encontrar series
        internal async Task<ObservableCollection<Serie>> LoadSeriesByNameStartWithAsync(string text)
        {
            ObservableCollection<Serie> res;
            using (var uow = new UnitOfWork())
            {
                var list2 = await uow.SerieRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Serie>(list2);
            }

            return res;
        }



        public async Task LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                //Loading = true;

                var list2 = await uow.SerieRepository.FindAllAsync();

                Series.Clear();
                foreach (var l in list2)
                {
                    Series.Add(l);
                }

                Loading = false;
            }
        }

        public async void LoadAllBySerieListAsync()
        {
            if (SerieList.Id != 0)
            {
                using (var uow = new UnitOfWork())
                {
                    var userId = App.UserViewModel.LoggedUser.Id;
                    var list2 = await uow.SerieRepository
                        .FindAllByUserAndSerieListIdAsync(userId, SerieList.Id);

                    MyListSeries.Clear();
                    foreach (var item in list2)
                    {
                        MyListSeries.Add(item);
                    }

                }

                TitleText = $"Séries da lista {SerieList.Nome}";
            }
        }


        private string _countSerieList;

        public string CountSerieList
        {
            get { return _countSerieList; }
            set
            {
                Set(ref _countSerieList, value);
            }
        }

        public async void countNumSerieLists()
        {
            if (SerieList.Id != 0)
            {
                using (var uow = new UnitOfWork())
                {
                    var userId = App.UserViewModel.LoggedUser.Id;
                    var list = await uow.SerieRepository
                        .FindAllByUserAndSerieListIdAsync(userId, SerieList.Id);

                    MyListSeries.Clear();
                    foreach (var item in MyListSeries)
                    {
                        _countSerieList += 1;
                    }

                }

            }
        }


        internal async void DeleteAsync(MyListSerie p)
        {
            using (var uow = new UnitOfWork())
            {
                uow.MyListSerieRepository.Delete(p);
                MyListSeries.Remove(p);
                await uow.SaveAsync();
            }
        }

        internal async Task AddSerieToSerieListAsync()
        {
            await SerieService.AddSerieToSerieListAsync(SerieList.Id, App.UserViewModel.LoggedUser.Id, CategoriaNome, SerieNome);
        }

        internal async Task<ObservableCollection<Serie>> LoadProductsByCategoryStartWithAsync(string text)
        {
            ObservableCollection<Serie> res = null;

            using (var uow = new UnitOfWork())
            {
                var list = new List<Serie>();
                int categoryId = 0;
                var cat = await uow.CategoriaRepository.FindByNameAsync(CategoriaNome);

                categoryId = cat?.Id ?? -1; //se o resultado for nulo o categoryId é igual a '-1' 

                list = await uow.SerieRepository.FindAllByCategoryStartWithAsync(categoryId, text);

                res = new ObservableCollection<Serie>(list);

            }

            return res;
        }




    }
}
