
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
                    oUser.remember_me = true;
                    User userCurrent = await App.oServiceManager.LoginAsync(oUser);
                    Settings.IsLoggedIn = userCurrent.success;

                    if (Settings.IsLoggedIn)
                    {
                        await Navigation.PushAsync(new RegisterPage());
                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Notificación", userCurrent.message, "Ok");
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
            IsBusy = true;
            App.KindPersonPiker = await Task.Run(() => App.oServiceManager.GetKindPersons());
            App.CountriesPiker = await Task.Run(() => App.oServiceManager.GetContry());
            App.StatesPiker = await Task.Run(() => App.oServiceManager.GetStates());
            IsBusy = false;
            await Navigation.PushAsync(new RegisterPage());

        }
    }
}
