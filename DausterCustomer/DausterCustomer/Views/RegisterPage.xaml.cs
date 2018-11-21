using DausterCustomer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DausterCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        private RegisterPageViewModel viewModel;
        public RegisterPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new RegisterPageViewModel();
            viewModel.Navigation = this.Navigation;
        }
    }
}