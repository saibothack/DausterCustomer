using DausterCustomer.Helpers;
using DausterCustomer.Models;
using DausterCustomer.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
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
        public static List<TypeCard> TypeCards { get; set; }
        public static List<Vehicle> Vehicles { get; set; }
        public static Service setService { get; set; }

        public App()
        {
            InitializeComponent();
            oServiceManager = new Services.ServiceManager(new Services.RestService());
            getCatalogs();
            Settings.IsLoggedProccesIn = false;

            if (Settings.IsLoggedIn)
            {
                MainPage = new MasterDetailPage()
                {
                    Master = new MasterPage() { Title = "Main Page" },
                    Detail = new NavigationPage(new HomePage())
                };
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        /// <summary>
        /// Obtiene catalogos necesarios para el funcionamiento del app
        /// </summary>
        /// <returns></returns>
        async Task getCatalogs() {
            KindPersonPiker = await oServiceManager.GetKindPersons();
            CountriesPiker = await oServiceManager.GetContry();
            StatesPiker = await oServiceManager.GetStates();
            TypeCards = await oServiceManager.GetTypeCardsAsync();
            Vehicles = await oServiceManager.GetVehicleAsync();
            setService = new Service();
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
