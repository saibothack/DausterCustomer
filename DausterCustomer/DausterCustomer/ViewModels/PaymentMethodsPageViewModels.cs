using DausterCustomer.Helpers;
using DausterCustomer.Models;
using DausterCustomer.Utils;
using DausterCustomer.Views.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class PaymentMethodsPageViewModels : ViewModelBase, IAsyncInitialization
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand MenuCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        #region Properties
        public Task Initialization { get; private set; }
        //public List<PaymentMethods> oPaymentMethodSource { get; set; }

        private ObservableCollection<PaymentMethods> _oPaymentMethodSource = new ObservableCollection<PaymentMethods>();
        public ObservableCollection<PaymentMethods> oPaymentMethodSource
        {
            get { return _oPaymentMethodSource; }
            set
            {
                _oPaymentMethodSource = value;
                OnPropertyChanged();
            }
        }
        public ImageSource imageSorceMenu { get; set; }
        public ImageSource imageSorceAdd { get; set; }
        #endregion

        public PaymentMethodsPageViewModels() {
            Initialization = InitializeAsync();

            imageSorceMenu = ImageSource.FromResource("DausterCustomer.Images.ic_menu.png");
            imageSorceAdd = ImageSource.FromResource("DausterCustomer.Images.ic_add.png");

            MenuCommand = new Command(showMenu);
            AddCommand = new Command(AddPaymentMethod);
            EditCommand = new Command(AddPaymentMethod);
            DeleteCommand = new Command<ViewCell>(deleteItem);

        }

        private void AddPaymentMethod() {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            navPage.PushAsync(new PaymenthMethodsCreatePage(0));
        }

        private void EditPaymentMethod(int id)
        {
            var mdp = (Application.Current.MainPage as MasterDetailPage);
            var navPage = mdp.Detail as NavigationPage;
            navPage.PushAsync(new PaymenthMethodsCreatePage(id));
        }

        private async void deleteItem(ViewCell viewCell) {

            var answer = await App.Current.MainPage.DisplayAlert("Notificación", "Esta usted seguro que desea eliminar el método de pago", "Si", "No");
            if (answer) {
                PaymentMethods paymentMethods = (PaymentMethods)viewCell.BindingContext;
                await App.oServiceManager.deletePaymentMethod(paymentMethods);
                viewCell.ContextActions.Clear();
                oPaymentMethodSource.Remove(paymentMethods);
            }

        }

        private async Task InitializeAsync()
        {
            IsBusy = true;

            List<PaymentMethods> payments = await App.oServiceManager.GetPaymentMethodsAsync();

            foreach (PaymentMethods item in payments) {
                oPaymentMethodSource.Add(item);
            }
            
            IsBusy = false;
        }
    }
}
