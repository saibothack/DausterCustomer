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
	public partial class AddressesPage : ContentPage
	{
        private AddressesPageViewModel viewModel;
        public AddressesPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new AddressesPageViewModel();
            viewModel.Navigation = this.Navigation;
        }
	}
}