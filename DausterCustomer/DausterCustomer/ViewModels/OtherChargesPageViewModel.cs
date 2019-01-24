using DausterCustomer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class OtherChargesPageViewModel : ViewModelBase
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public Command addOtherCharge { get; set; }
        public Command BackCommand { get; set; }
        #endregion

        #region Properties
        public ImageSource imgSourceBack { get; set; }

        OtherCharges otherCharges = new OtherCharges();

        public OtherCharges oOtherCharges
        {
            get { return otherCharges; }
            set { SetProperty(ref otherCharges, value); }
        }

        private bool _bNameError;
        public bool bNameError
        {
            get { return _bNameError; }
            set { SetProperty(ref _bNameError, value); }
        }

        private string _NameError;
        public string NameError
        {
            get { return _NameError; }
            set { SetProperty(ref _NameError, value); }
        }

        private bool _bDescriptionError;
        public bool bDescriptionError
        {
            get { return _bDescriptionError; }
            set { SetProperty(ref _bDescriptionError, value); }
        }

        private string _DescriptionError;
        public string DescriptionError
        {
            get { return _DescriptionError; }
            set { SetProperty(ref _DescriptionError, value); }
        }

        private bool _bCostError;
        public bool bCostError
        {
            get { return _bCostError; }
            set { SetProperty(ref _bCostError, value); }
        }

        private string _CostError;
        public string CostError
        {
            get { return _CostError; }
            set { SetProperty(ref _CostError, value); }
        }
        #endregion

        public OtherChargesPageViewModel() {
            imgSourceBack = ImageSource.FromResource("DausterCustomer.Images.ic_back.png");
            addOtherCharge = new Command(saveCharge);
            BackCommand = new Command(commandBack);
        }

        public void commandBack()
        {
            Navigation.PopAsync();
        }

        public void saveCharge() {
            if (validation()) {
                App.fullOtherCharges = oOtherCharges;
                Navigation.PopAsync();
            }
        }

        private bool validation() {
            bool success = true;

            if (string.IsNullOrEmpty(oOtherCharges.name))
            {
                bNameError = true;
                NameError = "Por favor ingrese el nombre de la compra";
                success = false;
            }
            else {
                bNameError = false;
                NameError = string.Empty;
            }

            if (string.IsNullOrEmpty(oOtherCharges.description))
            {
                bDescriptionError = true;
                DescriptionError = "Por favor ingrese una descripción";
                success = false;
            }
            else
            {
                bDescriptionError = false;
                DescriptionError = string.Empty;
            }

            if (string.IsNullOrEmpty(oOtherCharges.cost))
            {
                bCostError = true;
                CostError = "Por favor ingrese el costo";
                success = false;
            }
            else
            {
                bCostError = false;
                CostError = string.Empty;
            }

            return success;
        }
    }
}
