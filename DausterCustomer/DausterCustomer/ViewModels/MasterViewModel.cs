using DausterCustomer.Helpers;
using DausterCustomer.Models;
using DausterCustomer.Utils;
using DausterCustomer.Views;
using DausterCustomer.Views.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class MasterViewModel : IAsyncInitialization
    {
        public Task Initialization { get; private set; }
        public string nameCurrentUser { get; set; }
        public ImageSource imageSorceProfiler { get; set; }
        public ImageSource imageSorceBackgrond { get; set; }

        public MasterViewModel() {
            Initialization = InitializeAsync();
            imageSorceBackgrond = ImageSource.FromResource("DausterCustomer.Images.bk_login.jpg");
        }

        private async Task InitializeAsync()
        {
            //Consultamos la información cuando el usurio este logueado.
            if (!Settings.IsLoggedProccesIn)
            {
                User CurrUser = await App.oServiceManager.GetUser();

                nameCurrentUser = CurrUser.name + ' ' + CurrUser.surnames;
                
                if (string.IsNullOrEmpty(CurrUser.avatar))
                {
                    imageSorceProfiler = ImageSource.FromResource("DausterCustomer.Images.profiler.png");
                }
                else
                {
                    imageSorceProfiler = ImageSource.FromUri(new Uri(CurrUser.avatar));
                }
            }
        }

        public ICommand NavigationCommand
        {
            get
            {
                return new Command((value) =>
                {
                    // COMMENT: This is just quick demo code. Please don't put this in a production app.
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;

                    // Hide the Master page
                    mdp.IsPresented = false;

                    switch (value)
                    {
                        case "1":
                            navPage.PushAsync(new HomePage());
                            break;
                        case "2":
                            navPage.PushAsync(new PaymentsPage());
                            break;
                        case "3":
                            navPage.PushAsync(new RegisterPage());
                            break;
                        case "4":
                            navPage.PushAsync(new RegisterPage());
                            break;
                        case "5":
                            navPage.PushAsync(new AddressesPage());
                            break;
                        case "6":
                            navPage.PushAsync(new BillingPage());
                            break;
                        case "7":
                            navPage.PushAsync(new PaymenthMethodsPage());
                            break;
                        case "8":
                            navPage.PushAsync(new HelpPage());
                            break;
                        case "9":
                            Settings.AccessToken = string.Empty;
                            Settings.IdUserLogin = 0;
                            Settings.ImageProfiler = string.Empty;
                            Settings.IsLoggedIn = false;
                            Settings.IsLoggedProccesIn = false;
                            Settings.NameUserLogin = string.Empty;
                            App.Current.MainPage = new NavigationPage(new LoginPage());
                            break;
                    }

                });
            }
        }
    }
}
