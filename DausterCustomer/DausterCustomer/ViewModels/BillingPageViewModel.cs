using DausterCustomer.Helpers;
using DausterCustomer.Models;
using DausterCustomer.Utils;
using DausterCustomer.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class BillingPageViewModel : ViewModelBase
    {
        Global global = new Global();

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RegisterBillingCommand { get; set; }
        public ICommand TapGestureRecognizerSearchLocationCommand { get; set; }
        public ICommand OmitirCommand { get; set; }
        #endregion

        #region Properties
        public ImageSource imageSorceBackgrond { get; set; }
        public ImageSource imageSorceMarker { get; set; }
        public List<Country> ContriesPiker { get; set; }
        public List<State> StatesPikers { get; set; }

        private Country _CountrySelect;

        public Country CountrySelect
        {
            get { return _CountrySelect; }
            set { SetProperty(ref _CountrySelect, value); }
        }
        private State _StateSelect;

        public State StateSelect
        {
            get { return _StateSelect; }
            set { SetProperty(ref _StateSelect, value); }
        }

        Billing _oBilling = new Billing();

        public Billing oBilling
        {
            get { return _oBilling; }
            set { SetProperty(ref _oBilling, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _RFCError;
        public string RFCError
        {
            get { return _RFCError; }
            set { SetProperty(ref _RFCError, value); }
        }

        private bool _bRFCError;
        public bool bRFCError
        {
            get { return _bRFCError; }
            set { SetProperty(ref _bRFCError, value); }
        }

        private string _NameError;
        public string NameError
        {
            get { return _NameError; }
            set { SetProperty(ref _NameError, value); }
        }

        private bool _bNameError;
        public bool bNameError
        {
            get { return _bNameError; }
            set { SetProperty(ref _bNameError, value); }
        }

        private string _StreetError;
        public string StreetError
        {
            get { return _StreetError; }
            set { SetProperty(ref _StreetError, value); }
        }

        private bool _bStreetError;
        public bool bStreetError
        {
            get { return _bStreetError; }
            set { SetProperty(ref _bStreetError, value); }
        }

        private string _ExtError;
        public string ExtError
        {
            get { return _ExtError; }
            set { SetProperty(ref _ExtError, value); }
        }

        private bool _bExtError;
        public bool bExtError
        {
            get { return _bExtError; }
            set { SetProperty(ref _bExtError, value); }
        }

        private string _CpError;
        public string CpError
        {
            get { return _CpError; }
            set { SetProperty(ref _CpError, value); }
        }

        private bool _bCpError;
        public bool bCpError
        {
            get { return _bCpError; }
            set { SetProperty(ref _bCpError, value); }
        }

        private string _LocationError;
        public string LocationError
        {
            get { return _LocationError; }
            set { SetProperty(ref _LocationError, value); }
        }

        private bool _bLocationError;
        public bool bLocationError
        {
            get { return _bLocationError; }
            set { SetProperty(ref _bLocationError, value); }
        }

        private string _CountryError;
        public string CountryError
        {
            get { return _CountryError; }
            set { SetProperty(ref _CountryError, value); }
        }

        private bool _bCountryError;
        public bool bCountryError
        {
            get { return _bCountryError; }
            set { SetProperty(ref _bCountryError, value); }
        }

        private string _StateError;
        public string StateError
        {
            get { return _StateError; }
            set { SetProperty(ref _StateError, value); }
        }

        private bool _bStateError;
        public bool bStateError
        {
            get { return _bStateError; }
            set { SetProperty(ref _bStateError, value); }
        }
        #endregion

        public BillingPageViewModel()
        {
            imageSorceBackgrond = ImageSource.FromResource("DausterCustomer.Images.bk_inicial.jpg");
            imageSorceMarker = ImageSource.FromResource("DausterCustomer.Images.marker_white.png");

            ContriesPiker = App.CountriesPiker;
            StatesPikers = App.StatesPiker;

            TapGestureRecognizerSearchLocationCommand = new Command(SearchLocation);
            RegisterBillingCommand = new Command(RegisterBilling);
            OmitirCommand = new Command(OmitirFunction);
        }

        private void OmitirFunction() {
            App.Current.MainPage = new HomePage();
        }

        async private void RegisterBilling()
        {
            IsBusy = true;
            if (validate())
            {
                oBilling.users_id = Settings.IdUserLogin;
                oBilling.states_id = StateSelect.id;
                oBilling.countries_id = CountrySelect.id;
                Billing addresCurrent = await App.oServiceManager.SetBilling(oBilling);

                if (addresCurrent.success)
                {
                    App.Current.MainPage = new HomePage();
                }
                else
                {
                    IsBusy = false;
                    if (string.IsNullOrEmpty(addresCurrent.message))
                    {
                        await App.Current.MainPage.DisplayAlert("Notificación", "Por favor ingrese a la plataforme y complete su registro.", "Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notificación", addresCurrent.message, "Ok");
                    }
                }
            }
        }

        private bool validate()
        {
            bool isSuccess = true;

            if (string.IsNullOrEmpty(oBilling.RFC))
            {
                RFCError = "Ingrese su RFC";
                bRFCError = true;
                isSuccess = false;
            }
            else
            {
                RFCError = string.Empty;
                bRFCError = false;
            }

            if (string.IsNullOrEmpty(oBilling.name))
            {
                NameError = "Ingrese la Razón social";
                bNameError = true;
                isSuccess = false;
            }
            else
            {
                StreetError = string.Empty;
                bStateError = false;
            }

            if (string.IsNullOrEmpty(oBilling.street))
            {
                StreetError = "Ingrese la calle";
                bStateError = true;
                isSuccess = false;
            }
            else
            {
                StreetError = string.Empty;
                bStateError = false;
            }

            if (string.IsNullOrEmpty(oBilling.exterior))
            {
                ExtError = "Ingrese no. exterior";
                bExtError = true;
                isSuccess = false;
            }
            else
            {
                ExtError = string.Empty;
                bExtError = false;
            }

            if (string.IsNullOrEmpty(oBilling.cp))
            {
                CpError = "Ingrese CP";
                bCpError = true;
                isSuccess = false;
            }
            else
            {
                CpError = string.Empty;
                bCpError = false;
            }

            if (string.IsNullOrEmpty(oBilling.location))
            {
                LocationError = "Ingrese su localidad";
                bLocationError = true;
                isSuccess = false;
            }
            else
            {
                LocationError = string.Empty;
                bLocationError = false;
            }

            if (StateSelect == null)
            {
                StateError = "Ingrese su estado";
                bStateError = true;
                isSuccess = false;
            }
            else
            {
                StateError = string.Empty;
                bStateError = false;
            }

            if (CountrySelect == null)
            {
                CountryError = "Ingrese su pais";
                bCountryError = true;
                isSuccess = false;
            }
            else
            {
                CountryError = string.Empty;
                bCountryError = false;
            }

            return isSuccess;

        }

        public void SearchLocation()
        {
        }
    }
}
