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
    public class SerieListViewModel : BindableBase
    {
        public SerieList SerieList { get; set; }

        public Color SerieListColor { get; set; }

        public ObservableCollection<SerieList> SerieLists { get; set; }

        public SerieListViewModel()
        {
            SerieList = new SerieList();
            SerieLists = new ObservableCollection<SerieList>();
            SerieListColor = Color.FromArgb(0, 255, 255, 255);
        }

        public int CountSerieList = 0;

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var userId = App.UserViewModel.LoggedUser.Id;
                var list = await uow.SerieListRepository.FindAllByUserIdAsync(userId);

                SerieLists.Clear();
                foreach (var item in list)
                {
                    SerieLists.Add(item);
                }
            }
        }

       

        //Numero de listas de series do User
        public async void CountNumListSerie()
        {
            using (var uow = new UnitOfWork())
            {
                var userId = App.UserViewModel.LoggedUser.Id;
                var list = await uow.SerieListRepository.FindAllByUserIdAsync(userId);


                foreach (var item in list)
                {
                    CountSerieList = CountSerieList + 1;

                }
            }
        }

        internal async Task<SerieList> UpsertAsync()
        {
            SerieList newSerieList = null;
            using (var uow = new UnitOfWork())
            {

                SerieList.UserId = App.UserViewModel.LoggedUser.Id;
                if (SerieList.Id == 0)
                {
                    SerieList.Data_criacao = DateTime.Now;
                }
                SerieList.Color = SerieListColor.ToString();

                newSerieList = await uow.SerieListRepository.UpsertAsync(SerieList);
                await uow.SaveAsync();
            }
            return newSerieList;
        }

        internal async Task<bool> DeleteShoppingList(Domain.Models.SerieList model)
        {
            bool res = false;

            using (var uow = new UnitOfWork())
            {
                // TODO: Verify associated Products

                uow.SerieListRepository.Delete(model);
                SerieLists.Remove(model);
                await uow.SaveAsync();
                res = true;
            }

            return res;
        }

        internal void RefreshItem(Domain.Models.SerieList model)
        {
            if (model != null)
            {
                SerieList = model;
                SerieListColor = ColorConverter.GetColorFromCode(SerieList.Color);
            }
        }
    }
}
