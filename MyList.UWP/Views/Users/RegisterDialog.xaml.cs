using MyList.Domain.Models;
using MyList.UWP.ViewModels;
using MyList.UWP.Views.MyLists;
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

// O modelo de item de Caixa de Diálogo de Conteúdo está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyList.UWP.Views.Users
{
    public sealed partial class RegisterDialog : ContentDialog
    {
        public UserViewModel UserViewModel { get; set; }

        public RegisterDialog()
        {
            this.InitializeComponent();
            UserViewModel = App.UserViewModel;
            UserViewModel.User = new User();
     
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (verificarPass() == false)
            {
                this.Hide();
                RegisterDialog r = new RegisterDialog();
                r.verifPass.Visibility = Visibility.Visible;
                var res = await r.ShowAsync();
                
            }
            else
            {
                var deferral = args.GetDeferral();
                args.Cancel = !await UserViewModel.RegistarAsync();
                deferral.Complete();
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void aquiRegister_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Hide();
            LoginDialog dlg = new LoginDialog();
            var res = await dlg.ShowAsync();
           
        }

        private bool verificarPass()
        {
            if (passwordVerify.Password != password.Password)
            {
                
                return false;
                
            }
           

            return true;

            
        }
    }
}
