﻿using MyList.UWP.ViewModels;
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
    public sealed partial class SerieListFormPage : Page
    {
        public SerieListViewModel SerieListViewModel { get; set; }

        public SerieListFormPage()
        {
            this.InitializeComponent();
            SerieListViewModel = new SerieListViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var model = e.Parameter as Domain.Models.SerieList;
                SerieListViewModel.RefreshItem(model);

            }

            base.OnNavigatedTo(e);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (await SerieListViewModel.UpsertAsync() != null)
            {
                Frame.GoBack();
            }
            else
            {
                Flyout.ShowAttachedFlyout(sender as FrameworkElement);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
