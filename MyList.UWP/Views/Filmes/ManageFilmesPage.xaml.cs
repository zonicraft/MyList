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

namespace MyList.UWP.Views.Filmes
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class ManageFilmesPage : Page
    {

        public FilmeViewModel FilmeViewModel { get; set; }

        public ManageFilmesPage()
        {
            this.InitializeComponent();
            FilmeViewModel = new FilmeViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await FilmeViewModel.LoadAllAsync();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FilmeFormPage));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Filme f)
            {
                FilmeViewModel.Filme = f;
                Frame.Navigate(typeof(FilmeFormPage), FilmeViewModel);
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
        
                var deleteDialog = new ContentDialog
                {
                    Title = "Pretende eliminar o filme?",
                    Content = "Caso o faça, não será possível voltar atrás. Tem a certeza?",
                    PrimaryButtonText = "Eliminar",
                    CloseButtonText = "Cancelar"
                };

                var result = await deleteDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    if (sender is FrameworkElement fe && fe.DataContext is Filme f)
                    {
                        FilmeViewModel.DeleteProductAsync(f);
                    }
                }
            }
        }
    }

