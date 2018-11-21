using DausterCustomer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DausterCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
        private MasterPageViewModel viewModel;
        public MasterPage ()
		{
			InitializeComponent();
            BindingContext = viewModel = new MasterPageViewModel();
        }
	}
}