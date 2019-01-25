using DausterCustomer.Models;
using DausterCustomer.Utils;
using DausterCustomer.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class DeliveryPageViewModel : ViewModelBase
    {
        Global global = new Global();
        public INavigation Navigation { get; set; }
        public Command CommandNext { get; set; }

        #region Properties
        private Delivery _oDelivery;
        public Delivery oDelivery
        {
            get { return _oDelivery; }
            set { SetProperty(ref _oDelivery, value); }
        }

        private string _nameError;
        public string nameError
        {
            get { return _nameError; }
            set { SetProperty(ref _nameError, value); }
        }

        private bool _bnameError;
        public bool bnameError
        {
            get { return _bnameError; }
            set { SetProperty(ref _bnameError, value); }
        }

        private string _surnamesError;
        public string surnamesError
        {
            get { return _surnamesError; }
            set { SetProperty(ref _surnamesError, value); }
        }

        private bool _bsurnamesError;
        public bool bsurnamesError
        {
            get { return _bsurnamesError; }
            set { SetProperty(ref _bsurnamesError, value); }
        }

        private string _phoneError;
        public string phoneError
        {
            get { return _phoneError; }
            set { SetProperty(ref _phoneError, value); }
        }

        private bool _bphoneError;
        public bool bphoneError
        {
            get { return _bphoneError; }
            set { SetProperty(ref _bphoneError, value); }
        }

        private string _emailError;
        public string emailError
        {
            get { return _emailError; }
            set { SetProperty(ref _emailError, value); }
        }

        private bool _bemailError;
        public bool bemailError
        {
            get { return _bemailError; }
            set { SetProperty(ref _bemailError, value); }
        }
        #endregion

        public DeliveryPageViewModel() {
            CommandNext = new Command(NextCommand);
        }

        private async void NextCommand() {
            if (validate()) {
                App.setService.delivery = oDelivery;

                App.setService.id = await App.oServiceManager.setServiceAsync(App.setService);

                var mdp = (Application.Current.MainPage as MasterDetailPage);
                var navPage = mdp.Detail as NavigationPage;
                await navPage.PushAsync(new SearchDriverPage());
            }
        }

        private bool validate() {
            bool isSuccess = true;

            if (string.IsNullOrEmpty(oDelivery.name))
            {
                nameError = "Ingrese su nombre";
                bnameError = true;
                isSuccess = false;
            }
            else
            {
                nameError = string.Empty;
                bnameError = false;
            }

            if (string.IsNullOrEmpty(oDelivery.surnames))
            {
                surnamesError = "Ingrese su apellido";
                bsurnamesError = true;
                isSuccess = false;
            }
            else
            {
                surnamesError = string.Empty;
                bsurnamesError = false;
            }

            if (string.IsNullOrEmpty(oDelivery.phone))
            {
                phoneError = "Ingrese su Teléfono";
                bphoneError = true;
                isSuccess = false;
            }
            else
            {
                phoneError = string.Empty;
                bphoneError = false;
            }

            if (string.IsNullOrEmpty(oDelivery.email))
            {
                emailError = "Ingrese su Email";
                bemailError = true;
                isSuccess = false;
            }
            else
            {
                if (!global.IsValidEmail(oDelivery.email))
                {
                    emailError = "Su email no tiene el formato correcto.";
                    bemailError = true;
                }
                else
                {
                    emailError = string.Empty;
                    bemailError = false;
                }
            }

            return isSuccess;
        }
    }
}
