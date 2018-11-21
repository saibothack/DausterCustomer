using System;
using Xamarin.Forms;
using System.Windows.Input;
using DausterCustomer.Models;
using System.Collections.Generic;
using DausterCustomer.Utils;
using DausterCustomer.Helpers;
using DausterCustomer.Views;
using System.Threading.Tasks;

namespace DausterCustomer.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase, IAsyncInitialization
    {
        Global global = new Global();

        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        #region Properties
        public Task Initialization { get; private set; }
        public ImageSource imageSorceBackgrond { get; set; }
        public List<KindPersons> KindPersonPiker { get; set; }

        private KindPersons _KindPersonSelect;

        public KindPersons KindPersonSelect
        {
            get { return _KindPersonSelect; }
            set { SetProperty(ref _KindPersonSelect, value); }
        }

        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }

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

        private String _kindPersonError;
        public String kindPersonError
        {
            get { return _kindPersonError; }
            set { SetProperty(ref _kindPersonError, value); }
        }

        private Boolean _bKindPersonError;
        public Boolean bKindPersonError
        {
            get { return _bKindPersonError; }
            set { SetProperty(ref _bKindPersonError, value); }
        }

        private String _nameError;
        public String nameError
        {
            get { return _nameError; }
            set { SetProperty(ref _nameError, value); }
        }

        private Boolean _bNameError;
        public Boolean bNameError
        {
            get { return _bNameError; }
            set { SetProperty(ref _bNameError, value); }
        }

        private String _surnamesError;
        public String surnamesError
        {
            get { return _surnamesError; }
            set { SetProperty(ref _surnamesError, value); }
        }

        private Boolean _bSurnamesError;
        public Boolean bSurnamesError
        {
            get { return _bSurnamesError; }
            set { SetProperty(ref _bSurnamesError, value); }
        }

        private String _emailError;
        public String emailError
        {
            get { return _emailError; }
            set { SetProperty(ref _emailError, value); }
        }

        private Boolean _bEmailError;
        public Boolean bEmailError
        {
            get { return _bEmailError; }
            set { SetProperty(ref _bEmailError, value); }
        }

        private String _phoneError;
        public String phoneError
        {
            get { return _phoneError; }
            set { SetProperty(ref _phoneError, value); }
        }

        private Boolean _bPhoneError;
        public Boolean bPhoneError
        {
            get { return _bPhoneError; }
            set { SetProperty(ref _bPhoneError, value); }
        }

        private String _passwordError;
        public String passwordError
        {
            get { return _passwordError; }
            set { SetProperty(ref _passwordError, value); }
        }

        private Boolean _bPasswordError;
        public Boolean bPasswordError
        {
            get { return _bPasswordError; }
            set { SetProperty(ref _bPasswordError, value); }
        }

        private String _passwordConfirmError;
        public String passwordConfirmError
        {
            get { return _passwordConfirmError; }
            set {
                SetProperty(ref _passwordConfirmError, value);
            }
        }

        private Boolean _bPasswordConfirmError;
        public Boolean bPasswordConfirmError
        {
            get { return _bPasswordConfirmError; }
            set { SetProperty(ref _bPasswordConfirmError, value); }
        }

        #endregion

        public RegisterPageViewModel()
        {
            Initialization = InitializeAsync();
            imageSorceBackgrond = ImageSource.FromResource("DausterCustomer.Images.bk_inicial.jpg");
            KindPersonPiker = App.KindPersonPiker;

            DateTime dateTime = DateTime.Now;
            MinimumDate = dateTime.AddYears(-80);
            MaximumDate = dateTime.AddYears(-18);
            oUser.birthday = dateTime.AddYears(-20);

            RegisterCommand = new Command(RegisterProcess);
        }

        private async Task InitializeAsync()
        {
            //Consultamos la información cuando el usurio este logueado.
            if (Settings.IsLoggedIn) {
                IsBusy = true;
                oUser = await App.oServiceManager.GetUser(Settings.IdUserLogin);
                IsBusy = false;
            }
        }

        async public void RegisterProcess() {
            IsBusy = true;
            if (validate())
            {
                oUser.kind_persons_id = KindPersonSelect.id;
                oUser.authorized = true;
                User userCurrent = await App.oServiceManager.StoreUser(oUser);

                if (userCurrent != null)
                {
                    if (userCurrent.success)
                    {
                        Settings.IdUserLogin = userCurrent.id;
                        Settings.NameUserLogin = userCurrent.name + ' ' + userCurrent.surnames;
                        Settings.AccessToken = userCurrent.access_token;
                        Settings.AccessTokenType = userCurrent.token_type;
                        Settings.IsLoggedIn = true;

                        App.Current.MainPage = new AddressesPage();
                    }
                    else
                    {
                        IsBusy = false;
                        if (string.IsNullOrEmpty(userCurrent.message))
                        {
                            await App.Current.MainPage.DisplayAlert("Notificación", "Hubo un error en el sistema, por favor intenta más tarde.", "Ok");
                        }
                        else {
                            await App.Current.MainPage.DisplayAlert("Notificación", userCurrent.message, "Ok");
                        }
                    }
                }
                else
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Notificación", "Hubo un error en el sistema, por favor intenta más tarde.", "Ok");
                }
            }
            else
            {
                IsBusy = false;
            }
        }

        private bool validate() {
            bool bSuccess = true;
            string sErrores = string.Empty;
            string sComa = string.Empty;

            if (KindPersonSelect == null)
            {
                bSuccess = false;
                kindPersonError = "Seleccione el tipo de persona.";
                bKindPersonError = true;
            }
            else {
                kindPersonError = string.Empty;
                bKindPersonError = false;
            }

            if (string.IsNullOrEmpty(oUser.name))
            {
                bSuccess = false;
                nameError = "Ingrese sus nombres.";
                bNameError = true;
            }
            else {
                nameError = string.Empty;
                bNameError = false;
            }

            if (string.IsNullOrEmpty(oUser.surnames))
            {
                bSuccess = false;
                surnamesError = "Ingrese sus apellidos.";
                bSurnamesError = true;
            } else {
                surnamesError = string.Empty;
                bSurnamesError = false;
            }

            if (string.IsNullOrEmpty(oUser.email))
            {
                bSuccess = false;
                emailError = "Ingrese su email.";
                bEmailError = true;
            } else {
                if (!global.IsValidEmail(oUser.email))
                {
                    emailError = "Su email no tiene el formato correcto.";
                    bEmailError = true;
                }
                else {
                    emailError = string.Empty;
                    bEmailError = false;
                }
            }

            if (string.IsNullOrEmpty(oUser.phone))
            {
                bSuccess = false;
                phoneError = "Ingrese su teléfono.";
                bPhoneError = true;
            } else {
                if (oUser.phone.Length < 10)
                {
                    phoneError = "El teléfono debe ser de 10 digítos.";
                    bPhoneError = true;
                }
                else {
                    phoneError = string.Empty;
                    bPhoneError = false;
                }
            }

            if (string.IsNullOrEmpty(oUser.password))
            {
                bSuccess = false;
                passwordError = "Ingrese su contraseña.";
                bPasswordError = true;
            } else {
                if (!global.IsPasswordValid(oUser.password))
                {
                    passwordError = "El formato de la contraseña debe de contener mayusculas, minusculas, caracteres alfanumericos y conener por lo menos 8 caracteres.";
                    bPasswordError = true;
                }
                else {
                    passwordError = string.Empty;
                    bPasswordError = false;
                }
            }

            if (string.IsNullOrEmpty(oUser.password_confirmation))
            {
                bSuccess = false;
                passwordConfirmError = "Confirme su contraseña.";
                bPasswordConfirmError = true;
            }
            else {
                if (oUser.password != oUser.password_confirmation)
                {
                    bSuccess = false;
                    passwordConfirmError = "Las contraseñas no coinciden.";
                    bPasswordConfirmError = true;
                }
                else {
                    passwordConfirmError = string.Empty;
                    bPasswordConfirmError = false;
                }
            }

            return bSuccess;

        }
    }
}
