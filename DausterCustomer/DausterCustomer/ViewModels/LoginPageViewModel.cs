
using DausterCustomer.Helpers;
using DausterCustomer.Models;
using DausterCustomer.Utils;
using DausterCustomer.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DausterCustomer.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Properties
        Global global = new Global();
        public ImageSource imageSorceLogo { get; set; }
        public ImageSource imageSorceBackgrond { get; set; }

        private User _user = new User();

        public User oUser
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion

        public LoginPageViewModel()
        {
            imageSorceLogo = ImageSource.FromResource("DausterCustomer.Images.ic_logo.png");
            imageSorceBackgrond = ImageSource.FromResource("DausterCustomer.Images.bk_login.jpg");

            RegisterCommand = new Command(redirectRegister);
            LoginCommand = new Command(loginProcess);

        }

        public async void loginProcess() {
            IsBusy = true;
            String sError = String.Empty;
            String sComa = String.Empty;

            try
            {
                if (oUser.email == null)
                {
                    sError += "El email es obligatorio";
                    sComa = ", ";
                }
                else
                {
                    if (!global.IsValidEmail(oUser.email))
                    {
                        sError += "Por favor ingrese un email valido.";
                        sComa = ", ";
                    }
                }

                if (oUser.password == null) 
                {
                    sError += sComa + "La contraseña es obligatoria";
                    IsBusy = false;
                }

                if (String.IsNullOrEmpty(sError))
                {
                    UserLogin userCurrent = await App.oServiceManager.LoginAsync(oUser);

                    if (userCurrent.success)
                    {
                        Settings.IsLoggedIn = true;
                        Settings.AccessToken = userCurrent.token;

                        if (userCurrent.nextStep == Constants.HOME) {
                            App.Current.MainPage = new MasterDetailPage()
                            {
                                Master = new MasterPage() { Title = "Main Page" },
                                Detail = new NavigationPage(new HomePage())
                            };
                        }

                        if (userCurrent.nextStep == Constants.REGISTER)
                            await Navigation.PushAsync(new RegisterPage());

                        if (userCurrent.nextStep == Constants.ADDRESS)
                            await Navigation.PushAsync(new AddressesPage());

                        if (userCurrent.nextStep == Constants.BILLING)
                            await Navigation.PushAsync(new BillingPage());
                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Notificación", userCurrent.error.ToString(), "Ok");
                    }
                }
                else
                {
                    Message = sError;
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsBusy = false;
            }
        }

        public async void redirectRegister()
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
