using MyList.Domain.Models;
using MyList.Infrastructure;
using MyList.UWP.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MyList.UWP.ViewModels
{
    public class FilmeListViewModel : BindableBase
    {
        public FilmeList FilmeList { get; set; }

        public Color FilmeListColor { get; set; }

        public ObservableCollection<FilmeList> FilmeLists { get; set; }

        public FilmeListViewModel()
        {
            FilmeList = new FilmeList();
            FilmeLists = new ObservableCollection<FilmeList>();
            FilmeListColor = Color.FromArgb(0, 255, 255, 255);
        }

        public int CountFilmeList = 0;

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var userId = App.UserViewModel.LoggedUser.Id;
                var list = await uow.FilmeListRepository.FindAllByUserIdAsync(userId);

                    FilmeLists.Clear();
                foreach (var item in list)
                {
                        FilmeLists.Add(item);
                    
                    }
            }
        }


        //Numero de listas de filmes do User
        public async void CountNumListFilme()
        {
            using (var uow = new UnitOfWork())
            {
                var userId = App.UserViewModel.LoggedUser.Id;
                var list = await uow.FilmeListRepository.FindAllByUserIdAsync(userId);

                
                foreach (var item in list)
                {
                    CountFilmeList = CountFilmeList + 1;

                }
            }
        }

        



        internal async Task<FilmeList> UpsertAsync()
    {
        FilmeList newFilmeList = null;
        using (var uow = new UnitOfWork())
        {

                FilmeList.UserId = App.UserViewModel.LoggedUser.Id;
            if (FilmeList.Id == 0)
            {
                    FilmeList.Data_criacao = DateTime.Now;
            }
                FilmeList.Color = FilmeListColor.ToString();

                newFilmeList = await uow.FilmeListRepository.UpsertAsync(FilmeList);
            await uow.SaveAsync();
                
        }
        return newFilmeList;
    }

    internal async Task<bool> DeleteShoppingList(Domain.Models.FilmeList model)
    {
        bool res = false;

        using (var uow = new UnitOfWork())
        {
            // TODO: Verify associated Products

            uow.FilmeListRepository.Delete(model);
            FilmeLists.Remove(model);
            await uow.SaveAsync();
            res = true;
        }

        return res;
    }

    internal void RefreshItem(Domain.Models.FilmeList model)
    {
        if (model != null)
        {
            FilmeList = model;
            FilmeListColor = ColorConverter.GetColorFromCode(FilmeList.Color);
        }
    }
}
}
