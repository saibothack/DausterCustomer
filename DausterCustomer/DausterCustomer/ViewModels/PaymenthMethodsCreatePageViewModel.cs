using DausterCustomer.Models;
using DausterCustomer.Utils;
using DausterCustomer.Views.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class PaymenthMethodsCreatePageViewModel : ViewModelBase, IAsyncInitialization
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand CardTextChangeCommand { get; set; }
        #endregion

        #region Properties
        public Task Initialization { get; private set; }
        public ImageSource imageSorceBack { get; set; }
        public ImageSource imageSorceAdd { get; set; }
        public ImageSource imageSorceTypeCard { get; set; }

        PaymentMethods _oPaymentMethods = new PaymentMethods();

        public PaymentMethods oPaymentMethods
        {
            get { return _oPaymentMethods; }
            set { SetProperty(ref _oPaymentMethods, value); }
        }

        private string _ErrorName;
        public string ErrorName
        {
            get { return _ErrorName; }
            set { SetProperty(ref _ErrorName, value); }
        }

        private bool _bErrorName;
        public bool bErrorName
        {
            get { return _bErrorName; }
            set { SetProperty(ref _bErrorName, value); }
        }

        private string _ErrorCard;
        public string ErrorCard
        {
            get { return _ErrorCard; }
            set { SetProperty(ref _ErrorCard, value); }
        }

        private bool _bErrorCard;
        public bool bErrorCard
        {
            get { return _bErrorCard; }
            set { SetProperty(ref _bErrorCard, value); }
        }

        private string _ErrorMonth;
        public string ErrorMonth
        {
            get { return _ErrorMonth; }
            set { SetProperty(ref _ErrorMonth, value); }
        }

        private bool _bErrorMonth;
        public bool bErrorMonth
        {
            get { return _bErrorMonth; }
            set { SetProperty(ref _bErrorMonth, value); }
        }

        private string _ErrorYear;
        public string ErrorYear
        {
            get { return _ErrorYear; }
            set { SetProperty(ref _ErrorYear, value); }
        }

        private bool _bErrorYear;
        public bool bErrorYear
        {
            get { return _bErrorYear; }
            set {
                SetProperty(ref _bErrorYear, value);
                OnPropertyChanged();
            }
        }

        #endregion

        public PaymenthMethodsCreatePageViewModel(int id) {

            imageSorceBack = ImageSource.FromResource("DausterCustomer.Images.ic_back.png");
            imageSorceAdd = ImageSource.FromResource("DausterCustomer.Images.ic_accept.png");
            imageSorceTypeCard = ImageSource.FromResource("DausterCustomer.Images.ic_card.png");

            BackCommand = new Command(fnBack);
            AddCommand = new Command(addMethod);
            CardTextChangeCommand = new Command(textChange);

            Initialization = InitializeAsync(id);
        }

        private void textChange() {
            imageSorceTypeCard = ImageSource.FromUri(new Uri(Constants.BaseURL));
        }

        private async void addMethod() {
            if (validation()) {
                Regex visaRegex = new Regex(@"^4[0-9]{6,}$");
                Regex masterRegex = new Regex(@"^5[1-5][0-9]{5,}|222[1-9][0-9]{3,}|22[3-9][0-9]{4,}|2[3-6][0-9]{5,}|27[01][0-9]{4,}|2720[0-9]{3,}$");
                var number = oPaymentMethods.card.ToString();
                var numberNormalized = number.Replace(" ", string.Empty);

                if (visaRegex.IsMatch(numberNormalized))
                {
                    oPaymentMethods.type_cards_id = 1;
                }
                else if (masterRegex.IsMatch(numberNormalized))
                {
                    oPaymentMethods.type_cards_id = 2;
                }
                else {
                    await App.Current.MainPage.DisplayAlert("Notificación", "Por el momento solo se aceptan tarjetas Visa y MasterCard", "Aceptar");
                    return;
                }

                IsBusy = true;

                UserLogin userCurrent = await App.oServiceManager.SetPaymenthMethodsAsync(oPaymentMethods);

                IsBusy = false;

                if (userCurrent.success)
                {
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;
                    navPage.PushAsync(new PaymenthMethodsPage());
                }
                else {
                    await App.Current.MainPage.DisplayAlert("Notificación", "Ocurrio un error por favor intente mas tarde", "Aceptar");
                }
            }
        }

        private void fnBack() {
            
        }

        private bool validation() {
            bool succes = true;

            if (string.IsNullOrEmpty(oPaymentMethods.name))
            {
                succes = false;
                ErrorName = "Por favor ingrese su nombre";
                bErrorName = true;
            }
            else {
                ErrorName = string.Empty;
                bErrorName = false;
            }

            if (string.IsNullOrEmpty(oPaymentMethods.card))
            {
                succes = false;
                ErrorCard = "Por favor ingrese su número de tarjeta";
                bErrorCard = true;
            }
            else
            {
                ErrorCard = string.Empty;
                bErrorCard = false;
            }

            if (oPaymentMethods.month == 0)
            {
                succes = false;
                ErrorMonth = "Por favor ingrese el mes de vencimiento";
                bErrorMonth = true;
            }
            else
            {
                ErrorMonth = string.Empty;
                bErrorMonth = false;
            }

            if (oPaymentMethods.year == 0)
            {
                succes = false;
                ErrorYear = "Por favor ingrese el año de vencimiento";
                bErrorYear = true;
            }
            else
            {
                ErrorYear = string.Empty;
                bErrorYear = false;
            }

            return succes;
        }

        private async Task InitializeAsync(int id) {
            if (id != 0) {

            }
        }
    }
}
