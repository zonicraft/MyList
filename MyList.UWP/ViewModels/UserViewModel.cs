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
    public class UserViewModel : BindableBase
    {
        public UserViewModel()
        {
            User = new User();
            Users = new ObservableCollection<User>();
        }
        public ObservableCollection<User> Users { get; set; }


        public User User { get; set; }

        private User _loggedUser { get; set; }
        public User LoggedUser
        {

            get { return _loggedUser; }
            set
            {
                _loggedUser = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLogged));
                OnPropertyChanged(nameof(IsNotLogged));
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsLogged
        {
            get => LoggedUser != null;
        }

        public bool IsNotLogged
        {
            get => !IsLogged;
        }

        public bool IsAdmin => LoggedUser != null && LoggedUser.IsAdmin;

        private bool _showError;

        public bool ShowError
        {
            get { return _showError; }
            set
            {
                _showError = value;
                OnPropertyChanged();
            }
        }

        internal async Task<bool> DoLoginAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var user = await uow.UserRepository
                    .FindByUsernameAndPassword(User.Username, User.Password);

                LoggedUser = user;
                ShowError = user == null;

            }

            return !ShowError;
        }

        internal async Task<bool> RegistarAsync()
        {
            bool res = true;

            using (var uow = new UnitOfWork())
            {
                var user = await uow.UserRepository.FindByUsername(User.Username);
                if (user == null)
                {
                    user = uow.UserRepository.Create(User);
                    await uow.SaveAsync();
                    LoggedUser = user;
                    res = false;
                }
            }

            ShowError = res;
            return !ShowError;
        }

        public void DoLogout()
        {
            User = new User();
            LoggedUser = null;
        }

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { Set(ref _loading, value); }
        }


        public async Task LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                //Loading = true;

                var list2 = await uow.UserRepository.FindAllAsync();

                Users.Clear();
                foreach (var l in list2)
                {
                    Users.Add(l);
                }

                Loading = false;
            }
        }

        internal async void DeleteUserAsync(User s)
        {
            using (var uow = new UnitOfWork())
            {
                uow.UserRepository.Delete(s);
                Users.Remove(s);
                await uow.SaveAsync();
            }
        }


    }
}
