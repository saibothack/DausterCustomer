using DausterCustomer.Helpers;
using DausterCustomer.Models;
using DausterCustomer.Views;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DausterCustomer
{
    public partial class App : Application
    {
        public static Services.ServiceManager oServiceManager { get; private set; }
        public static List<KindPersons> KindPersonPiker { get; set; }
        public static List<Country> CountriesPiker { get; set; }
        public static List<State> StatesPiker { get; set; }

        public App()
        {
            InitializeComponent();
            oServiceManager = new Services.ServiceManager(new Services.RestService());

            if (Settings.IsLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
