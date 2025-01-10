using MyList.UWP.ViewModels;
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

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyList.UWP.Views.Perfil
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class PerfilPage : Page
    {
        UserViewModel UserViewModel { get; set; }
        FilmeListViewModel FilmeListViewModel { get; set; }
        SerieListViewModel SerieListViewModel { get; set; }

        public PerfilPage()
        {
            this.InitializeComponent();
            UserViewModel = App.UserViewModel;
            FilmeListViewModel = new FilmeListViewModel();
            SerieListViewModel = new SerieListViewModel();

            FilmeListViewModel.CountNumListFilme();
            SerieListViewModel.CountNumListSerie();

        }

        private void verInformacoes_Click(object sender, RoutedEventArgs e)
        {
            userPassword.Visibility = Visibility.Visible;
            NomeUtilizador.Visibility = Visibility.Visible;
            NomePassword.Visibility = Visibility.Visible;
            userName.Visibility = Visibility.Visible;
            userPassword.Visibility = Visibility.Visible;
            userPassword.Text = "******************";
            numListaFilmes.Visibility = Visibility.Visible;
            numListaSeries.Visibility = Visibility.Visible;
            verPass.Visibility = Visibility.Visible;

        }

        private void esconderPassword_Click(object sender, RoutedEventArgs e)
        {
            userPassword.Visibility = Visibility.Collapsed;
            NomeUtilizador.Visibility = Visibility.Collapsed;
            NomePassword.Visibility = Visibility.Collapsed;
            userName.Visibility = Visibility.Collapsed;
            userPassword.Visibility = Visibility.Collapsed;
            numListaFilmes.Visibility = Visibility.Collapsed;
            numListaSeries.Visibility = Visibility.Collapsed;
            verPass.IsChecked = false;
            verPass.Visibility = Visibility.Collapsed;

        }


        private void verPass_Unchecked(object sender, RoutedEventArgs e)
        {
            userPassword.Text = "******************";
        }

        private void verPass_Checked(object sender, RoutedEventArgs e)
        {
            userPassword.Text = UserViewModel.LoggedUser.Password;
        }
    }
}
