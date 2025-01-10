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

namespace MyList.UWP.Views.Série
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class ManageSeriesPage : Page
    {


        public SerieViewModel SerieViewModel;

        public ManageSeriesPage()
        {
            this.InitializeComponent();
            SerieViewModel = new SerieViewModel();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await SerieViewModel.LoadAllAsync();
        }

       

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
           
                Frame.Navigate(typeof(SerieFormPage));
            
        }

 

        private async void BtnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new ContentDialog
            {
                Title = "Pretende eliminar a série?",
                Content = "Caso o faça, não será possível voltar atrás. Tem a certeza?",
                PrimaryButtonText = "Eliminar",
                CloseButtonText = "Cancelar"
            };

            var result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is Serie s)
                {
                    SerieViewModel.DeleteProductAsync(s);
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Serie f)
            {
                SerieViewModel.Serie = f;
                Frame.Navigate(typeof(SerieFormPage), SerieViewModel);
            }
        }
    }
}
