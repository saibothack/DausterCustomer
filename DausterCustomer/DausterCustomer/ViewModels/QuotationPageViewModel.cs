using DausterCustomer.Models;
using DausterCustomer.Utils;
using DausterCustomer.Views;
using DausterCustomer.Views.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class QuotationPageViewModel : ViewModelBase, IAsyncInitialization
    {
        Global global = new Global();

        #region "Commands"
        public INavigation Navigation { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public Task Initialization { get; private set; }
        #endregion

        #region "Images"
        public ImageSource imgSourceBack { get; set; }
        public ImageSource imgSourceAdd { get; set; }
        #endregion

        #region "Properties"
        public List<Vehicle> lsVehicles { get; set; }

        private Vehicle _itemSelectVehicle;
        public Vehicle itemSelectVehicle
        {
            get { return _itemSelectVehicle; }
            set {
                SetProperty(ref _itemSelectVehicle, value);
                OnChangeVehicle();
            }
        }

        private PaymentMethods _itemSelectPaymentMethod;
        public PaymentMethods itemSelectPaymentMethod
        {
            get { return _itemSelectPaymentMethod; }
            set { SetProperty(ref _itemSelectPaymentMethod, value); }
        }
        public float intDistance { get; set; }
        public float intDuration { get; set; }
        public float fCost { get; set; }

        private string _fCharges;
        public string fCharges
        {
            get { return _fCharges; }
            set { SetProperty(ref _fCharges, value); }
        }

        private string _sTime;
        public string sTime
        {
            get { return _sTime; }
            set { SetProperty(ref _sTime, value); }
        }

        private string _sMiles;
        public string sMiles
        {
            get { return _sMiles; }
            set { SetProperty(ref _sMiles, value); }
        }

        private ObservableCollection<OtherCharges> _lsOtherCharge = new ObservableCollection<OtherCharges>();
        public ObservableCollection<OtherCharges> lsOtherCharge
        {
            get { return _lsOtherCharge; }
            set {
                _lsOtherCharge = value;
                OnPropertyChanged();
            }
        }

        private List<PaymentMethods> _oPaymentMethodSource;
        public List<PaymentMethods> oPaymentMethodSource
        {
            get { return _oPaymentMethodSource; }
            set { SetProperty(ref _oPaymentMethodSource, value); }
        }

        #endregion

        public QuotationPageViewModel(int iDistance, int iDuration) {

            lsVehicles = App.Vehicles;
            itemSelectVehicle = lsVehicles[0];
            fCharges = "$0.00";

            imgSourceBack = ImageSource.FromResource("DausterCustomer.Images.ic_back.png");
            imgSourceAdd = ImageSource.FromResource("DausterCustomer.Images.ic_add.png");


            intDistance = iDistance;
            intDuration = iDuration;

            TimeSpan result = TimeSpan.FromHours(intDuration / 60);
            sTime = result.ToString("hh':'mm") + " min";

            sMiles = (intDistance / 1000).ToString("n2") + " km";

            Initialization = InitializeAsync();

            BackCommand = new Command(pageBack);
            AddCommand = new Command(addOtherCharges);
            DeleteItemCommand = new Command<ViewCell>(deleteItem);

        }

        private async Task InitializeAsync()
        {   
            oPaymentMethodSource = await App.oServiceManager.GetPaymentMethodsAsync();

            if (oPaymentMethodSource.Count <= 0) {
                await global.ShowMessage("Notificación", "Por favor agregue una forma de pago", "Aceptar", async () =>
                {
                    var mdp = (Application.Current.MainPage as MasterDetailPage);
                    var navPage = mdp.Detail as NavigationPage;
                    await navPage.PushAsync(new PaymenthMethodsPage());
                });
            }
        }

        private void deleteItem(ViewCell viewCell) {
            viewCell.ContextActions.Clear();
            lsOtherCharge.Remove((OtherCharges)viewCell.BindingContext);

            calculateCost();
        }

        public void pageBack() {
            Navigation.PopAsync();
        }

        public async void addOtherCharges()
        {
            OtherChargesPage page = new OtherChargesPage();
            await Navigation.PushAsync(page);
        }

        public async void setQuotation(float iDistance, float iDuration, int iVehicle) {

            Dictionary<string, int> dValuesCoordinates = new Dictionary<string, int>();
            dValuesCoordinates["distance"] = int.Parse(iDistance.ToString());
            dValuesCoordinates["time"] = int.Parse(iDuration.ToString());
            dValuesCoordinates["vehicles_id"] = iVehicle;

            fCost = await App.oServiceManager.GetQuotation(dValuesCoordinates);

            fCharges = fCost.ToString("c2");
        }

        public void callPopOption() {
            if (!string.IsNullOrEmpty(App.fullOtherCharges.cost))
            {
                lsOtherCharge.Add(App.fullOtherCharges);
                App.fullOtherCharges = new OtherCharges();
            }

            calculateCost();
        }

        private void calculateCost() {
            float cost = fCost;

            foreach (OtherCharges item in lsOtherCharge)
            {
                cost += float.Parse(item.cost);
            }

            fCharges = cost.ToString("c2");
        }

        private async void OnChangeVehicle() {
            setQuotation(intDistance, intDuration, itemSelectVehicle.id);
            calculateCost();
        }
    }
}
