using DausterCustomer.ViewModels;
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