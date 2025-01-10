using MyList.Domain.Models;
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

namespace MyList.UWP.Views.Categorias
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class ManageCategoriasPage : Page
    {

        public CategoriaViewModel CategoriaViewModel { get; set; }

        public ManageCategoriasPage()
        {
            this.InitializeComponent();
            CategoriaViewModel = new CategoriaViewModel();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoriasFormPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CategoriaViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void GrdMain_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new ContentDialog
            {
                Title = "Pretender apagar a categoria?",
                Content = "Não será possível recuperá-la. Tem a certeza?",
                PrimaryButtonText = "Eliminar",
                CloseButtonText = "Cancelar"
            };

            var result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is Categoria c)
                {
                    if (c.Filmes.Any()) //implementar aqui series e filmes posteriormente
                    {
                        FlyoutBase.ShowAttachedFlyout(fe);
                    }
                    else
                    {
                        CategoriaViewModel.DeleteAsync(c);
                    }
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Categoria c)
            {
                CategoriaViewModel.Categoria = c;
                Frame.Navigate(typeof(CategoriasFormPage), CategoriaViewModel);
            }
        }
    }
}
