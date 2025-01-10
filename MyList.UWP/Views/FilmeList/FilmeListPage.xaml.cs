using MyList.UWP.ViewModels;
using MyList.UWP.Views.Filmes;
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

namespace MyList.UWP.Views.FilmeList
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class FilmeListPage : Page
    {
        public FilmeListViewModel FilmeListViewModel { get; set; }

        public FilmeListPage()
        {
            this.InitializeComponent();
            FilmeListViewModel = new FilmeListViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.UserViewModel.IsLogged)
            {
                FilmeListViewModel.LoadAllAsync();
            }
            else
            {
                Frame.Content = null;
            }

            base.OnNavigatedTo(e);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FilmeListFormPage));
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as Domain.Models.FilmeList;
            if (model != null)
            {
                Frame.Navigate(typeof(FilmesPage), model);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as Domain.Models.FilmeList;
            if (model != null)
            {
                Frame.Navigate(typeof(FilmeListFormPage), model);
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as Domain.Models.FilmeList;
            if (model != null)
            {
                ContentDialog deleteDialog = new ContentDialog
                {
                    Title = "Pretende eliminar?",
                    Content = "Caso elimine esta lista,não será possível recuperá-la. Tem a certeza?",
                    PrimaryButtonText = "Eliminar",
                    CloseButtonText = "Cancelar",
                };
                var result = await deleteDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var res = await FilmeListViewModel.DeleteShoppingList(model);
                    if (!res)
                    {
                        FlyoutBase.ShowAttachedFlyout(grid);
                    }
                }
            }
        }
    }    
}
