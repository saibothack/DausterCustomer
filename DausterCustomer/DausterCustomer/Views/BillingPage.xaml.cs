using DausterCustomer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DausterCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BillingPage : ContentPage
	{
        private BillingPageViewModel viewModel;
        public BillingPage ()
		{
			InitializeComponent();
            BindingContext = viewModel = new BillingPageViewModel();
            viewModel.Navigation = this.Navigation;
        }
	}
}