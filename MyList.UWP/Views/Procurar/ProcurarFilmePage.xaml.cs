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
    public sealed partial class ProcurarFilmePage : Page
    {
        ObservableCollection<Filme> listaProcura = new ObservableCollection<Filme>();

        public FilmeViewModel FilmeViewModel { get; }

        public ProcurarFilmePage()
        {
            this.InitializeComponent();
            FilmeViewModel = new FilmeViewModel();
            ProcurarFilme.ItemsSource = listaProcura;
        }

        private async void ProcurarFilme_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var list = await FilmeViewModel.LoadFilmesByNameStartWithAsync(sender.Text);
            sender.ItemsSource = list;

        }

        private void ProcurarFilme_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                sender.Text = args.ChosenSuggestion.ToString().ToLower();
            }
        }

        private  void ProcurarFilme_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var p = args.SelectedItem as Filme;

            listaProcura.Clear();
            listaProcura.Add(p);



        }
    }
}
