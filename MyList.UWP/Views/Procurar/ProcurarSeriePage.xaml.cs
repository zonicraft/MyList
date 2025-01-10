using MyList.Domain.Models;
using MyList.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MyList.UWP.Views.Procurar
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class ProcurarSeriePage : Page
    {
        ObservableCollection<Serie> listaProcura = new ObservableCollection<Serie>();

        public SerieViewModel SerieViewModel { get; }

        public ProcurarSeriePage()
        {
            this.InitializeComponent();
            SerieViewModel = new SerieViewModel();
            ProcurarSerie.ItemsSource = listaProcura;
        }

        private async void ProcurarSerie_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var list = await SerieViewModel.LoadSeriesByNameStartWithAsync(sender.Text);
            sender.ItemsSource = list;
        }

        private void ProcurarSerie_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                sender.Text = args.ChosenSuggestion.ToString().ToLower();
            }
        }

        private void ProcurarSerie_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var p = args.SelectedItem as Serie;

            listaProcura.Clear();
            listaProcura.Add(p);
        }
    }
}
