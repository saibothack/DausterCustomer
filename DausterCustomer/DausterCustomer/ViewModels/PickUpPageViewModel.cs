using DausterCustomer.Models;
using DausterCustomer.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class PickUpPageViewModel : ViewModelBase
    {
        public INavigation Navigation { get; set; } 
        public Command CommandNext { get; set; }

        #region Properties
        private PickUp _oPickUp = new PickUp();
        public PickUp oPickUp
        {
            get { return _oPickUp; }
            set { SetProperty(ref _oPickUp, value); }
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

        private string _packagesError;
        public string packagesError
        {
            get { return _packagesError; }
            set { SetProperty(ref _packagesError, value); }
        }

        private bool _bpackagesError;
        public bool bpackagesError
        {
            get { return _bpackagesError; }
            set { SetProperty(ref _bpackagesError, value); }
        }

        #endregion

        public PickUpPageViewModel()
        {
            CommandNext = new Command(NextCommand);
        }

        private async void NextCommand() {
            if (validate()) {
                App.setService.pickUp = oPickUp;
                DeliveryPage delivery = new DeliveryPage();
                await this.Navigation.PushAsync(delivery);
            }
        }

        /// <summary>
        /// Validamos los campos requeridos
        /// </summary>
        /// <returns></returns>
        private bool validate() {
            bool isSuccess = true;

            if (string.IsNullOrEmpty(oPickUp.name))
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

            if (string.IsNullOrEmpty(oPickUp.surnames))
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

            if (string.IsNullOrEmpty(oPickUp.phone))
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

            if (string.IsNullOrEmpty(oPickUp.packages))
            {
                packagesError = "Ingrese el número de paquetes a enviar";
                bpackagesError = true;
                isSuccess = false;
            }
            else
            {
                packagesError = string.Empty;
                bpackagesError = false;
            }

            return isSuccess;
        }
    }
}
