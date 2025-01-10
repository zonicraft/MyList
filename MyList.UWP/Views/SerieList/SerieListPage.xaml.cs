using MyList.UWP.ViewModels;
using MyList.UWP.Views.Série;
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

namespace MyList.UWP.Views.SerieList
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class SerieListPage : Page
    {
        public SerieListViewModel SerieListViewModel { get; set; }

        public SerieListPage()
        {
            this.InitializeComponent();
            SerieListViewModel = new SerieListViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.UserViewModel.IsLogged)
            {
                SerieListViewModel.LoadAllAsync();
            }
            else
            {
                Frame.Content = null;
            }

            base.OnNavigatedTo(e);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SerieListFormPage));
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as Domain.Models.SerieList;
            if (model != null)
            {
                Frame.Navigate(typeof(SeriesPage), model);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as Domain.Models.SerieList;
            if (model != null)
            {
                Frame.Navigate(typeof(SerieListFormPage), model);
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var model = fe.DataContext as Domain.Models.SerieList;
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
                    var res = await SerieListViewModel.DeleteShoppingList(model);
                    if (!res)
                    {
                        FlyoutBase.ShowAttachedFlyout(grid);
                    }
                }
            }
        }
    }
}
