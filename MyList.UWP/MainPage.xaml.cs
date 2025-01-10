using MyList.UWP.ViewModels;
using MyList.UWP.Views;
using MyList.UWP.Views.Categorias;
using MyList.UWP.Views.FilmeList;
using MyList.UWP.Views.Filmes;
using MyList.UWP.Views.MyLists;
using MyList.UWP.Views.Perfil;
using MyList.UWP.Views.Procurar;
using MyList.UWP.Views.Série;
using MyList.UWP.Views.SerieList;
using MyList.UWP.Views.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace MyList.UWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public UserViewModel UserViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            UserViewModel = App.UserViewModel;
        }

        public Frame AppFrame => frame;

        private void NvMain_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Tag)
                {
                    case "categorias":
                        AppFrame.Navigate(typeof(ManageCategoriasPage));
                        break;

                    case "Nvisearch_page":
                        AppFrame.Navigate(typeof(ProcurarMainPage));
                        break;

                    case "Nvi_profile":
                        AppFrame.Navigate(typeof(PerfilPage));
                        break;

                    case "Nvi_list_filmes":
                        AppFrame.Navigate(typeof(FilmeListPage));
                        break;

                    case "Nvi_list_series":
                        AppFrame.Navigate(typeof(SerieListPage));
                        break;

                    


                }
            }
        }

        private void NvMain_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (AppFrame.CanGoBack)
            {
                AppFrame.GoBack();
            }
        }


       //Dropdown Button (Admin) e as suas diversas opções

        private void NviCategories_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.Navigate(typeof(ManageCategoriasPage));
        }

        private void NviFilmes_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.Navigate(typeof(ManageFilmesPage));
        }

        private void NviSeries_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.Navigate(typeof(ManageSeriesPage));
        }

      
        private async void Nvi_login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            var res = await dlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.UserViewModel.IsLogged)
                {
                    AppFrame.Navigate(typeof(LandingPage));
                }
            }

        }

        private void Nvi_logout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserViewModel.DoLogout();
            AppFrame.BackStack.Clear();
            AppFrame.Content = null;
        }

        private async void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RegisterDialog rlg = new RegisterDialog();
            var res = await rlg.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                if (App.UserViewModel.IsLogged)
                {
                    AppFrame.Navigate(typeof(LandingPage));
                }
            }
        }

        private void NviUsers_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.Navigate(typeof(ListaUsers));
        }
    }
}
